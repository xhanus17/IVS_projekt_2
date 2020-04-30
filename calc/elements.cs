using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNS;

namespace calc
{
    /// <summary>
    ///     Kořenová třída pro všecny funkce + konstantu, definuje základní společné rysy
    /// </summary>
    public abstract class Function
    {
        
        public Function parentFunction;
        public Function leftFunction { get; protected set; }
        public Function rightFunction { get; protected set; }

        public FuncName name { get; protected set; }
        /// <summary>
        /// Enum se jmény funkcí
        /// </summary>
        public enum FuncName
        {
            Factorial,
            Pow,
            Sqrt,
            Add,
            Div,
            Sub,
            Mul,
            Mod
        }
        /// <summary>
        /// Enum s typy funkcí
        /// </summary>
        public enum FuncType
        {
            Constant,
            Unary,
            Binary,
            Unset
        }

        public FuncType type { get; protected set; }

        public Function()
        {

        }
        /// <summary>
        ///     Nastaví funkci v levém rameni aktivní funkce
        /// </summary>
        /// <param name="leftFunction">Funkce která se má nastavit na levé rameno</param>
        public void setLeftFunction(Function leftFunction)
        {
            this.leftFunction = leftFunction;
        }
        /// <summary>
        ///     Nastaví funkci v pravém rameni aktivní funkce
        /// </summary>
        /// <param name="rightFunction">Funkce která se má nastavit na pravé rameno</param>
        public void setRightFunction(Function rightFunction)
        {
            this.rightFunction = rightFunction;
        }
        /// <summary>
        ///     Vrátí funkci z levého ramene aktivní funkce
        /// </summary>
        /// <returns>Funkce z levého ramene</returns>
        public Function getleftFunction()
        {
            return leftFunction;
        }
        /// <summary>
        ///     Vrátí funkci z pravého ramene aktivní funkce
        /// </summary>
        /// <returns>Funkce z pravého ramene</returns>
        public Function getRightFunction()
        {
            return rightFunction;
        }
        /// <summary>
        ///     Prototyp funkce pro výpočet hodnoty funkce, funkce si vyžádá hodnotu funkce z levého a pravého ramene a provede nad ním vlastní operaci, výslednou hodnotu vrátí
        /// </summary>
        /// <returns>Výsledná hodnota funkce</returns>
        public abstract double Solve();



    }
    /// <summary>
    /// Potomek funkce sloužící k uložení konstant
    /// </summary>
    public class ConstNum : Function
    {
        double value;
        public bool point;
        public int pointPos;

