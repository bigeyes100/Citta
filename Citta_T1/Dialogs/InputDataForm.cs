﻿using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Citta_T1.Utils;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using static Citta_T1.Utils.DSUtil;

namespace Citta_T1.Dialogs
{
    // 
    public delegate void DelegateInputData(string name, string filePath, char separator, DSUtil.ExtType extType, DSUtil.Encoding encoding);
    public partial class InputDataForm : Form
    {
        private static LogUtil log = LogUtil.GetInstance("FormInputData"); // 获取日志模块

        private DSUtil.Encoding encoding = DSUtil.Encoding.GBK;
        private ExtType extType = ExtType.Unknow;
        private string fullFilePath;
        private int maxNumOfRow = 100;
        private Font bold_font = new Font("微软雅黑", 12F, (FontStyle.Bold | FontStyle.Underline), GraphicsUnit.Point, 134);
        private Font font = new Font("微软雅黑", 12F, FontStyle.Underline, GraphicsUnit.Point, 134);
        private char separator = '\t';
        
        private string invalidChars = "&\"' ";
        private string invalidCharsPattern;
        private string[] invalidStringArr;

        public InputDataForm()
        {
            InitializeComponent();
            this.dataGridView1.DoubleBuffered(true);
            this.InitInvalidCharPattern();
        }


        public void ReSetParams()
        {
            this.encoding = DSUtil.Encoding.GBK;
            this.gbkLable.Font = bold_font;
            this.utf8Lable.Font = font;

            this.radioButton1.Checked = true;
            this.separator = '\t';

            this.extType = DSUtil.ExtType.Text;

            this.Clean();
        }
        private void PreviewButton_Click(object sender, EventArgs e)
        {
            /*
             * 数据预览
             */
            string fileName = "";
            string ext;
            OpenFileDialog fd = new OpenFileDialog();           
            fd.Filter = "files|*.txt;*.bcp;*.xls;*.xlsx";
            if (this.gbkLable.Font.Bold)
                this.encoding = DSUtil.Encoding.GBK;
            else
                this.encoding = DSUtil.Encoding.UTF8;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                fullFilePath = fd.FileName;     
                fileName = Path.GetFileNameWithoutExtension(fullFilePath);
                ext = Path.GetExtension(fullFilePath);
                if (ext == ".xls" || ext == ".xlsx")
                {
                    this.extType = ExtType.Excel;
                    PreViewExcelFile();
                }
                else
                {
                    this.extType = ExtType.Text;
                    PreViewBcpFile();
                }          
            }
            //if (this.textBox1.Text == "请输入数据名称" || this.textBox1.Text == "")
            this.textBox1.Text = fileName;

        }
        

        // 添加按钮
        public event DelegateInputData InputDataEvent;
        private void AddButton_Click(object sender, EventArgs e)
        {
            string name = this.textBox1.Text;
            if (this.textBox1.Text == "请输入数据名称")
            {
                MessageBox.Show("请输入数据名称！");
            }
            else if (String.IsNullOrEmpty(this.fullFilePath))
            {
                MessageBox.Show("请选择数据路径！");
            }
            else if (Global.GetDataSourceControl().DataSourceDictI2B.ContainsKey(this.fullFilePath))
            {
                String dsName = Global.GetDataSourceControl().DataSourceDictI2B[this.fullFilePath].DataSourceName;
                MessageBox.Show("该文件已导入，数据源名为：" + dsName);
            }
            else if (IsContainsInvalidChars(this.fullFilePath))
            {
                MessageBox.Show(String.Format("数据源路径中不能有空格、&、\"等特殊字符,当前选择的路径为: {0}", this.fullFilePath));
            }
            // 非法字符不得成为文件名
            //else if (IsContainsInvalidChars(this.textBox1.Text))
            //{
            //    MessageBox.Show(String.Format("数据源命名不能有、&、\"等特殊字符, 当前数据源名为: {0}", this.textBox1.Text));
            //}
            else
            {
                if (this.fullFilePath.EndsWith(".xls") || this.fullFilePath.EndsWith(".xlsx"))
                    this.extType = DSUtil.ExtType.Excel;
                else
                    this.extType = DSUtil.ExtType.Text;

                BCPBuffer.GetInstance().TryLoadFile(this.fullFilePath, this.extType, this.encoding);
                InputDataEvent(name, this.fullFilePath, this.separator, this.extType, this.encoding);
                DvgClean();
                Close();
            }
            this.extType = DSUtil.ExtType.Unknow;
            this.encoding = DSUtil.Encoding.UTF8;
        }
        private void InitInvalidCharPattern()
        {
            this.invalidStringArr = new string[this.invalidChars.Length];
            for (int i = 0; i < this.invalidChars.Length; i++)
            {
                invalidStringArr[i] = this.invalidChars[i].ToString();
            }
            this.invalidCharsPattern = string.Join("|", invalidStringArr);
        }
        private bool IsContainsInvalidChars(string text)
        {
            return Regex.Matches(text, this.invalidCharsPattern).Count > 0;
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            // 关闭按钮
            DvgClean();
            this.ReSetParams();
            Close();
        }

