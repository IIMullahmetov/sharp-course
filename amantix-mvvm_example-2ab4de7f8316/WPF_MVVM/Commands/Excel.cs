using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using WPF_MVVM.Models;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace WPF_MVVM.Commands
{
    public class Excel
    {
        Application ObjExcel = null;
        Workbook ObjWorkBook = null;
        Worksheet ObjWorkSheet = null;
        List list;
        Project project;

        public Excel(List list, Project project, bool isRead)
        {
            this.list = list;
            this.project = project;

            ObjExcel = new Application();

            try
            {
                if (isRead)
                {
                    Read();
                }
                else
                {
                    Settings();
                    Write();
                    SaveAs();
                }
            }
            finally
            {
                Marshal.ReleaseComObject(ObjWorkBook);
                Marshal.ReleaseComObject(ObjExcel);
                GC.Collect();
                /*
                ObjWorkBook.Close(0);
                ObjExcel.Quit();
                */
                /*
                ObjExcel = null;
                */
            }
        }

        public void Read()
        {
            ObjWorkBook = ObjExcel.Workbooks.Open(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + project.Name + ".xlsx",
                 Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                 Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                 Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                 Type.Missing, Type.Missing);

            for (int i = 1; i < ObjExcel.SheetsInNewWorkbook + 1; i++)
            {
                ObjWorkSheet = (Worksheet) ObjWorkBook.Sheets[i];
//TODO: TRUBBLE!
                for (int j = 1; j < project.Goals.Count * 4; j++) 
                {
                    Range rangeName = ObjWorkSheet.get_Range("A" + (j + 1).ToString()); //, "A" + (project.Goals.Count + 1).ToString());
                    Range rangeDescription = ObjWorkSheet.get_Range("B" + (j + 1).ToString());
                    Range rangeWorkTime = ObjWorkSheet.get_Range("C" + (j + 1).ToString());
                    Range rangeStartTime = ObjWorkSheet.get_Range("D" + (j + 1).ToString());

                    try
                    {
                        switch (i)
                        {
                            case 1:
                               // project.Goals.Add(new Note(rangeName.Text, rangeDescription.Text, TimeSpan.Parse(rangeWorkTime.Text), DateTime.Parse(rangeStartTime.Text)));
                                break;
                            case 2:
                               // project.Active.Add(new Note(rangeName.Text, rangeDescription.Text, rangeWorkTime.Text, rangeStartTime.Text));
                                break;
                            case 3:
                              //  project.Done.Add(new Note(rangeName.Text, rangeDescription.Text, rangeWorkTime.Text, rangeStartTime.Text));
                                break;
                            case 4:
                               // project.Canceled.Add(new Note(rangeName.Text, rangeDescription.Text, rangeWorkTime.Text, rangeStartTime.Text));
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        public void Settings()
        {
            ObjExcel.SheetsInNewWorkbook = 4;
            ObjExcel.Visible = true;
            ObjExcel.UserControl = true;
        }

        public void Write()
        {
            CreateBook();
            for (int i = 1; i < ObjExcel.SheetsInNewWorkbook + 1; i++)
            {
                CreateSheet(i);
                CreateFill();
                Filling(i);
            }
            Settings();
        }

        private void CreateBook()
        {
            ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);      
        }

        private void CreateSheet(int i)
        {
            ObjWorkSheet = (Worksheet) ObjWorkBook.Sheets[i];
            switch (i)
            {
                case 1:
                    ObjWorkSheet.Name = "Goals";
                    break;
                case 2:
                    ObjWorkSheet.Name = "Active";
                    break;
                case 3:
                    ObjWorkSheet.Name = "Done";
                    break;
                case 4:
                    ObjWorkSheet.Name = "Canceled";
                    break;
            }
        }

        private void CreateFill()
        {
            ObjWorkSheet.Cells[1, 1] = "Name";
            ObjWorkSheet.Cells[1, 1].ColumnWidth = 25;
            ObjWorkSheet.Cells[1, 1].Font.Bold = true;
            ObjWorkSheet.Cells[1, 2] = "Description";
            ObjWorkSheet.Cells[1, 2].ColumnWidth = 50;
            ObjWorkSheet.Cells[1, 2].Font.Bold = true;
            ObjWorkSheet.Cells[1, 3] = "WorkTime";
            ObjWorkSheet.Cells[1, 3].ColumnWidth = 15;
            ObjWorkSheet.Cells[1, 3].Font.Bold = true;
            ObjWorkSheet.Cells[1, 4] = "StartTime";
            ObjWorkSheet.Cells[1, 4].ColumnWidth = 20;
            ObjWorkSheet.Cells[1, 4].Font.Bold = true;
        }

        private void Filling(int i)
        {
            for (int n = 0; n < project.Goals.Count; n++)
            {
                try
                {
                    switch (i)
                    {
                        case 1:
                            ObjWorkSheet.Cells[n + 2, 1] = project.Goals.ElementAt(n).Name;
                            ObjWorkSheet.Cells[n + 2, 2] = project.Goals.ElementAt(n).Description;
                            ObjWorkSheet.Cells[n + 2, 3] = project.Goals.ElementAt(n).WorkTime.ToString();
                            ObjWorkSheet.Cells[n + 2, 4] = project.Goals.ElementAt(n).StartTime.ToString();
                            break;
                        case 2:
                            ObjWorkSheet.Cells[n + 2, 1] = project.Active.ElementAt(n).Name;
                            ObjWorkSheet.Cells[n + 2, 2] = project.Active.ElementAt(n).Description;
                            ObjWorkSheet.Cells[n + 2, 3] = project.Active.ElementAt(n).WorkTime.ToString();
                            ObjWorkSheet.Cells[n + 2, 4] = project.Active.ElementAt(n).StartTime.ToString();
                            break;
                        case 3:
                            ObjWorkSheet.Cells[n + 2, 1] = project.Done.ElementAt(n).Name;
                            ObjWorkSheet.Cells[n + 2, 2] = project.Done.ElementAt(n).Description;
                            ObjWorkSheet.Cells[n + 2, 3] = project.Done.ElementAt(n).WorkTime.ToString();
                            ObjWorkSheet.Cells[n + 2, 4] = project.Done.ElementAt(n).StartTime.ToString();
                            break;
                        case 4:
                            ObjWorkSheet.Cells[n + 2, 1] = project.Canceled.ElementAt(n).Name;
                            ObjWorkSheet.Cells[n + 2, 2] = project.Canceled.ElementAt(n).Description;
                            ObjWorkSheet.Cells[n + 2, 3] = project.Canceled.ElementAt(n).WorkTime.ToString();
                            ObjWorkSheet.Cells[n + 2, 4] = project.Canceled.ElementAt(n).StartTime.ToString();
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void SaveAs()
        {
            /*
            for (int i = 0; i < ObjWorkBook.Length; i++)
            {
                ObjWorkBook[i].SaveAs(path + ObjWorkBook[i].Name + ".xlsx");
            }
            */
            try
            {
                //ObjExcel.DisplayAlerts = false; //будет записывать документ поверх существующего без спроса
                ObjWorkBook.SaveAs(project.Name + ".xlsx");
            }
            catch (Exception e)
            {
               // File.Delete(@"C:\\Users\\danil\\Documents\\" + project.Name + ".xlsx");
                Console.WriteLine(e.Message);
            }
            //File.Delete(@"C:\\Users\\danil\\Documents\\" + project.Name + ".xlsx");
        }
    }
}
