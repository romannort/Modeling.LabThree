using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree.SmsElements
{
    public class SmsChannelElement: SmsElementWithProbability, ICapacityElement
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

        public override bool IsDone
        {
            get
            {
                Boolean result = State == SmsElementStateCode.Busy;
                if (result)
                {
                    return base.IsDone;
                }
                return result;
            }
        }

        public Boolean IsFree
        {
            get
            {
                return State == SmsElementStateCode.Free;
            }
        }

        public SmsChannelElement(Double probability)
            : base(probability)
        {
            // It can be changed after c-tor call
            MaxCapacity = 1;
            CurrentCapacity = 0;
        }

        public override SmsElementState GetState()
        {
            SmsElementState state = base.GetState();
            state.Code = IsBlocked ? MaxCapacity + 1 : CurrentCapacity;
            return state;
        }

        public override void TakeRequest()
        {
            base.TakeRequest();
            CurrentCapacity++;
        }

        public override void SetFree()
        {
            base.SetFree();
            CurrentCapacity--;
        }
    }
}
