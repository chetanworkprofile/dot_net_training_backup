using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Electricity_bill;

namespace Electricity_bill
{
    internal class Service : ICalculation
    {
        double? Surcharge;
        double? BillAmount;
        double? Total;
        CalculationObj Calc;
        public Service() {
            Surcharge = 0;
            BillAmount = 0 ;
            Total = 0;
            Calc = new CalculationObj();
        }

        public void ServiceMain(List<Bill> objs)
        {
            if (objs is not null)
            {   
                Stopwatch s1 = new Stopwatch();
                s1.Start();
                int i = 2;
                foreach (Bill obj in objs)
                {   //copy attributes we have 
                    Calc.id = obj.id;
                    Calc.name = obj.name;
                    Calc.units = obj.units;

                    //calculate other attributes
                    BillAmount = CalculateBill(obj.units);
                    Calc.bill = BillAmount;

                    Surcharge = CalculateSurcharge(BillAmount);
                    Calc.surcharge = Surcharge;

                    Total = BillAmount + Surcharge;
                    if (Total is not null)
                    {
                        Total = Math.Max(val1: (double)Total, val2: (double)100);
                    }
                    else
                    {
                        Total = 100;
                    }
                    Calc.total = Total;

                    //log output
                    /*Console.WriteLine(obj.id);
                    Console.WriteLine(obj.name);
                    Console.WriteLine(obj.units);
                    Console.WriteLine(BillAmount);
                    Console.WriteLine(Surcharge);
                    Console.WriteLine(Total);*/
                    LocalExcel.AddRow(i, Calc);
                    Incsv.AddCsvRow(Calc);
                    i++;
                }
                i = 2;
                s1.Stop();
                LocalExcel.CloseExcel();
                Incsv.EndCSV();

                LocalExcel.Initialize();             //initialize excel application,workbook,worksheet

                // Establish column headings in cells A1 and B1.
                LocalExcel.MakeHeadings();

                //initalize csv
                Incsv.InitializeCSV();

                Stopwatch s2 = new Stopwatch();
                s2.Start();

                /*Parallel.ForEach(objs, obj =>
                {   //copy attributes we have 
                    lock (this)
                    {
                        Calc.id = obj.id;
                        Calc.name = obj.name;
                        Calc.units = obj.units;

                        //calculate other attributes
                        BillAmount = CalculateBill(obj.units);
                        Calc.bill = BillAmount;

                        Surcharge = CalculateSurcharge(BillAmount);
                        Calc.surcharge = Surcharge;

                        Total = BillAmount + Surcharge;
                        if (Total is not null)
                        {
                            Total = Math.Max(val1: (double)Total, val2: (double)100);
                        }
                        else
                        {
                            Total = 100;
                        }
                        Calc.total = Total;

                        //log output
                        *//*Console.WriteLine(obj.id);
                        Console.WriteLine(obj.name);
                        Console.WriteLine(obj.units);
                        Console.WriteLine(BillAmount);
                        Console.WriteLine(Surcharge);
                        Console.WriteLine(Total);*/
                /*Parallel.Invoke(
                    () => LocalExcel.AddRow(i, Calc),
                    () => Incsv.AddCsvRow(Calc));*//*
                LocalExcel.AddRow(i, Calc);
                Incsv.AddCsvRow(Calc);
                i++;
            }
        });*/

                Thread t1 = new Thread(() =>
                {
                    for(int j=0;j<objs.Count/6;j++)
                    {   //copy attributes we have 
                        Calc.id = objs[j].id;
                        Calc.name = objs[j].name;
                        Calc.units = objs[j].units;

                        //calculate other attributes
                        BillAmount = CalculateBill(objs[j].units);
                        Calc.bill = BillAmount;

                        Surcharge = CalculateSurcharge(BillAmount);
                        Calc.surcharge = Surcharge;

                        Total = BillAmount + Surcharge;
                        if (Total is not null)
                        {
                            Total = Math.Max(val1: (double)Total, val2: (double)100);
                        }
                        else
                        {
                            Total = 100;
                        }
                        Calc.total = Total;
                        LocalExcel.AddRow(i, Calc);
                        Incsv.AddCsvRow(Calc);
                        i++;
                    }
                });
                Thread t2 = new Thread(() =>
                {
                    for (int j = objs.Count/6; j < objs.Count/3; j++)
                    {   //copy attributes we have 
                        Calc.id = objs[j].id;
                        Calc.name = objs[j].name;
                        Calc.units = objs[j].units;

                        //calculate other attributes
                        BillAmount = CalculateBill(objs[j].units);
                        Calc.bill = BillAmount;

                        Surcharge = CalculateSurcharge(BillAmount);
                        Calc.surcharge = Surcharge;

                        Total = BillAmount + Surcharge;
                        if (Total is not null)
                        {
                            Total = Math.Max(val1: (double)Total, val2: (double)100);
                        }
                        else
                        {
                            Total = 100;
                        }
                        Calc.total = Total;
                        LocalExcel.AddRow(i, Calc);
                        Incsv.AddCsvRow(Calc);
                        i++;
                    }
                });

                Thread t3 = new Thread(() =>
                {
                    for (int j = objs.Count / 3; j < objs.Count/2; j++)
                    {   //copy attributes we have 
                        Calc.id = objs[j].id;
                        Calc.name = objs[j].name;
                        Calc.units = objs[j].units;

                        //calculate other attributes
                        BillAmount = CalculateBill(objs[j].units);
                        Calc.bill = BillAmount;

                        Surcharge = CalculateSurcharge(BillAmount);
                        Calc.surcharge = Surcharge;

                        Total = BillAmount + Surcharge;
                        if (Total is not null)
                        {
                            Total = Math.Max(val1: (double)Total, val2: (double)100);
                        }
                        else
                        {
                            Total = 100;
                        }
                        Calc.total = Total;
                        LocalExcel.AddRow(i, Calc);
                        Incsv.AddCsvRow(Calc);
                        i++;
                    }
                });
                Thread t4 = new Thread(() =>
                {
                    for (int j = objs.Count/2; j < objs.Count*2/3; j++)
                    {   //copy attributes we have 
                        Calc.id = objs[j].id;
                        Calc.name = objs[j].name;
                        Calc.units = objs[j].units;

                        //calculate other attributes
                        BillAmount = CalculateBill(objs[j].units);
                        Calc.bill = BillAmount;

                        Surcharge = CalculateSurcharge(BillAmount);
                        Calc.surcharge = Surcharge;

                        Total = BillAmount + Surcharge;
                        if (Total is not null)
                        {
                            Total = Math.Max(val1: (double)Total, val2: (double)100);
                        }
                        else
                        {
                            Total = 100;
                        }
                        Calc.total = Total;
                        LocalExcel.AddRow(i, Calc);
                        Incsv.AddCsvRow(Calc);
                        i++;
                    }
                });
                Thread t5 = new Thread(() =>
                {
                    for (int j = objs.Count * 2 / 3; j < objs.Count*5/6; j++)
                    {   //copy attributes we have 
                        Calc.id = objs[j].id;
                        Calc.name = objs[j].name;
                        Calc.units = objs[j].units;

                        //calculate other attributes
                        BillAmount = CalculateBill(objs[j].units);
                        Calc.bill = BillAmount;

                        Surcharge = CalculateSurcharge(BillAmount);
                        Calc.surcharge = Surcharge;

                        Total = BillAmount + Surcharge;
                        if (Total is not null)
                        {
                            Total = Math.Max(val1: (double)Total, val2: (double)100);
                        }
                        else
                        {
                            Total = 100;
                        }
                        Calc.total = Total;
                        LocalExcel.AddRow(i, Calc);
                        Incsv.AddCsvRow(Calc);
                        i++;
                    }
                });
                Thread t6 = new Thread(() =>
                {
                    for (int j = objs.Count * 5 / 6; j < objs.Count; j++)
                    {   //copy attributes we have 
                        Calc.id = objs[j].id;
                        Calc.name = objs[j].name;
                        Calc.units = objs[j].units;

                        //calculate other attributes
                        BillAmount = CalculateBill(objs[j].units);
                        Calc.bill = BillAmount;

                        Surcharge = CalculateSurcharge(BillAmount);
                        Calc.surcharge = Surcharge;

                        Total = BillAmount + Surcharge;
                        if (Total is not null)
                        {
                            Total = Math.Max(val1: (double)Total, val2: (double)100);
                        }
                        else
                        {
                            Total = 100;
                        }
                        Calc.total = Total;
                        LocalExcel.AddRow(i, Calc);
                        Incsv.AddCsvRow(Calc);
                        i++;
                    }
                });
                t1.Start();
                t2.Start();
                t3.Start();
                t4.Start();
                t5.Start();
                t6.Start();

                t1.Join();
                t2.Join();
                t3.Join();
                t4.Join();
                t5.Join();
                t6.Join();


                s2.Stop();
                Console.WriteLine("Without threading: " + s1.ElapsedMilliseconds);
                Console.WriteLine("With threading: " + s2.ElapsedMilliseconds);
            }
        }

        public double? CalculateBill(int? units)
        {   
            double? billAmount = 0;
            while (units > 0)
            {
                if (units >= Constants.Limit_3)
                {
                    billAmount += (units - Constants.Limit_3 + 1) * Constants.Charge_4;
                    units = Constants.Limit_3-1;
                    continue;
                }
                else if (units >= Constants.Limit_2)
                {
                    billAmount += (units - Constants.Limit_2 + 1) * Constants.Charge_3;
                    units = Constants.Limit_2-1;
                    continue;
                }
                else if (units >= Constants.Limit_1)
                {
                    billAmount += (units - Constants.Limit_1 + 1) * Constants.Charge_2;
                    units = Constants.Limit_1-1;
                    continue;
                }
                else
                {
                    billAmount += units * Constants.Charge_1; 
                    break;
                }
            }
            return billAmount;
        }

        public double? CalculateSurcharge(double? billAmount)
        {
            double? surchargeAmount = 0;
            if (billAmount > Constants.BillThershold)
            {
                surchargeAmount = Constants.Surcharge * billAmount;
            }
            return surchargeAmount;
        }

    }
}
