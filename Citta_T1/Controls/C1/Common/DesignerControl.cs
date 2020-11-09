﻿using C2.Controls.MapViews;
using C2.Dialogs.Base;
using C2.Dialogs.C2OperatorViews;
using C2.Globalization;
using C2.Model;
using C2.Model.MindMaps;
using C2.Model.Widgets;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace C2.Controls.Common
{
    public partial class DesignerControl : BorderPanel
    {
        private List<OpType> ComboOperator;
        public Topic SelectedTopic { get; set; }
        public MindMapView MindmapView { get; set; }
        public OperatorWidget OpWidget { get; set; }
        public DataItem SelectedDataSource { get; set; }
        public string SelectedOperator { get; set; }
        public List<DataItem> ComboDataSource { get; set; }

        public DesignerControl()
        {
            InitializeComponent();
            Font = UITheme.Default.DefaultFont;
            ComboDataSource = new List<DataItem>();
            InitComboOperator();
        }

        private void InitComboOperator()
        {
            ComboOperator = new List<OpType>();
            foreach (OpType opType in Enum.GetValues(typeof(OpType)))
            {
                if(opType==OpType.Null || opType == OpType.ModelOperator)
                    continue;
                string tmpOpType = Lang._(opType.ToString());
                ComboOperator.Add(opType);
                this.operatorCombo.Items.Add(tmpOpType);
            }
        }

        public void SetSelectedTopicDesign(Topic topic,MindMapView mindmapview)
        {
            SelectedTopic = topic;
            MindmapView = mindmapview;
            if(SelectedTopic == null)
            {
                this.topicName.Text = "未选中主题";
                this.dataSourceCombo.Text = String.Empty;
                this.dataSourceCombo.Items.Clear();
                this.operatorCombo.Text = String.Empty;
            }
            else
            {
                OpWidget = SelectedTopic.FindWidget<OperatorWidget>();
                SetSelectedTopic();//设置选中主题√
                SetComboDataSource();//设置数据源下拉选项√
                SetSelectedOperator();//设置选中算子
                SetSelectedDataSource();//设置选中数据源
            }

        }

        private void SetSelectedTopic()
        {
            this.topicName.Text = SelectedTopic.Text;
        }

        private void SetSelectedDataSource()
        {
            /*
             * 1、当算子挂件不存在时，置空
             * 2、算子挂件存在时
             *      2.1  opw.DataSourceItem 为空，置空
             *      2.2                                  不为空，赋值
             *      
             *  算子挂件中存的数据源，和下拉数据源对比问题：
             *  (1)有数据源，有下拉数据源，比较是否包含
             *       包含：正常显示
             *       不包含：置空
             *  (2)至少有一个为空，直接置空
             */
            if(OpWidget == null)
            {
                SelectedDataSource = null;
                this.dataSourceCombo.Text = string.Empty;
                return;
            }

            DataItem d1 = OpWidget.DataSourceItem;
            if(ComboDataSource == null || d1 == null)
            {
                SelectedDataSource = null;
                this.dataSourceCombo.Text = string.Empty;
            }
            else if (ComboDataSource.Contains(d1))
            {
                SelectedDataSource = d1;
                this.dataSourceCombo.Text = d1.FileName;
            }
            else
            {
                SelectedDataSource = null;
                this.dataSourceCombo.Text = string.Empty;
            }
        }

        private void SetComboDataSource()
        {
            this.dataSourceCombo.Items.Clear();

            //TODO
            //数据大纲，父类所有数据源,暂用固定列表模拟
            DataSourceWidget dtw = SelectedTopic.FindWidget<DataSourceWidget>();
            if (dtw != null)
            {
                List<DataItem> di = dtw.DataItems;
                foreach (DataItem dataItem in di)
                {
                    this.dataSourceCombo.Items.Add(dataItem.FileName);
                }
                ComboDataSource = di;
            }

        }

        private void SetSelectedOperator()
        {
            if (OpWidget != null && OpWidget.OpType != OpType.Null)
            {
                this.operatorCombo.Text = Lang._(OpWidget.OpType.ToString());
                SelectedOperator = Lang._(OpWidget.OpType.ToString());
            }
            else
            {
                this.operatorCombo.Text = string.Empty;
                SelectedOperator = string.Empty;
            }
        }


        private void Button1_Click(object sender, System.EventArgs e)
        {
            if(this.topicName.Text == "未选中主题")
            {
                MessageBox.Show("未选中主题，请选中主题后再配置");
                return;
            }

            if (SelectedDataSource ==null || SelectedDataSource.IsEmpty())
            {
                MessageBox.Show("未选中数据源,请添加后再配置");
                return;
            }

            if (string.IsNullOrEmpty(SelectedOperator))
            {
                MessageBox.Show("未添加算子,请添加后再配置");
                return;
            }

            if (OpWidget == null)
            {
                MindmapView.AddOperator(new Topic[] { SelectedTopic });
                OpWidget = SelectedTopic.FindWidget<OperatorWidget>();
            }

            OpWidget.OpType =ComboOperator[this.operatorCombo.SelectedIndex];
            OpWidget.DataSourceItem = ComboDataSource[this.dataSourceCombo.SelectedIndex];

            C2BaseOperatorView dialog = GenerateOperatorView();
            if (dialog != null && dialog.ShowDialog(this) == DialogResult.OK)
                OpWidget.Status = OpStatus.Ready;


        }

        private C2BaseOperatorView GenerateOperatorView()
        {
            switch (OpWidget.OpType)
            {
                case OpType.MaxOperator:return new C2MaxOperatorView(OpWidget);
                case OpType.CustomOperator:return new C2CustomOperatorView(OpWidget);
                case OpType.MinOperator:return new C2MinOperatorView(OpWidget);
                case OpType.AvgOperator:return new C2AvgOperatorView(OpWidget);
                case OpType.DataFormatOperator:return new C2DataFormatOperatorView(OpWidget);
                case OpType.RandomOperator:return new C2RandomOperatorView(OpWidget);
                case OpType.FreqOperator:return new C2FreqOperatorView(OpWidget);
                case OpType.SortOperator:return new C2SortOperatorView(OpWidget);
                case OpType.FilterOperator:return new C2FilterOperatorView(OpWidget);
                case OpType.GroupOperator:return new C2GroupOperatorView(OpWidget);
                case OpType.PythonOperator:return new C2PythonOperatorView(OpWidget);
                default:return null;
            }
        }

        private void DataSourceCombo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            SelectedDataSource = ComboDataSource[this.dataSourceCombo.SelectedIndex];
        }

        private void OperatorCombo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            SelectedOperator = Lang._(ComboOperator[this.operatorCombo.SelectedIndex].ToString());
        }
    }
}