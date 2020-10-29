﻿using C2.Dialogs.C2OperatorViews;
using C2.Model;
using C2.Model.MindMaps;
using C2.Model.Widgets;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C2.Controls.Common
{
    public partial class DesignerControl : UserControl
    {


        public Topic SelectedTopic { get; set; }
        public OperatorWidget OpWidget { get; set; }
        public DataItem SelectedDataSource { get; set; }
        public string SelectedOperator { get; set; }
        public List<DataItem> ComboDataSource { get; set; }

        public void SetSelectedTopicDesign(Topic topic)
        {
            SelectedTopic = topic;
            if(SelectedTopic == null)
            {
                this.topicName.Text = "未选中主题";
                this.dataSourceCombo.Text = "";
                this.dataSourceCombo.Items.Clear();
                this.operatorName.Text = "";
            }
            else
            {
                OpWidget = SelectedTopic.FindWidget<OperatorWidget>();
                SetSelectedTopic();//设置选中主题
                SetSelectedDataSource();//设置选中数据源
                SetComboDataSource();
                SetSelectedOperator();
            }

        }

        private void SetSelectedTopic()
        {
            this.topicName.Text = SelectedTopic.Text;
        }

        private void SetSelectedDataSource()
        {
            if (OpWidget != null)
            {
                //TODO
                //dtw.选中数据源;
                DataItem d1 = OpWidget.DataSourceItem;
                if (d1 != null)
                {
                    SelectedDataSource = d1;
                    this.dataSourceCombo.Text = d1.FileName;
                }
                else
                {
                    SelectedDataSource = null;
                    this.dataSourceCombo.Text = "请选择数据源";
                }
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
            this.operatorName.Text = OpWidget != null ? OpWidget.OpType : "";
            SelectedOperator = OpWidget != null ? OpWidget.OpType : "";
        }

        public DesignerControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if(this.topicName.Text == "未选中主题")
            {
                MessageBox.Show("未选中主题，请选中主题后再配置");
                return;
            }

            if (OpWidget.DataSourceItem == null)
            {
                MessageBox.Show("未选中数据源,请添加后再配置");
                return;
            }

            if (string.IsNullOrEmpty(SelectedOperator))
            {
                MessageBox.Show("未添加算子,请添加后再配置");
                return;
            }


            switch (SelectedOperator)
            {
                case "最大值":
                    var dialog = new C2MaxOperatorView(OpWidget);
                    if(dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        OpWidget.OpName = OpWidget.DataSourceItem.FileName + "-" + OpWidget.OpType;
                        DataItem resultItem = OpWidget.ResultItem;
                        SelectedTopic.Widgets.Add(new ResultWidget());
                        OpWidget.Status = OpStatus.Ready;
                    }
                    break;
                case "AI实践":
                    new C2CustomOperatorView(OpWidget).ShowDialog(this);
                    break;
                default:
                    break;
            }

        }

        private void dataSourceCombo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            OperatorWidget opw = SelectedTopic.FindWidget<OperatorWidget>();

            if (opw != null)
            {
                opw.DataSourceItem = ComboDataSource[this.dataSourceCombo.SelectedIndex];
            }
        }
    }
}