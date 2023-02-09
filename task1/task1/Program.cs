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
            string path = @"../../userElectricityUnits.json";
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
}
*//*

using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;

using Excel = Microsoft.Office.Interop.Excel;

public class Account
{
    public int ID { get; set; }
    public double Balance { get; set; }
}

public class Program
{

    static void DisplayInExcel(IEnumerable<Account> accounts)
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
        Excel._Worksheet workSheet = excelApp.ActiveSheet;

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
        workSheet.Columns[1].AutoFit();
        workSheet.Columns[2].AutoFit();
        //excelwb.SaveCopyAs(@"../../output.xlsx");
        excelApp.GetSaveAsFilename("hello.xlsx");
        excelwb.Close();
        excelApp.Quit();
    }



    public static void Main()
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

}*/

/*

using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Principal;
using System.IO;

namespace excel
{
    public class Account
    {
        public int ID { get; set; }
        public double Balance { get; set; }
    }
    class Program
    {
        public static void write(IEnumerable<Account> accounts)
        {
            string filePath = @"../../file.xlsx";
            
            File.Create(filePath);
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = excel.Workbooks.Open(filePath);
            Worksheet workSheet = wb.Worksheets[1];

            workSheet.Cells[1, "A"] = "ID Number";
            workSheet.Cells[1, "B"] = "Current Balance";

            var row = 1;
            foreach (var acct in accounts)
            {
                row++;
                workSheet.Cells[row, "A"] = acct.ID;
                workSheet.Cells[row, "B"] = acct.Balance;
            }
            workSheet.Columns[1].AutoFit();
            workSheet.Columns[2].AutoFit();

        }
        public static void Main()
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
            write(bankAccounts);
        }

    }
}*/


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
        public string id { get; set; }
        public string name { get; set; }
        public int units { get; set; }
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
}