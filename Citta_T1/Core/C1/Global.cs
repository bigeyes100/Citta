﻿using C2.Business.Model;
using C2.Business.Option;
using C2.Controls;
using C2.Controls.Bottom;
using C2.Controls.Flow;
using C2.Controls.Left;
using C2.Controls.Right;
using C2.Controls.Top;
using C2.Forms;
using System.IO;
using System.Windows.Forms;

namespace C2.Core
{
    class Global
    {
        private static MainForm mainForm;
        private static MyModelControl myModelControl;
        private static BottomLogControl logView;
        private static DataSourceControl dataSourceControl; // 左侧数据源面板
        private static Panel bottomViewPanle;
        private static Panel leftToolBoxPanel;
        private static TaskBar taskBar;
        private static MindMapModelControl mindMapModelControl;





        public static MainForm GetMainForm() { return mainForm; }
        public static TaskBar GetTaskBar() { return taskBar; }
        public static Panel GetLeftToolBoxPanel() { return leftToolBoxPanel; }
        public static DataSourceControl GetDataSourceControl() { return dataSourceControl; }
        public static MyModelControl GetMyModelControl() { return myModelControl; }
        public static BottomLogControl GetLogView() { return logView; }
        public static Control GetBottomViewPanel() { return bottomViewPanle; }
        public static MindMapModelControl GetMindMapModelControl() { return mindMapModelControl; }

        public static CanvasForm GetCanvasForm()
        {
            CanvasForm cf = null;
            if (mainForm != null && mainForm.MdiClient != null && mainForm.MdiClient.ActivedMdiForm is CanvasForm)
            {
                cf = mainForm.MdiClient.ActivedMdiForm as CanvasForm;
            }
            return cf;
        }

        public static CanvasPanel GetCanvasPanel()
        {
            CanvasPanel ret = null;
            if (Global.GetCanvasForm() != null)
                ret = Global.GetCanvasForm().CanvasPanel;
            return ret;
        }
        public static NaviViewControl GetNaviViewControl()
        {
            NaviViewControl ret = null;
            if (Global.GetCanvasForm() != null)
            {
                ret = Global.GetCanvasForm().NaviViewControl;
            }
            return ret;
        }
        public static ModelDocumentDao GetModelDocumentDao() {
            ModelDocumentDao ret = null;
            if (Global.GetCanvasForm() != null)
                ret = Global.GetCanvasForm().ModelDocumentDao;
            return ret;
        }

        public static ModelDocument GetCurrentDocument()
        {
            ModelDocument ret = null;
            if (Global.GetCanvasForm() != null)
                ret = Global.GetCanvasForm().Document;
            return ret;
        }
       
        public static OperatorControl GetOperatorControl() {
            OperatorControl ret = null;
            if (Global.GetCanvasForm() != null)
                ret = Global.GetCanvasForm().OperatorControl;
            return ret;
        }
        public static RemarkControl GetRemarkControl()
        {
            RemarkControl ret = null;
            if (Global.GetCanvasForm() != null)
                ret = Global.GetCanvasForm().RemarkControl;
            return ret;
        }
        public static OptionDao GetOptionDao() {
            OptionDao ret = null;
            if (Global.GetCanvasForm() != null)
                ret = Global.GetCanvasForm().OptionDao;
            return ret;
        }

        public static TopToolBarControl GetTopToolBarControl()
        {
            TopToolBarControl ret = null;
            if (Global.GetCanvasForm() != null)
                ret = Global.GetCanvasForm().TopToolBarControl;
            return ret;
        }


        public static void SetMainForm(MainForm mf) { mainForm = mf; }
        public static void SetTaskBar(TaskBar tb) { taskBar = tb; }
        public static void SetLeftToolBoxPanel(Panel ltbp) { leftToolBoxPanel = ltbp; }
        public static void SetDataSourceControl(DataSourceControl dsc) { dataSourceControl = dsc; }
        public static void SetMyModelControl(MyModelControl mmc) { myModelControl = mmc; }
        public static void SetLogView(BottomLogControl lv) { logView = lv; }
        public static void SetBottomViewPanel(Panel bv) { bottomViewPanle = bv; }
        public static void SetMindMapModelControl(MindMapModelControl mmmc) { mindMapModelControl = mmmc; }
        private static string workspaceDirectory;           // 用户模型工作目录
        public static string WorkspaceDirectory { get => workspaceDirectory; set => workspaceDirectory = value; }
        public static string UserWorkspacePath { get => Path.Combine( workspaceDirectory,mainForm.UserName); }
        public const float Factor = 1.3F;
        private static string versionType;
        public static string VersionType { get => versionType; set => versionType = value; }
        public const string GreenLevel = "Green";
        public const string Nolanding = "NoLogin";
        public const string GreenPath = "source";
        public const string regPath = @"^(?<fpath>([a-zA-Z]:\\)([\s\.\-\w]+\\)*)(?<fname>[\w]+.[\w]+)";
    }
}
