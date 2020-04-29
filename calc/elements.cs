using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNS;

namespace calc
{
    public abstract class Function
    {
        public Function parentFunction;
        public Function leftFunction { get; protected set; }
        public Function rightFunction { get; protected set; }

        public FuncName name { get; protected set; }

        public enum FuncName
        {
            Factorial,
            Pow,
            Sqrt,
            Add,
            Div,
            Sub,
            Mul
        }

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

        public bool setLeftFunction(Function leftFunction)
        {
            this.leftFunction = leftFunction;
            return true;
        }

        public bool setRightFunction(Function rightFunction)
        {
            this.rightFunction = rightFunction;
            return true;
        }

        public Function getleftFunction()
        {
            return leftFunction;
        }

        public Function getRightFunction()
        {
            return rightFunction;
        }

        public abstract double Solve();



    }
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

        public void SetValue(double NewValue)
        {
            value = NewValue;
        }
        public double GetValue()
        {
            return value;
        }
        public void Clear()
        {
            value = 0;
        }
        public override string ToString()
        {
            string text = value.ToString();
            if (point)
            {
                int len = 0;
                if (text.IndexOf(',') > 0)
                {
                    if (text.Remove(0, text.IndexOf(',')).Length < pointPos+1)
                        len = pointPos - text.Remove(0, text.IndexOf(',')).Length+1;
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
        public override double Solve()
        {
            return value;
        }
    }
    public abstract class UnaryFunction : Function
    {
        protected double value = 0;
        protected double modifier = 0;

        public double GetValue()
        {
            switch (name)
            {
                case FuncName.Factorial:
                    {
                        return CalcMath.Factorial((int)value); ;
                    }
                case FuncName.Pow:
                    {
                        return CalcMath.Pow(value, modifier);
                    }
                case FuncName.Sqrt:
                    {
                        return CalcMath.Sqrt(value, modifier);
                    }
                default:
                    throw new Exception("Unrecognized function name");
            }
        }

        public override string ToString()
        {
            switch (name)
            {
                case FuncName.Factorial:
                    {
                        return value + "!";
                    }
                case FuncName.Pow:
                    {
                        return value + "^" + modifier;
                    }
                case FuncName.Sqrt:
                    {
                        return modifier + "√" + value;
                    }
                default:
                    throw new Exception("Unrecognized function name");
            }
        }
        public void SetValue(double newValue)
        {
            value = newValue;
        }
        public void SetModifier(double newModifier)
        {
            modifier = newModifier;
        }
    }
    public class FactorialFunc : UnaryFunction
    {
        public FactorialFunc(double value, Function parentFunction)
        {
            name = FuncName.Factorial;
            this.value = value;
            this.parentFunction = parentFunction;
        }
        public override double Solve()
        {
            return CalcMath.Factorial((int)value);
        }
    }
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
            return CalcMath.Sqrt(leftFunction.Solve(), rightFunction.Solve());
        }
    }
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
            return leftFunction.Solve() + rightFunction.Solve();
        }
    }
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
            return leftFunction.Solve() - rightFunction.Solve();
        }
    }
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
            return leftFunction.Solve() / rightFunction.Solve();
        }
    }
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
            return leftFunction.Solve()*rightFunction.Solve();
        }
    }
}
