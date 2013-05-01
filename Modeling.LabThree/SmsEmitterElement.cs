using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    public class SmsEmitterElement: SmsElementWithProbability
    {

        public override bool IsDone
        {
            get
            {
                Boolean result = State == SmsElementState.Free;
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
    }
}
