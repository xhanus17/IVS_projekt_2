using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace MathNS
{
    public class math
    {

        public static double Sum(double cis1, double cis2) => cis1 + cis2;
        public static double Sub(double cis1, double cis2) => cis1 - cis2;
        public static double Mul(double cis1, double cis2) => cis1 * cis2;
        public static double Div(double cis1, double cis2)
        {
            try
            {
                if (cis2 == 0)
                {
                    throw new DivideByZeroException();
                }
                return cis1 / cis2;
            }
            catch (DivideByZeroException){ return 0; }
        }
        public static double Pow(double cis1, double cis2)
        {
            return Math.Pow(cis1, cis2);
        }
        public static double Sqrt(double cis1,double cis2) {//Odmocnina 2
            double val = Div(1, cis1);
            return math.Pow(cis2, 1 / cis1);
        }
    }
}
