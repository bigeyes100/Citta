﻿using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.OperatorViews.Base;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{

    public partial class KeywordOperatorView : BaseOperatorView
    {
        private const int colIndexDefault = 0;
        private string keywordEncoding, keywordExtType, keywordSep;

        public KeywordOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitByDataSource();
            LoadOption();
        }
       

        private void KeywordComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        #region 加载连接数据
        private void InitByDataSource()
        {
            // 初始化左右表数据源配置信息
            this.InitDataSource();
            // 每个控件自定义的数据源配置逻辑
            dataInfo.TryGetValue("encoding1", out keywordEncoding);
            dataInfo.TryGetValue("extType1", out keywordExtType);
            dataInfo.TryGetValue("separator1", out keywordSep);

            comboBox0.Items.AddRange(nowColumnsName0);
            comboBox0.SelectedIndex = colIndexDefault;

            comboBox1.Items.AddRange(nowColumnsName1);
            comboBox1.SelectedIndex = colIndexDefault;

            outListCCBL0.Items.AddRange(nowColumnsName0);
            conditionSelectBox.SelectedIndex = colIndexDefault;
            UpdatePreviewText();
        }
        private void LoadOption()
        {
            if (Global.GetOptionDao().IsCleanBinaryOperatorOption(this.opControl, this.nowColumnsName0, this.nowColumnsName1))
                return;

            string[] checkIndexs = opControl.Option.GetOptionSplit("outfield");
            int[] indexs = Array.ConvertAll(checkIndexs, int.Parse);
            oldOutList0 = indexs.ToList();
            outListCCBL0.LoadItemCheckIndex(indexs);
            oldOutName0.AddRange(from int index in indexs
                                     select outListCCBL0.Items[index].ToString());
            comboBox0.SelectedIndex = Convert.ToInt32(opControl.Option.GetOption("dataSelectIndex", null));
            comboBox1.SelectedIndex = Convert.ToInt32(opControl.Option.GetOption("keySelectIndex", null));
            conditionSelectBox.SelectedIndex = Convert.ToInt32(opControl.Option.GetOption("conditionSlect", null));
        }
        protected override void SaveOption()
        {
            opControl.Option.OptionDict.Clear();
            opControl.Option.SetOption("outfield", outListCCBL0.GetItemCheckIndex());
            opControl.Option.SetOption("columnname0", opControl.FirstDataSourceColumns);
            opControl.Option.SetOption("columnname1", opControl.SecondDataSourceColumns);
            opControl.Option.SetOption("dataSelectIndex", comboBox0.SelectedIndex);
            opControl.Option.SetOption("keySelectIndex", comboBox1.SelectedIndex);
            opControl.Option.SetOption("conditionSlect", conditionSelectBox.SelectedIndex);
            opControl.Option.SetOption("keyWordText", keywordPreviewBox.Text);
            this.selectedColumns = this.outListCCBL0.GetItemCheckText();

            //更新子图所有节点状态
            UpdateSubGraphStatus();
        }

        #endregion
        #region 检查
        protected override bool IsOptionNotReady()
        {
            bool notReady = true;
            if (this.outListCCBL0.GetItemCheckIndex().Count == colIndexDefault)
            {
                MessageBox.Show("您需要选择输出字段");
                return notReady;
            }
            return !notReady;
        }
        #endregion
        #region 配置信息的保存与更新
        private void UpdatePreviewText()
        {
            this.keywordPreviewBox.Text = new KeywordCombine().KeywordPreView(dataSourceFFP1,
                                                                              keywordSep.ToCharArray(),
                                                                              comboBox1.SelectedIndex,
                                                                              keywordExtType,
                                                                              keywordEncoding);
        }
        #endregion
    }
    public class KeywordCombine
    {
        private const string defaultInfo = "发生未知的原因，关键词组合失败，您需要联系开发团队或者重命名关键词文件并导入";
        private const string blankSpaceSepInfo = "空格分隔符与当前的关键词组合逻辑冲突，组合效果会有误差，建议您更换文件格式";
        private List<string> datas = new List<string>();

        public string KeywordPreView(string keywordFile,
                                     char[] separator,
                                     int colIndex,
                                     string extType,
                                     string encoding)
        {
            string result;
            if (separator.Equals(' '))
            {
                return blankSpaceSepInfo;
            }
            KeywordRead(keywordFile,
                        separator,
                        colIndex,
                        OpUtil.ExtTypeEnum(extType),
                        OpUtil.EncodingEnum(encoding));
            result = string.Join(" OR ", datas);
            if (result.Equals(string.Empty))
                result = defaultInfo;
            return result;
        }
        public void KeywordRead(string fullFilePath,
                                        char[] separator,
                                        int colIndex,
                                        OpUtil.ExtType extType = OpUtil.ExtType.Text,
                                        OpUtil.Encoding encoding = OpUtil.Encoding.UTF8,
                                        bool isForceRead = false,
                                        int maxNumOfFile = 100)
        {
            List<string> rows;
            string line;
            if (extType == OpUtil.ExtType.Excel)
            {
                separator = "\t".ToCharArray();
                rows = new List<string>(BCPBuffer.GetInstance().GetCachePreViewExcelContent(fullFilePath,
                                                                                            isForceRead).Split('\n'));
            }
            else if (extType == OpUtil.ExtType.Text)
            {
                rows = new List<string>(BCPBuffer.GetInstance().GetCachePreViewBcpContent(fullFilePath,
                                                                                          encoding,
                                                                                          isForceRead).Split('\n'));
            }
            else
            {
                rows = new List<string>();
            }

            for (int i = 0; i < Math.Min(rows.Count - 1, maxNumOfFile); i++)
            {
                List<string> lines = new List<string>(rows[i + 1].TrimEnd('\r').Split(separator));
                if (colIndex >= lines.Count)
                {
                    continue;
                }
                line = lines[colIndex];
                if (line.Equals(string.Empty))
                {
                    continue;
                }
                datas.Add(line);
            }
        }
    }
}
