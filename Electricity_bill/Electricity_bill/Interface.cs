using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electricity_bill
{
    public interface ICalculation
    {
        abstract public void ServiceMain(List<Bill> objs);
        abstract public  double? CalculateBill(int? units);
        abstract public double? CalculateSurcharge(double? bill);
    }
}
