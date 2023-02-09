/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
*/

// ---- Bill class that is used to correspond to json entries in input json file and is going to create objects array/list of this class

namespace Electricity_bill
{
    public class Bill
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public int? units { get; set; }
    }

    public class CalculationObj
    {
        public string? id { get; set;}
        public string? name { get; set;}
        public int? units { get; set;}
        public double? bill { get; set;}
        public double? surcharge { get; set;}
        public double? total { get; set;}

    }

}
