﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    public class SmsEmitterElement: SmsServiceElement
    {

        public SmsEmitterElement(Double probability): base(probability)
        {
        }

        private void EmitRequest()
        {
            currentTimeInterval = timeIntervals.ElementAt(nextTimeIntervalIndex);
            nextTimeIntervalIndex++;

        }

        public override bool IsDone()
        {
            Boolean result = base.IsDone();
            
            if (result)
            {
                EmitRequest();
            }
            return result;
        }

        public Boolean NoRequests()
        {
            return nextTimeIntervalIndex == timeIntervals.Count;
        }
    }
}