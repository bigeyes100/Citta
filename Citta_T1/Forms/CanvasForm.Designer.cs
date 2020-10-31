﻿namespace C2.Forms
{
    partial class CanvasForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CanvasForm));
            this.canvasPanel = new C2.Controls.CanvasPanel();
            this.topToolBarControl = new C2.Controls.Top.TopToolBarControl();
            this.currentModelRunBackLab = new System.Windows.Forms.Label();
            this.currentModelRunLab = new System.Windows.Forms.Label();
            this.currentModelFinLab = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.resetButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.runButton = new System.Windows.Forms.Button();
            this.rightHideButton = new C2.Controls.Flow.RightHideButton();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.progressBarLabel = new System.Windows.Forms.Label();
            this.remarkControl = new C2.Controls.Flow.RemarkControl();
            this.operatorControl = new C2.Controls.Left.OperatorControl(this.components);
            this.naviViewControl = new C2.Controls.Flow.NaviViewControl();
            this.blankButton = new System.Windows.Forms.Button();
            this.currentModelRunBackLab.SuspendLayout();
            this.SuspendLayout();
            // 
            // canvasPanel
            // 
            this.canvasPanel.AllowDrop = true;
            this.canvasPanel.BackColor = System.Drawing.Color.White;
            this.canvasPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.canvasPanel.DelEnable = false;
            this.canvasPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasPanel.Document = null;
            this.canvasPanel.EndC = null;
            this.canvasPanel.EndP = ((System.Drawing.PointF)(resources.GetObject("canvasPanel.EndP")));
            this.canvasPanel.LeftButtonDown = false;
            this.canvasPanel.Location = new System.Drawing.Point(0, 50);
            this.canvasPanel.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.canvasPanel.Name = "canvasPanel";
            this.canvasPanel.Size = new System.Drawing.Size(1659, 712);
            this.canvasPanel.StartC = null;
            this.canvasPanel.StartP = ((System.Drawing.PointF)(resources.GetObject("canvasPanel.StartP")));
            this.canvasPanel.TabIndex = 8;
            // 
            // topToolBarControl
            // 
            this.topToolBarControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.topToolBarControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.topToolBarControl.Location = new System.Drawing.Point(0, 0);
            this.topToolBarControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.topToolBarControl.Name = "topToolBarControl";
            this.topToolBarControl.SelectDrag = false;
            this.topToolBarControl.SelectFrame = false;
            this.topToolBarControl.SelectRemark = false;
            this.topToolBarControl.Size = new System.Drawing.Size(1659, 50);
            this.topToolBarControl.TabIndex = 25;
            // 
            // currentModelRunBackLab
            // 
            this.currentModelRunBackLab.Controls.Add(this.currentModelRunLab);
            this.currentModelRunBackLab.Image = global::C2.Properties.Resources.currentModelRunningBack;
            this.currentModelRunBackLab.Location = new System.Drawing.Point(450, 102);
            this.currentModelRunBackLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.currentModelRunBackLab.Name = "currentModelRunBackLab";
            this.currentModelRunBackLab.Size = new System.Drawing.Size(225, 150);
            this.currentModelRunBackLab.TabIndex = 26;
            this.currentModelRunBackLab.Visible = false;
            // 
            // currentModelRunLab
            // 
            this.currentModelRunLab.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.currentModelRunLab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.currentModelRunLab.Image = global::C2.Properties.Resources.currentModelRunning;
            this.currentModelRunLab.Location = new System.Drawing.Point(504, 130);
            this.currentModelRunLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.currentModelRunLab.Name = "currentModelRunLab";
            this.currentModelRunLab.Size = new System.Drawing.Size(110, 70);
            this.currentModelRunLab.TabIndex = 28;
            this.currentModelRunLab.Visible = false;
            // 
            // currentModelFinLab
            // 
            this.currentModelFinLab.Image = global::C2.Properties.Resources.currentModelFin;
            this.currentModelFinLab.Location = new System.Drawing.Point(918, 102);
            this.currentModelFinLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.currentModelFinLab.Name = "currentModelFinLab";
            this.currentModelFinLab.Size = new System.Drawing.Size(225, 150);
            this.currentModelFinLab.TabIndex = 27;
            this.currentModelFinLab.Visible = false;
            // 
            // resetButton
            // 
            this.resetButton.BackColor = System.Drawing.Color.White;
            this.resetButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.resetButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.resetButton.FlatAppearance.BorderSize = 0;
            this.resetButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.resetButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.resetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.resetButton.Image = global::C2.Properties.Resources.reset;
            this.resetButton.Location = new System.Drawing.Point(334, 200);
            this.resetButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(78, 80);
            this.resetButton.TabIndex = 32;
            this.resetButton.UseVisualStyleBackColor = false;
            this.resetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.BackColor = System.Drawing.Color.White;
            this.stopButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stopButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.stopButton.FlatAppearance.BorderSize = 0;
            this.stopButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.stopButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.stopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
            this.stopButton.Location = new System.Drawing.Point(207, 200);
            this.stopButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(78, 80);
            this.stopButton.TabIndex = 31;
            this.stopButton.UseVisualStyleBackColor = false;
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // runButton
            // 
            this.runButton.BackColor = System.Drawing.Color.White;
            this.runButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.runButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.runButton.FlatAppearance.BorderSize = 0;
            this.runButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.runButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.runButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.runButton.Image = ((System.Drawing.Image)(resources.GetObject("runButton.Image")));
            this.runButton.Location = new System.Drawing.Point(84, 200);
            this.runButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(78, 80);
            this.runButton.TabIndex = 30;
            this.runButton.UseVisualStyleBackColor = false;
            this.runButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // rightHideButton
            // 
            this.rightHideButton.BackColor = System.Drawing.Color.Transparent;
            this.rightHideButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rightHideButton.BackgroundImage")));
            this.rightHideButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rightHideButton.Location = new System.Drawing.Point(274, 58);
            this.rightHideButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rightHideButton.Name = "rightHideButton";
            this.rightHideButton.Size = new System.Drawing.Size(82, 82);
            this.rightHideButton.TabIndex = 33;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(1209, 160);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(188, 15);
            this.progressBar.TabIndex = 35;
            this.progressBar.Visible = false;
            // 
            // progressBarLabel
            // 
            this.progressBarLabel.AutoSize = true;
            this.progressBarLabel.BackColor = System.Drawing.Color.Transparent;
            this.progressBarLabel.Font = new System.Drawing.Font("微软雅黑", 8.25F);
            this.progressBarLabel.ForeColor = System.Drawing.Color.Black;
            this.progressBarLabel.Location = new System.Drawing.Point(1310, 200);
            this.progressBarLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.progressBarLabel.Name = "progressBarLabel";
            this.progressBarLabel.Size = new System.Drawing.Size(0, 23);
            this.progressBarLabel.TabIndex = 36;
            this.progressBarLabel.Visible = false;
            // 
            // remarkControl
            // 
            this.remarkControl.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.remarkControl.BackColor = System.Drawing.Color.Transparent;
            this.remarkControl.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("remarkControl.BackgroundImage")));
            this.remarkControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.remarkControl.Location = new System.Drawing.Point(777, 75);
            this.remarkControl.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.remarkControl.Name = "remarkControl";
            this.remarkControl.RemarkDescription = "";
            this.remarkControl.Size = new System.Drawing.Size(267, 494);
            this.remarkControl.TabIndex = 26;
            this.remarkControl.Visible = false;
            // 
            // operatorControl
            // 
            this.operatorControl._setRoundRadius = 30;
            this.operatorControl.AllowDrop = true;
            this.operatorControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.operatorControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.operatorControl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.operatorControl.Location = new System.Drawing.Point(652, 92);
            this.operatorControl.Margin = new System.Windows.Forms.Padding(0);
            this.operatorControl.Name = "operatorControl";
            this.operatorControl.Size = new System.Drawing.Size(322, 480);
            this.operatorControl.TabIndex = 40;
            // 
            // naviViewControl
            // 
            this.naviViewControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.naviViewControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.naviViewControl.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.naviViewControl.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.naviViewControl.Location = new System.Drawing.Point(1236, 495);
            this.naviViewControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.naviViewControl.Name = "naviViewControl";
            this.naviViewControl.Size = new System.Drawing.Size(308, 158);
            this.naviViewControl.TabIndex = 41;
            // 
            // blankButton
            // 
            this.blankButton.Location = new System.Drawing.Point(829, 381);
            this.blankButton.Margin = new System.Windows.Forms.Padding(2);
            this.blankButton.Name = "blankButton";
            this.blankButton.Size = new System.Drawing.Size(0, 0);
            this.blankButton.TabIndex = 42;
            this.blankButton.Text = "button1";
            this.blankButton.UseVisualStyleBackColor = true;
            // 
            // CanvasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1659, 762);
            this.Controls.Add(this.blankButton);
            this.Controls.Add(this.naviViewControl);
            this.Controls.Add(this.operatorControl);
            this.Controls.Add(this.remarkControl);
            this.Controls.Add(this.progressBarLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.rightHideButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.currentModelFinLab);
            this.Controls.Add(this.currentModelRunBackLab);
            this.Controls.Add(this.canvasPanel);
            this.Controls.Add(this.topToolBarControl);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CanvasForm";
            this.Text = "CanvasForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CanvasForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CanvasForm_FormClosed);
            this.SizeChanged += new System.EventHandler(this.CanvasForm_SizeChanged);
            this.currentModelRunBackLab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CanvasPanel canvasPanel;
        private Controls.Top.TopToolBarControl topToolBarControl;
        private System.Windows.Forms.Label currentModelRunBackLab;
        private System.Windows.Forms.Label currentModelFinLab;
        private System.Windows.Forms.Label currentModelRunLab;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button runButton;
        private Controls.Flow.RightHideButton rightHideButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label progressBarLabel;
        private Controls.Flow.RemarkControl remarkControl;
        private Controls.Left.OperatorControl operatorControl;
        private Controls.Flow.NaviViewControl naviViewControl;
        private System.Windows.Forms.Button blankButton;
    }
}