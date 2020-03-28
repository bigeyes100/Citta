﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Citta_T1.Controls.Flow;
using Citta_T1.Controls.Move;
using Citta_T1.Utils;
using Citta_T1.Business.Option;

namespace Citta_T1.Business.Model
{
    class DocumentSaveLoad
    {
        private string modelPath;
        private string modelFilePath;
        private ModelDocument modelDocument;

        public DocumentSaveLoad(ModelDocument model)
        {
            this.modelPath = model.SavePath;
            this.modelFilePath = this.modelPath +  model.ModelTitle + ".xml";
            this.modelDocument = model;
        }
        public void WriteXml()
        {
            Directory.CreateDirectory(modelPath);
            XmlDocument xDoc = new XmlDocument();
            XmlElement modelDocumentXml = xDoc.CreateElement("ModelDocument");
            xDoc.AppendChild(modelDocumentXml);
            // 写坐标原点
            XmlElement mapOriginNode = xDoc.CreateElement("MapOrigin");
            mapOriginNode.InnerText = this.modelDocument.MapOrigin.ToString();
            modelDocumentXml.AppendChild(mapOriginNode);
            // 写算子,数据源，Result
            List<ModelElement> modelElements = this.modelDocument.ModelElements;
            WriteModelElements(xDoc, modelDocumentXml, modelElements);
            // 写关系
            List<ModelRelation> modelRelations = this.modelDocument.ModelRelations;    
            WriteModelRelations(xDoc, modelDocumentXml, modelRelations);
            // 写备注
            WriteModelRemark(xDoc, modelDocumentXml, this.modelDocument.RemarkDescription);
            xDoc.Save(modelFilePath);
        }
        private void WriteModelElements(XmlDocument xDoc, XmlElement modelDocumentXml, List<ModelElement> modelElements)
        {
            foreach (ModelElement me in modelElements)
            {
                XmlElement modelElementXml = xDoc.CreateElement("ModelElement");
                modelDocumentXml.AppendChild(modelElementXml);

                XmlElement typeNode = xDoc.CreateElement("type");
                typeNode.InnerText = me.Type.ToString();
                modelElementXml.AppendChild(typeNode);
            
                XmlElement nameNode = xDoc.CreateElement("name");
                nameNode.InnerText = me.GetDescription();
                modelElementXml.AppendChild(nameNode);

                XmlElement subTypeNode = xDoc.CreateElement("subtype");
                subTypeNode.InnerText = me.SubType.ToString();
                modelElementXml.AppendChild(subTypeNode);

                XmlElement locationNode = xDoc.CreateElement("location");
                locationNode.InnerText = me.Location.ToString();
                modelElementXml.AppendChild(locationNode);

                XmlElement statusNode = xDoc.CreateElement("status");
                statusNode.InnerText = me.Status.ToString();
                modelElementXml.AppendChild(statusNode);

                XmlElement idNode = xDoc.CreateElement("id");
                idNode.InnerText = me.ID.ToString();
                modelElementXml.AppendChild(idNode);

                if (me.Type == ElementType.DataSource)
                {
                    XmlElement pathNode = xDoc.CreateElement("path");
                    pathNode.InnerText = me.GetPath();
                    modelElementXml.AppendChild(pathNode);

                    XmlElement encodingNode = xDoc.CreateElement("encoding");
                    encodingNode.InnerText = me.Encoding.ToString();
                    modelElementXml.AppendChild(encodingNode);
                }
                if (me.Type == ElementType.Operator)
                { 
                    //有配置信息才保存到xml中
                    if((me.GetControl as MoveOpControl).Option.OptionDict.Count() > 0)
                        WriteModelOption(me.SubType, (me.GetControl as MoveOpControl).Option, xDoc, modelElementXml);
                }
                   
                
            }
        }
        #region 配置信息存到xml
        private void WriteModelOption(ElementSubType type,OperatorOption option, XmlDocument xDoc, XmlElement modelElementXml)
        {
            XmlElement optionNode = xDoc.CreateElement("option");
            modelElementXml.AppendChild(optionNode);
            switch (type)
            {
                case ElementSubType.MaxOperator:
                    WriteOptionElement(xDoc, optionNode, option, "maxfield");
                    WriteOptionElement(xDoc, optionNode, option, "outfield");
                    break;
                case ElementSubType.MinOperator:
                    WriteOptionElement(xDoc, optionNode, option, "minfield");
                    WriteOptionElement(xDoc, optionNode, option, "outfield");
                    break;
                case ElementSubType.FilterOperator:
                    foreach (string name in option.OptionDict.Keys)
                    { WriteOptionElement(xDoc, optionNode, option, name); }
                    break;
                case ElementSubType.RandomOperator:
                    WriteOptionElement(xDoc, optionNode, option, "randomnum");
                    WriteOptionElement(xDoc, optionNode, option, "outfield");
                    break;
                case ElementSubType.AvgOperator:
                    WriteOptionElement(xDoc, optionNode, option, "avgfield");
                    break;
                default:
                    break;
            }
                
        }
        private void WriteOptionElement(XmlDocument xDoc, XmlElement optionNode, OperatorOption option, string key)
        {
            if (option.GetOption(key) != "")
            {
                XmlElement outFieldNode = xDoc.CreateElement(key);
                outFieldNode.InnerText = option.GetOption(key);
                optionNode.AppendChild(outFieldNode);
            }
        }
        #endregion
        private void WriteModelRelations(XmlDocument xDoc, XmlElement modelDocumentXml,List<ModelRelation> modelRelations)
        {
           
            foreach (ModelRelation mr in modelRelations)
            {
                XmlElement modelElementXml = xDoc.CreateElement("ModelElement");
                modelDocumentXml.AppendChild(modelElementXml);

                XmlElement typeNode = xDoc.CreateElement("type");
                typeNode.InnerText = mr.Type.ToString();
                modelElementXml.AppendChild(typeNode);

                XmlElement startControlNode = xDoc.CreateElement("start");
                startControlNode.InnerText = mr.Start.ToString();
                modelElementXml.AppendChild(startControlNode);

                XmlElement endControlNode = xDoc.CreateElement("end");
                endControlNode.InnerText = mr.End.ToString();
                modelElementXml.AppendChild(endControlNode);

                XmlElement startLocationNode = xDoc.CreateElement("startlocation");
                startLocationNode.InnerText = mr.StartLocation.ToString();
                modelElementXml.AppendChild(startLocationNode);

                XmlElement endLocationNode = xDoc.CreateElement("endlocation");
                endLocationNode.InnerText = mr.EndLocation.ToString();
                modelElementXml.AppendChild(endLocationNode);

                XmlElement endPinLabelNode = xDoc.CreateElement("endpin");
                endPinLabelNode.InnerText = mr.EndPin.ToString();
                modelElementXml.AppendChild(endPinLabelNode);
            }
        }
        private void WriteModelRemark(XmlDocument xDoc, XmlElement modelDocumentXml, string remarkDescription)
        {
            XmlElement modelElementXml = xDoc.CreateElement("ModelElement");

            XmlElement typeNode = xDoc.CreateElement("type");
            typeNode.InnerText = "Remark";
            modelElementXml.AppendChild(typeNode);

            XmlElement nameNode = xDoc.CreateElement("name");
            nameNode.InnerText = remarkDescription;
            modelElementXml.AppendChild(nameNode);
            modelDocumentXml.AppendChild(modelElementXml);
        }
        public void ReadXml()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(modelFilePath);

