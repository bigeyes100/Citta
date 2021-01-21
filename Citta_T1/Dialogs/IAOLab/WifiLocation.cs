﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Controls;
using C2.IAOLab.BaseStation;
using C2.IAOLab.WifiMac;
using log4net.Util;
using C2.IAOLab.BankTool;
using C2.IAOLab.Transform;
using System.Threading;

namespace C2.Dialogs.IAOLab
{
    public partial class WifiLocation : BaseDialog
    {
        private string formType;
        public WifiLocation()
        {
            InitializeComponent();
        }
        public void ReLayoutForm()
        {

        }
        public string Tip { set { this.tipLable.Text = value; } }
        public string InputLable { set { this.inputLabel.Text = value; } }
        public Point InputLableLaction { set { this.inputLabel.Location = value; }get { return this.inputLabel.Location;  } }

        public string FormType { get { return this.formType; } set { this.formType = value; } }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void Search_Click(object sender, EventArgs e)
        {
            int i = 0;
            string[] inputArray = this.inputAndResult.Text.Split('\n');
            StringBuilder tmpResult = new StringBuilder();
            this.Cursor = Cursors.WaitCursor;
            switch (FormType)
            {
                case "APK":
                  
                    break;
                case "BaseStation":
                    i = 0;
                    foreach (string baseStation in inputArray)
                    {
                        
                        i++;
                        textBox1.Text = string.Format("正在处理第{0}条数据", i);
                        if (!string.IsNullOrEmpty(baseStation) && i < 1000)
                        {
                            tmpResult.Append(BaseStation.GetInstance().BaseStationLocate(baseStation.Split('\t')[0]));
                            inputAndResult.Text = tmpResult.ToString();
                        }
                    }
                    textBox1.Text = string.Format("处理完成，共查询{0}条", i);
                    break;
                case "Wifi":
                    i = 0;
                    foreach (string mac in inputArray)
                    {
                        i++;
                        textBox1.Text = string.Format("正在处理第{0}条数据", i);
                        if (!string.IsNullOrEmpty(mac) && i<1000)
                        {
                            tmpResult.Append(WifiMac.GetInstance().MacLocate(mac.Split('\t')[0]));
                            inputAndResult.Text = tmpResult.ToString();
                        }
                        
                    }
                    textBox1.Text = string.Format("处理完成，共查询{0}条",i);
                    break;
                case "Card":
                     i = 0;
                    foreach (string bankCard in inputArray)
                    {
                        if (!string.IsNullOrEmpty(bankCard) && i < 1000)
                        {
                            i++;
                            textBox1.Text = string.Format("正在处理第{0}条数据", i);
                            if (i % 50 == 0 )
                            {
                                Thread.Sleep(500);
                                
                            }
                            tmpResult.Append(BankTool.GetInstance().BankToolSearch(bankCard.Split('\t')[0]));
                            inputAndResult.Text = tmpResult.ToString();
                        }
                       
                    }
                    textBox1.Text = string.Format("处理完成，共查询{0}条", i);
                    break;             
                default:
                    break;
            }
            this.Cursor = Cursors.Arrow;
        }

        private void Cancle_Click(object sender, EventArgs e)
        {
            this.inputAndResult.Text = null;
            Close();
        }

        private void tipLable_Click(object sender, EventArgs e)
        {

        }

        private void WifiLocation_Load(object sender, EventArgs e)
        {

        }

    }
}
