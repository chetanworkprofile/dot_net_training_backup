using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electricity_bill
{
    internal class Constants
    {   
        public static string path = @"./userElectricityUnitscp.json";
        public const int NoOfColumns = 6;
        public static string[] Headings = new string[NoOfColumns] { "GUID", "Name", "Units", "Bill", "Surcharge", "Total" };
        public static int Limit_1 = 200;
        public static int Limit_2 = 400;
        public static int Limit_3 = 600;
        public static double Surcharge = 0.15;
        public static double Charge_1 = 1.2;
        public static double Charge_2 = 1.5;
        public static double Charge_3 = 1.8;
        public static double Charge_4 = 2.0;
        public static int BillThershold = 400;
    }
}
