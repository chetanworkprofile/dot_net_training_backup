using System.Diagnostics;
using System;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Text;
using System.Threading;
using Electricity_bill;

namespace Electricity_bill
{
    public static class Incsv
    {
        public static MemoryStream mem = new MemoryStream();
        public static StreamWriter writer = new StreamWriter(mem);
        public static CsvWriter csvWriter = new CsvWriter(writer, CultureInfo.CurrentCulture);

        public static void InitializeCSV()
        {
            csvWriter.WriteField("id");
            csvWriter.WriteField("Name");
            csvWriter.WriteField("Unit");
            csvWriter.WriteField("Amount");
            csvWriter.WriteField("netAmount");
            csvWriter.WriteField("surcharge");
            csvWriter.NextRecord();
        }
        public static void AddCsvRow(CalculationObj obj ){
          /*  Stopwatch s3 = new Stopwatch();
            s3.Start();*/

                csvWriter.WriteField(obj.id);
                csvWriter.WriteField(obj.name);
                csvWriter.WriteField(obj.units);
                csvWriter.WriteField(obj.bill);
                csvWriter.WriteField(obj.surcharge);
                csvWriter.WriteField(obj.total);
                csvWriter.NextRecord();
            //Console.WriteLine("added in csv");
       }

        public static void EndCSV()
        {
            writer.Flush();
            StringBuilder sb = new StringBuilder();
            DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;  // using System
            string timestamp = now.ToString("yyyyMMddHHmmssfff");          //generating current time stamp
            sb.Clear();
            sb.Append(@"C:\Users\User\source\repos\Electricity_bill\Electricity_bill\output\output");
            sb.Append(timestamp);
            sb.Append(".csv");
            String path = sb.ToString();
            var result = Encoding.UTF8.GetString(mem.ToArray());
            File.AppendAllText(path, result);
            //s3.Stop();
            //Console.WriteLine($"This is time elapsed of incsv file: {s3.ElapsedMilliseconds}");

        }
    }

}

