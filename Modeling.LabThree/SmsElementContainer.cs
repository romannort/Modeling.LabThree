using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    public class SmsElementContainer : SmsElementBase
    {

        private UInt32 maxCapacity;

        private UInt32 currentCapacity;

        public SmsElementContainer(UInt32 maxCapacity)
        {
            this.maxCapacity = maxCapacity;
            currentCapacity = 0;
        }

        public static SmsElementContainer operator++(SmsElementContainer o)
        {
            if (o.State != SmsElementState.Full)
            {
                o.currentCapacity++;
            }
            if (o.currentCapacity == o.maxCapacity)
            {
                o.State = SmsElementState.Full;
            }
            return o;
        }


        public static SmsElementContainer operator --(SmsElementContainer o)
        {
            if (o.State != SmsElementState.Free)
            {
                o.currentCapacity--;
            }
            if (o.currentCapacity == 0)
            {
                o.State = SmsElementState.Free;
            }
            return o;
        }
    }
}
