using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Electricity_bill;
using Microsoft.Office.Interop.Excel;
using System.Globalization;

namespace Electricity_bill
{
    internal class LocalExcel
    {
        //IntToChar is for making cell headings in worksheet.Cells[i,A] function acc to headings in constants file
        static readonly string[] IntToCharHead = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        
        public static Excel.Application? excelApp;          //excelapp declaration
        public static Excel.Workbook? excelwb;              //workbook declaration
        public static Excel._Worksheet? workSheet;          //worksheet declaration
        public static void Initialize()
        {
            //----------excel initialization-------------initialization of various fields --------//
            excelApp = new Excel.Application
            {
                Visible = true
            };

            excelwb = excelApp.Workbooks.Add();
            workSheet = (Excel._Worksheet)excelApp.ActiveSheet;         //creating worksheet instance
        }

        public static void MakeHeadings()               // creating heading fields in our excel sheet acc to headings in constants file
        {
            for(int i=0;i<Constants.NoOfColumns;i++)            //adding headings in loop acc to the Constants.NoOfColumns
            {
                if (workSheet is not null)
                {
                    workSheet.Cells[1, IntToCharHead[i]] = Constants.Headings[i];   
                }
            }
        }

        //-------add row/ new fields in excel sheet--------------//
        public static void AddRow(int row, CalculationObj obj)
        {
            if (workSheet is not null)              //checking if worksheet is not null
            {
                workSheet.Cells[row, IntToCharHead[0]] = obj.id;
                workSheet.Cells[row, IntToCharHead[1]] = obj.name;
                workSheet.Cells[row, IntToCharHead[2]] = obj.units;
                workSheet.Cells[row, IntToCharHead[3]] = obj.bill;
                if (obj.surcharge == 0)
                {
                    workSheet.Cells[row, IntToCharHead[4]] = "N/A";
                }
                else
                {
                    workSheet.Cells[row, IntToCharHead[4]] = obj.surcharge;
                }
                workSheet.Cells[row, IntToCharHead[5]] = obj.total;
                //Console.WriteLine("added in excel");
            }
            else
            {
                Console.WriteLine("worksheet null error");
            }
        }


        //----------close exce------------//
        //function used to autofir columns and close excel file
        public static void CloseExcel()
        {   if(workSheet is not null && excelwb is not null)
            {
                for (int i = 1; i <= Constants.NoOfColumns; i++)         //loop making every column autofit
                {
                    ((Excel.Range)workSheet.Columns[i]).AutoFit();
                }
   
                DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;  // using System;

                string timestamp = now.ToString("yyyyMMddHHmmssfff");          //generating current time stamp

                excelwb.SaveAs(@"C:\Users\User\source\repos\Electricity_bill\Electricity_bill\output\output" + timestamp + ".xlsx");       //saving file with current timestamp
                /*excelwb.Close();*/
                /*excelApp.Quit();*/
            }
            else
            {
                Console.WriteLine("error : worksheet is null");
            }
        }
    }
}
