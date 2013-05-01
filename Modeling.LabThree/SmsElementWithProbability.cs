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
        protected Double Probability { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="probability"></param>
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


        public void SetFree()
        {
            State = SmsElementState.Free;
        }


        public void SetBlocked()
        {
            State = SmsElementState.Blocked;
        }

        public Boolean IsBlocked
        {
            get
            {
                return SmsElementState.Blocked == State;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected Boolean CalculateIsDone()
        {
            Boolean result = false;
            if (RandomProbability())
            {
                result = true;
            }
            return result;
        }

        private Boolean RandomProbability()
        {
            Double next = RandomGenerator.Next();
            return next > Probability;
        }

    }
}
