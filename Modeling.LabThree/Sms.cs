using Modeling.LabThree.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modeling.LabThree.SmsElements;


namespace Modeling.LabThree
{
    public class Sms
    {
        public static UInt32 TotalTaktsCount;
        
        /// <summary>
        /// Total count of takts for emulating.
        /// </summary>
        public Int32 TotalCount { get; set; }

        /// <summary>
        /// Probablity for 1st pipe.
        /// </summary>
        public Double P1 { get; set; }

        /// <summary>
        /// Probability for 2nd pipe.
        /// </summary>
        public Double P2 { get; set; }

        /// <summary>
        /// Probability for emitter.
        /// </summary>
        public Double R { get; set; }

        /// <summary>
        /// Container capacity.
        /// </summary>
        public Int32 ContainerCapacity { get; set; }

        /// <summary>
        /// Starts emulating process.
        /// </summary>
        /// <returns>Statistics for current run.</returns>
        public StatisticResults Emulate()
        {
            StatisticResults result = new StatisticResults();
            SmsChannelElement channelOne = new SmsChannelElement(this.P1);
            SmsChannelElement channelTwo = new SmsChannelElement(this.P2);
            SmsEmitterElement emitter = new SmsEmitterElement(this.R);
            SmsContainerElement container = new SmsContainerElement(ContainerCapacity);
            
            UInt32 tiks = 0;
            while (tiks++ != TotalCount)
            {

                result.Add(container, emitter, channelOne, channelTwo);
                if (channelTwo.IsDone)
                {
                    channelTwo.SetFree();
                }

                if (container.NonEmpty && channelTwo.IsFree)
                {
                    --container;
                    channelTwo.TakeRequest();
                }

                if (channelOne.IsBlocked || channelOne.IsDone)
                {
                    if (channelTwo.IsFree)
                    {
                        channelOne.SetFree();
                        channelTwo.TakeRequest();
                    }
                    else
                    {
                        if (container.NotFull)
                        {
                            channelOne.SetFree();
                            ++container;
                        }
                        else
                        {
                            channelOne.SetBlocked();
                        }
                    }
                }
                
                if (emitter.IsBlocked || emitter.IsDone) 
                {
                    if (channelOne.IsFree)
                    {
                        channelOne.TakeRequest();
                        emitter.SetFree();
                    }
                    else
                    {
                        emitter.SetBlocked();
                    }
                }
                
            }
            Sms.TotalTaktsCount = tiks;
            return result;
        }
    }
}
