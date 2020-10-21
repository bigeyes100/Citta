﻿namespace C2.OperatorViews
{
    partial class GroupOperatorView
    {
       

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupOperatorView));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.sortByString = new System.Windows.Forms.RadioButton();
            this.sortByNum = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ascendingOrder = new System.Windows.Forms.RadioButton();
            this.descendingOrder = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.repetition = new System.Windows.Forms.RadioButton();
            this.noRepetition = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bottomPanel.SuspendLayout();
            this.keyPanel.SuspendLayout();
            this.valuePanel.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataSourceTB0
            // 
            this.dataSourceTB0.Location = new System.Drawing.Point(6, 2);
            this.dataSourceTB0.Size = new System.Drawing.Size(150, 29);
            this.dataSourceTB0.TabIndex = 9;
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Location = new System.Drawing.Point(459, 11);
            this.cancelButton.Size = new System.Drawing.Size(63, 27);
            // 
            // confirmButton
            // 
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Location = new System.Drawing.Point(358, 11);
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            // 
            // comboBox0
            // 
            this.comboBox0.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox0.Location = new System.Drawing.Point(2, 3);
            this.comboBox0.Size = new System.Drawing.Size(134, 29);
            // 
            // topPanel
            // 
            this.topPanel.Size = new System.Drawing.Size(525, 21);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(0, 377);
            this.bottomPanel.Size = new System.Drawing.Size(525, 49);
            // 
            // keyPanel
            // 
            this.keyPanel.Controls.Add(this.label5);
            this.keyPanel.Controls.Add(this.label4);
            this.keyPanel.Controls.Add(this.label3);
            this.keyPanel.Controls.Add(this.label2);
            this.keyPanel.Controls.Add(this.label1);
            this.keyPanel.Location = new System.Drawing.Point(0, 21);
            this.keyPanel.Size = new System.Drawing.Size(116, 356);
            this.keyPanel.Controls.SetChildIndex(this.label1, 0);
            this.keyPanel.Controls.SetChildIndex(this.label2, 0);
            this.keyPanel.Controls.SetChildIndex(this.label3, 0);
            this.keyPanel.Controls.SetChildIndex(this.label4, 0);
            this.keyPanel.Controls.SetChildIndex(this.label5, 0);
            // 
            // valuePanel
            // 
            this.valuePanel.Controls.Add(this.groupBox3);
            this.valuePanel.Controls.Add(this.dataSourceTB0);
            this.valuePanel.Controls.Add(this.groupBox2);
            this.valuePanel.Controls.Add(this.tableLayoutPanel2);
            this.valuePanel.Controls.Add(this.groupBox1);
            this.valuePanel.Controls.Add(this.panel1);
            this.valuePanel.Location = new System.Drawing.Point(116, 21);
            this.valuePanel.Size = new System.Drawing.Size(409, 356);
            this.valuePanel.Controls.SetChildIndex(this.panel1, 0);
            this.valuePanel.Controls.SetChildIndex(this.groupBox1, 0);
            this.valuePanel.Controls.SetChildIndex(this.tableLayoutPanel2, 0);
            this.valuePanel.Controls.SetChildIndex(this.groupBox2, 0);
            this.valuePanel.Controls.SetChildIndex(this.dataSourceTB0, 0);
            this.valuePanel.Controls.SetChildIndex(this.groupBox3, 0);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(32, 2);
            this.label1.Size = new System.Drawing.Size(123, 30);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(32, 343);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 30);
            this.label3.TabIndex = 0;
            this.label3.Text = "是否去重：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(32, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "分组字段：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(32, 267);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 30);
            this.label5.TabIndex = 1;
            this.label5.Text = "排序内容：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(32, 305);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 30);
            this.label4.TabIndex = 0;
            this.label4.Text = "排序方式：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.sortByString);
            this.groupBox3.Controls.Add(this.sortByNum);
            this.groupBox3.Location = new System.Drawing.Point(6, 260);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(208, 25);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // sortByString
            // 
            this.sortByString.AutoSize = true;
            this.sortByString.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sortByString.Location = new System.Drawing.Point(95, 8);
            this.sortByString.Margin = new System.Windows.Forms.Padding(2);
            this.sortByString.Name = "sortByString";
            this.sortByString.Size = new System.Drawing.Size(133, 27);
            this.sortByString.TabIndex = 1;
            this.sortByString.Text = "按字符串排序";
            this.sortByString.UseVisualStyleBackColor = true;
            // 
            // sortByNum
            // 
            this.sortByNum.AutoSize = true;
            this.sortByNum.Checked = true;
            this.sortByNum.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sortByNum.Location = new System.Drawing.Point(4, 8);
            this.sortByNum.Margin = new System.Windows.Forms.Padding(2);
            this.sortByNum.Name = "sortByNum";
            this.sortByNum.Size = new System.Drawing.Size(116, 27);
            this.sortByNum.TabIndex = 0;
            this.sortByNum.TabStop = true;
            this.sortByNum.Text = "按数字排序";
            this.sortByNum.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ascendingOrder);
            this.groupBox2.Controls.Add(this.descendingOrder);
            this.groupBox2.Location = new System.Drawing.Point(7, 300);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(207, 25);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // ascendingOrder
            // 
            this.ascendingOrder.AutoSize = true;
            this.ascendingOrder.Checked = true;
            this.ascendingOrder.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ascendingOrder.Location = new System.Drawing.Point(4, 6);
            this.ascendingOrder.Margin = new System.Windows.Forms.Padding(2);
            this.ascendingOrder.Name = "ascendingOrder";
            this.ascendingOrder.Size = new System.Drawing.Size(99, 27);
            this.ascendingOrder.TabIndex = 1;
            this.ascendingOrder.TabStop = true;
            this.ascendingOrder.Text = "从小到大";
            this.ascendingOrder.UseVisualStyleBackColor = true;
            // 
            // descendingOrder
            // 
            this.descendingOrder.AutoSize = true;
            this.descendingOrder.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.descendingOrder.Location = new System.Drawing.Point(94, 6);
            this.descendingOrder.Margin = new System.Windows.Forms.Padding(2);
            this.descendingOrder.Name = "descendingOrder";
            this.descendingOrder.Size = new System.Drawing.Size(99, 27);
            this.descendingOrder.TabIndex = 0;
            this.descendingOrder.Text = "从大到小";
            this.descendingOrder.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel2.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBox0, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 54);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(176, 32);
            this.tableLayoutPanel2.TabIndex = 6;
            this.tableLayoutPanel2.Controls.SetChildIndex(this.comboBox0, 0);
            this.tableLayoutPanel2.Controls.SetChildIndex(this.button1, 0);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::C2.Properties.Resources.add;
            this.button1.Location = new System.Drawing.Point(140, 4);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 24);
            this.button1.TabIndex = 5;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.repetition);
            this.groupBox1.Controls.Add(this.noRepetition);
            this.groupBox1.Location = new System.Drawing.Point(7, 335);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(207, 25);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // repetition
            // 
            this.repetition.AutoSize = true;
            this.repetition.Checked = true;
            this.repetition.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.repetition.Location = new System.Drawing.Point(94, 8);
            this.repetition.Margin = new System.Windows.Forms.Padding(2);
            this.repetition.Name = "repetition";
            this.repetition.Size = new System.Drawing.Size(82, 27);
            this.repetition.TabIndex = 1;
            this.repetition.TabStop = true;
            this.repetition.Text = "不去重";
            this.repetition.UseVisualStyleBackColor = true;
            // 
            // noRepetition
            // 
            this.noRepetition.AutoSize = true;
            this.noRepetition.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.noRepetition.Location = new System.Drawing.Point(4, 8);
            this.noRepetition.Margin = new System.Windows.Forms.Padding(2);
            this.noRepetition.Name = "noRepetition";
            this.noRepetition.Size = new System.Drawing.Size(65, 27);
            this.noRepetition.TabIndex = 0;
            this.noRepetition.Text = "去重";
            this.noRepetition.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(4, 87);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 160);
            this.panel1.TabIndex = 1;
            // 
            // GroupOperatorView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(525, 426);
            this.ControlBox = true;
            this.Controls.Add(this.valuePanel);
            this.Controls.Add(this.keyPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(543, 475);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(543, 475);
            this.Name = "GroupOperatorView";
            this.ShowIcon = true;
            this.Text = "分组算子设置";
            this.bottomPanel.ResumeLayout(false);
            this.keyPanel.ResumeLayout(false);
            this.keyPanel.PerformLayout();
            this.valuePanel.ResumeLayout(false);
            this.valuePanel.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton repetition;
        private System.Windows.Forms.RadioButton noRepetition;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton ascendingOrder;
        private System.Windows.Forms.RadioButton descendingOrder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton sortByString;
        private System.Windows.Forms.RadioButton sortByNum;
    }
}