using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    /// <summary>
    /// 
    /// </summary>
    public class SmsState
    {

        public static UInt32 TotalTaktsCount;

        private UInt32 taktsCount;

        public SmsStateKey Key { get; private set; }

        public String Code { get; private set; }

        public Double Probability
        {
            get
            {
                return (Double)taktsCount / TotalTaktsCount;
            }
        }


        public SmsState( String code, params SmsElementState[] elements ): this(elements)
        {
            this.Code = code;
        }

        public SmsState(params SmsElementState[] elements)
        {
            this.Key = new SmsStateKey(elements);
        }

        //public SmsState(SmsStateKey key)
        //{
        //    this.Key = key;
        //    this.Code = CodeBuilder();
        //}


        public static SmsState operator++(SmsState o)
        {
            o.taktsCount++;
            return o;
        }

        //private String CodeBuilder()
        //{
        //    StringBuilder builder = new StringBuilder();
        //    foreach(SmsElementState state in Key.ElementsStates)
        //    {
        //        builder.Append((int)state);
        //    }
        //     return builder.ToString();
        //}


    }
}