        private void UTF8Lable_Click(object sender, EventArgs e)
        {
            if (this.extType == ExtType.Text)
            {
                this.gbkLable.Font = font;
                this.utf8Lable.Font = bold_font;
                this.encoding = DSUtil.Encoding.UTF8;
                PreViewBcpFile();
            }
        }
        private void Clean()
        {
            this.fullFilePath = null;
            this.textBox1.Text = null;
            this.textBoxEx1.Text = null;
            this.DvgClean();
        }

        private void PreViewBcpFile()
        {
            /*
             * @param this.isUTF8
             * @param this.fileName
             * 预览文件
             * 1. 编码格式
             * 2. 分隔符
             * 3. 初始化表头
             * 4. 清理表格数据
             * 5. 写入数据
             */
            if (this.fullFilePath == null)
                return;
            System.IO.StreamReader sr;
            if (this.encoding == DSUtil.Encoding.UTF8)
            {
                sr = File.OpenText(fullFilePath);
            }
            else
            {
                FileStream fs = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs, System.Text.Encoding.Default);
            }
            String header = sr.ReadLine();
            if (header == null)
            {
                MessageBox.Show("导入文件错误，文件\"" + this.fullFilePath + "\" 为空");
                this.Clean();
                return;
            }
            String[] headers = header.Split(this.separator);
            int numOfCol = header.Split(this.separator).Length;
            DataGridViewTextBoxColumn[] ColumnList = new DataGridViewTextBoxColumn[numOfCol];
            this.dataGridView1.DataSource = null;
            DvgClean(false);
            try
            {
                DataTable table = new DataTable();
                DataRow row;
                DataView view;
                DataColumn[] cols = new DataColumn[numOfCol];
                for (int i = 0; i < numOfCol; i++)
                {
                    cols[i] = new DataColumn();
                    cols[i].ColumnName = headers[i];
                }

                table.Columns.AddRange(cols);

                for (int rowIndex = 0; rowIndex < maxNumOfRow && !sr.EndOfStream; rowIndex++)
                {
                    String line = sr.ReadLine();
                    if (line == null)
                        continue;
                    row = table.NewRow();
                    String[] eles = line.Split(this.separator);
                    for (int colIndex = 0; colIndex < Math.Min(numOfCol, eles.Length); colIndex++)
                    {
                        row[colIndex] = eles[colIndex];
                    }
                    table.Rows.Add(row);
                }

                view = new DataView(table);
                this.dataGridView1.DataSource = view;
            }
            catch (Exception ex)
            {
                Console.WriteLine("FromInputData.OverViewFile occurs error! " + ex);
            }
        }

        private void PreViewExcelFile(string sheetName = null, bool isFirstRowColumn = true)
        {
            ISheet sheet = null;
            XSSFWorkbook workbook2007;
            HSSFWorkbook workbook2003;
            FileStream fs;
            DataTable data = new DataTable();
            int startRow = 0;
            //
            try
            {
                fs = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read);
                if (fullFilePath.IndexOf(".xlsx") > 0) // 2007版本
                {
                    workbook2007 = new XSSFWorkbook(fs);
                    if (sheetName != null)
                    {
                        sheet = workbook2007.GetSheet(sheetName);
                    }
                    else
                    {
                        sheet = workbook2007.GetSheetAt(0);
                    }
                }
                else
                {
                    workbook2003 = new HSSFWorkbook(fs);   // 2003版本
                    if (sheetName != null)
                    {
                        sheet = workbook2003.GetSheet(sheetName);
                    }
                    else
                    {
                        sheet = workbook2003.GetSheetAt(0);
                    }
                }

                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数
                    DataGridViewTextBoxColumn[] ColumnList = new DataGridViewTextBoxColumn[cellCount];
                    DvgClean(false);
                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ColumnList[i] = new DataGridViewTextBoxColumn();
                            ColumnList[i].HeaderText = firstRow.GetCell(i).StringCellValue;
                            ColumnList[i].Name = "Col " + i.ToString();
                        }
                        this.dataGridView1.Columns.AddRange(ColumnList);
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = 0; i <= Math.Min(maxNumOfRow, rowCount); ++i)
                    {
                        IRow row = sheet.GetRow(i + startRow);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataGridViewRow dr = new DataGridViewRow();
                        this.dataGridView1.Rows.Add(dr);
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                            {
                                string content = row.GetCell(j).ToString();
                                this.dataGridView1.Rows[i].Cells[j].Value = content;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        public void DvgClean(bool isCleanDataName = true)
        {
            if (isCleanDataName) { this.textBox1.Text = null; }
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Columns.Clear();
        }

        private void GBKLable_Click(object sender, EventArgs e)
        {
            if (this.extType == ExtType.Text)
            {
                this.gbkLable.Font = bold_font;
                this.utf8Lable.Font = font;
                this.encoding = DSUtil.Encoding.GBK;
                PreViewBcpFile();
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.extType == ExtType.Text)
            {
                this.separator = '\t';
                PreViewBcpFile();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.extType == ExtType.Text)
            {
                this.separator = ',';
                PreViewBcpFile();
            }

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.extType == ExtType.Text && this.textBoxEx1.Text != null && this.textBoxEx1.Text != "")
            {
                try
                {
                    this.separator = System.Text.RegularExpressions.Regex.Unescape(this.textBoxEx1.Text).ToCharArray()[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("指定的分隔符有误！目前分隔符为：" + this.textBoxEx1.Text);
                }
                PreViewBcpFile();
            }
        }
        private void radioButton1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.extType != ExtType.Text)
                return;
        }

        private void radioButton2_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.extType != ExtType.Text)
                return;
        }
        private void radioButton3_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.extType != ExtType.Text)
                return;
            else
            {
                this.radioButton3.Checked = true;
                this.textBoxEx1.Focus();
                if (this.textBoxEx1.Text == null || this.textBoxEx1.Text == "")
                    return;
                try
                {
                    this.separator = System.Text.RegularExpressions.Regex.Unescape(this.textBoxEx1.Text).ToCharArray()[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("指定的分隔符有误！目前分隔符为：" + this.textBoxEx1.Text);
                }
            }

        }
        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public DataTable ExcelToDataTable(string fileName, string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            XSSFWorkbook workbook2007;
            HSSFWorkbook workbook2003;
            FileStream fs;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                {
                    workbook2007 = new XSSFWorkbook(fs);
                    if (sheetName != null)
                    {
                        sheet = workbook2007.GetSheet(sheetName);
                    }
                    else
                    {
                        sheet = workbook2007.GetSheetAt(0);
                    }
                }
                else
                {
                    workbook2003 = new HSSFWorkbook(fs);   // 2003版本
                    if (sheetName != null)
                    {
                        sheet = workbook2003.GetSheet(sheetName);
                    }
                    else
                    {
                        sheet = workbook2003.GetSheetAt(0);
                    }
                }

                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数
                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            DataColumn column = new DataColumn(firstRow.GetCell(i).StringCellValue);
                            data.Columns.Add(column);
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        private void textBoxEx1_TextChanged(object sender, EventArgs e)
        {
            if (this.extType == ExtType.Text)
            {
                this.radioButton3.Checked = true;
                if (this.textBoxEx1.Text == null || this.textBoxEx1.Text == "")
                    this.separator = '\0';
                else
                    try
                    {
                        this.separator = System.Text.RegularExpressions.Regex.Unescape(this.textBoxEx1.Text).ToCharArray()[0];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("指定的分隔符有误！目前分隔符为：" + this.textBoxEx1.Text);
                    }
                PreViewBcpFile();
            }
        }
    }
}
