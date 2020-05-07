﻿using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move;
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
    public partial class UnionOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath0;
        private string dataPath1;
        private string[] columnName0;
        private string[] columnName1;
        private string oldOptionDict;
        private List<string> selectColumn;
        private List<bool> oldCheckedItems = new List<bool>();
        private List<string> oldColumnName;
        public UnionOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            oldColumnName = new List<string>();
            this.opControl = opControl;          
            this.columnName0 = new string[] { };
            this.columnName1 = new string[] { };
            selectColumn = new List<string>();
            InitOptionInfo();
            LoadOption();
            this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());
            this.oldCheckedItems.Add(this.noRepetition.Checked);
            this.oldCheckedItems.Add(this.repetition.Checked);
    

            SetTextBoxName(this.dataSource0);
            SetTextBoxName(this.dataSource1);
        }
        #region 初始化配置
        private void InitOptionInfo()
        {
            
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetDataSourceInfo(this.opControl.ID, false);
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataPath0 = dataInfo["dataPath0"];
                this.dataSource0.Text = Path.GetFileNameWithoutExtension(this.dataPath0);
                this.toolTip1.SetToolTip(this.dataSource0, this.dataSource0.Text);
                this.columnName0 = SetOption(this.dataPath0, this.dataSource0.Text, dataInfo["encoding0"]);
                this.opControl.DoubleDataSourceColumns["0"] = this.columnName0.ToList();
            }
            if (dataInfo.ContainsKey("dataPath1") && dataInfo.ContainsKey("encoding1"))
            {
                this.dataPath1 = dataInfo["dataPath1"];
                this.dataSource1.Text = Path.GetFileNameWithoutExtension(dataInfo["dataPath1"]);
                this.toolTip2.SetToolTip(this.dataSource1, this.dataSource1.Text);
                this.columnName1 = SetOption(this.dataPath1, this.dataSource1.Text, dataInfo["encoding1"]);
                this.opControl.DoubleDataSourceColumns["1"] = this.columnName1.ToList();
            }
           if(this.opControl.Option.GetOption("outname") != "")
           {
                this.oldColumnName = this.opControl.Option.GetOption("outname").Split('\t').ToList();
            }
           

            foreach (string name in this.columnName0)
                this.comboBox1.Items.Add(name);

            foreach (string name in this.columnName1)
                this.comboBox2.Items.Add(name);
        }

        private string[] SetOption(string path, string dataName, string encoding)
        {

            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Null, EnType(encoding));
            string column = bcpInfo.columnLine;
            string[] columnName = column.Split('\t');
          
            return columnName;
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
        private void InitNewFactorControl(int count)
        {
            for (int line = 0; line < count; line++)
            {
                this.tableLayoutPanel1.RowCount++;
                this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40));
                createLine(line);
            }
        }
        private void SaveOption()
        {
            this.opControl.Option.OptionDict.Clear();
            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.opControl.DoubleDataSourceColumns["0"]));
            this.opControl.Option.SetOption("columnname1", String.Join("\t", this.opControl.DoubleDataSourceColumns["1"]));
            string factor1 = this.comboBox1.SelectedIndex.ToString() + "," + this.comboBox2.SelectedIndex.ToString() + "," + this.textBoxEx1.Text;
            this.opControl.Option.SetOption("factor1", factor1);
            this.selectColumn.Add(OutColumnName(this.comboBox1.Text, this.textBoxEx1.Text));
            if (this.tableLayoutPanel1.RowCount > 0)
            {
                for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
                {
                    Control control1 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 0];
                    Control control2 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 1];
                    Control control3 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 2];
                    string factor = (control1 as ComboBox).SelectedIndex.ToString() + "," + (control2 as ComboBox).SelectedIndex.ToString() + "," + control3.Text;
                    this.opControl.Option.SetOption("factor" + (i + 2).ToString(), factor);
                    this.selectColumn.Add(OutColumnName((control1 as ComboBox).Text, control3.Text));
                }
            }
            this.opControl.Option.SetOption("outname", String.Join("\t", this.selectColumn));
            this.opControl.Option.SetOption("noRepetition", this.noRepetition.Checked.ToString());
            this.opControl.Option.SetOption("repetition", this.repetition.Checked.ToString());
            if (this.oldOptionDict == string.Join(",", this.opControl.Option.OptionDict.ToList()) && this.opControl.Status != ElementStatus.Null)
                return;
            else
                this.opControl.Status = ElementStatus.Ready;

        }
        private string OutColumnName(string name,string alias)
        {
            string columnName = alias == "别名"? name: alias;
            return columnName;
        }
        private void LoadOption()
        {
            int count = this.opControl.Option.KeysCount("factor");
            string factor1 = this.opControl.Option.GetOption("factor1");
            if (this.opControl.Option.GetOption("noRepetition") != "")
                this.noRepetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("noRepetition"));
            if (this.opControl.Option.GetOption("repetition") != "")
                this.repetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("repetition"));
            if (factor1 != "")
            {
                string[] factorList = factor1.Split(',');
                int[] Nums = Array.ConvertAll<string, int>(factorList.Take(factorList.Length - 1).ToArray(), int.Parse);
                List<int> fieldColumn = new List<int>(Nums);
                if (Global.GetOptionDao().IsDoubleDataSourceChange(this.opControl, this.columnName0, this.columnName1, "factor1", fieldColumn))
                {
                    this.comboBox1.Text = this.comboBox1.Items[Nums[0]].ToString();
                    this.comboBox2.Text = this.comboBox2.Items[Nums[1]].ToString();
                    this.textBoxEx1.Text = factorList[2];
                }
            }
            if (count > 1)
                InitNewFactorControl(count - 1);
            else
            {
                this.opControl.Option.SetOption("columnname0", String.Join("\t", this.opControl.DoubleDataSourceColumns["0"]));
                this.opControl.Option.SetOption("columnname1", String.Join("\t", this.opControl.DoubleDataSourceColumns["1"]));
                return;
            }
            for (int i = 2; i < (count + 1); i++)
            {
                string factor = this.opControl.Option.GetOption("factor" + i.ToString());
                if (factor == "") continue;
                string[] factorList = factor.Split(',');
                int[] Nums = Array.ConvertAll<string, int>(factorList.Take(factorList.Length - 1).ToArray(), int.Parse);
                List<int> fieldColumn = new List<int>(Nums);
                if (!Global.GetOptionDao().IsDoubleDataSourceChange(this.opControl, this.columnName0, this.columnName1, "factor" + i.ToString(), fieldColumn)) continue;

                Control control1 = (Control)this.tableLayoutPanel1.Controls[(i - 2) * 5 + 0];
                control1.Text = (control1 as ComboBox).Items[Nums[0]].ToString();
                Control control2 = (Control)this.tableLayoutPanel1.Controls[(i - 2) * 5 + 1];
                control2.Text = (control2 as ComboBox).Items[Nums[1]].ToString();
                Control control3 = (Control)this.tableLayoutPanel1.Controls[(i - 2) * 5 + 2];
                control3.Text = factorList[2];
            }
            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.opControl.DoubleDataSourceColumns["0"]));
            this.opControl.Option.SetOption("columnname1", String.Join("\t", this.opControl.DoubleDataSourceColumns["1"]));
        }


        #endregion
        #region 添加取消
        private void confirmButton_Click(object sender, EventArgs e)
        {
            bool empty = IsOptionReay();
            if (empty) return;
            SaveOption();
            this.DialogResult = DialogResult.OK;
            //内容修改，引起文档dirty
           
            if (this.oldOptionDict != string.Join(",", this.opControl.Option.OptionDict.ToList()))
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件
            ModelElement hasResutl = Global.GetCurrentDocument().SearchResultOperator(this.opControl.ID);
            if (hasResutl == null)
            {
                Global.GetOptionDao().CreateResultControl(this.opControl, this.selectColumn);
                return;
            }
            //输出变化，重写BCP文件
            if (hasResutl != null && !this.oldColumnName.SequenceEqual(this.selectColumn))
                Global.GetOptionDao().IsModifyOut(this.oldColumnName, this.selectColumn, this.opControl.ID);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        private bool IsOptionReay()
        {
            bool empty = false;
            List<string> types = new List<string>();
            types.Add(this.comboBox1.GetType().Name);
            types.Add(this.textBoxEx1.GetType().Name);
            foreach (Control ctl in this.tableLayoutPanel2.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请填写过滤条件!");
                    empty = true;
                    return empty;
                }
            }
            foreach (Control ctl in this.tableLayoutPanel1.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请填写过滤条件!");
                    empty = true;
                    return empty;
                }
            }
            return empty;
        }
        #endregion
        private void createLine(int addLine)
        {
            // 添加控件

            ComboBox dataBox = new ComboBox();
            dataBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dataBox.Items.AddRange(this.columnName0);
            this.tableLayoutPanel1.Controls.Add(dataBox, 0, addLine);

            ComboBox filterBox = new ComboBox();
            filterBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            filterBox.Items.AddRange(this.columnName1);
            this.tableLayoutPanel1.Controls.Add(filterBox, 1, addLine);

            TextBox textBox = new TextBox();
            textBox.Text = "别名";
            textBox.ForeColor= SystemColors.ActiveCaption;
            textBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox.Enter += textBoxEx1_Enter;
            textBox.Leave += textBoxEx1_Leave;
            this.tableLayoutPanel1.Controls.Add(textBox, 2, addLine);

            Button addButton1 = new Button();
            addButton1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            addButton1.FlatAppearance.BorderSize = 0;
            addButton1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            addButton1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            addButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            addButton1.BackColor = System.Drawing.SystemColors.Control;
            addButton1.BackgroundImage = global::Citta_T1.Properties.Resources.add;
            addButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            addButton1.Click += new System.EventHandler(this.add_Click);
            addButton1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            addButton1.Name = addLine.ToString();
            addButton1.UseVisualStyleBackColor = true;
            this.tableLayoutPanel1.Controls.Add(addButton1, 3, addLine);

            Button delButton1 = new Button();
            delButton1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            delButton1.FlatAppearance.BorderSize = 0;
            delButton1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            delButton1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            delButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            delButton1.BackColor = System.Drawing.SystemColors.Control;
            delButton1.UseVisualStyleBackColor = true;
            delButton1.BackgroundImage = global::Citta_T1.Properties.Resources.div;
            delButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            delButton1.Click += new System.EventHandler(this.del_Click);
            delButton1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            delButton1.Name = addLine.ToString();
            this.tableLayoutPanel1.Controls.Add(delButton1, 4, addLine);
        }

        private void add_Click(object sender, EventArgs e)
        {
            Button tmp = (Button)sender;
            int addLine;
            if (this.tableLayoutPanel1.RowCount == 0)
            {
                this.tableLayoutPanel1.RowCount++;
                this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40));
                addLine = 0;
                createLine(addLine);
            }
            else
            {
                if (tmp.Name == "button1")
                    addLine = 0;
                else
                    addLine = int.Parse(tmp.Name) + 1;

                this.tableLayoutPanel1.RowCount++;
                this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;

                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40));
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
                createLine(addLine);
            }

        }

        private void del_Click(object sender, EventArgs e)
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
            this.tableLayoutPanel1.RowCount = this.tableLayoutPanel1.RowCount - 1;

            this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;

        }
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

       
        private DSUtil.Encoding EnType(string type)
        { return (DSUtil.Encoding)Enum.Parse(typeof(DSUtil.Encoding), type); }

        private void textBoxEx1_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxEx = sender as TextBox;
            if (TextBoxEx.Text == "别名")
            {
                TextBoxEx.Text = "";
            }
            TextBoxEx.ForeColor = Color.Black;
        }

        private void textBoxEx1_Leave(object sender, EventArgs e)
        {
            TextBox TextBoxEx = sender as TextBox;
            if (TextBoxEx.Text == "")
            {
                TextBoxEx.Text = "别名";
                TextBoxEx.ForeColor = SystemColors.ActiveCaption;
            }           
        }

        private void dataSource1_MouseClick(object sender, MouseEventArgs e)
        {
            this.dataSource1.Text = Path.GetFileNameWithoutExtension(this.dataPath1);
        }

        private void dataSource1_LostFocus(object sender, EventArgs e)
        {
            SetTextBoxName(this.dataSource1);
        }

        private void dataSource0_MouseClick(object sender, MouseEventArgs e)
        {
            this.dataSource0.Text = Path.GetFileNameWithoutExtension(this.dataPath0);
        }

        private void dataSource0_LostFocus(object sender, EventArgs e)
        {
            SetTextBoxName(this.dataSource0);
        }
    }
}
