using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace MathNS
{
    public static class CalcMath
    {

        public static double Sum(double cis1, double cis2) => cis1 + cis2;
        public static double Sub(double cis1, double cis2) => cis1 - cis2;
        public static double Mul(double cis1, double cis2) => cis1 * cis2;
        public static double Div(double cis1, double cis2)
        {
            return cis1 / cis2;
        }
        public static bool TryDiv(double cis1, double cis2, out double vysledek)
        {
            if (cis2 != 0) 
            { 
                vysledek = Div(cis1, cis2);
                return true;
            }
            else
            {
                vysledek = 0;
                return false;
            }
        }
        public static double Pow(double cis1, double cis2)
        {
            return Math.Pow(cis1, cis2);
        }
        public static double Sqrt(double cis1,double cis2) {//Odmocnina 2
            double val = 0;
            if (TryDiv(1.0, cis1, out val))
            {
                if (cis2 >= 0)
                    return Math.Pow(cis2, val);
                else if (cis2 < 0 && cis1 % 2 == 1)
                    return -1 * Math.Pow(-cis2, val);
                else throw new ArgumentException();
            }
            else return 0;
        }
        public static double Factorial(int num_base)
        {
            for(int i = num_base-1;i>1;i--)
            {
                num_base *= i;
            }
            return num_base;
        }
        public static double Abs(double num)
        {
            if (num < 0)
            {
                num *= -1;
            }
            return num;
        }
        public static double Mod(double cis1, double cis2)
        {
            return cis1 % cis2;
        }
    }
}