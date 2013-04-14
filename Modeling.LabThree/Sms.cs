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

        public StatisticResults Emulate()
        {

            StatisticResults result = new StatisticResults();            
            GeneratorBase.DesiredSize = TotalCount;
            SmsChannelElement channelOne = new SmsChannelElement(this.P1);
            SmsChannelElement channelTwo = new SmsChannelElement(this.P2);
            SmsEmitterElement emitter = new SmsEmitterElement(this.R);
            SmsElementContainer container = new SmsElementContainer(ContainerCapacity);

            UInt32 totalTaktsCount = 0;
            while( emitter.NoRequests() == false )
            {

                result.Add(container, emitter, channelOne, channelTwo);

                if (channelTwo.State == SmsElementState.Busy &&
                    channelTwo.IsDone())
                {
                    channelTwo.State = SmsElementState.Free;
                }
                // Container non empty
                if (container.State != SmsElementState.Free &&
                    channelTwo.State == SmsElementState.Free)
                {
                    --container;
                    channelTwo.TakeRequest();
                }
                // request in channel One
                if (channelOne.State == SmsElementState.Busy)
                {
                    // request processed
                    if (channelOne.IsDone())
                    {
                        // Check channel Two is it free.
                        if (channelTwo.State == SmsElementState.Free)
                        {
                            channelTwo.TakeRequest();
                            channelOne.State = SmsElementState.Free;
                        }
                        else
                        {
                            if (container.State != SmsElementState.Full)
                            {
                                container++;
                                channelOne.State = SmsElementState.Free;
                            }
                            else
                            {
                                channelOne.State = SmsElementState.Blocked;
                            }
                        }
                    }
                }
                if (channelOne.State == SmsElementState.Blocked)
                {
                    if (channelTwo.State == SmsElementState.Free)
                    {
                        channelTwo.TakeRequest();
                        channelOne.State = SmsElementState.Free;
                    }
                    else
                    {
                        if (container.State != SmsElementState.Full)
                        {
                            ++container;
                            channelOne.State = SmsElementState.Free;
                        }
                    }
                }
                // 
                if (emitter.IsDone())
                {
                    if (channelOne.State == SmsElementState.Free)
                    {
                        channelOne.TakeRequest();
                        emitter.State = SmsElementState.Free;
                    }
                    else
                    {
                        emitter.State = SmsElementState.Blocked;
                    }
                }
                else
                {
                    emitter.State = SmsElementState.Free;
                }

                channelOne.UpdateTime();
                channelTwo.UpdateTime();
                emitter.UpdateTime();
                ++totalTaktsCount;
            }


            SmsState.TotalTaktsCount = totalTaktsCount;
            return result;
        }

    }
}
