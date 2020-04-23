﻿using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls;
using Citta_T1.Controls.Move;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class MaxOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath;
        private string oldMaxfield;
        private List<int> oldOutList;
        private ElementStatus oldstatus;
        private string[] columnName;
        private string oldOptionDict;
        private List<string> oldColumnName;

        private LogUtil log = LogUtil.GetInstance("MoveRsControl");

        public MaxOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            dataPath = "";
            columnName = new string[] { };
            oldColumnName = new List<string>();
            oldOutList = new List<int>();
            this.opControl = opControl;
            InitOptionInfo();
            LoadOption();
                       
            this.oldMaxfield = this.MaxValueBox.Text;           
            this.oldstatus = opControl.Status;
            this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());
            


        }
        #region 添加取消
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            //未设置字段警告
            if (this.DataInfoBox.Text == "") return;
            if (this.MaxValueBox.Text == "")
            {
                MessageBox.Show("请选择最大值字段!");
                return;
            }
            if (this.OutList.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择输出字段!");
                return;
            }
            this.DialogResult = DialogResult.OK;
           
            SaveOption();
            //内容修改，引起文档dirty
            if (this.oldMaxfield != this.MaxValueBox.Text || !this.oldOutList.SequenceEqual(this.OutList.GetItemCheckIndex()))
                Global.GetMainForm().SetDocumentDirty();

            //生成结果控件,创建relation,bcp结果文件
            if (this.oldOptionDict == "")
            {
                Global.GetOptionDao().CreateResultControl(this.opControl, this.OutList.GetItemCheckText());
                this.opControl.DataSourceColumns =String.Join("\t",this.OutList.GetItemCheckText()) ;
            }
            //输出变化，重写BCP文件
            if (this.oldOptionDict != "" && !this.oldOutList.SequenceEqual(this.OutList.GetItemCheckIndex()))
                this.opControl.DataSourceColumns = Global.GetOptionDao().IsModifyOut(this.oldColumnName, this.OutList.GetItemCheckText(), this.opControl.ID);
            this.opControl.Option.SetOption("columnname", this.opControl.DataSourceColumns);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
        #region 配置信息的保存与加载
        private void SaveOption()
        {
            List<int> checkIndexs = this.OutList.GetItemCheckIndex();
            List<int> outIndexs =new List<int>(this.oldOutList);
            List<int> removeIndex = new List<int>();
            foreach (int index in checkIndexs)
            {
                if (!outIndexs.Contains(index))
                    outIndexs.Add(index);
            }
            foreach (int index in outIndexs)
            {
                if (!checkIndexs.Contains(index))
                {
                    outIndexs = new List<int>(checkIndexs);
                    break;
                }                 
            }
            string outField = string.Join(",", outIndexs);
            if(this.MaxValueBox.Text == "")
                this.opControl.Option.SetOption("maxfield", "");
            else
                this.opControl.Option.SetOption("maxfield", this.MaxValueBox.SelectedIndex.ToString());
            this.opControl.Option.SetOption("outfield", outField);
            
            if (this.MaxValueBox.Text != "" && outField != "")
                this.opControl.Status = ElementStatus.Ready;

        }

        private void LoadOption()
        {
            if (this.opControl.Option.GetOption("maxfield") != "")
            {
                int index = Convert.ToInt32(this.opControl.Option.GetOption("maxfield"));
                this.MaxValueBox.Text = this.MaxValueBox.Items[index].ToString();
            }
            if (this.opControl.Option.GetOption("outfield") != "")
            {
                
                string[] checkIndexs = this.opControl.Option.GetOption("outfield").Split(',');
                int[] indexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList = indexs.ToList();
                this.OutList.LoadItemCheckIndex(indexs);
                foreach(int index in indexs)
                    this.oldColumnName.Add(this.OutList.Items[index].ToString());
            }
        }
        #endregion
        #region 初始化配置
        private void InitOptionInfo()
        {
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetDataSourceInfo(this.opControl.ID);
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataPath = dataInfo["dataPath0"];
                this.DataInfoBox.Text = Path.GetFileNameWithoutExtension(this.dataPath);                
                SetOption(this.dataPath, this.DataInfoBox.Text, dataInfo["encoding0"]);
            }
        }

        private void SetOption(string path, string dataName, string encoding)
        {
 
            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Null, EnType(encoding));
            string column = bcpInfo.columnLine;
            this.columnName = column.Split('\t');
           
            foreach (string name in this.columnName)
            {
                this.OutList.AddItems(name);
                this.MaxValueBox.Items.Add(name);
            }
        }

        #endregion
        private DSUtil.Encoding EnType(string type)
        { return (DSUtil.Encoding)Enum.Parse(typeof(DSUtil.Encoding), type); }


    }

}
