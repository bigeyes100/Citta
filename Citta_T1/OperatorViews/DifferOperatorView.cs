﻿using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.OperatorViews.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class DifferOperatorView : BaseOperatorView
    {

        public DifferOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitByDataSource();
            LoadOption();
        }

        #region 初始化配置
        private void InitByDataSource()
        {
            // 初始化左右表数据源配置信息
            this.InitDataSource();
            // 窗体自定义的初始化逻辑
            this.comboBox0.Items.AddRange(nowColumnsName0);
            this.outListCCBL0.Items.AddRange(nowColumnsName0);
            this.comboBox1.Items.AddRange(nowColumnsName1);
        }

        #endregion

        #region 配置信息的保存与加载

        private void LoadOption()
        {
            if (Global.GetOptionDao().IsCleanBinaryOperatorOption(this.opControl, this.nowColumnsName0, this.nowColumnsName1))
                return;

            string[] checkIndexs = this.opControl.Option.GetOptionSplit("outfield");
            int[] indexs = Array.ConvertAll(checkIndexs, int.Parse);
            this.oldOutList0 = indexs.ToList();
            this.outListCCBL0.LoadItemCheckIndex(indexs);
            foreach (int index in indexs)
                this.oldOutName0.Add(this.outListCCBL0.Items[index].ToString());

           
            string factor1 = this.opControl.Option.GetOption("factor1");
            int[] factorList0 = Array.ConvertAll(factor1.Split('\t'), int.Parse);
            this.comboBox0.Text = this.comboBox0.Items[factorList0[0]].ToString();
            this.comboBox1.Text = this.comboBox1.Items[factorList0[1]].ToString();
            this.comboBox0.Tag = factorList0[0].ToString();
            this.comboBox1.Tag = factorList0[1].ToString();

            int count = this.opControl.Option.KeysCount("factor");
            if (count <= 1)
                return;
            InitNewFactorControl(count - 1);

            for (int i = 2; i < (count + 1); i++)
            {
                string name = "factor" + i.ToString();
                string factor = this.opControl.Option.GetOption(name);
                if (factor == String.Empty) continue;

                int[] factorList1 = Array.ConvertAll<string, int>(factor.Split('\t'), int.Parse);               
                Control control1 = this.tableLayoutPanel1.Controls[(i - 2) * 5 + 0];
                Control control2 = this.tableLayoutPanel1.Controls[(i - 2) * 5 + 1];
                Control control3 = this.tableLayoutPanel1.Controls[(i - 2) * 5 + 2];
                control1.Text = (control1 as ComboBox).Items[factorList1[0]].ToString();
                control2.Text = (control2 as ComboBox).Items[factorList1[1]].ToString();
                control3.Text = (control3 as ComboBox).Items[factorList1[2]].ToString();
                control1.Tag = factorList1[0].ToString();
                control2.Tag = factorList1[1].ToString();
                control3.Tag = factorList1[2].ToString();
            }
        }
        protected override void SaveOption()
        {
            this.opControl.Option.OptionDict.Clear();

            this.opControl.Option.SetOption("columnname0", opControl.FirstDataSourceColumns);
            this.opControl.Option.SetOption("columnname1", opControl.SecondDataSourceColumns);
            this.opControl.Option.SetOption("outfield", outListCCBL0.GetItemCheckIndex());
            this.selectedColumns = this.outListCCBL0.GetItemCheckText();

            string index00 = comboBox0.Tag == null ? comboBox0.SelectedIndex.ToString() : comboBox0.Tag.ToString();
            string index11 = comboBox1.Tag == null ? comboBox1.SelectedIndex.ToString() : comboBox1.Tag.ToString();
            string factor1 = index00 + "\t" + index11;
            this.opControl.Option.SetOption("factor1", factor1);
            if (this.tableLayoutPanel1.RowCount > 0)
            {
                for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
                {
                    ComboBox control1 = this.tableLayoutPanel1.Controls[i * 5 + 0] as ComboBox;
                    ComboBox control2 = this.tableLayoutPanel1.Controls[i * 5 + 1] as ComboBox;
                    ComboBox control3 = this.tableLayoutPanel1.Controls[i * 5 + 2] as ComboBox;
                    string index1 = control1.Tag == null ? control1.SelectedIndex.ToString() : control1.Tag.ToString();
                    string index2 = control2.Tag == null ? control2.SelectedIndex.ToString() : control2.Tag.ToString();
                    string index3 = control3.Tag == null ? control3.SelectedIndex.ToString() : control3.Tag.ToString();
                    string factor = index1 + "\t" + index2 + "\t" + index3;
                    this.opControl.Option.SetOption("factor" + (i + 2).ToString(), factor);
                }
            }

            //更新子图所有节点状态
            UpdateSubGraphStatus();
        }
        #endregion
        #region 判断是否配置完毕
    
        protected override bool IsOptionNotReady()
        {
            bool notReady = true;
            List<string> types = new List<string>() { this.comboBox0.GetType().Name, this.outListCCBL0.GetType().Name };
            foreach (Control ctl in this.tableLayoutPanel2.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请填写过滤条件!");
                    return notReady;
                }
            }
            foreach (Control ctl in this.tableLayoutPanel1.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请填写过滤条件!");
                    return notReady;
                }
            }
            if (this.outListCCBL0.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请填写输出字段!");
                return notReady;
            }
            return !notReady;
        }
        #endregion
        protected override void CreateLine(int addLine)
        {
            // 添加控件
            ComboBox regBox = new ComboBox();
            regBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            regBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            regBox.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            regBox.Anchor = AnchorStyles.None;
            regBox.Items.AddRange(new object[] {
            "AND",
            "OR"});
            regBox.Leave += new System.EventHandler(this.Control_Leave);
            regBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            regBox.SelectionChangeCommitted += new System.EventHandler(this.GetSelectedItemIndex);
            this.tableLayoutPanel1.Controls.Add(regBox, 0, addLine);

            ComboBox dataBox = new ComboBox();
            dataBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            dataBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            dataBox.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            dataBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dataBox.Items.AddRange(this.nowColumnsName0);
            dataBox.Leave += new System.EventHandler(this.Control_Leave);
            dataBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            dataBox.SelectionChangeCommitted += new System.EventHandler(this.GetSelectedItemIndex);
            this.tableLayoutPanel1.Controls.Add(dataBox, 1, addLine);

            ComboBox filterBox = NewFilterComboBox();
            this.tableLayoutPanel1.Controls.Add(filterBox, 2, addLine);

            Button addButton = NewAddButton(addLine.ToString());
            addButton.Click += new EventHandler(this.Add_Click);
            this.tableLayoutPanel1.Controls.Add(addButton, 3, addLine);

            Button delButton = NewDelButton(addLine.ToString());
            delButton.Click += new EventHandler(this.Del_Click);
            this.tableLayoutPanel1.Controls.Add(delButton, 4, addLine);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            Button tmp = (Button)sender;
            int addLine;
            if (this.tableLayoutPanel1.RowCount == 0)
            {
                this.tableLayoutPanel1.RowCount++;
                this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40));
                addLine = 0;
                CreateLine(addLine);
            }
            else
            {
                if (tmp.Name == "button1")
                    addLine = 0;
                else
                    addLine = int.Parse(tmp.Name) + 1;

                this.tableLayoutPanel1.RowCount++;
                this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;

                this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
                for (int k = this.tableLayoutPanel1.RowCount - 2; k >= addLine; k--)
                {
                    Control ctlNext = this.tableLayoutPanel1.GetControlFromPosition(0, k);
                    this.tableLayoutPanel1.SetCellPosition(ctlNext, new TableLayoutPanelCellPosition(0, k + 1));
                    Control ctlNext1 = this.tableLayoutPanel1.GetControlFromPosition(1, k);
                    this.tableLayoutPanel1.SetCellPosition(ctlNext1, new TableLayoutPanelCellPosition(1, k + 1));
                    Control ctlNext2 = this.tableLayoutPanel1.GetControlFromPosition(2, k);
                    this.tableLayoutPanel1.SetCellPosition(ctlNext2, new TableLayoutPanelCellPosition(2, k + 1));
                    Control ctlNext3 = this.tableLayoutPanel1.GetControlFromPosition(3, k);
                    ctlNext3.Name = (k + 1).ToString();
                    this.tableLayoutPanel1.SetCellPosition(ctlNext3, new TableLayoutPanelCellPosition(3, k + 1));
                    Control ctlNext4 = this.tableLayoutPanel1.GetControlFromPosition(4, k);
                    ctlNext4.Name = (k + 1).ToString();
                    this.tableLayoutPanel1.SetCellPosition(ctlNext4, new TableLayoutPanelCellPosition(4, k + 1));
                }
                CreateLine(addLine);
            }

        }

        private void Del_Click(object sender, EventArgs e)
        {
            Button tmp = (Button)sender;
            int delLine = int.Parse(tmp.Name);

            for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
            {
                Control bt1 = this.tableLayoutPanel1.Controls[(i * 5) + 4];
                if (bt1.Name == tmp.Name)
                {
                    for (int j = (i * 5) + 4; j >= (i * 5); j--)
                    {
                        this.tableLayoutPanel1.Controls.RemoveAt(j);
                    }
                    break;
                }

            }

            for (int k = delLine; k < this.tableLayoutPanel1.RowCount - 1; k++)
            {
                Control ctlNext = this.tableLayoutPanel1.GetControlFromPosition(0, k + 1);
                this.tableLayoutPanel1.SetCellPosition(ctlNext, new TableLayoutPanelCellPosition(0, k));
                Control ctlNext1 = this.tableLayoutPanel1.GetControlFromPosition(1, k + 1);
                this.tableLayoutPanel1.SetCellPosition(ctlNext1, new TableLayoutPanelCellPosition(1, k));
                Control ctlNext2 = this.tableLayoutPanel1.GetControlFromPosition(2, k + 1);
                this.tableLayoutPanel1.SetCellPosition(ctlNext2, new TableLayoutPanelCellPosition(2, k));
                Control ctlNext3 = this.tableLayoutPanel1.GetControlFromPosition(3, k + 1);
                ctlNext3.Name = k.ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext3, new TableLayoutPanelCellPosition(3, k));
                Control ctlNext4 = this.tableLayoutPanel1.GetControlFromPosition(4, k + 1);
                ctlNext4.Name = k.ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext4, new TableLayoutPanelCellPosition(4, k));
            }
            this.tableLayoutPanel1.RowStyles.RemoveAt(this.tableLayoutPanel1.RowCount - 1);
            this.tableLayoutPanel1.RowCount -= 1;

            this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;
        }
    }
}
