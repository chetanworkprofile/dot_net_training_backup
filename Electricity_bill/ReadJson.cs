using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

//-----------class to read data from json file 
//--------takes input parameter as a list of bill class objects as out parameter
//-----returns output as reading entries from json and storing in list of bill objects

namespace Electricity_bill
{
    public class Json
    {
        public static void ReadJson(out List<Bill> objs,string ppath)
        {
            try
            {
                string path = ppath;       //path to input json file
                string data = File.ReadAllText(path);                 //stores whole json file in string variable
                //Console.WriteLine(data);

                //deserialize json string into list of objects
                objs = JsonSerializer.Deserialize<List<Bill>>(data)!;      //null forgiving operator

                //checks if list of objs is not null to avoid null  object errors
                if (objs is not null)
                {
                    Console.WriteLine("Total length of input entries: " + objs.Count);
                }
                else
                {
                    Console.WriteLine("null json file");
                    return;
                }
            }
            catch (JsonException e)
            {
                Console.WriteLine("json exception " + e.Message);
                throw;
            }
        }
    }
}
