using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    public class SmsChannelElement: SmsElementWithProbability
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


        public void Block()
        {
            currentTimeInterval = timeIntervals.ElementAt(nextTimeIntervalIndex);
            nextTimeIntervalIndex++;
            State = SmsElementState.Blocked;
        }
    }
}
