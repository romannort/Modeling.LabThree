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

        private static IList<SmsElementState> possibleStates = new List<SmsElementState>() { 
            SmsElementState.Free,
            SmsElementState.One,
            SmsElementState.Two,
            SmsElementState.Three,
            SmsElementState.Full
        };

        public SmsContainerElement(UInt32 maxCapacity)
        {
            this.maxCapacity = maxCapacity;
            currentCapacity = 0;
        }

        public Boolean NotFull
        {
            get
            {
                return State != SmsElementState.Full;
            }
        }

        public Boolean NonEmpty
        {
            get
            {
                return State != SmsElementState.Free;
            }
        }
        
        public static SmsContainerElement operator++(SmsContainerElement o)
        {
            Add(o);
            return o;
        }


        public static SmsContainerElement operator--(SmsContainerElement o)
        {
            Remove(o);
            return o;
        }

        public override void TakeRequest()
        {
            Add(this);
        }

        private static void Add(SmsContainerElement o)
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
                o.State = CapacityToStateConverter(o.currentCapacity, o.maxCapacity);
            }
        }

        private static void Remove(SmsContainerElement o)
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
                o.State = CapacityToStateConverter(o.currentCapacity, o.maxCapacity);
            }
        }

        private static SmsElementState CapacityToStateConverter(UInt32 currentCapacity, UInt32 maxCapacity)
        {
            if (currentCapacity == maxCapacity)
            {
                return possibleStates.Last(); // FULL
            }
            if (currentCapacity == 0)
            {
                return possibleStates.First();
            }
            return possibleStates[(int)currentCapacity];
        }
    }
}