            XmlNode rootNode = xDoc.SelectSingleNode("ModelDocument");    
            XmlNode mapOriginNode = rootNode.SelectSingleNode("MapOrigin");
            if(mapOriginNode != null)
                this.modelDocument.MapOrigin = ToPointType(mapOriginNode.InnerText);

            var nodeLists = rootNode.SelectNodes("ModelElement");
            foreach (XmlNode xn in nodeLists)
            {
                string type = xn.SelectSingleNode("type").InnerText;
                try
                {
                    if (type == "Operator")
                    {
                        String name = xn.SelectSingleNode("name").InnerText;
                        string status = xn.SelectSingleNode("status").InnerText;
                        string subType = xn.SelectSingleNode("subtype").InnerText;
                        int id = Convert.ToInt32(xn.SelectSingleNode("id").InnerText);
                        Point loc = ToPointType(xn.SelectSingleNode("location").InnerText);
                        MoveOpControl ctl = new MoveOpControl(0, name, loc);
                        ctl.ID = id;
                        ModelElement operatorElement = ModelElement.CreateOperatorElement(ctl, name, EStatus(status), SEType(subType), id);
                        this.modelDocument.ModelElements.Add(operatorElement);
                        if (xn.SelectSingleNode("option") != null)
                            ctl.Option = ReadOption(xn);


                    }
                    else if (type == "DataSource")
                    {
                        String name = xn.SelectSingleNode("name").InnerText;
                        string status = xn.SelectSingleNode("status").InnerText;
                        string subType = xn.SelectSingleNode("subtype").InnerText;
                        string bcpPath = xn.SelectSingleNode("path").InnerText;
                        int id = Convert.ToInt32(xn.SelectSingleNode("id").InnerText);
                        Point xnlocation = ToPointType(xn.SelectSingleNode("location").InnerText);
                        MoveDtControl cotl = new MoveDtControl(bcpPath, 0, name, xnlocation);
                        cotl.Encoding = EnType(xn.SelectSingleNode("encoding").InnerText);
                        ModelElement dataSourceElement = ModelElement.CreateDataSourceElement(cotl, name, bcpPath, id);
                        this.modelDocument.ModelElements.Add(dataSourceElement);
                    }
                    else if (type == "Remark")
                    {
                        String name = xn.SelectSingleNode("name").InnerText;
                        this.modelDocument.RemarkDescription = name;
                    }
                    else if (type == "Result")
                    {
                        String name = xn.SelectSingleNode("name").InnerText;
                        string status = xn.SelectSingleNode("status").InnerText;
                        string subType = xn.SelectSingleNode("subtype").InnerText;
                        int id = Convert.ToInt32(xn.SelectSingleNode("id").InnerText);
                        Point loc = ToPointType(xn.SelectSingleNode("location").InnerText);
                        MoveRsControl ctl = new MoveRsControl(0, name, loc);
                        ModelElement resultElement = ModelElement.CreateResultElement(ctl, name, EStatus(status), SEType(subType), id);
                        this.modelDocument.ModelElements.Add(resultElement);
                    }
                    else if (type == "Relation")
                    {
                        int startID = Convert.ToInt32(xn.SelectSingleNode("start").InnerText);
                        int endID = Convert.ToInt32(xn.SelectSingleNode("end").InnerText);
                        Point startLocation = ToPointType(xn.SelectSingleNode("startlocation").InnerText);
                        Point endLocation = ToPointType(xn.SelectSingleNode("endlocation").InnerText);
                        int endPin = Convert.ToInt32(xn.SelectSingleNode("endpin").InnerText);
                        ModelRelation modelRelationElement = new ModelRelation(startID, endID, startLocation, endLocation, endPin);
                        this.modelDocument.ModelRelations.Add(modelRelationElement);
                    }
                }
                catch(Exception e) { System.Console.WriteLine(e.Message); }
               
            }
        }
        private OperatorOption ReadOption(XmlNode xn)
        {
            OperatorOption option = new OperatorOption();
                foreach (XmlNode node in xn.SelectSingleNode("option").ChildNodes)
                    option.SetOption(node.Name, node.InnerText);
            return option;
        }
        public ElementSubType SEType(string subType)
        { return (ElementSubType)Enum.Parse(typeof(ElementSubType), subType); }
        public ElementStatus EStatus(string status)
        { return (ElementStatus)Enum.Parse(typeof(ElementStatus), status); }
        public DSUtil.Encoding EnType(string type)
        { return (DSUtil.Encoding)Enum.Parse(typeof(DSUtil.Encoding), type); }
        private Point ToPointType(string point)
        {
            Point location = new Point();
            try
            {
                string coordinate = Regex.Replace(point, @"[^\d,-]*", "");
                string[] xy = coordinate.Split(',');
                location = new Point(Convert.ToInt32(xy[0]), Convert.ToInt32(xy[1]));
            }
            catch (Exception e) { System.Console.WriteLine(e.Message); }
            return location;
        }
    }
}
