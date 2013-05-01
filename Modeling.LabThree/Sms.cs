using Modeling.LabThree.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    public class Sms
    {
        /// <summary>
        /// 
        /// </summary>
        public UInt32 TotalCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Double P1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Double P2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Double R { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UInt32 ContainerCapacity { get; set; }


        public StatisticResults Emulate(bool a)
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
            SmsState.TotalTaktsCount = tiks;
            return result;
        }
    }
}
