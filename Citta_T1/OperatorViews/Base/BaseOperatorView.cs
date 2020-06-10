﻿using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews.Base
{
    public partial class BaseOperatorView : Form
    {
        protected MoveOpControl opControl;          // 对应的OP算子 
        protected string dataSourceFFP0;            // 左表数据源路径
        protected string dataSourceFFP1;            // 右表数据源路径
        protected string[] nowColumnsName0;         // 当前左表(pin0)数据源表头字段(columnName)
        protected string[] nowColumnsName1;         // 当前右表(pin1)数据源表头字段
        protected List<string> oldOutName0;         // 上一次（左输出）输出表头字段
        protected List<string> oldOutName1;         // 上一次（右输出）输出表头字段
        protected List<int> oldOutList0;            // 上一次用户选择的左表输出字段的索引
        protected List<int> oldOutList1;            // 上一次用户选择的右表输出字段的索引
        protected List<string> selectedColumns;     // 本次配置用户选择的输出字段名称
        protected string oldOptionDictStr;          // 旧配置字典的字符串表述

        protected Dictionary<string, string> dataInfo; // 加载左右表数据源基本信息: FFP, Description, EXTType, encoding, sep等
        public BaseOperatorView()
        {
            this.opControl = null;
            oldOptionDictStr = String.Empty;
            dataSourceFFP0 = String.Empty;
            dataSourceFFP1 = String.Empty;
            nowColumnsName0 = new string[0];
            nowColumnsName1 = new string[0];
            oldOutName0 = new List<string>();
            oldOutName1 = new List<string>();
            oldOutList0 = new List<int>();
            oldOutList1 = new List<int>();
            selectedColumns = new List<string>();
            dataInfo = new Dictionary<string, string>();
            InitializeComponent();
        }
        public BaseOperatorView(MoveOpControl opControl) : this()
        {
            this.opControl = opControl;
            oldOptionDictStr = opControl.Option.ToString();
        }
        // 初始化左右表数据源
        protected void InitDataSource()
        {
            dataInfo = Global.GetOptionDao().GetDataSourceInfoDict(this.opControl.ID);
            // 左表
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataSourceFFP0 = dataInfo["dataPath0"];
                this.dataSourceTB0.Text = dataInfo["description0"];
                BcpInfo bcpInfo = new BcpInfo(dataSourceFFP0, OpUtil.EncodingEnum(dataInfo["encoding0"]), dataInfo["separator0"].ToCharArray());
                opControl.FirstDataSourceColumns = bcpInfo.ColumnArray;
                this.nowColumnsName0 = bcpInfo.ColumnArray;
                SetTextBoxName(this.dataSourceTB0);
            }
            // 右表
            if (dataInfo.ContainsKey("dataPath1") && dataInfo.ContainsKey("encoding1"))
            {
                this.dataSourceFFP1 = dataInfo["dataPath1"];
                this.dataSourceTB1.Text = dataInfo["description1"]; ;
                BcpInfo bcpInfo = new BcpInfo(dataSourceFFP1, OpUtil.EncodingEnum(dataInfo["encoding1"]), dataInfo["separator1"].ToCharArray());
                opControl.SecondDataSourceColumns = bcpInfo.ColumnArray;
                this.nowColumnsName1 = bcpInfo.ColumnArray;
                SetTextBoxName(this.dataSourceTB1); // 一元算子,TB1是不可见,赋值了也没事,统一逻辑后可以减少重复代码
            }
        }


        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        protected virtual bool IsOptionNotReady()
        {
            return false;
        }
        protected virtual void SaveOption()
        {
        }
        protected virtual bool IsDuplicateSelect()
        {
            return false;
        }
        protected virtual void ConfirmButton_Click(object sender, EventArgs e)
        {
            //配置窗口所有选项是否配置完毕
            if (IsOptionNotReady()) return;
            //判断标准化字段是否重复选择
            if (IsDuplicateSelect()) return;//数据标准化窗口
            SaveOption();
            this.DialogResult = DialogResult.OK;
            //内容修改，引起文档dirty
            if (this.oldOptionDictStr != this.opControl.Option.ToString())
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.selectedColumns);
                return;
            }
            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.FullFilePath);
            //输出变化，重写BCP文件
            Global.GetOptionDao().DoOutputCompare(this.oldOutName0, this.selectedColumns, this.opControl.ID);
        }

        protected void SetTextBoxName(TextBox textBox)
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

        private void DataSourceTB1_MouseHover(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(dataSourceTB1, this.dataSourceFFP1);
        }

        private void DataSourceTB0_MouseHover(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(dataSourceTB0, this.dataSourceFFP0);
        }

        protected void Control_Leave(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.Items.Count == 0 || String.IsNullOrEmpty(comboBox.Text)) return;
            if (!comboBox.Items.Contains(comboBox.Text))
            {
                comboBox.Text = String.Empty;
                MessageBox.Show("未输入正确列名，请从下拉列表中选择正确列名");
            }
        }
        protected void Control_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Control_Leave(sender, e);
        }

        protected void IsIllegalCharacter(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Contains("\\t"))
            {
                (sender as TextBox).Text = String.Empty;
                MessageBox.Show("输入非法字TAB键，请重新输入过滤条件");
            }
        }

        protected void GetSelectedItemIndex(object sender, EventArgs e)
        {
            (sender as ComboBox).Tag = (sender as ComboBox).SelectedIndex.ToString();
        }
    }
}