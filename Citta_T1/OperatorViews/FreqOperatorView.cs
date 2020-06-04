﻿using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class FreqOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath;
        private string[] columnName;
        private string oldOptionDict;
        private List<int> oldOutList;
        private List<string> selectColumn;
        private List<bool> oldCheckedItems=new List<bool>();
        private List<string> oldColumnName;
        private static LogUtil log = LogUtil.GetInstance("FreqOperatorView");
        private OptionInfoCheck optionInfoCheck;

        public FreqOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            this.optionInfoCheck = new OptionInfoCheck();
            this.oldOutList = new List<int>();
            this.opControl = opControl;
            dataPath = String.Empty;
            oldColumnName = new List<string>();
            InitOptionInfo();
            LoadOption();
            
            this.oldCheckedItems.Add(this.repetition.Checked);
            this.oldCheckedItems.Add(this.noRepetition.Checked);
            this.oldCheckedItems.Add(this.ascendingOrder.Checked);
            this.oldCheckedItems.Add(this.descendingOrder.Checked);
            this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());

            SetTextBoxName(this.dataInfo);

        }
        #region 初始化配置
        private void InitOptionInfo()
        {
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetDataSourceInfo(this.opControl.ID);
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataPath = dataInfo["dataPath0"];
                this.dataInfo.Text = Path.GetFileNameWithoutExtension(this.dataPath);
                this.toolTip1.SetToolTip(this.dataInfo, this.dataInfo.Text);
                SetOption(this.dataPath, this.dataInfo.Text, dataInfo["encoding0"], dataInfo["separator0"].ToCharArray());
            }
        }

        private void SetOption(string path, string dataName, string encoding, char[] separator)
        {
            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Empty, OpUtil.EncodingEnum(encoding), separator);
            string column = bcpInfo.ColumnLine;
            this.columnName = column.Split(separator);
            foreach (string name in this.columnName)
                this.outList.AddItems(name);

            this.opControl.FirstDataSourceColumns = this.columnName;
            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.columnName));
        }
      
        public void SetTextBoxName(TextBox textBox)
        {
            string dataName = textBox.Text;
            int maxLength = 18;
            MatchCollection chs = Regex.Matches(dataName, "[\u4E00-\u9FA5]");
            int sumcount = chs.Count * 2;
            int sumcountDigit = Regex.Matches(dataName, "[a-zA-Z0-9]").Count;

            //防止截取字符串时中文乱码
            foreach (Match mc in chs)
            {
                if (dataName.IndexOf(mc.ToString()) == maxLength)
                {
                    maxLength -= 1;
                    break;
                }
            }

            if (sumcount + sumcountDigit > maxLength)
            {
                textBox.Text = ConvertUtil.GB2312.GetString(ConvertUtil.GB2312.GetBytes(dataName), 0, maxLength) + "...";
            }
        }
        #endregion
        #region 添加取消
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            //未设置字段警告           
            if (this.outList.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择输出字段!");
                return;
            }
            if (!this.noRepetition.Checked && !this.repetition.Checked)
            {
                MessageBox.Show("请选择数据是否进行去重");
                return;
            }
            if (!this.ascendingOrder.Checked && !this.descendingOrder.Checked)
            {
                MessageBox.Show("请选择数据排序");
                return;
            }
            this.DialogResult = DialogResult.OK;
            if (this.dataInfo.Text == "") return;
            SaveOption();
            //内容修改，引起文档dirty

            if (this.oldCheckedItems[0] != this.repetition.Checked)
                Global.GetMainForm().SetDocumentDirty();
            else if(this.oldCheckedItems[1] != this.noRepetition.Checked)
                Global.GetMainForm().SetDocumentDirty();
            else if(this.oldCheckedItems[2] != this.ascendingOrder.Checked)
                Global.GetMainForm().SetDocumentDirty();
            else if(this.oldCheckedItems[3] != this.descendingOrder .Checked)
                Global.GetMainForm().SetDocumentDirty();
            else if (String.Join(",", this.oldOutList) != this.opControl.Option.GetOption("outfield"))
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件
           
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                this.selectColumn = this.outList.GetItemCheckText();
                this.selectColumn.Add("频率统计结果");
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.selectColumn);
                return;
            }

            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.FullFilePath);

            List<string> newData = new List<string>(this.outList.GetItemCheckText());
            newData.Add("频率统计结果");
            //输出变化，重写BCP文件,它只要输出列名变化，表头就会改变
            if (String.Join(",", this.oldOutList) != this.opControl.Option.GetOption("outfield"))
                Global.GetOptionDao().IsNewOut(newData, this.opControl.ID);

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
        #region 配置信息的保存与加载
        private void SaveOption()
        {
            List<int> checkIndexs = this.outList.GetItemCheckIndex();
            this.opControl.Option.SetOption("outfield", string.Join("\t", checkIndexs));

            this.opControl.Option.SetOption("repetition", this.repetition.Checked.ToString());
            this.opControl.Option.SetOption("noRepetition", this.noRepetition.Checked.ToString());
            this.opControl.Option.SetOption("ascendingOrder", this.ascendingOrder.Checked.ToString());
            this.opControl.Option.SetOption("descendingOrder", this.descendingOrder.Checked.ToString());

            ElementStatus oldStatus = this.opControl.Status;
            if (this.oldOptionDict != string.Join(",", this.opControl.Option.OptionDict.ToList()))
                this.opControl.Status = ElementStatus.Ready;

            if (oldStatus == ElementStatus.Done && this.opControl.Status == ElementStatus.Ready)
                Global.GetCurrentDocument().DegradeChildrenStatus(this.opControl.ID);

        }

        private void LoadOption()
        {
            if (this.opControl.Option.GetOption("noRepetition") != "")
                this.repetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("repetition"));
            if (this.opControl.Option.GetOption("repetition") != "")
                this.noRepetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("noRepetition"));
            if (this.opControl.Option.GetOption("ascendingOrder") != "")
                this.ascendingOrder.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("ascendingOrder"));
            if (this.opControl.Option.GetOption("descendingOrder") != "")
                this.descendingOrder.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("descendingOrder"));            
            if (!Global.GetOptionDao().IsCleanOption(this.opControl, this.columnName, "outfield"))
            {
                string[] checkIndexs = this.opControl.Option.GetOptionSplit("outfield");
                int[] indexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList = indexs.ToList();
                this.outList.LoadItemCheckIndex(indexs);
                foreach (int index in indexs)
                    this.oldColumnName.Add(this.outList.Items[index].ToString());
            }
           
        }
        #endregion
        private void GroupBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private void GroupBox2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private void DataInfo_MouseClick(object sender, MouseEventArgs e)
        {
            this.dataInfo.Text = Path.GetFileNameWithoutExtension(this.dataPath);
        }

        private void DataInfo_LostFocus(object sender, EventArgs e)
        {
            SetTextBoxName(this.dataInfo);
        }
    }
}
