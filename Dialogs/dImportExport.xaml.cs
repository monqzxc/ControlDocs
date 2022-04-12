using DocsControl.Model;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static DocsControl.Model.Modules;
using Excel = Microsoft.Office.Interop.Excel;

namespace DocsControl.Dialogs
{
    /// <summary>
    /// Interaction logic for dImportExport.xaml
    /// </summary>
    public partial class dImportExport : Window
    {
        public dImportExport(string user)
        {
            InitializeComponent();
            this.role = int.Parse(user.Split('|')[0]);
            this.nickname = user.Split('|')[1];
            this.user = user;

            this.DataContext = this;

            if (role > 1)
            {
                sp1.IsEnabled = false;
            }
        }

        private string user;
        private string nickname;
        private int role;
        dbDocs db = new dbDocs();
        string currentFileName;
        Excel.Application xlApp;//open app
        Excel.Workbook xlWorkBook;//open workbook
        Excel.Worksheet xlWorkSheet;//open worksheet
        object misValue = System.Reflection.Missing.Value;
        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton.Equals(MouseButton.Left))
                DragMove();
        }

        private void ExportCsv()
        {
            var sfd = new SaveFileDialog()
            {
                Title = "EXPORT CSV",
                Filter = "Excel File | *.xls"
            };

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var incoming = db.DocDatas.Where(x => x.Tag.Equals("I")).ToList();

                var dt = new DataTable();
                var dgv = new System.Windows.Controls.DataGrid();
                
                var IncomingList = new ObservableCollection<IncomingClass>();
                foreach (var item in incoming)
                {
                    var focalIDList = db.DocDatas.Where(x => x.Id.Equals(item.Id)).Select(x => x.FocalID).FirstOrDefault().Split(',').ToList(); //get focals id then put them into list
                    var dbFocalList = new List<string>();
                    foreach (var fid in focalIDList) //getting all the id in the previous list then get their nickname column before adding into the list
                    {
                        int id = int.Parse(fid);
                        dbFocalList.Add(db.Focals.Where(x => x.Id.Equals(id)).Select(x => x.NickName).FirstOrDefault());
                    }
                    IncomingList.Add(new IncomingClass()
                    {
                        DateAdded = item.DateAdd,
                        ORDNumber = item.DocControlNumber.Split('|')[0],
                        RODNumber = item.DocControlNumber.Split('|')[1],
                        DocumentType = item.DoctTypes,
                        DocNumber = item.DocControlNumber.Split('|')[2],
                        DateOfDocument = item.ForSigned,
                        OriginOffice = item.Addressee.Office,
                        OriginSignatory = item.Addressee.FullName,
                        DocSubject = item.DocSubject,
                        FocalID = string.Join(",", dbFocalList),
                        Remarks1 = item.Remarks.Split('|')[0],
                        DateReceivedByFocal = item.Signed,
                        Remarks2 = item.Remarks.Split('|')[1],
                        ActionDate = item.ForRelease,
                        Status = item.CurrentStatus,
                        FilePath = item.DocPaths.Select(x => x.Path).FirstOrDefault(),
                    });
                }
                
                dgv.ItemsSource = IncomingList;

                foreach (DataGridCellInfo item in dgv.SelectedCells)
                {
                    DataRowView dr = (DataRowView)item.Item;

                    showInfo(dr[1].ToString());
                }
                xlApp = new Microsoft.Office.Interop.Excel.Application();

                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Rows.Font.Name = "Century Gothic";

                //Header
                ExcelRange("Technical Education and Skills Development Authority", 2, 3, 14, true, 20, true, Excel.XlHAlign.xlHAlignCenter, Excel.XlVAlign.xlVAlignCenter, Excel.XlRgbColor.rgbWhite);
                ExcelRange("REGION IV-A (CALABARZON)", 3, 3, 14, true, 20, true, Excel.XlHAlign.xlHAlignCenter, Excel.XlVAlign.xlVAlignCenter, Excel.XlRgbColor.rgbWhite);
                ExcelRange("PLEASE TAKE INTO CONSIDERATION", 5, 3, 14, true, 15, true, Excel.XlHAlign.xlHAlignLeft, Excel.XlVAlign.xlVAlignTop, Excel.XlRgbColor.rgbWhite);
                ExcelRange("KINDLY UPDATE THE STATUS OF 2022 INCOMING DOCUMENTS WHICH ARE ASSIGNED TO YOU. (COLUMNS N-P)", 6, 3, 3, false, 13, false, Excel.XlHAlign.xlHAlignLeft, Excel.XlVAlign.xlVAlignTop, Excel.XlRgbColor.rgbWhite);
                ExcelRange("YOU CAN CREATE FILTER OR USE CTRL + F THEN SEARCH YOUR NAME TO EASILY PINPOINT YOUR TAKS.", 7, 3, 3, false, 13, false, Excel.XlHAlign.xlHAlignLeft, Excel.XlVAlign.xlVAlignTop, Excel.XlRgbColor.rgbWhite);

                ExcelRange("2022 INCOMING DOCUMENTS", 10, 1, 10, true, 13, true, Excel.XlHAlign.xlHAlignCenter, Excel.XlVAlign.xlVAlignCenter, Excel.XlRgbColor.rgbDeepSkyBlue);
                ExcelRange("MONITORING", 10, 11, 17, true, 13, true, Excel.XlHAlign.xlHAlignCenter, Excel.XlVAlign.xlVAlignCenter, Excel.XlRgbColor.rgbOrangeRed);

                var header = new string[]
                {
                    "DATE & TIME RECEIVED BY ROIVA-ROD",
                    "TIME RECEIVED BY ROIVA-ROD",
                    "ORD NUMBER",
                    "ROD DOCUMENT NUMBER",
                    "DOCUMENT TYPE",
                    "DOCUMENT NUMBER",
                    "DATE OF DOCUMENT",
                    "ORIGIN",
                    "SIGNATORY",
                    "SUBJECT",
                    "ASSIGNED FOCAL PERSON",
                    "INSTRUCTIONS/ REMARKS FROM ROD CHIEF",
                    "DATE RECEIVED BY FOCAL PERSON",
                    "ACTION/S TAKEN BY FOCAL PERSON",
                    "DATE OF ACTION",
                    "STATUS",
                    "LINK"
                };

                //columns
                int xlColIndex = 1;
                foreach (var item in header)
                {
                    xlWorkSheet.Cells[12, xlColIndex] = item;
                    Excel.Range range = xlWorkSheet.Range[xlWorkSheet.Cells[12, xlColIndex], xlWorkSheet.Cells[12, xlColIndex]];
                    range.ColumnWidth = 20;
                    range.RowHeight = 50;
                    range.WrapText = true;
                    range.Font.Bold = true;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    range.Interior.Color = xlColIndex < 11 ? Excel.XlRgbColor.rgbDeepSkyBlue : Excel.XlRgbColor.rgbOrangeRed;
                    xlColIndex++;
                }

                Console.WriteLine(IncomingList.Count);

                //foreach (var item in IncomingList)
                //{
                    for (int i = 0; i < IncomingList.Count; i++)
                    {
                        xlWorkSheet.Cells[13 + i, 1] = IncomingList.Select(x => x.DateAdded.ToString("MMMM dd, yyyy")).Skip(i).FirstOrDefault();
                        xlWorkSheet.Cells[13 + i, 2] = IncomingList.Select(x => x.DateAdded.ToString("hh:mm tt")).Skip(i).FirstOrDefault();
                        xlWorkSheet.Cells[13 + i, 3] = IncomingList.Select(x => x.ORDNumber).Skip(i).FirstOrDefault();
                        xlWorkSheet.Cells[13 + i, 4] = IncomingList.Select(x => x.RODNumber).Skip(i).FirstOrDefault();
                        xlWorkSheet.Cells[13 + i, 5] = IncomingList.Select(x => x.DocumentType).Skip(i).FirstOrDefault();
                        xlWorkSheet.Cells[13 + i, 6] = IncomingList.Select(x => x.DocNumber).Skip(i).FirstOrDefault();
                        xlWorkSheet.Cells[13 + i, 7] = IncomingList.Select(x => x.DateOfDocument).Skip(i).FirstOrDefault();
                        xlWorkSheet.Cells[13 + i, 8] = IncomingList.Select(x => x.OriginOffice).Skip(i).FirstOrDefault();
                        xlWorkSheet.Cells[13 + i, 9] = IncomingList.Select(x => x.OriginSignatory).Skip(i).FirstOrDefault();
                        xlWorkSheet.Cells[13 + i, 10] = IncomingList.Select(x => x.DocSubject).Skip(i).FirstOrDefault();
                        xlWorkSheet.Cells[13 + i, 11] = IncomingList.Select(x => x.FocalID).Skip(i).FirstOrDefault();
                        xlWorkSheet.Cells[13 + i, 12] = IncomingList.Select(x => x.Remarks1).Skip(i).FirstOrDefault();
                        xlWorkSheet.Cells[13 + i, 13] = IncomingList.Select(x => x.DateReceivedByFocal).Skip(i).FirstOrDefault();
                        xlWorkSheet.Cells[13 + i, 14] = IncomingList.Select(x => x.Remarks2).Skip(i).FirstOrDefault();
                        xlWorkSheet.Cells[13 + i, 15] = IncomingList.Select(x => x.ActionDate).Skip(i).FirstOrDefault();
                        xlWorkSheet.Cells[13 + i, 16] = IncomingList.Select(x => x.Status).Skip(i).FirstOrDefault();
                        xlWorkSheet.Cells[13 + i, 17] = IncomingList.Select(x => x.FilePath).Skip(i).FirstOrDefault();
                    }
                //}

                //for (int i = 0; i < dgv.Items.Count; i++)
                //{
                //    DataRowView row = dgv.Items[i] as DataRowView;
                //    Console.WriteLine(row.Row.ItemArray[i]);
                //}
                //var rows = GetDataGridRows(dgv);
                //foreach (DataGridRow row in rows)
                //{
                //    DataRowView rowView = (DataRowView)row.Item;
                //    foreach (DataGridColumn column in dgv.Columns)
                //    {
                //        if (column.GetCellContent(row) is TextBlock)
                //        {
                //            TextBlock cellContent = column.GetCellContent(row) as TextBlock;
                //            showInfo(cellContent.Text);
                //        }
                //    }
                //}

                try
                {
                    xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();

                    showInfo("File created !");
                }
                catch (Exception ex)
                {
                    showError("An error occur. Please close the existing file first!. " + ex.Message);
                }
            }
        }

        private Excel.Range ExcelRange(string title, int cellFirstIndex1, int cellFirstIndex2, int cellLastIndex, bool merge, int fontSize, bool fontWeight, Excel.XlHAlign horizontalAlignment, Excel.XlVAlign verticalAlignment, Excel.XlRgbColor backColor)
        {
            xlWorkSheet.Cells[cellFirstIndex1, cellLastIndex] = title;
            var er = xlWorkSheet.Range[xlWorkSheet.Cells[cellFirstIndex1, cellFirstIndex2], xlWorkSheet.Cells[cellFirstIndex1, cellLastIndex]];
            er.Merge(merge);
            er.Font.Size = fontSize;
            er.Font.Bold = fontWeight;
            er.HorizontalAlignment = horizontalAlignment;
            er.VerticalAlignment = verticalAlignment;
            er.Interior.Color = backColor;
            return er;
        }


        public IEnumerable<DataGridRow> GetDataGridRows(System.Windows.Controls.DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }

        private bool ImportCsv()
        {
            //open dialog tool to open a file then store the filename into label
            var openFileDialog = new System.Windows.Forms.OpenFileDialog();

            openFileDialog.InitialDirectory = @"C:\Desktop";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                currentFileName = openFileDialog.FileName;
                return true;
            }
            else
            {
                return false;
            }
        }
        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            ExportCsv();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            ImportCsv();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (showWarning("DO YOU WANT TO CANCEL?").Equals(true))
            {
                //store the path in the list to be deleted/removed
                this.Close();
            }
            else
                return;
        }
    }

}
