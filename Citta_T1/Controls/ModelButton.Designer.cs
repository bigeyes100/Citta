﻿namespace Citta_T1.Controls
{
    partial class ModelButton
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelButton));
            this.rightPictureBox = new System.Windows.Forms.PictureBox();
            this.lelfPictureBox = new System.Windows.Forms.PictureBox();
            this.textButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lelfPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // rightPictureBox
            // 
            this.rightPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("rightPictureBox.Image")));
            this.rightPictureBox.Location = new System.Drawing.Point(119, 3);
            this.rightPictureBox.Name = "rightPictureBox";
            this.rightPictureBox.Size = new System.Drawing.Size(23, 23);
            this.rightPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.rightPictureBox.TabIndex = 0;
            this.rightPictureBox.TabStop = false;
            // 
            // lelfPictureBox
            // 
            this.lelfPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("lelfPictureBox.Image")));
            this.lelfPictureBox.Location = new System.Drawing.Point(2, 1);
            this.lelfPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.lelfPictureBox.Name = "lelfPictureBox";
            this.lelfPictureBox.Size = new System.Drawing.Size(23, 23);
            this.lelfPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.lelfPictureBox.TabIndex = 1;
            this.lelfPictureBox.TabStop = false;
            // 
            // textButton
            // 
            this.textButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textButton.FlatAppearance.BorderSize = 0;
            this.textButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.textButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textButton.Location = new System.Drawing.Point(29, 3);
            this.textButton.Name = "textButton";
            this.textButton.Size = new System.Drawing.Size(91, 25);
            this.textButton.TabIndex = 9;
            this.textButton.Text = "模型";
            this.textButton.UseVisualStyleBackColor = false;
            // 
            // ModelButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.textButton);
            this.Controls.Add(this.lelfPictureBox);
            this.Controls.Add(this.rightPictureBox);
            this.Name = "ModelButton";
            this.Size = new System.Drawing.Size(141, 27);
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lelfPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox rightPictureBox;
        private System.Windows.Forms.PictureBox lelfPictureBox;
        private System.Windows.Forms.Button textButton;
    }
}
