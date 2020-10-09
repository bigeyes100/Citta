using Citta_T1.Business.Model;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace Citta_T1.Controls.Left
{
    public partial class ModelButton : UserControl
    {
        private string oldTextString;
        private string fullFilePath;

        public ModelButton(string modelTitle)
        {
            InitializeComponent();
            this.textButton.Text = modelTitle;
            this.oldTextString = modelTitle;
            fullFilePath = Path.Combine(Global.GetCurrentDocument().UserPath, this.textButton.Text, this.textButton.Text + ".xml");
            finallyName = string.Empty;
            dataPath = string.Empty;
            tmpModelPath = string.Empty;
        }


        public string ModelTitle => this.textButton.Text;

        public void EnableOpenDocumentMenu() { this.OpenToolStripMenuItem.Enabled = true; }
        public void EnableRenameDocumentMenu() { this.RenameToolStripMenuItem.Enabled = true; }
        public void EnableDeleteDocumentMenu() { this.DeleteToolStripMenuItem.Enabled = true; }
        public string FullFilePath { get => fullFilePath; set => fullFilePath = value; }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 文件打开后,不能重复打开,不能删除,不能重命名
            Global.GetMainForm().LoadDocument(this.textButton.Text);
            this.OpenToolStripMenuItem.Enabled = false;
            this.RenameToolStripMenuItem.Enabled = false;
            this.DeleteToolStripMenuItem.Enabled = false;
        }

        private void ModelButton_Load(object sender, EventArgs e)
        {
            // 模型全路径浮动提示信息
            String helpInfo = FullFilePath;
            this.toolTip1.SetToolTip(this.rightPictureBox, helpInfo);

            // 模型名称浮动提示信息
            helpInfo = ModelTitle;
            this.toolTip1.SetToolTip(this.textButton, helpInfo);
        }

        private void ExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.ExploreDirectory(FullFilePath);
        }

        private void CopyFilePathToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.TryClipboardSetText(FullFilePath);
        }

        private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  容错处理, 标题栏中文档未关闭时,不能重命名
            if (Global.GetModelTitlePanel().ContainModel(this.ModelTitle))
                return;

            this.textBox.Text = ModelTitle;
            this.textBox.ReadOnly = false;
            this.oldTextString = ModelTitle;
            this.textButton.Visible = false;
            this.textBox.Visible = true;
            this.textBox.Focus();//获取焦点
            this.textBox.Select(this.textBox.TextLength, 0);
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 容错处理, 标题栏中文档未关闭时,不能删除
            if (Global.GetModelTitlePanel().ContainModel(this.ModelTitle))
                return;
            // 删除前用对话框确认
            DialogResult rs = MessageBox.Show(String.Format("删除模型 {0}, 继续删除请点击 \"确定\"", ModelTitle),
                    "删除 " + this.ModelTitle,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);

            if (rs != DialogResult.OK)
                return;

            string modelDic = System.IO.Path.Combine(Global.GetCurrentDocument().UserPath, ModelTitle);
            FileUtil.DeleteDirectory(modelDic);
            Global.GetMyModelControl().RemoveModelButton(this);
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 按下回车键
            if (e.KeyChar == 13)
            {
                FinishTextChange();
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            FinishTextChange();
        }

        private void TextButton_MouseDown(object sender, MouseEventArgs e)
        {
            // 鼠标左键双击触发
            if (e.Button != MouseButtons.Left || e.Clicks != 2)
                return;
            RenameToolStripMenuItem_Click(sender, e);
        }


        private void FinishTextChange()
        {
            if (this.textBox.Text.Trim().Length == 0)
                this.textBox.Text = this.oldTextString;
            this.textBox.ReadOnly = true;
            this.textBox.Visible = false;
            this.textButton.Visible = true;
            if (this.oldTextString == this.textBox.Text)
                return;
            this.textButton.Text = this.textBox.Text.Trim();

            // 新旧名称相同, 不需要做目录操作
            if (ModelTitle == oldTextString)
                return;

            string newModelDirectory = Path.Combine(Global.GetCurrentDocument().UserPath, ModelTitle);
            string oldModelDirectory = Path.Combine(Global.GetCurrentDocument().UserPath, oldTextString);
            string tmpFFP = Path.Combine(newModelDirectory, oldTextString + ".xml");
            string newFFP = Path.Combine(newModelDirectory, ModelTitle + ".xml");

            // 开始移动文件
            bool ret = FileUtil.DirecotryMove(oldModelDirectory, newModelDirectory);
            if (!ret) // 失败回滚
            {
                this.textButton.Text = oldTextString;
                return;
            }
            // 目前的机制，到这两步，一旦失败就无法回滚了
            ret = ModelDocument.ModifyRsPath(tmpFFP, oldModelDirectory, newModelDirectory);
            ret = FileUtil.FileMove(tmpFFP, newFFP);

            // 重命名
            this.oldTextString = ModelTitle;
            FullFilePath = newFFP;
            this.toolTip1.SetToolTip(this.textButton, ModelTitle);
            this.toolTip1.SetToolTip(this.rightPictureBox, FullFilePath);
        }

        private void ImportModelButton_Click(object sender, EventArgs e)
        {
            //模型文档不存在返回
            if (!File.Exists(this.FullFilePath))
            {
                MessageBox.Show("模型文档不存在，可能已被删除");
                return;
            }            

            //准备要导出的模型文档
            if (!CopyFiles())
                return;

            //获取文档要保存的路径
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.Filter = "模型文件(*.iao)|*.iao"; //文件类型
            saveFileDialog1.Title = "导出模型";//标题
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string fileName = saveFileDialog1.FileName;
                // 压缩成iao导出模型文件
                Utils.ZipAndUnZipUtil.CreateZip(Path.GetDirectoryName(this.FullFilePath),fileName);
                MessageBox.Show("模型导出成功,存储路径："+ fileName);

            }

            // 压缩完，删除data文件夹，删除临时模型文件xml
            if (Directory.Exists(this.dataPath))
                Directory.Delete(this.dataPath,true);
            if (File.Exists(this.FullFilePath))
                File.Delete(this.FullFilePath);
            if (File.Exists(tmpModelPath))
                File.Move(tmpModelPath, this.FullFilePath);

        }
        private string tmpModelPath;
        private bool CopyFiles()
        {
            string modelPath = Path.GetDirectoryName(this.FullFilePath);
            this.tmpModelPath= Path.Combine(modelPath, "_"+Path.GetFileNameWithoutExtension(this.FullFilePath)+".md");
            // tmpModelPath为模型xml的副本
            File.Copy(this.FullFilePath, tmpModelPath,true);
            // 创建存储数据的_datas文件夹
            this.dataPath = Path.Combine(modelPath, "_datas");
            if (Directory.Exists(dataPath))
                Directory.Delete(dataPath,true);
            Directory.CreateDirectory(dataPath);
            return CopyDatas();
     
         
        }
        private bool CopyDatas()
        {
            bool copySuccess = true;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(this.FullFilePath);
            XmlNode rootNode = xDoc.SelectSingleNode("ModelDocument");
            // 数据源
            XmlNodeList nodes = rootNode.SelectNodes("//ModelElement[type='DataSource']");
            // dataSourceNames存放拷贝到_datas目录中的文件名称
            List<string> dataSourceNames = new List<string>();
            Dictionary<string, string> allPaths = new Dictionary<string, string>();
            if (!CopyFile(nodes, allPaths, dataSourceNames))
                return !copySuccess;

            // AI、多源算子
            XmlNodeList customNodes0 = rootNode.SelectNodes("//ModelElement[subtype='CustomOperator1']");
            XmlNodeList customNodes1 = rootNode.SelectNodes("//ModelElement[subtype='CustomOperator2']");
            if (!CopyFile(customNodes0, allPaths, dataSourceNames)|| !CopyFile(customNodes1, allPaths, dataSourceNames))
                return !copySuccess;

            // Python算子
            /*
             * 遍历XML所有python算子的cmd节点
             * 取出cmd节点的路径
             * 是已经处理过相同路径，跳过
             * 不是处理过的路径，文件拷贝并修改xml中文件名称
             */
            XmlNodeList pythonNodes = rootNode.SelectNodes("//ModelElement[subtype='PythonOperator']");
            Regex reg0 = new Regex(@"^(?<fpath>([a-zA-Z]:\\)([\s\.\-\w]+\\)*)(?<fname>[\w]+.[\w]+)");          
            foreach (XmlNode pythonNode in pythonNodes)
            {
                XmlNode optionNode= pythonNode.SelectSingleNode("option");
                if (optionNode == null)
                    continue;

                string outputParamPath = string.Empty;
                if (optionNode.SelectSingleNode("outputParamPath") != null)
                    outputParamPath = optionNode.SelectSingleNode("outputParamPath").InnerText;
                string[] cmd = optionNode.SelectSingleNode("cmd").InnerText.Split(' ');
                List<string> paths = new List<string>();
                foreach (string item in cmd)
                {
                    bool factor0 = reg0.IsMatch(item) && !item.ToLower().Contains("python.exe");
                    bool factor1 = string.IsNullOrEmpty(outputParamPath)||!string.IsNullOrEmpty(outputParamPath) && !item.Contains(outputParamPath);
                    if (factor0 && factor1)
                        paths.Add(item);
                }
                foreach (string path in paths)
                {
                    if (allPaths.ContainsKey(path))
                    {
                        // 相同数据源，直接使用已经命名好的数据源
                        optionNode.SelectSingleNode("cmd").InnerText.Replace(path, allPaths[path]);
                        continue;
                    }

                    if(!CopyDatas(optionNode, path, dataSourceNames,false))
                        return !copySuccess;
                    //修改cmd中路径的文件名
                    string newPath = Path.Combine(Path.GetDirectoryName(path), this.finallyName);
                    optionNode.SelectSingleNode("cmd").InnerText.Replace(path, newPath);
                    //修改对应节点中路径的文件名
                    RePlacePythonNodePath(optionNode, path, newPath);
                    allPaths[path] = Path.Combine(Path.GetDirectoryName(path), this.finallyName);
                }             
            }
      
            // Python、AI、多源算子连接的结果算子路径赋值
            XmlNodeList resultNodes = rootNode.SelectNodes("//ModelElement[type='Result']");
            foreach (XmlNode node in resultNodes)
            {
                if (node.SelectSingleNode("path") == null)
                    throw new ArgumentNullException("message: The path of the result operator is empty"); ;
                string path = node.SelectSingleNode("path").InnerText;
                if (allPaths.ContainsKey(path))
                    node.SelectSingleNode("path").InnerText = allPaths[path];
            }
            xDoc.Save(this.FullFilePath);
            return copySuccess;
        }
        private bool CopyFile(XmlNodeList nodes, Dictionary<string, string>  allPaths, List<string> dataSourceNames)
        {
            bool copySuccess = true;
            
            foreach (XmlNode xmlNode in nodes)
            {
                bool isAIOperator = xmlNode.SelectSingleNode("subtype").InnerText.Contains("CustomOperator");
                if (!isAIOperator && xmlNode.SelectSingleNode("path") == null)
                    continue;
                
                if (isAIOperator&& xmlNode.SelectSingleNode("option").SelectSingleNode("path") == null)
                    continue;
                string path = isAIOperator? xmlNode.SelectSingleNode("option").SelectSingleNode("path").InnerText:xmlNode.SelectSingleNode("path").InnerText;
                if (HasSamePathNode(path, xmlNode, allPaths))
                    continue;
                if (!CopyDatas(xmlNode, path, dataSourceNames))
                    return !copySuccess;
                if (!isAIOperator)
                {
                    if (xmlNode.SelectSingleNode("path")==null)
                        continue;
                    allPaths[path] = xmlNode.SelectSingleNode("path").InnerText; 
                }
                   
                else
                {
                    if (xmlNode.SelectSingleNode("option") == null)
                        continue;
                    allPaths[path] = xmlNode.SelectSingleNode("option/path").InnerText;
                }
                 
            }
            return copySuccess;
        }
        private void RePlacePythonNodePath(XmlNode node,string path,string newPath)
        {
            XmlNode pyFullPath= node.SelectSingleNode("pyFullPath");
            if (pyFullPath!=null && string.Equals(pyFullPath.InnerText,path))
            {
                pyFullPath.InnerText = newPath;
            }
            XmlNode browseChosen= node.SelectSingleNode("browseChosen");
            if (browseChosen != null && string.Equals(browseChosen.InnerText, path))
            {
                browseChosen.InnerText = newPath;
            }
        }
        private string dataPath;
        private string finallyName;
        private bool CopyDatas(XmlNode xmlNode, String path, List<string> dataSourceNames, bool notPython = true)
        {
            bool copySuccess = true;
            string pathName = Path.GetFileName(path);
            if (!File.Exists(path))
            {
                MessageBox.Show(path + "文件不存在，无法完成模型导出。");
                return !copySuccess;
            }
            if (!dataSourceNames.Contains(pathName))
            {
                File.Copy(path, Path.Combine(this.dataPath, pathName));
                dataSourceNames.Add(pathName);
                finallyName = pathName;
            }
            else
            {
                string reName = RenameFile(pathName, dataSourceNames);
                File.Copy(path, Path.Combine(this.dataPath, reName));
                dataSourceNames.Add(reName);
                if (notPython)
                    xmlNode.SelectSingleNode("path").InnerText = Path.Combine(Path.GetDirectoryName(path),reName);
                finallyName = reName;
            }
            return copySuccess;
        }
        private bool HasSamePathNode(string path, XmlNode node, Dictionary<string, string> allPaths)
        {
            bool isAIOperator = node.SelectSingleNode("subtype").InnerText.Contains("CustomOperator");
            bool hasNode = true;
            if (allPaths.ContainsKey(path))
            {
                // 相同数据源，直接使用已经命名好的数据源
                if (!isAIOperator)
                    node.SelectSingleNode("path").InnerText = allPaths[path];
                else
                    node.SelectSingleNode("option").SelectSingleNode("path").InnerText = allPaths[path];
                return hasNode;
            }
            return !hasNode;
        }
        private string RenameFile(string pathName,List<string> dataSourceNames)
        {

            if (dataSourceNames.Contains(pathName))
            {
                pathName = "副本-" + pathName;
                pathName = RenameFile(pathName, dataSourceNames);
            }
           
            return pathName;
        }
        private void ExportModelMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ModelDocument model = Global.GetModelDocumentDao().FindModelDocument(this.ModelTitle);
            //模型没打开能够导出，否则文档非dirty才能导出
            if (model == null)
                this.ExportModel.Enabled = true;
            else
                this.ExportModel.Enabled = !model.Dirty;
        }
    }


}
