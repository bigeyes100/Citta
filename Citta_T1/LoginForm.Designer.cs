﻿namespace Citta_T1
{
    partial class loginform
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
            this.label1 = new System.Windows.Forms.Label();
            this.usernamecomboBox = new System.Windows.Forms.ComboBox();
            this.logincheckBox = new System.Windows.Forms.CheckBox();
            this.loginbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(166, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名";
            // 
            // usernamecomboBox
            // 
            this.usernamecomboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.usernamecomboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.usernamecomboBox.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.usernamecomboBox.FormattingEnabled = true;
            this.usernamecomboBox.Location = new System.Drawing.Point(104, 97);
            this.usernamecomboBox.Name = "usernamecomboBox";
            this.usernamecomboBox.Size = new System.Drawing.Size(191, 23);
            this.usernamecomboBox.TabIndex = 2;
            // 
            // logincheckBox
            // 
            this.logincheckBox.AutoSize = true;
            this.logincheckBox.Location = new System.Drawing.Point(104, 177);
            this.logincheckBox.Name = "logincheckBox";
            this.logincheckBox.Size = new System.Drawing.Size(84, 16);
            this.logincheckBox.TabIndex = 4;
            this.logincheckBox.Text = "记住用户名";
            this.logincheckBox.UseVisualStyleBackColor = true;
            // 
            // loginbutton
            // 
            this.loginbutton.Location = new System.Drawing.Point(104, 138);
            this.loginbutton.Name = "loginbutton";
            this.loginbutton.Size = new System.Drawing.Size(191, 23);
            this.loginbutton.TabIndex = 6;
            this.loginbutton.Text = "登录";
            this.loginbutton.UseVisualStyleBackColor = true;
            this.loginbutton.Click += new System.EventHandler(this.loginbutton_Click);
            // 
            // loginform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 252);
            this.Controls.Add(this.loginbutton);
            this.Controls.Add(this.logincheckBox);
            this.Controls.Add(this.usernamecomboBox);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "loginform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户登录";
            this.Load += new System.EventHandler(this.loginform_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox usernamecomboBox;
        private System.Windows.Forms.CheckBox logincheckBox;
        private System.Windows.Forms.Button loginbutton;
    }
}