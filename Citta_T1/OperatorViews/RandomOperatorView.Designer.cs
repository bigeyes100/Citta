﻿namespace Citta_T1.OperatorViews
{
    partial class RandomOperatorView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.topPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.confirmButton = new System.Windows.Forms.Button();
            this.keyPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.valuePanel = new System.Windows.Forms.Panel();
            this.RandomNumBox = new System.Windows.Forms.TextBox();
            this.DataInfoBox = new System.Windows.Forms.TextBox();
            this.OutList = new UserControlDLL.ComCheckBoxList();
            this.bottomPanel.SuspendLayout();
            this.keyPanel.SuspendLayout();
            this.valuePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(571, 56);
            this.topPanel.TabIndex = 0;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.cancelButton);
            this.bottomPanel.Controls.Add(this.confirmButton);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 316);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(571, 96);
            this.bottomPanel.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.cancelButton.Location = new System.Drawing.Point(440, 28);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(94, 40);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // confirmButton
            // 
            this.confirmButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.confirmButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.confirmButton.Location = new System.Drawing.Point(312, 28);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(90, 40);
            this.confirmButton.TabIndex = 0;
            this.confirmButton.Text = "确认";
            this.confirmButton.UseVisualStyleBackColor = false;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // keyPanel
            // 
            this.keyPanel.Controls.Add(this.label3);
            this.keyPanel.Controls.Add(this.label2);
            this.keyPanel.Controls.Add(this.label1);
            this.keyPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.keyPanel.Location = new System.Drawing.Point(0, 56);
            this.keyPanel.Name = "keyPanel";
            this.keyPanel.Size = new System.Drawing.Size(174, 260);
            this.keyPanel.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(48, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 31);
            this.label3.TabIndex = 0;
            this.label3.Text = "输出字段：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(48, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 31);
            this.label2.TabIndex = 0;
            this.label2.Text = "随机条数：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(48, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据信息：";
            // 
            // valuePanel
            // 
            this.valuePanel.Controls.Add(this.RandomNumBox);
            this.valuePanel.Controls.Add(this.DataInfoBox);
            this.valuePanel.Controls.Add(this.OutList);
            this.valuePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valuePanel.Location = new System.Drawing.Point(174, 56);
            this.valuePanel.Name = "valuePanel";
            this.valuePanel.Size = new System.Drawing.Size(397, 260);
            this.valuePanel.TabIndex = 3;
            // 
            // RandomNumBox
            // 
            this.RandomNumBox.Location = new System.Drawing.Point(0, 96);
            this.RandomNumBox.Name = "RandomNumBox";
            this.RandomNumBox.Size = new System.Drawing.Size(202, 28);
            this.RandomNumBox.TabIndex = 4;
            // 
            // DataInfoBox
            // 
            this.DataInfoBox.Location = new System.Drawing.Point(0, 0);
            this.DataInfoBox.Name = "DataInfoBox";
            this.DataInfoBox.ReadOnly = true;
            this.DataInfoBox.Size = new System.Drawing.Size(202, 28);
            this.DataInfoBox.TabIndex = 3;
            this.DataInfoBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // OutList
            // 
            this.OutList.DataSource = null;
            this.OutList.Location = new System.Drawing.Point(0, 192);
            this.OutList.Margin = new System.Windows.Forms.Padding(0);
            this.OutList.Name = "OutList";
            this.OutList.Size = new System.Drawing.Size(202, 27);
            this.OutList.TabIndex = 2;
            // 
            // RandomOperatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 412);
            this.ControlBox = false;
            this.Controls.Add(this.valuePanel);
            this.Controls.Add(this.keyPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "RandomOperatorView";
            this.ShowIcon = false;
            this.Text = "随机算子设置";
            this.bottomPanel.ResumeLayout(false);
            this.keyPanel.ResumeLayout(false);
            this.keyPanel.PerformLayout();
            this.valuePanel.ResumeLayout(false);
            this.valuePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel keyPanel;
        private System.Windows.Forms.Panel valuePanel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private UserControlDLL.ComCheckBoxList OutList;
        private System.Windows.Forms.TextBox RandomNumBox;
        private System.Windows.Forms.TextBox DataInfoBox;
    }
}