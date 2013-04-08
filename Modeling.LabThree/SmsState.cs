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

        public ICollection<SmsElementState> ElementsStates { get; private set; }

        public String Code { get; private set; }

        public Decimal Probability
        {
            get
            {
                return (Decimal)taktsCount / TotalTaktsCount;
            }
        }


        public SmsState( String code, params SmsElementState[] elements )
        {
            this.Code = code;
            ElementsStates = new List<SmsElementState>();
            foreach( SmsElementState e in elements )
            {
                ElementsStates.Add(e);
            }
        }


        public static SmsState operator++(SmsState o)
        {
            o.taktsCount++;
            return o;
        }
    }
}
