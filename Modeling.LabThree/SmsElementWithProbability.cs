using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modeling.LabThree.Generator;

namespace Modeling.LabThree
{
    public abstract class SmsElementWithProbability : SmsElementBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected ICollection<Int32> timeIntervals;

        /// <summary>
        /// 
        /// </summary>
        protected Int32 currentTimeInterval = 0;

        /// <summary>
        /// 
        /// </summary>
        protected Int32 nextTimeIntervalIndex = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="probability"></param>
        protected SmsElementWithProbability(Double probability)
        {
            timeIntervals = ExponentialDistribution.Generate(probability);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual Boolean IsDone()
        {
            Boolean result = false;
            if (currentTimeInterval == 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void UpdateTime()
        {
            if (State != SmsElementState.Blocked)
            {
                currentTimeInterval--;
            }
        }
    }
}
