﻿using C2.Controls.MapViews;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using C2.Business.Model;
using C2.Utils;
using C2.Core;

namespace C2.Model.Widgets
{
    public abstract class C2BaseWidget : Widget
    {
        [Browsable(false)]
        public virtual string Description { get;}

        [Browsable(false)]
        public List<DataItem> DataItems { get; set; } = new List<DataItem>();
        public override bool ResponseMouse { get => true; }

        public override Size CalculateSize(MindMapLayoutArgs e)
        {
            return new Size(20, 20);
        }
        #region 挂件 读/写
        protected void WriteDataItem(XmlElement node, List<DataItem> dataItems,string nodeName)
        {
            if (this.DataItems.Count < 0)
                return;
            XmlElement dataItemsNode = node.OwnerDocument.CreateElement(nodeName);
            foreach (var dataItem in dataItems)
            {
                WriteAttribute(dataItemsNode, dataItem, nodeName.TrimEnd('s'));
            }
            node.AppendChild(dataItemsNode);

        }
        protected void WriteAttribute(XmlElement parentNode, DataItem dataItem, string nodeName)
        {
            ModelXmlWriter mexw = new ModelXmlWriter(nodeName, parentNode);
            mexw.WriteAttribute("path", dataItem.FilePath)
                .WriteAttribute("name", dataItem.FileName)
                .WriteAttribute("separator", dataItem.FileSep.ToString())
                .WriteAttribute("encoding", dataItem.FileEncoding)
                .WriteAttribute("file_type", dataItem.FileType);
            // 图标挂件属性写入
            if (!string.IsNullOrEmpty(dataItem.ChartType))
            {
                mexw.WriteAttribute("chart_type", dataItem.ChartType)
                    .WriteAttribute("selected_indexs", string.Join(",", dataItem.SelectedIndexs))
                    .WriteAttribute("selected_items", string.Join(",", dataItem.SelectedItems));
            }
        }
        protected void ReadAttribute(XmlNodeList data_items,List<DataItem> DataItems)
        {
            foreach (XmlElement dataItem in data_items)
            {
                DataItem item = new DataItem(
                   dataItem.GetAttribute("path"),
                   dataItem.GetAttribute("name"),
                   ConvertUtil.TryParseAscii(dataItem.GetAttribute("separator")),
                   OpUtil.EncodingEnum(dataItem.GetAttribute("encoding")),
                   OpUtil.ExtTypeEnum(dataItem.GetAttribute("file_type")));

                // 图表格挂件属性读取
                item.ChartType = dataItem.GetAttribute("chart_type");
                if (!string.IsNullOrEmpty(item.ChartType))
                {
                    item.SelectedIndexs = Utils.ConvertUtil.TryParseIntList(dataItem.GetAttribute("selected_indexs"));
                    item.SelectedItems = new List<string>(dataItem.GetAttribute("selected_items").Split(','));
                }                               
                DataItems.Add(item);
            }
        }
        #endregion

        public static void DoPreViewDataSource(DataItem hitItem)
        {
            if (hitItem != null)
                Global.GetMainForm().PreViewDataByFullFilePath(hitItem);
        }
    }
}
