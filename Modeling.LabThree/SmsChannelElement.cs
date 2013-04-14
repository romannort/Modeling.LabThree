using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    public class SmsChannelElement: SmsServiceElement
    {

        public SmsChannelElement(Double probability)
            : base(probability)
        {
        }

        public void TakeRequest()
        {
            currentTimeInterval = timeIntervals.ElementAt(nextTimeIntervalIndex);
            nextTimeIntervalIndex++;
            State = SmsElementState.Busy;
        }
    }
}
