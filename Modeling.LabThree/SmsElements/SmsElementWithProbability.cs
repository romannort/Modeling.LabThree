using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modeling.LabThree.Generator;

namespace Modeling.LabThree.SmsElements
{
    public abstract class SmsElementWithProbability : SmsElementBase
    {
        
        protected Double Probability { get; set; }

        protected SmsElementWithProbability(Double probability)
        {
            Probability = probability;
        }

        public virtual Boolean IsDone
        {
            get
            {
                return CalculateIsDone();
            }
        }

        /// <summary>
        /// Marks sms element as free.
        /// </summary>
        public virtual void SetFree()
        {
            State = SmsElementStateCode.Free;
        }

        /// <summary>
        /// Mark sms element as blocked.
        /// </summary>
        public virtual void SetBlocked()
        {
            State = SmsElementStateCode.Blocked;
        }

        
        public Boolean IsBlocked
        {
            get
            {
                return SmsElementStateCode.Blocked == State;
            }
        }

        
        protected Boolean CalculateIsDone()
        {
            Boolean result = false;
            if (RandomProbability())
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Emulate element work with given probability.
        /// </summary>
        /// <returns>Boolean result if sms elements works on current state or not.</returns>
        private Boolean RandomProbability()
        {
            Double next = RandomGenerator.Next();
            return next > Probability;
        }

    }
}
