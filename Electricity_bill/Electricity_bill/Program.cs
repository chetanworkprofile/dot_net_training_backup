
//--------------task combined starts here----------------//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Runtime.InteropServices;
using Electricity_bill;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

namespace Electricity_bill
{
    internal class Program
    {
        static void Main()
        {
            try
            {   Stopwatch s0 = new Stopwatch();
                s0.Start();
                //Bill.Bill[]? objs;
                string path = Constants.path;      //path to input json file
                List<Bill> objs = new();       //creating list of objects to store json deserialized objects
                Json.ReadJson(out objs, path);                       // calling json reader from ReadJson.cs

                LocalExcel.Initialize();             //initialize excel application,workbook,worksheet

                // Establish column headings in cells A1 and B1.
                LocalExcel.MakeHeadings();

                //initalize csv
                Incsv.InitializeCSV();

                try
                {
                    //dependecny injaction with the help of interface
                    ICalculation ServiceObj = new Service();
                    WholeWork WorkObj = new WholeWork(ServiceObj);
                    WorkObj.DoWork(objs);

                }
                catch (InvalidComObjectException e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                //Console.ReadKey();
                LocalExcel.CloseExcel();
                Incsv.EndCSV();
                s0.Stop();
                Console.WriteLine("total time taken: " + s0.ElapsedMilliseconds);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("file not found " + e.Message);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Index out of bound please check" + e.Message);
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


    }


    // used to create dependency injection to insert service class dependecies by using interface

    public class WholeWork
    {
        private readonly ICalculation _Icalc;
        public WholeWork(ICalculation icalc)
        {
            _Icalc = icalc;
        }
        public void DoWork(List<Bill> objs)
        {
            _Icalc.ServiceMain(objs);
        }
    }
}


