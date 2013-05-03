using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree.SmsElements
{
    public class SmsEmitterElement: SmsElementWithProbability
    {

        public override bool IsDone
        {
            get
            {
                Boolean result = State == SmsElementStateCode.Free;
                if (result)
                {
                    return base.IsDone;
                }
                return result;
            }
        }

        public SmsEmitterElement(Double probability): base(probability)
        {

        }

        public override SmsElementState GetState()
        {
            SmsElementState state = base.GetState();
            state.Code = IsBlocked ? 1 : 0;
            return state;
        }
    }
}
