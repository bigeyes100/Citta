﻿
namespace Citta_T1.Controls.Move
{
    partial class MoveOpControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveOpControl));
            this.rightPictureBox = new System.Windows.Forms.PictureBox();
            this.leftPicture = new System.Windows.Forms.PictureBox();
            this.leftPinPictureBox = new System.Windows.Forms.PictureBox();
            this.rightPinPictureBox = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.菜单2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.备注ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.运行到此ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.异常日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nameToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.textBox = new System.Windows.Forms.TextBox();
            this.helpToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.statusBox = new System.Windows.Forms.PictureBox();
            this.txtButton = new System.Windows.Forms.Button();
            this.idToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPinPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPinPictureBox)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusBox)).BeginInit();
            this.SuspendLayout();
            // 
            // rightPictureBox
            // 
            this.rightPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("rightPictureBox.Image")));
            this.rightPictureBox.Location = new System.Drawing.Point(154, 8);
            this.rightPictureBox.Name = "rightPictureBox";
            this.rightPictureBox.Size = new System.Drawing.Size(14, 14);
            this.rightPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.rightPictureBox.TabIndex = 1;
            this.rightPictureBox.TabStop = false;
            this.rightPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseDown);
            this.rightPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseMove);
            this.rightPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            // 
            // leftPicture
            // 
            this.leftPicture.Image = ((System.Drawing.Image)(resources.GetObject("leftPicture.Image")));
            this.leftPicture.Location = new System.Drawing.Point(15, 2);
            this.leftPicture.Name = "leftPicture";
            this.leftPicture.Size = new System.Drawing.Size(18, 22);
            this.leftPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.leftPicture.TabIndex = 0;
            this.leftPicture.TabStop = false;
            this.leftPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseDown);
            this.leftPicture.MouseEnter += new System.EventHandler(this.leftPicture_MouseEnter);
            this.leftPicture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseMove);
            this.leftPicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            // 
            // leftPinPictureBox
            // 
            this.leftPinPictureBox.Location = new System.Drawing.Point(0, 0);
            this.leftPinPictureBox.Name = "leftPinPictureBox";
            this.leftPinPictureBox.Size = new System.Drawing.Size(100, 50);
            this.leftPinPictureBox.TabIndex = 0;
            this.leftPinPictureBox.TabStop = false;
            // 
            // rightPinPictureBox
            // 
            this.rightPinPictureBox.Location = new System.Drawing.Point(0, 0);
            this.rightPinPictureBox.Name = "rightPinPictureBox";
            this.rightPinPictureBox.Size = new System.Drawing.Size(100, 50);
            this.rightPinPictureBox.TabIndex = 0;
            this.rightPinPictureBox.TabStop = false;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OptionToolStripMenuItem,
            this.菜单2ToolStripMenuItem,
            this.备注ToolStripMenuItem,
            this.运行到此ToolStripMenuItem,
            this.异常日志ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(134, 148);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // OptionToolStripMenuItem
            // 
            this.OptionToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.OptionToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("OptionToolStripMenuItem.BackgroundImage")));
            this.OptionToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.OptionToolStripMenuItem.Enabled = false;
            this.OptionToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.OptionToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.OptionToolStripMenuItem.Name = "OptionToolStripMenuItem";
            this.OptionToolStripMenuItem.Size = new System.Drawing.Size(133, 24);
            this.OptionToolStripMenuItem.Text = "设置";
            this.OptionToolStripMenuItem.Click += new System.EventHandler(this.OptionMenuItem_Click);
            // 
            // 菜单2ToolStripMenuItem
            // 
            this.菜单2ToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("菜单2ToolStripMenuItem.BackgroundImage")));
            this.菜单2ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.菜单2ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.菜单2ToolStripMenuItem.Name = "菜单2ToolStripMenuItem";
            this.菜单2ToolStripMenuItem.Size = new System.Drawing.Size(133, 24);
            this.菜单2ToolStripMenuItem.Text = "重命名";
            this.菜单2ToolStripMenuItem.Click += new System.EventHandler(this.RenameMenuItem_Click);
            // 
            // 备注ToolStripMenuItem
            // 
            this.备注ToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("备注ToolStripMenuItem.BackgroundImage")));
            this.备注ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.备注ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.备注ToolStripMenuItem.Name = "备注ToolStripMenuItem";
            this.备注ToolStripMenuItem.Size = new System.Drawing.Size(133, 24);
            this.备注ToolStripMenuItem.Text = "备注";
            this.备注ToolStripMenuItem.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // 运行到此ToolStripMenuItem
            // 
            this.运行到此ToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("运行到此ToolStripMenuItem.BackgroundImage")));
            this.运行到此ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.运行到此ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.运行到此ToolStripMenuItem.Name = "运行到此ToolStripMenuItem";
            this.运行到此ToolStripMenuItem.Size = new System.Drawing.Size(133, 24);
            this.运行到此ToolStripMenuItem.Text = "运行到此";
            this.运行到此ToolStripMenuItem.ToolTipText = "将执行模型到本阶段为止";
            // 
            // 异常日志ToolStripMenuItem
            // 
            this.异常日志ToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("异常日志ToolStripMenuItem.BackgroundImage")));
            this.异常日志ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.异常日志ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.异常日志ToolStripMenuItem.Name = "异常日志ToolStripMenuItem";
            this.异常日志ToolStripMenuItem.Size = new System.Drawing.Size(133, 24);
            this.异常日志ToolStripMenuItem.Text = "异常日志";
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("删除ToolStripMenuItem.BackgroundImage")));
            this.删除ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.删除ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(133, 24);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.SystemColors.Control;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.textBox.Location = new System.Drawing.Point(37, 1);
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(93, 23);
            this.textBox.TabIndex = 5;
            this.textBox.Text = "连接算子";
            this.textBox.Visible = false;
            this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            this.textBox.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // statusBox
            // 
            this.statusBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.statusBox.Image = ((System.Drawing.Image)(resources.GetObject("statusBox.Image")));
            this.statusBox.Location = new System.Drawing.Point(131, 8);
            this.statusBox.Name = "statusBox";
            this.statusBox.Size = new System.Drawing.Size(14, 14);
            this.statusBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.statusBox.TabIndex = 9;
            this.statusBox.TabStop = false;
            this.helpToolTip.SetToolTip(this.statusBox, "配置算子");
            this.statusBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StatusBox_MouseDown);
            this.statusBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseMove);
            this.statusBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            // 
            // txtButton
            // 
            this.txtButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.txtButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtButton.FlatAppearance.BorderSize = 0;
            this.txtButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtButton.Location = new System.Drawing.Point(37, 1);
            this.txtButton.Margin = new System.Windows.Forms.Padding(2);
            this.txtButton.Name = "txtButton";
            this.txtButton.Size = new System.Drawing.Size(93, 25);
            this.txtButton.TabIndex = 8;
            this.txtButton.Text = "button1";
            this.txtButton.UseVisualStyleBackColor = false;
            this.txtButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TxtButton_MouseDown);
            this.txtButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseMove);
            this.txtButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            // 
            // MoveOpControl
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.statusBox);
            this.Controls.Add(this.txtButton);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.rightPictureBox);
            this.Controls.Add(this.leftPicture);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "MoveOpControl";
            this.Size = new System.Drawing.Size(190, 25);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MoveOpControl_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPinPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPinPictureBox)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statusBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox rightPictureBox;
        public System.Windows.Forms.PictureBox leftPicture;
        public System.Windows.Forms.PictureBox leftPinPictureBox;
        public System.Windows.Forms.PictureBox rightPinPictureBox;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem OptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 菜单2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 备注ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 运行到此ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 异常日志ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        public System.Windows.Forms.ToolTip nameToolTip;
        public System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ToolTip helpToolTip;
        public System.Windows.Forms.Button txtButton;
        private System.Windows.Forms.PictureBox statusBox;
        public System.Windows.Forms.ToolTip idToolTip;
    }
}