        public ConstNum(Function parentFunction)
        {
            value = 0;
            point = false;
            pointPos = 0;
            type = FuncType.Constant;
            this.parentFunction = parentFunction;
        }
        public ConstNum(double InitValue, Function parentFunction)
        {
            this.parentFunction = parentFunction;
            value = InitValue;
            if (InitValue.ToString().Contains(","))
            {
                point = true;
                int points = InitValue.ToString().Count() - 1 - InitValue.ToString().IndexOf(',');
            }
            else
            {
                point = false;
                pointPos = 0;
            }
            type = FuncType.Constant;
        }
        /// <summary>
        /// nastaví hodnotu konstanty
        /// </summary>
        /// <param name="NewValue">hodnota na kterou se má konstanta nastavit</param>
        public void SetValue(double NewValue)
        {
            value = NewValue;
        }
        /// <summary>
        /// Vrací hodnotu konstanty
        /// </summary>
        /// <returns>Hodnota konstanty</returns>
        public double GetValue()
        {
            return value;
        }
        /// <summary>
        /// Vynuluje konstantu
        /// </summary>
        public void Clear()
        {
            value = 0;
        }
        /// <summary>
        /// Vrací hodnotu konstanty zpracovanou jako string
        /// </summary>
        /// <returns>Hodnotu jako string</returns>
        public override string ToString()
        {
            string text = value.ToString();
            if (point)
            {
                int len = 0;
                if (text.IndexOf(',') > 0)
                {
                    if (text.Remove(0, text.IndexOf(',')).Length < pointPos + 1)
                        len = pointPos - text.Remove(0, text.IndexOf(',')).Length + 1;
                    else
                        len = 0;
                }
                else
                {
                    text += ",";
                    len = pointPos;
                }

                for (int i = 0; i < len; i++)
                {
                    text += "0";
                }
            }
            return text;
        }
        /// <summary>
        /// Přepsání funkce Solve, vrací hodnotu konstanty pro výpočty nadřazených funkcí
        /// </summary>
        /// <returns></returns>
        public override double Solve()
        {
            return value;
        }
    }
    /// <summary>
    /// potomek funkce Function slouží jako předek pro unární funkce
    /// </summary>
    public abstract class UnaryFunction : Function
    {
        protected double value = 0;
        /// <summary>
        /// Vrací číslo, nad kterým se má provést operace
        /// </summary>
        /// <returns>Číslo, nad kterým se má provést operace</returns>
        public double GetValue()
        {
            return value;
        }
        /// <summary>
        /// Pokud potomek této funkce nemá vlastní funkci pro vypsání, vyvolá se výjimka
        /// </summary>
        /// <returns>null</returns>
        public override string ToString()
        {
            throw new Exception("Unrecognized function name");
        }
        /// <summary>
        /// Nastaví hodnotu čísla nad kterým se má provést operace
        /// </summary>
        /// <param name="newValue">Nová hodnota</param>
        public void SetValue(double newValue)
        {
            value = newValue;
        }
    }
    /// <summary>
    /// Funkce pro výpočet faktoriálu
    /// </summary>
    public class FactorialFunc : UnaryFunction
    {
        public FactorialFunc(double value, Function parentFunction)
        {
            name = FuncName.Factorial;
            type = FuncType.Unary;
            this.value = value;
            this.parentFunction = parentFunction;
        }
        public override double Solve()
        {
            return CalcMath.Factorial((int)value);
        }
        public override string ToString()
        {
            return value + "!";
        }
    }
    /// <summary>
    /// Funkce pro výpočet mocniny
    /// </summary>
    public class PowFunc : Function
    {
        public PowFunc(Function LeftFunction, Function parentFunction)
        {
            name = FuncName.Pow;
            type = FuncType.Binary;
            this.leftFunction = LeftFunction;
            this.parentFunction = parentFunction;
        }
        public override string ToString()
        {
            return leftFunction.ToString() + '^' + rightFunction.ToString();
        }
        public override double Solve()
        {
            return CalcMath.Pow(leftFunction.Solve(), rightFunction.Solve());
        }
    }
    /// <summary>
    /// Funkce pro výpočet odmocniny
    /// </summary>
    public class SqrtFunc : Function
    {
        public SqrtFunc(Function LeftFunction, Function parentFunction)
        {
            name = FuncName.Sqrt;
            type = FuncType.Binary;
            this.leftFunction = LeftFunction;
            this.parentFunction = parentFunction;
        }
        public override string ToString()
        {
            return leftFunction.ToString() + '√' + rightFunction.ToString();
        }
        public override double Solve()
        {
            double right = rightFunction.Solve();
            double left = leftFunction.Solve();
            if (right >= 0 || CalcMath.Mod(left, 2) == 1)
                return CalcMath.Sqrt(left, right);
            else
                throw new ArgumentException();
        }
    }
    /// <summary>
    /// Funkce pro výpočet součtu
    /// </summary>
    public class AddFunc : Function
    {
        public AddFunc(Function LeftFunction, Function parentFunction)
        {
            name = FuncName.Add;
            type = FuncType.Binary;
            this.leftFunction = LeftFunction;
            this.parentFunction = parentFunction;
        }
        public override string ToString()
        {
            return leftFunction.ToString() + "+" + rightFunction.ToString();
        }
        public override double Solve()
        {
            return CalcMath.Sum(leftFunction.Solve(), rightFunction.Solve());
        }
    }
    /// <summary>
    /// Funkce pro výpočet rozdílu
    /// </summary>
    public class SubFunc : Function
    {
        public SubFunc(Function LeftFunction, Function parentFunction)
        {
            name = FuncName.Sub;
            type = FuncType.Binary;
            this.leftFunction = LeftFunction;
            this.parentFunction = parentFunction;
        }
        public override string ToString()
        {
            return leftFunction.ToString() + "-" + rightFunction.ToString();
        }
        public override double Solve()
        {
            return CalcMath.Sub(leftFunction.Solve(), rightFunction.Solve());
        }
    }
    /// <summary>
    /// Funkce pro výpočet poddílu
    /// </summary>
    public class DivFunc : Function
    {
        public DivFunc(Function LeftFunction, Function parentFunction)
        {
            name = FuncName.Div;
            type = FuncType.Binary;
            this.leftFunction = LeftFunction;
            this.parentFunction = parentFunction;
        }
        public override string ToString()
        {
            return leftFunction.ToString() + "÷" + rightFunction.ToString();
        }
        public override double Solve()
        {
            double res = 0;
            if (!CalcMath.TryDiv(leftFunction.Solve(), rightFunction.Solve(), out res))
            {
                throw new DivideByZeroException();
            }
            else
                return res;
        }
    }
    /// <summary>
    /// Funkce pro výpočet násobku
    /// </summary>
    public class MulFunc : Function
    {
        public MulFunc(Function LeftFunction, Function parentFunction)
        {
            name = FuncName.Mul;
            type = FuncType.Binary;
            this.leftFunction = LeftFunction;
            this.parentFunction = parentFunction;
        }
        public override string ToString()
        {
            return leftFunction.ToString() + "×" + rightFunction.ToString();
        }
        public override double Solve()
        {
            return CalcMath.Mul(leftFunction.Solve(), rightFunction.Solve());
        }
    }
    /// <summary>
    /// Funkce pro výpočet modula
    /// </summary>
    public class ModFunc : Function
    {
        public ModFunc(Function LeftFunction, Function parentFunction)
        {
            name = FuncName.Mod;
            type = FuncType.Binary;
            this.leftFunction = LeftFunction;
            this.parentFunction = parentFunction;
        }
        public override string ToString()
        {
            return leftFunction.ToString() + "%" + rightFunction.ToString();
        }
        public override double Solve()
        {
            if (rightFunction.Solve() != 0)
                return CalcMath.Mod(leftFunction.Solve(), rightFunction.Solve());
            else
                throw new DivideByZeroException();
        }
    }
}
