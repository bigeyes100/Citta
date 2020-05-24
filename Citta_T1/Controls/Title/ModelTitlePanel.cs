﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Citta_T1.Utils;

namespace Citta_T1.Controls.Title
{
    public delegate void NewDocumentEventHandler(string modelTitle);
    public delegate void DocumentSwitchHandler(string modelTitle);
    public partial class ModelTitlePanel : UserControl
    {
        private static LogUtil log = LogUtil.GetInstance("ModelTitlePanel");

        private static Point OriginalLocation = new System.Drawing.Point(1, 6);
        private List<ModelTitleControl> models;
        private int rawModelTitleNum = 9;
        public event NewDocumentEventHandler NewModelDocument;
        public event DocumentSwitchHandler ModelDocumentSwitch;
 
        public ModelTitlePanel()
        {
            models = new List<ModelTitleControl>();
            InitializeComponent();
        }


        public void UpModelTitle()
        {
            rawModelTitleNum = this.Width / 142;
            foreach (ModelTitleControl mt in models)
            {
                if (models.Count <= rawModelTitleNum)
                    mt.SetOriginalModelTitle(mt.ModelTitle);
                else if (models.Count > rawModelTitleNum && models.Count < 17)
                    mt.SetNewModelTitle(mt.ModelTitle, 3);
                else if (models.Count >= 17 && models.Count < 20)
                    mt.SetNewModelTitle(mt.ModelTitle, 2);
                else if (models.Count >= 20 && models.Count < 24)
                    mt.SetNewModelTitle(mt.ModelTitle, 1);
                else if (models.Count >= 24)
                    mt.SetNewModelTitle(mt.ModelTitle, 0);
            }
        }
        public void LoadModelDocument(string[] modelTitles) 
        {
            int end = modelTitles.Length - 1;
            for (int i = 0; i < modelTitles.Length; i++)
            {
                ModelTitleControl mtControl = new ModelTitleControl();
                mtControl.ModelDocumentSwitch += DocumentSwitch;
                this.models.Add(mtControl);
                this.Controls.Add(mtControl);

                // 根据元素个数调整位置和大小
                mtControl.SetOriginalModelTitle(modelTitles[i]);
                if (i == 0)
                    mtControl.Location = OriginalLocation;
                else
                {
                    ModelTitleControl preMTC = models[models.Count - 2];
                    mtControl.Location = new Point(preMTC.Location.X + preMTC.Width + 2, 6);
                    ResizeModel();
                    UpModelTitle();
                }
                if (i == end)
                { 
                    mtControl.BorderStyle = BorderStyle.FixedSingle;
                    mtControl.Selected = true;
                }
                    
                
            }
           
        }

        public void AddModel(string modelTitle)
        {
            ModelTitleControl mtControl = new ModelTitleControl();
            mtControl.ModelDocumentSwitch += DocumentSwitch;
            models.Add(mtControl);
            mtControl.SetOriginalModelTitle(modelTitle);
            NewModelDocument?.Invoke(modelTitle);



            // 根据容器中最后一个ModelTitleControl的Location
            // 设置新控件在ModelTitlePanel中的Location
            if (models.Count <= 1)
            {
                mtControl.Location = OriginalLocation;
                this.Controls.Add(mtControl);
                mtControl.ShowSelectedBorder();
            }
            else // models.Count > 1
            {
                ModelTitleControl preMTC = models[models.Count - 2];
                mtControl.Location = new Point(preMTC.Location.X + preMTC.Width + 2, 6);
                this.Controls.Add(mtControl);
                ResizeModel();
                UpModelTitle();
                mtControl.ShowSelectedBorder();
            }
        }

        /*
         * removeTag  删除动作引起的ResizeModel
         */
        public void ResizeModel(bool removeTag = false)
        {

            rawModelTitleNum = this.Width / 142;
            try
            {
                if (0 < models.Count && models.Count <= rawModelTitleNum && removeTag)
                {
                    for (int i = 0; i < models.Count; i++)
                    {
                        models[i].Size = new Size(140, 26);
                        if (i == 0)
                            models[i].Location = OriginalLocation;
                        else
                        {
                            ModelTitleControl preMTC = models[i - 1];
                            models[i].Location = new Point(preMTC.Location.X + preMTC.Width + 2, 6);
                        }
                    }
                }
                if (models.Count > rawModelTitleNum)
                {
                    for (int i = 0; i < models.Count; i++)
                    {
                        ModelTitleControl mtc = models[i];
                        mtc.Width = (this.Size.Width - 1) / models.Count - 2;
                        int origWidth = mtc.Width;
                        if (i == 0)
                            mtc.Location = OriginalLocation;
                        else if (i >= models.Count - 3)
                        {
                            mtc.Width = (this.Size.Width - (origWidth + 2) * models.Count) / 3 + origWidth;
                            mtc.Location = new Point((origWidth + 2) * (models.Count - 3) + (mtc.Width + 2) * (i - models.Count + 3), 6);
                        }
                        else
                            mtc.Location = new Point((mtc.Width + 2) * i, 6);
                    }
                }
            }
            catch (Exception ex)
            { log.Error("ModelTitlePanel 未将对象引用设置到对象的实例: " + ex.ToString()); }
            
        }
        public void RemoveModel(ModelTitleControl mtControl)
        {
            // 关闭正是当前文档，需要重新选定左右两边的文档中的一个
            if (mtControl.Selected)
            {
                int index = models.IndexOf(mtControl);
                // 优先选择右边的
                if (index != -1 && index + 1 < models.Count)
                    models[index + 1].ShowSelectedBorder();
                // 其次选择左边的
                else if (index != -1 && index - 1 >= 0)
                    models[index - 1].ShowSelectedBorder();
                log.Info("删除的index为" + index.ToString());
            }
            models.Remove(mtControl);
            this.Controls.Remove(mtControl);
            mtControl.Dispose();
            // 当文档全部关闭时，自动创建一个新的默认文档
            if (models.Count == 0)
                AddModel("新建模型");
            UpModelTitle();
            ResizeModel(true);//重新设置model大小
           

        }
        public void ClearSelectedBorder()
        {
            foreach (ModelTitleControl mtc in this.models)
                mtc.BorderStyle = BorderStyle.None;
        }
        public void SelectedModel(string modelTitle)
        {
            foreach (ModelTitleControl mtc in this.models)
            {
                if(mtc.ModelTitle == modelTitle)
                 mtc.ShowSelectedBorder();
            }
               
        }

        private void ModelTitlePanel_SizeChanged(object sender, EventArgs e)
        {
            ResizeModel(true);
            UpModelTitle();
        }
        public void DocumentSwitch(string modelTitle)
        {
            ModelDocumentSwitch?.Invoke(modelTitle);
        }

        public bool ContainModel(string modelTitle)
        {
            bool ret = false;
            foreach (Control ct in this.Controls)
            {
                if (ct is ModelTitleControl && (ct as ModelTitleControl).ModelTitle == modelTitle)
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }

        public bool ResetDirtyPictureBox(string modelTitle, bool dirty)
        {
            bool ret = false;
            foreach (Control ct in this.Controls)
            {
                if (ct is ModelTitleControl && (ct as ModelTitleControl).ModelTitle == modelTitle)
                {
                    if (dirty)
                        (ct as ModelTitleControl).SetDirtyPictureBox();
                    else
                        (ct as ModelTitleControl).ClearDirtyPictureBox();

                    ret = true;
                }
            }
            return ret;
        }
    }
}
