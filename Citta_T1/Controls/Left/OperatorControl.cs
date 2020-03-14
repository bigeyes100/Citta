﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Business;

namespace Citta_T1.Controls.Left
{
    public partial class OperatorControl : UserControl
    {
        private Point mouseOffset; //记录鼠标指针的坐标
        public OperatorControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void LeftPaneOp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataObject dragDropData = new DataObject();
                dragDropData.SetData("Type", ElementType.Operator);
                dragDropData.SetData("Path", "");
                dragDropData.SetData("Text", (sender as Button).Text);
                leftPanelOpIntersect.DoDragDrop(dragDropData, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOffset.X = e.X;
                mouseOffset.Y = e.Y;
            }
        }
    }
}