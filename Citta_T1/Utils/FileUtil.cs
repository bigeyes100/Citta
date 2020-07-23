﻿using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace Citta_T1.Utils
{
    class FileUtil
    {
        public static LogUtil log = LogUtil.GetInstance("FileUtil");
        public static void AddPathPower(string pathName, string power)
        {
            string userName = System.Environment.UserName;
            DirectoryInfo dirInfo = new DirectoryInfo(pathName);

            if ((dirInfo.Attributes & FileAttributes.ReadOnly) != 0)
            {
                dirInfo.Attributes = FileAttributes.Normal;
            }

            //取得访问控制列表   
            DirectorySecurity dirsecurity = dirInfo.GetAccessControl();

            switch (power)
            {
                case "FullControl":
                    dirsecurity.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.FullControl, InheritanceFlags.ContainerInherit, PropagationFlags.InheritOnly, AccessControlType.Allow));
                    break;
                case "ReadOnly":
                    dirsecurity.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.Read, AccessControlType.Allow));
                    break;
                case "Write":
                    dirsecurity.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.Write, AccessControlType.Allow));
                    break;
                case "Modify":
                    dirsecurity.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.Modify, AccessControlType.Allow));
                    break;
            }
            dirInfo.SetAccessControl(dirsecurity);
        }

        // 实践中发现复制粘贴板有时会出异常
        // 非核心功能,捕捉异常忽略之
        public static bool TryClipboardSetText(string text)
        {
            bool ret = true;
            try { Clipboard.SetText(text); }
            catch { ret = false; }
            return ret;
        }
        public static void ExploreDirectory(string fullFilePath)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "explorer.exe",  //资源管理器
                    Arguments = "/e,/select," + fullFilePath
                };
                System.Diagnostics.Process.Start(processStartInfo);
            }
            catch (Exception)
            {
                //某些机器直接打开文档目录会报“拒绝访问”错误，此时换一种打开方式
                FileUtil.AnotherOpenFilePathMethod(fullFilePath);
            }
        }


        private static void AnotherOpenFilePathMethod(string fullFilePath)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "explorer.exe",  //资源管理器
                    Arguments = System.IO.Path.GetDirectoryName(fullFilePath)
                };
                System.Diagnostics.Process.Start(processStartInfo);
            }
            catch { }; // 非核心功能, Double异常就不用管了
        }

        public static void DeleteDirectory(string directoryPath)
        {
            try
            {
                System.IO.Directory.Delete(directoryPath, true);
            }
            catch
            {
                // 如果无法回滚的话,这个地方只能直接忽略了
            }
        }

        public static bool CreateDirectory(string dicectoryPath)
        {
            bool ret = true;
            try
            {
                Directory.CreateDirectory(dicectoryPath);
            }
            catch { ret = false; }
            return ret;
        }

        public static bool FileMove(string oldFFP, string newFFP)
        {
            bool ret = true;
            try
            {
                File.Move(oldFFP, newFFP);
            }
            catch { ret = false; }
            return ret;
        }

        public static bool DirecotryMove(string oldDirectory, string newDirectory)
        {
            bool ret = true;
            try
            {
                Directory.Move(oldDirectory, newDirectory);
            }
            catch { ret = false; }
            return ret;
        }

        public static string TryGetPathRoot(string path)
        {
            string root;
            try
            {
                root = Path.GetPathRoot(path);
            }
            catch (ArgumentException)
            {
                root = String.Empty;
            }
            return root;
        }

        public static string[] TryListDirectory(string path)
        {
            try
            {
                return System.IO.Directory.GetDirectories(path);
            }
            catch
            {
                return new string[0];
            }
        }

        public static List<List<string>> ReadExcel(string fullFilePath, int maxRow, string sheetName = "")
        {
            FileStream fs = null;
            List<List<string>> rowContentList = new List<List<string>>();
            try
            {
                fs = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read);
                //03版(xls)用npoi,07版(xlsx)用epplus
                //npoi的索引从0开始，epplus的索引从1开始
                if (fullFilePath.EndsWith(".xlsx"))
                {
                    using (ExcelPackage package = new ExcelPackage(fs))
                    {
                        ExcelWorksheet worksheet = string.IsNullOrEmpty(sheetName) ? package.Workbook.Worksheets[0] : package.Workbook.Worksheets[sheetName];
                        if (worksheet == null)
                        {
                            fs.Close();
                            return rowContentList;
                        }
                        int rowCount = worksheet.Dimension.End.Row;
                        int colCount = worksheet.Dimension.End.Column;
                        //int realColCount = colCount;
                        //因为是最大列数，可能出现表头列数小于最大列数的情况
                        //for (int i = colCount; i > 0; i--)
                        //{
                        //    if (worksheet.Cells[1, i].Value == null || worksheet.Cells[1, i].Value.ToString() == string.Empty)
                        //        realColCount--;
                        //    else
                        //        break;//从后往前，遇到不为空的代表剩下的表头均有值
                        //}

                        //遍历单元格赋值
                        for (int row = 1; row <= Math.Min(maxRow, rowCount); row++)
                        {
                            List<string> tmpRowValueList = new List<string>();
                            for (int col = 1; col <= colCount; col++)
                            {
                                ExcelRange cell = worksheet.Cells[row, col];
                                string unit = ExcelUtil.GetCellValue(cell).Replace('\n',' ');
                                tmpRowValueList.Add(unit);
                            }
                            rowContentList.Add(tmpRowValueList);
                        }
                    }
                }
                else
                {
                    IWorkbook workbook = new HSSFWorkbook(fs);
                    ISheet sheet = String.IsNullOrEmpty(sheetName) ? workbook.GetSheetAt(0) : workbook.GetSheet(sheetName);
                    if (sheet == null)
                    {
                        fs.Close();
                        return rowContentList;
                    }
                    IRow firstRow = sheet.GetRow(0);
                    int rowCount = sheet.LastRowNum + 1;
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    for (int i = 0; i < Math.Min(maxRow + 1, rowCount); i++)
                    {
                        List<string> tmpRowValueList = new List<string>();
                        for (int j = 0; j < cellCount; j++)
                        {
                            if (sheet.GetRow(i) == null || sheet.GetRow(i).GetCell(j) == null) //同理，没有数据的单元格都默认是null
                            {
                                tmpRowValueList.Add(string.Empty);
                            }
                            else
                            {
                                ICell cell = sheet.GetRow(i).GetCell(j);
                                string unit = ExcelUtil.GetCellValue(workbook, cell).Replace('\n',' ');
                                tmpRowValueList.Add(unit);
                            }
                        }
                        rowContentList.Add(tmpRowValueList);
                    }

                }
            }
            catch (System.IO.IOException)
            {
                string errorMsg = string.Format("文件{0}已被打开，请先关闭该文件", fullFilePath);
                MessageBox.Show(errorMsg);
                log.Error(errorMsg);
            }
            catch (Exception ex)
            {
                log.Error("预读Excel: " + fullFilePath + " 失败, error: " + ex.Message);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }

            return rowContentList;
        }
        public static List<List<string>> FormatDatas(List<List<string>> datas, int maxNumOfRow)
        {
            int maxNumOfCol = 0;
            List<string> blankRow = new List<string> { };
            List<string> headers = datas[0];
            if (datas.Count <=1)
                return new List<List<string>> { headers };
            for (int i = 0; i < datas.Count; i++)
                maxNumOfCol = Math.Max(datas[i].Count, maxNumOfCol);
            for (int i = 0; i < maxNumOfCol; i++)
                blankRow.Add("");
            for (int i = 0; i < Math.Max(maxNumOfRow, datas.Count); i++)
            {
                if (i >= datas.Count)
                    datas.Add(blankRow);
                else
                {
                    int numOfCurRow = datas[i].Count;
                    for (int j = 0; j < maxNumOfCol - numOfCurRow; j++)
                        datas[i].Add("");
                }
            }
            return datas;
        }

        public static bool ContainIllegalCharacters(string userName, string target)
        {
            string[] illegalCharacters = new string[] { "*", "\\", "/", "$", "[", "]", "+", "-", "&", "%", "#", "!", "~", "`", " ", "\\t", "\\n", "\\r", ":" };
            foreach (string character in illegalCharacters)
            {
                if (userName.Contains(character))
                {
                    MessageBox.Show(target + "包含非法字符，请输入新的" + target + "." + System.Environment.NewLine + "非法字符包含：*, \\, $, [, ], +, -, &, %, #, !, ~, `, \\t, \\n, \\r, :, 空格");
                    return true;
                }
            }
            return false;
        }
        public static bool NameTooLong(string userName, string target)
        {
            if (userName.Length > 128)
            {
                MessageBox.Show(target + "长度过长,请重新输入" + target);
                return true;
            }
            return false;
        }

        public static Tuple<List<string>, List<List<string>>> ReadBcpFile(string fullFilePath, OpUtil.Encoding encoding, char separator, int maxNumOfRow)
        {
            List<string> headers = new List<string> { };
            int maxColsNum = 0;
            List<List<string>> rows = new List<List<string>> {  };
            Tuple<List<string>, List<List<string>>> result;
            if (fullFilePath == null)
                return new Tuple<List<string>, List<List<string>>>(headers, rows);

            System.IO.StreamReader sr = null;
            FileStream fs = null;
            if (encoding == OpUtil.Encoding.UTF8)
                sr = File.OpenText(fullFilePath);
            else
            {
                fs = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs, System.Text.Encoding.Default);
            }
            try
            {
                headers = new List<string>(sr.ReadLine().Split(separator));
                for (int rowIndex = 1; rowIndex < maxNumOfRow && !sr.EndOfStream; rowIndex++)
                {
                    String line = sr.ReadLine();
                    if (line == null)
                        continue;
                    String[] eles = line.Split(separator);
                    if (eles.Length > maxColsNum)
                        maxColsNum = eles.Length;
                    rows.Add(new List<string>(eles));
                }
                for (int headersColNum = headers.Count; headersColNum < maxColsNum; headersColNum++)
                    headers.Add("");
            }
            catch (Exception e)
            {
                log.Error(string.Format("加载数据 '{0}' 失败, error: {1}", fullFilePath, e.ToString()));
            }
            finally
            {
                result = new Tuple<List<string>, List<List<string>>>(headers, rows);
                if (fs != null)
                    fs.Close();
                if (sr != null)
                    sr.Close();
            }
            return result;
        }
        public static void FillTable(DataGridView dgv, List<string> headers, List<List<string>> rows, int maxNumOfRow=100)
        {
            try
            {
                DataTable table = new DataTable();
                DataRow newRow;
                DataView view;
                int maxNumOfCol = headers.Count;
                DataColumn[] cols = new DataColumn[maxNumOfCol];
                Dictionary<string, int> induplicatedName = new Dictionary<string, int>() { };
                string headerText;
                char[] seperator = new char[] { '_' };

                // 可能有同名列，这里需要重命名一下
                for (int i = 0; i < maxNumOfCol; i++)
                {
                    cols[i] = new DataColumn();
                    headerText = headers[i];
                    if (!induplicatedName.ContainsKey(headerText))
                        induplicatedName.Add(headerText, 0);
                    else
                    {
                        induplicatedName[headerText] += 1;
                        induplicatedName[headerText] = induplicatedName[headerText];
                    }
                    headerText = induplicatedName[headerText] + "_" + headerText;
                    cols[i].ColumnName = headerText;
                }
                // 表头
                table.Columns.AddRange(cols);

                for (int rowIndex = 0; rowIndex < Math.Max(maxNumOfRow, rows.Count); rowIndex++)
                {
                    List<string> row = rows[rowIndex];
                    newRow = table.NewRow();
                    for (int colIndex = 0; colIndex < row.Count; colIndex++)
                        newRow[colIndex] = row[colIndex];
                    for (int colIndex = row.Count; colIndex < maxNumOfCol; colIndex++)
                        newRow[colIndex] = "";
                    table.Rows.Add(newRow);
                }

                view = new DataView(table);
                dgv.DataSource = view;
                FileUtil.ResetColumnsWidth(dgv);


                // 取消重命名
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    try
                    {
                        dgv.Columns[i].HeaderText = dgv.Columns[i].Name.Split(seperator, 2)[1];
                    }
                    catch
                    {
                        dgv.Columns[i].HeaderText = dgv.Columns[i].Name;
                        log.Error("读取文件时，去重命名过程出错");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("FromInputData.OverViewFile occurs error! " + ex);
            }
            finally
            {

            }
        }
        public static void CleanDgv(DataGridView dgv)
        {
            dgv.DataSource = null; // System.InvalidOperationException:“操作无效，原因是它导致对 SetCurrentCellAddressCore 函数的可重入调用。”
            dgv.Rows.Clear();
            dgv.Columns.Clear();
        }
        public static void ResetColumnsWidth(DataGridView dgv, int minWidth = 50)
        {
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].MinimumWidth = minWidth;
            }
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
    }
}
