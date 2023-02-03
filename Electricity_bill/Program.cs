
//--------------task combined starts here----------------//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace ELectricity_bill
{
    //---------bill class corresponding to json entries------------//
    public class Bill
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public int? units { get; set; }
    }

    internal class Program
    {
        static void Main()
        {
            try
            {
                Bill[]? objs;
                try
                {
                    string path = @"./userElectricityUnits.json";
                    string data = File.ReadAllText(path);
                    //Console.WriteLine(data);
                    objs = JsonSerializer.Deserialize<Bill[]>(data);
                    if (objs is not null)
                    {
                        Console.WriteLine("Total length of input entries: " + objs.Length);
                    }
                    else
                    {
                        Console.WriteLine("null json file");
                        return;
                    }
                }
                catch (JsonException e)
                {
                    Console.WriteLine("json exception "+e.Message);
                    throw;
                }
                

                //----------excel initialization-------------//
                var excelApp = new Excel.Application
                {
                    Visible = true
                };

                Excel.Workbook excelwb = excelApp.Workbooks.Add();
                Excel._Worksheet workSheet = (Excel._Worksheet)excelApp.ActiveSheet;

                // Establish column headings in cells A1 and B1.
                workSheet.Cells[1, "A"] = "GUID";
                workSheet.Cells[1, "B"] = "Name";
                workSheet.Cells[1, "C"] = "Units";
                workSheet.Cells[1, "D"] = "Bill";
                workSheet.Cells[1, "E"] = "Surcharge";
                workSheet.Cells[1, "F"] = "Total";

                double? surcharge;
                double? billAmount;
                double? total;

                try
                {
                    if (objs is not null)
                    {
                        int i = 2;
                        foreach (Bill obj in objs)
                        {
                            Console.WriteLine(obj.id);
                            Console.WriteLine(obj.name);
                            Console.WriteLine(obj.units);
                            billAmount = CalculateBill(obj.units);
                            Console.WriteLine(billAmount);
                            surcharge = CalcSurcharge(billAmount);
                            Console.WriteLine(surcharge);
                            total = billAmount + surcharge;
                            if (total is not null)
                            {
                                total = Math.Max(val1: (double)total, val2: (double)100);
                            }
                            else
                            {
                                total = 100;
                            }
                            Console.WriteLine(total);
                            AddRow(workSheet, i, obj.id, obj.name, obj.units, billAmount, surcharge, total);
                            i++;
                        }
                    }
                }
                catch (InvalidComObjectException e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                //Console.ReadKey();
                CloseExcel(excelApp, excelwb, workSheet);
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine("file not found "+e.Message);
            }
            catch(IndexOutOfRangeException e)
            {
                Console.WriteLine("Index out of bound please check"+e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            finally
            {
                Console.WriteLine("Closing program processes");
                GC.Collect();
            }
            Console.ReadKey();
        }


        //----------calculation functions---------------------//
        public static double? CalculateBill(int? units)
        {
            double? billAmount = 0;
            while (units > 0)
            {
                if (units >= 600)
                {
                    billAmount += (units - 599) * 2.0;
                    units = 599;
                    continue;
                }
                else if (units >= 400)
                {
                    billAmount += (units - 399) * 1.8;
                    units = 399;
                    continue;
                }
                else if (units >= 200)
                {
                    billAmount += (units - 199) * 1.5;
                    units = 199;
                    continue;
                }
                else
                {
                    billAmount += units * 1.2; 
                    break;
                }
            }
            return billAmount;
        }
        public static double? CalcSurcharge(double? billAmount)
        {
            double? surchargeAmount = 0;
            if (billAmount > 400)
            {
                surchargeAmount = 0.15 * billAmount;
            }
            return surchargeAmount;
        }


        //-------add row--------------//
        public static void AddRow (Excel._Worksheet workSheet,int row,String? id,String? name,int? units,Double? bill,Double? surcharge,Double? total )
        {
            workSheet.Cells[row, "A"] = id;
            workSheet.Cells[row, "B"] = name;
            workSheet.Cells[row, "C"] = units;
            workSheet.Cells[row, "D"] = bill;
            if (surcharge == 0)
            {
                workSheet.Cells[row, "E"] = "N/A";
            }
            else
            {
                workSheet.Cells[row, "E"] = surcharge;
            }
            workSheet.Cells[row, "F"] = total;
        }

        //----------close exce------------//
        public static void CloseExcel( Excel.Application excelApp, Excel.Workbook excelwb, Excel._Worksheet workSheet)
        {
            ((Excel.Range)workSheet.Columns[1]).AutoFit();
            ((Excel.Range)workSheet.Columns[2]).AutoFit();
            ((Excel.Range)workSheet.Columns[3]).AutoFit();
            ((Excel.Range)workSheet.Columns[4]).AutoFit();
            ((Excel.Range)workSheet.Columns[5]).AutoFit();
            ((Excel.Range)workSheet.Columns[6]).AutoFit();
            //string n = excelApp.GetSaveAsFilename("hello.xlsx");
            //excelwb.SaveCopyAs(@"../../"+n);
            excelwb.SaveAs(@"C:\Users\User\source\repos\Electricity_bill\Electricity_bill\outputFile.xlsx");
            excelwb.Close();
            /*excelApp.Quit();*/
        }


    }
}










/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace task1
{
    public class Bill
    {
        public string id { get; set; }
        public string name { get; set; }
        public int units { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"../../../userElectricityUnits.json";
            string data = File.ReadAllText(path);
            //Console.WriteLine(data);
            Bill[] objs = JsonSerializer.Deserialize<Bill[]>(data);
            Console.WriteLine(objs.Length);

            foreach (Bill obj in objs)
            {
                Console.WriteLine(obj.id);
                Console.WriteLine(obj.name);
                Console.WriteLine(obj.units);
            }
            //Console.ReadKey();
        }
    }
}*/

/*
using System;
using System.Collections.Generic;
using System.IO;

using Excel = Microsoft.Office.Interop.Excel;

namespace Electricity_bill
{

    public class Account
    {
        public int ID { get; set; }
        public double Balance { get; set; }
    }
    public class Program
    {

        public static void DisplayInExcel(IEnumerable<Account> accounts)
        {
            var excelApp = new Excel.Application
            {
                // Make the object visible.
                Visible = true
            };

            Excel.Workbook excelwb = excelApp.Workbooks.Add();
            //Workbook wb = excelApp.Workbooks.Open(filePath);
            //Worksheet workSheet = wb.Worksheets[0];

            // This example uses a single workSheet. The explicit type casting is
            // removed in a later procedure.
            Excel._Worksheet workSheet = (Excel._Worksheet)excelApp.ActiveSheet;

            // Establish column headings in cells A1 and B1.
            workSheet.Cells[1, "A"] = "ID Number";
            workSheet.Cells[1, "B"] = "Current Balance";

            var row = 1;
            foreach (var acct in accounts)
            {
                row++;
                workSheet.Cells[row, "A"] = acct.ID;
                workSheet.Cells[row, "B"] = acct.Balance;
            }
            ((Excel.Range)workSheet.Columns[1]).AutoFit();
            ((Excel.Range)workSheet.Columns[2]).AutoFit();
            //excelwb.SaveCopyAs(@"../../output.xlsx");
            excelApp.GetSaveAsFilename("hello.xlsx");
            excelwb.Close();
            excelApp.Quit();
        }



        public static void Main(string[] args)
        {
            // Create a list of accounts.
            var bankAccounts = new List<Account> {
                new Account {
                              ID = 345678,
                              Balance = 541.27
                            },
                new Account {
                              ID = 1230221,
                              Balance = -127.44
                            }
            };

            // Display the list in an Excel spreadsheet.
            DisplayInExcel(bankAccounts);
        }

    }
}

*/

/*

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace task1
{
    public class Bill
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public int? units { get; set; }
    }

    internal class Program
    {
        public static double? calculateBill(int? units)
        {
            double? billAmount = 0;
            while (units > 0)
            {
                if (units >= 600)
                {
                    billAmount += (units - 599) * 2.0;
                    units = 599;
                    continue;
                }
                else if (units >= 400)
                {
                    billAmount += (units - 399) * 1.8;
                    units = 399;
                    continue;
                }
                else if (units >= 200)
                {
                    billAmount += (units - 199) * 1.5;
                    units = 199;
                    continue;
                }
                else
                {
                    billAmount += units * 1.2;
                    units = 0;
                    break;
                }
            }
            return billAmount;
        }
        public static double? calcSurcharge(double? billAmount)
        {
            double? surchargeAmount = 0;
            if (billAmount > 400)
            {
                surchargeAmount = 0.15 * billAmount;
            }
            return surchargeAmount;
        }

        static void Main(string[] args)
        {
            //Bill[] objs = new Bill();
            List<Bill> objs = new List<Bill>();
            objs.Add(new Bill { id = "1dcf2744-2603-4c11-81c8-f40ef8805e32", name = "Andreas Edinburough", units = 253 });
            objs.Add(new Bill { id = "1dcf2744-2603-4c11-81c8-f40ef8805r45", name = "ghsagd", units = 90 });
            objs.Add(new Bill { id = "1dcf2744-2603-4c11-81c8-f40ef8805e89", name = "hfkjsha", units = 789 });

            double? surcharge = 0;
            double? billAmount = 0;
            double total = 0;

            foreach (Bill obj in objs)
            {
                Console.WriteLine(obj.id);
                Console.WriteLine(obj.name);
                Console.WriteLine(obj.units);
                billAmount = calculateBill(obj.units);
                Console.WriteLine(billAmount);
                surcharge = calcSurcharge(billAmount);
                Console.WriteLine(surcharge);
                total = Math.Max((double)(billAmount + surcharge), (double)100);
                Console.WriteLine(total);
            }
            //Console.ReadKey();
            
        }
    }
}*/
