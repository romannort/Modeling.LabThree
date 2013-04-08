﻿using System;
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
        public Decimal P1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal P2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal R { get; set; }

        public UInt32 ContainerCapacity { get; set; }

        public StatisticResults Emulate()
        {
            StatisticResults result = new StatisticResults();
            SmsState.TotalTaktsCount = TotalCount;
            SmsServiceElement channelOne = new SmsServiceElement() { Probability = this.P1 };
            SmsServiceElement channelTwo = new SmsServiceElement() { Probability = this.P2};
            SmsServiceElement emitter = new SmsServiceElement() { Probability = this.R };
            SmsElementContainer container = new SmsElementContainer(ContainerCapacity);
            
            for (UInt32 takt = 1; takt < this.TotalCount; ++takt)
            {
                //
                if (channelTwo.State == SmsElementState.Busy &&
                    channelTwo[takt])
                {
                    channelTwo.State = SmsElementState.Free;
                }
                // Container non empty
                if (container.State != SmsElementState.Empty &&
                    channelTwo.State == SmsElementState.Free)
                {
                    --container;
                    channelTwo.State = SmsElementState.Busy;
                }
                // request in channel One
                if (channelOne.State == SmsElementState.Busy)
                {
                    // request processed
                    if (channelOne[takt])
                    {
                        // Check channel Two is it free.
                        if (channelTwo.State == SmsElementState.Free)
                        {
                            channelTwo.State = SmsElementState.Busy;
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
                // 
                if (emitter[takt])
                {
                    if (channelOne.State == SmsElementState.Free)
                    {
                        channelOne.State = SmsElementState.Busy;
                    }
                    else
                    {
                        emitter.State = SmsElementState.Blocked;
                    }
                }
                else
                {
                    emitter.State = SmsElementState.Empty;
                }
                result.Add(container, emitter, channelOne, channelTwo);
            }

            return result;
        }

    }
}