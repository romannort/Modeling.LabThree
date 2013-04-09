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


        public SmsState( String code, params SmsElementState[] elements )
        {
            this.Code = code;
            this.Key = new SmsStateKey(elements);
        }


        public static SmsState operator++(SmsState o)
        {
            o.taktsCount++;
            return o;
        }
    }
}
