using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    public class SmsChannelElement: SmsElementWithProbability
    {

        public override bool IsDone
        {
            get
            {
                Boolean result = State == SmsElementState.Busy;
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
                return State == SmsElementState.Free;
            }
        }

        public SmsChannelElement(Double probability)
            : base(probability)
        {
        }
    }
}
