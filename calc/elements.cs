using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calc
{
    public abstract class Function
    {
        protected Function prevElement = null;
        protected Function nextElement = null;

        public enum FuncType
        {
            Constant,
            Unary,
            Binary,
            Unset
        }

        protected FuncType type;

        public Function()
        {
        }

        protected void setType(FuncType type)
        {
            this.type = type;
        }



    }
    public class ConstNum : Function
    {
        double value;

        public ConstNum()
        {
            value = 0;
            type = FuncType.Constant;
        }
        public ConstNum(double InitValue)
        {
            value = InitValue;
            type = FuncType.Constant;
        }

        public void SetValue(double NewValue)
        {
            value = NewValue;
        }
        public void Clear()
        {
            value = 0;
        }
        public override string ToString()
        {
            return value.ToString();
        }
    }
    public class UnaryFunction : Function
    {
        private FuncName name;
        double value;
        double modyfier;

        public enum FuncName
        {
            Factorial,
            Pow,
            Sqrt
        }
        public UnaryFunction()
        {
            type = FuncType.Unary;
        }

        public double GetValue()
        {
            switch(name)
            {
                case FuncName.Factorial:
                    {
                        return 0;
                    }
                case FuncName.Pow:
                    {
                        return 1;
                    }
                case FuncName.Sqrt:
                    {
                        return 0;
                    }
                default:
                    throw new Exception("Unrecognized function name");
            }
        }
    }
    public class BinaryFunction : Function
    {
        public enum FuncName
        {
            Add,
            Sub,
            Div,
            Mul
        }
        public BinaryFunction()
        {
            type = FuncType.Binary;
        }
    }
}
