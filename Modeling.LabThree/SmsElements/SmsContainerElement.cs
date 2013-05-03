using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree.SmsElements
{
    public class SmsContainerElement : SmsElementBase, ICapacityElement
    {

        public int MaxCapacity
        {
            get;
            set;
        }

        public int CurrentCapacity
        {
            get;
            set;
        }

        public SmsContainerElement(Int32 maxCapacity)
        {
            this.MaxCapacity = maxCapacity;
            CurrentCapacity = 0;
        }

        public Boolean NotFull
        {
            get
            {
                return State != SmsElementStateCode.Full;
            }
        }

        public Boolean NonEmpty
        {
            get
            {
                return State != SmsElementStateCode.Free;
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
            if (o.State != SmsElementStateCode.Full)
            {
                o.CurrentCapacity++;
            }
            if (o.CurrentCapacity == o.MaxCapacity)
            {
                o.State = SmsElementStateCode.Full;
            }
            else
            {
                o.State = CapacityToStateConverter(o.CurrentCapacity, o.MaxCapacity);
            }
        }

        private static void Remove(SmsContainerElement o)
        {
            if (o.State != SmsElementStateCode.Free)
            {
                o.CurrentCapacity--;
            }
            if (o.CurrentCapacity == 0)
            {
                o.State = SmsElementStateCode.Free;
            }
            else
            {
                o.State = CapacityToStateConverter(o.CurrentCapacity, o.MaxCapacity);
            }
        }

        private static SmsElementStateCode CapacityToStateConverter(Int32 currentCapacity, Int32 maxCapacity)
        {
            if (currentCapacity == maxCapacity)
            {
                //return possibleStates.Last(); // FULL
                return SmsElementStateCode.Full;
            }
            if (currentCapacity == 0)
            {
                //return possibleStates.First();
                return SmsElementStateCode.Free;
            }
            //return possibleStates[(int)currentCapacity];
            return SmsElementStateCode.NotFull;
        }

        public override SmsElementState GetState()
        {
            SmsElementState state = base.GetState();
            state.Code = CurrentCapacity;
            return state;
        }

    }
}
