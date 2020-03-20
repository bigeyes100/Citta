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
            XmlElement mapOriginNode = xDoc.CreateElement("MapOrigin");
            mapOriginNode.InnerText = this.modelDocument.MapOrigin.ToString();
            modelDocumentXml.AppendChild(mapOriginNode);

            List<ModelElement> modelElements = this.modelDocument.ModelElements;
            List<ModelRelation> modelRelations = this.modelDocument.ModelRelations;
            WriteModelElements(xDoc, modelDocumentXml, modelElements);
            WriteModelRelations(xDoc, modelDocumentXml, modelRelations);
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
                if (me.Type == ElementType.DataSource || me.Type == ElementType.Operator || me.Type == ElementType.Result)
                {
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
                    }
                }
                else if (me.Type == ElementType.Remark)
                {
                    XmlElement nameNode = xDoc.CreateElement("name");
                    nameNode.InnerText = me.RemarkName;
                    modelElementXml.AppendChild(nameNode);
                }
            }
        }
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
                startControlNode.InnerText = mr.Start;
                modelElementXml.AppendChild(startControlNode);

                XmlElement endControlNode = xDoc.CreateElement("end");
                endControlNode.InnerText = mr.End;
                modelElementXml.AppendChild(endControlNode);

                XmlElement startLocationNode = xDoc.CreateElement("startlocation");
                startLocationNode.InnerText = mr.StartLocation;
                modelElementXml.AppendChild(startLocationNode);

                XmlElement endLocationNode = xDoc.CreateElement("endlocation");
                endLocationNode.InnerText = mr.EndLocation;
                modelElementXml.AppendChild(endLocationNode);

                XmlElement endPinLabelNode = xDoc.CreateElement("endpin");
                endPinLabelNode.InnerText = mr.EndPin;
                modelElementXml.AppendChild(endPinLabelNode);
            }
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
                if (type == "Operator")
                {
                    try
                    {
                        String name = xn.SelectSingleNode("name").InnerText;                       
                        string status = xn.SelectSingleNode("status").InnerText;
                        string subType = xn.SelectSingleNode("subtype").InnerText;
                        int id = Convert.ToInt32(xn.SelectSingleNode("id").InnerText);
                        Point loc = ToPointType(xn.SelectSingleNode("location").InnerText);
                        MoveOpControl ctl = new MoveOpControl(0, name, loc);
                        ctl.textBox.Text = name;
                        ctl.Location = loc;
                        ModelElement operatorElement = ModelElement.CreateOperatorElement(ctl, name, EStatus(status), SEType(subType), id);
                        this.modelDocument.ModelElements.Add(operatorElement);
                    }
                    catch (Exception e) { System.Console.WriteLine(e.Message); }
                }
                else if (type == "DataSource")
                {
                    try
                    {
                        String name = xn.SelectSingleNode("name").InnerText;
                        string status = xn.SelectSingleNode("status").InnerText;
                        string subType = xn.SelectSingleNode("subtype").InnerText;
                        string bcpPath = xn.SelectSingleNode("path").InnerText;
                        int id = Convert.ToInt32(xn.SelectSingleNode("id").InnerText);
                        Point xnlocation =ToPointType(xn.SelectSingleNode("location").InnerText);
                        MoveDtControl cotl = new MoveDtControl(bcpPath, 0, name, xnlocation);//暂时定为为moveopctrol
                                                                                             //cotl.textBox1.Text = name;//暂时定为为moveopctrol
                        ModelElement dataSourceElement = ModelElement.CreateDataSourceElement(cotl, name, bcpPath, id);
                        this.modelDocument.ModelElements.Add(dataSourceElement);
                    }
                    catch (Exception e) { System.Console.WriteLine(e.Message); }
                }
                else if (type == "Remark")
                {
                    String name = xn.SelectSingleNode("name").InnerText;
                    ModelElement remarkElement = ModelElement.CreateRemarkElement(name);
                    this.modelDocument.ModelElements.Add(remarkElement);
                }
                else if (type == "Result")
                {
                    try 
                    {
                        String name = xn.SelectSingleNode("name").InnerText;
                        string status = xn.SelectSingleNode("status").InnerText;
                        string subType = xn.SelectSingleNode("subtype").InnerText;
                        int id = Convert.ToInt32(xn.SelectSingleNode("id").InnerText);
                        Point loc =ToPointType(xn.SelectSingleNode("location").InnerText);
                        MoveRsControl ctl = new MoveRsControl(0, name, loc);
                        ctl.textBox.Text = name;
                        ctl.Location = loc;
                        ModelElement resultElement = ModelElement.CreateResultElement(ctl, name, EStatus(status), SEType(subType), id);
                        this.modelDocument.ModelElements.Add(resultElement);
                    }
                    catch (Exception e) { System.Console.WriteLine(e.Message); }
                }
                else if (type == "Relation")
                {
                    string startID = xn.SelectSingleNode("start").InnerText;
                    string endID = xn.SelectSingleNode("end").InnerText;
                    string startLocation = xn.SelectSingleNode("startlocation").InnerText;
                    string endLocation = xn.SelectSingleNode("endlocation").InnerText;
                    string endPin = xn.SelectSingleNode("endpin").InnerText;
                    ModelRelation modelRelationElement = new ModelRelation(startID, endID, startLocation, endLocation, endPin);
                    this.modelDocument.ModelRelations.Add(modelRelationElement);
                }
            }
        }

        public ElementSubType SEType(string subType)
        { return (ElementSubType)Enum.Parse(typeof(ElementSubType), subType); }
        public ElementStatus EStatus(string status)
        { return (ElementStatus)Enum.Parse(typeof(ElementStatus), status); }
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