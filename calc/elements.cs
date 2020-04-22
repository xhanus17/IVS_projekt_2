using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calc
{
    public abstract class element
    {
        protected element prevElement = null;
        protected element nextElement = null;

        public enum elType
        {
            constant,
            add,
            sub,
            div,
            mul,
            fact,
            pow,
            sqrt,
            unset
        }

        protected elType type;

        public element()
        {
        }

        protected void setType(elType type)
        {
            this.type = type;
        }



    }
    public class ConstNum : element
    {
        double value;

        public ConstNum()
        {
            value = 0;
            type = elType.constant;
        }
        public ConstNum(double InitValue)
        {
            value = InitValue;
            type = elType.constant;
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
}
