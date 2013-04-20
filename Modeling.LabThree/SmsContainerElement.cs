using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    public class SmsContainerElement : SmsElementBase
    {

        private UInt32 maxCapacity;

        private UInt32 currentCapacity;
        
        public SmsContainerElement(UInt32 maxCapacity)
        {
            this.maxCapacity = maxCapacity;
            currentCapacity = 0;
        }

        
        public static SmsContainerElement operator++(SmsContainerElement o)
        {
            if (o.State != SmsElementState.Full)
            {
                o.currentCapacity++;
            }
            if (o.currentCapacity == o.maxCapacity)
            {
                o.State = SmsElementState.Full;
            }
            else
            {
                o.State = SmsElementState.NotFull;
            }
            return o;
        }


        public static SmsContainerElement operator--(SmsContainerElement o)
        {
            if (o.State != SmsElementState.Free)
            {
                o.currentCapacity--;
            }
            if (o.currentCapacity == 0)
            {
                o.State = SmsElementState.Free;
            }
            else
            {
                o.State = SmsElementState.NotFull;
            }
            return o;
        }
    }
}
