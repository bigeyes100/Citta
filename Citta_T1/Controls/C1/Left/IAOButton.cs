﻿
using C2.Business.Model;
using C2.Core;
using C2.Dialogs.IAOLab;
using C2.Globalization;
using C2.Model;
using C2.Utils;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace C2.Controls.Left
{
    public partial class IAOButton : UserControl
    {
        private WifiLocation baseForm0;
        private ApkTool baseForm1;
        public IAOButton(string ffp)
        {
            InitializeComponent();
            txtButton.Name = ffp;
            txtButton.Text = ffp;
            this.leftPictureBox.Image = global::C2.Properties.Resources.Apk;
            this.ContextMenuStrip = contextMenuStrip1;
            switch (ffp)
            {
                case "APK":
                    this.txtButton.Text = Lang._("APK");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Apk;
                    ApkToolForm();
                    break;
                case "BaseStation":
                    this.txtButton.Text = Lang._("BaseStation");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.BaseStation;
                    BaseStationForm();
                    break;
                case "Wifi":
                    this.txtButton.Text = Lang._("Wifi");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Wifi;
                    baseForm0 = new WifiLocation();
                    break;
                case "Card":
                    this.txtButton.Text = Lang._("Card");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Card;
                    BankToolForm();
                    break;
                case "Tude":
                    this.txtButton.Text = Lang._("Tude");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Tude;
                    GPSTransformForm();
                    break;
                case "Ip":
                    this.txtButton.Text = Lang._("Ip");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Ip;
                    TimeAndIPTransformForm();
                    break;
            }
            if (baseForm0!= null)
                baseForm0.FormType = ffp;
        }
        #region 定义6种弹窗
        private void ApkToolForm()
        {
            baseForm1 = new ApkTool();
        }
        private void BaseStationForm()
        {
            baseForm0 = new WifiLocation();
            baseForm0.Text = "基站查询";
            baseForm0.InputLable = "请在下方输入基站号码";
            baseForm0.Tip = @"单次输入格式：4600051162c01
批量查询格式：多个基站号码间用\n换行，最多支持1000条同时查询";
        }
        private void BankToolForm()
        {
            baseForm0 = new WifiLocation();
            baseForm0.Text = "银行卡归属地查询";
            baseForm0.InputLable = "请在下方输入银行卡";
            baseForm0.Tip = @"单次输入格式：621085718896476
批量查询格式：多个基站号码间用\n换行，最多支持1000条同时查询";
        }
        private void GPSTransformForm()
        {
            baseForm0 = new WifiLocation();
            baseForm0.InputLableLaction = new Point(30, 14);           
            RadioButton xy = new RadioButton() { Location = new Point(17, 14) };
            RadioButton distance = new RadioButton() { Location = new Point(210, 14) ,Size=new Size(15,15)};
            Label distanceLabel = new Label { Location = new Point(240, 14) };
            distanceLabel.Text = "计算两个坐标间距离";

            baseForm0.Controls.Add(xy);
            baseForm0.Controls.Add(distance);
            baseForm0.Controls.Add(distanceLabel);
            baseForm0.Text = "经纬度转换";
            baseForm0.InputLable = "请在下方输入经纬度";
            baseForm0.Tip = @"单次输入格式：04a1518006c2
批量查询格式：多个基站号码间用\n换行，最多支持1000条同时查询";

        }
        private void TimeAndIPTransformForm()
        {
            baseForm0 = new WifiLocation();
            baseForm0.Text = "时间";
            baseForm0.InputLable = "请在下方输入基站号码";
            baseForm0.Tip = @"单次输入格式：04a1518006c2
批量查询格式：多个基站号码间用\n换行，最多支持1000条同时查询";
        }
        #endregion
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                baseForm0.ShowDialog();
            }
            catch
            {
                baseForm1.ShowDialog();
            }
        }
    }
}
