﻿using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{

    public partial class RandomOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath;
        private string oldRandomNum;
        private List<int> oldOutList;
        private string oldOptionDict;
        private string[] columnName;
        private List<string> selectColumn;
        private List<string> oldColumnName;
        private static LogUtil log = LogUtil.GetInstance("RandomOperatorView");
        public RandomOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            this.dataPath = "";
            this.oldOutList = new List<int>();
            oldColumnName = new List<string>();
            this.opControl = opControl;
            InitOptionInfo();
            LoadOption();
            this.oldRandomNum =this.RandomNumBox.Text;         
            this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());

            SetTextBoxName(this.DataInfoBox);
        }
        #region 初始化配置
        private void InitOptionInfo()
        {
            int startID = -1;
            string encoding = "";
            char separator = '\t';
            List<ModelRelation> modelRelations = Global.GetCurrentDocument().ModelRelations;
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            foreach (ModelRelation mr in modelRelations)
            {
                if (mr.EndID == this.opControl.ID)
                {
                    startID = mr.StartID;
                    break;
                }
            }
            foreach (ModelElement me in modelElements)
            {
                if (me.ID == startID)
                {
                    this.dataPath = me.GetFullFilePath();
                    if (me.GetControl is MoveDtControl)
                        separator = (me.GetControl as MoveDtControl).Separator;
                    //设置数据信息选项
                    this.DataInfoBox.Text = Path.GetFileNameWithoutExtension(this.dataPath);
                    this.toolTip1.SetToolTip(this.DataInfoBox, this.DataInfoBox.Text);
                    encoding = me.Encoding.ToString();
                    break;
                }
            }
            if (this.dataPath != "")
                SetOption(this.dataPath, this.DataInfoBox.Text, encoding, separator);

        }
        private void SetOption(string path, string dataName, string encoding, char separator)
        {

            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Null, EnType(encoding));
            string column = bcpInfo.columnLine;
            this.columnName = column.Split(separator);
            foreach (string name in columnName)
            {
                this.OutList.AddItems(name);
            }
            CompareDataSource();
            this.opControl.SingleDataSourceColumns = String.Join("\t", this.columnName);
            this.opControl.Option.SetOption("columnname", this.opControl.SingleDataSourceColumns);
        }
        private void CompareDataSource()
        {
            //新数据源与旧数据源表头不匹配，对应配置内容是否情况进行判断
            if (this.opControl.Option.GetOption("columnname") == "") return;
            string[] oldColumnList = this.opControl.Option.GetOption("columnname").Split('\t');
            try
            {
                if (this.opControl.Option.GetOption("outfield") != "")
                {
                    string[] checkIndexs = this.opControl.Option.GetOption("outfield").Split(',');
                    int[] outIndex = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                    if (Global.GetOptionDao().IsDataSourceEqual(oldColumnList, this.columnName, outIndex))
                        this.opControl.Option.OptionDict.Remove("outfield");
                }
            }
            catch (Exception ex) { log.Error(ex.Message); };
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
                textBox.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(System.Text.Encoding.GetEncoding("GB2312").GetBytes(dataName), 0, maxLength) + "...";
            }
        }
        #endregion
        #region 配置信息的保存与加载
        private void SaveOption()
        {
            this.opControl.Option.SetOption("randomnum", this.RandomNumBox.Text);
            List<int> checkIndexs = this.OutList.GetItemCheckIndex();
            List<int> outIndexs = new List<int>(this.oldOutList);
            Global.GetOptionDao().UpdateOutputCheckIndexs(checkIndexs, outIndexs);
            string outField = string.Join(",", outIndexs);
            this.opControl.Option.SetOption("outfield", outField);

            if (this.oldOptionDict == string.Join(",", this.opControl.Option.OptionDict.ToList()) && this.opControl.Status != ElementStatus.Null)
                return;
            else
                this.opControl.Status = ElementStatus.Ready;

        }

        private void LoadOption()
        {
            if (this.opControl.Option.GetOption("randomnum") != "")
                this.RandomNumBox.Text = this.opControl.Option.GetOption("randomnum");
            if (this.opControl.Option.GetOption("outfield") != "")
            {
                string[] checkIndexs = this.opControl.Option.GetOption("outfield").Split(',');
                int[] indexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList = indexs.ToList();
                this.OutList.LoadItemCheckIndex(indexs);
                foreach (int index in indexs)
                    this.oldColumnName.Add(this.OutList.Items[index].ToString());
            }
          
        }
        #endregion
        #region 添加取消
        private void confirmButton_Click(object sender, EventArgs e)
        {
            //未设置字段警告
            if (this.RandomNumBox.Text == "")
            {
                MessageBox.Show("请选择随机条数字段!");
                return;
            }
            if (this.OutList.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择输出字段!");
                return;
            }
            this.DialogResult = DialogResult.OK;
            if (this.DataInfoBox.Text == "") return;
            SaveOption();
            //内容修改，引起文档dirty
            if (this.oldRandomNum!= this.RandomNumBox.Text)
                Global.GetMainForm().SetDocumentDirty();
            else if (String.Join(",", this.oldOutList) != this.opControl.Option.GetOption("outfield"))
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件
            this.selectColumn = this.OutList.GetItemCheckText();
            ModelElement hasResutl = Global.GetCurrentDocument().SearchResultOperator(this.opControl.ID);
            if (hasResutl == null)
            { 
                Global.GetOptionDao().CreateResultControl(this.opControl, this.selectColumn);
                return;
            }
            //输出变化，重写BCP文件
            List<string> outName = new List<string>();
            foreach (string index in this.opControl.Option.GetOption("outfield").Split(','))
            { outName.Add(this.columnName[Convert.ToInt32(index)]); }
            if (hasResutl != null && String.Join(",", this.oldOutList) != this.opControl.Option.GetOption("outfield"))
                Global.GetOptionDao().IsModifyOut(this.oldColumnName, outName, this.opControl.ID);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
        private DSUtil.Encoding EnType(string type)
        {
            return (DSUtil.Encoding)Enum.Parse(typeof(DSUtil.Encoding), type);
        }

        private void DataInfoBox_MouseClick(object sender, MouseEventArgs e)
        {
            this.DataInfoBox.Text = Path.GetFileNameWithoutExtension(this.dataPath);
        }

        private void DataInfoBox_LostFocus(object sender, EventArgs e)
        {
            SetTextBoxName(this.DataInfoBox);
        }

        private void RandomNumBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                Global.GetOptionDao().NonNumeric_ControlText(this.RandomNumBox);
        }

        private void RandomNumBox_Leave(object sender, EventArgs e)
        {
            Global.GetOptionDao().NonNumeric_ControlText(this.RandomNumBox);
        }
    }
}
