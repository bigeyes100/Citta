﻿using Citta_T1.Business.Schedule;
using Citta_T1.Controls.Flow;
using Citta_T1.Controls.Move;
using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Controls.Move.Rs;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace Citta_T1.Business.Model
{
    class ModelDocumentDao
    {
        private ModelDocument currentDocument;
        private List<ModelDocument> modelDocuments;
        
        
        public List<ModelDocument> ModelDocuments { get => modelDocuments; set => modelDocuments = value; }
        public ModelDocument CurrentDocument { get => currentDocument; set => currentDocument = value; }
        private string userInfoPath = Path.Combine(Global.WorkspaceDirectory, "UserInformation.xml");
        public ModelDocumentDao()
        {
            ModelDocuments = new List<ModelDocument>();         
        }
        public void AddBlankDocument(string modelTitle, string userName)
        {
            ModelDocument modelDocument = new ModelDocument(modelTitle, userName);
            ModelDocuments.ForEach(md => md.Hide());
            ModelDocuments.Add(modelDocument);
            CurrentDocument = modelDocument;
            Global.GetCanvasPanel().FrameWrapper.InitFrame();
        }
        public string SaveCurrentDocument()
        {
            CurrentDocument.Save();
            CurrentDocument.Dirty = false;
            return CurrentDocument.ModelTitle;
        }

        public string[] SaveAllDocuments()
        {
            List<string> titles = new List<string>();
            foreach (ModelDocument md in ModelDocuments)
            {
                // 不Dirty的大文档, 不重复保存,减少硬盘写
                if (!md.Dirty && md.ModelElements.Count > 10)
                    continue;

                md.Save();
                md.Dirty = false;
                titles.Add(md.ModelTitle);
            }
            return titles.ToArray();
        }
        public ModelDocument LoadDocument(string modelTitle, string userName)
        {
            
            ModelDocument md = new ModelDocument(modelTitle, userName);
            md.Load();
            md.Hide();
            md.ReCountDocumentMaxElementID();
            CurrentDocument = md;
            ModelDocuments.Add(md);
            Global.GetCanvasPanel().FrameWrapper.InitFrame();
            return md;

        }
        public void SwitchDocument(string modelTitle)
        {
            CurrentDocument = FindModelDocument(modelTitle);
            foreach (ModelDocument md in ModelDocuments)
            {
                if (md.ModelTitle == modelTitle)
                    md.Show();
                else
                    md.Hide();
            }
            Global.GetCanvasPanel().FrameWrapper.InitFrame();
        }
        public ModelElement AddDocumentOperator(MoveBaseControl ct)
        {
            ct.ID = this.currentDocument.ElementCount++;
            ModelElement e = ModelElement.CreateModelElement(ct);
            CurrentDocument.AddModelElement(e);
            return e;
        }

        private ModelDocument FindModelDocument(string modelTitle)
        {
            foreach (ModelDocument md in this.modelDocuments)
            {
                if (md.ModelTitle == modelTitle)
                    return md;
            }
            return null;
        }
        public List<ModelElement> DeleteCurrentDocument()
        {
            if (CurrentDocument == null)
                throw new NullReferenceException();
            this.ModelDocuments.Remove(CurrentDocument);
            return CurrentDocument.ModelElements; 
        }
        public void UpdateRemark(RemarkControl remarkControl)
        { 
            if (this.CurrentDocument == null)
                return;
            this.CurrentDocument.RemarkDescription = remarkControl.RemarkText;
        }

        public string GetRemark()
        {
            string remark = String.Empty;
            if (this.CurrentDocument != null)
                remark = this.CurrentDocument.RemarkDescription;
            return remark;
        }

        public bool WithoutDocumentLogin(string userName)
        {
            //新用户登录
            string userDir = Path.Combine(Global.WorkspaceDirectory,  userName);
            if (!Directory.Exists(userDir))
                return true;
            //非新用户但无模型文档
            DirectoryInfo di = new DirectoryInfo(userDir);
            DirectoryInfo[] directoryInfos = di.GetDirectories();
            return (directoryInfos.Length == 0); 
        }
        public void SaveEndDocuments(string userName)
        {
           
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(userInfoPath);
            var node = xDoc.SelectSingleNode("login");
            XmlNodeList bodyNodes = xDoc.GetElementsByTagName("user");
            foreach (XmlNode xn in bodyNodes)
            {
                if (xn.SelectSingleNode("name")!=null && xn.SelectSingleNode("name").InnerText == userName)
                {
                    XmlNodeList childNodes = xn.SelectNodes("modeltitle");
                    foreach (XmlNode xmlNode in childNodes)
                        xn.RemoveChild(xmlNode);
                    string[] saveTitle = LoadAllModelTitle(userName);
                    if (saveTitle.Length == 0)
                        return;
                    foreach (ModelDocument mb in this.modelDocuments)
                    {
                        if (!saveTitle.Contains(mb.ModelTitle))
                            continue;
                        XmlElement childElement = xDoc.CreateElement("modeltitle");
                        childElement.InnerText = mb.ModelTitle;
                        xn.AppendChild(childElement);                     
                    }
                    //关闭界面，用户只留下一个未保存的文档，则加载时随机打开一个文档
                    if (this.modelDocuments.Count == 1 && !saveTitle.Contains(this.modelDocuments[0].ModelTitle))
                    {
                        XmlElement childElement = xDoc.CreateElement("modeltitle");
                        childElement.InnerText = saveTitle[0];
                        xn.AppendChild(childElement);
                    }
                    xDoc.Save(userInfoPath);
                    return;
                }
            }
            

        }
        public string[] LoadSaveModelTitle(string userName)
        {
            List<string> modelTitleList = new List<string>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(userInfoPath);
           
            XmlNodeList userNode = xDoc.GetElementsByTagName("user");
            foreach (XmlNode xn in userNode)
            {
                if (xn.SelectSingleNode("name") != null && xn.SelectSingleNode("name").InnerText == userName)
                { 
                    XmlNodeList childNodes = xn.SelectNodes("modeltitle");
                    foreach (XmlNode xn2 in childNodes)
                    {
                        string modelTitle = xn2.InnerText;
                        if (Directory.Exists(System.IO.Path.Combine(Global.WorkspaceDirectory, userName, modelTitle)))
                            modelTitleList.Add(modelTitle);
                    }
                    if (modelTitleList.Count > 0)
                        return modelTitleList.Distinct().ToArray();
                }                   
            }                       
            return LoadAllModelTitle(userName);
        }
        public string[] LoadAllModelTitle(string userName)
        {
            string[] modelTitles = new string[0];
            try
            {
                DirectoryInfo userDir = new DirectoryInfo(Path.Combine(Global.WorkspaceDirectory, userName));
                DirectoryInfo[] dir = userDir.GetDirectories();
                modelTitles = Array.ConvertAll(dir, value => Convert.ToString(value));
            }
            catch { }          
            return modelTitles;
        }

        public ModelDocument GetManagerRelateModel(TaskManager manager)
        {
            return modelDocuments.Find(md => md.TaskManager == manager);
        }
        // 统计当前用户有多少元素
        public int CountAllModelElements()
        {
            int count = 0;
            foreach (ModelDocument md in this.ModelDocuments)
                count += md.ModelElements.Count;

            return count;
        }
        // 特定Datasource在当前所有打开模型中的引用次数
        public int CountDataSourceUsage(string ffp)
        {
            int count = 0;
            foreach (ModelDocument md in this.ModelDocuments)
                foreach (ModelElement me in md.ModelElements)
                    if (me.Type == ElementType.DataSource && me.FullFilePath == ffp)
                        count++;
            return count;
        }

    }
}
