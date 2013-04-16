using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    public class StatisticResults
    {

        private UInt32 emitterBlockedTakts = 0;

        private ICollection<Int32> containerContentLength = new List<Int32>();

        private ISet<SmsState> SmsStates = new HashSet<SmsState>()
        {
            new SmsState("0000", SmsElementState.Free, SmsElementState.Free, SmsElementState.Free, SmsElementState.Free),
            new SmsState("0100", SmsElementState.Free, SmsElementState.Blocked, SmsElementState.Free, SmsElementState.Free),
            new SmsState("0010", SmsElementState.Free, SmsElementState.Free, SmsElementState.Busy, SmsElementState.Free),
            new SmsState("0001", SmsElementState.Free, SmsElementState.Free, SmsElementState.Free, SmsElementState.Busy),
            new SmsState("0101", SmsElementState.Free, SmsElementState.Blocked, SmsElementState.Free, SmsElementState.Busy),
            new SmsState("0011", SmsElementState.Free, SmsElementState.Free, SmsElementState.Busy, SmsElementState.Busy),
            new SmsState("0110", SmsElementState.Free, SmsElementState.Blocked, SmsElementState.Busy, SmsElementState.Free),
            new SmsState("1001", SmsElementState.Full, SmsElementState.Free, SmsElementState.Free, SmsElementState.Busy),
            new SmsState("1101", SmsElementState.Full, SmsElementState.Blocked, SmsElementState.Free, SmsElementState.Busy),
            new SmsState("0111", SmsElementState.Free, SmsElementState.Blocked, SmsElementState.Busy, SmsElementState.Busy),
            new SmsState("1011", SmsElementState.Full, SmsElementState.Free, SmsElementState.Busy, SmsElementState.Busy),
            new SmsState("1111", SmsElementState.Full, SmsElementState.Blocked, SmsElementState.Busy, SmsElementState.Busy),
            new SmsState("1021", SmsElementState.Full, SmsElementState.Free, SmsElementState.Blocked, SmsElementState.Busy),
            new SmsState("1121", SmsElementState.Full, SmsElementState.Blocked, SmsElementState.Blocked, SmsElementState.Busy)
        };


        public Double AverageContainerContent
        {
            get
            {
                return containerContentLength.Average();
            }
        }


        public Double EmitterBlockingProbability
        {
            get
            {
                return (Double)emitterBlockedTakts / SmsState.TotalTaktsCount;
            }
        }

        internal void Add(params ISmsElement[] elements)
        {
            SmsStateKey key = new SmsStateKey(elements.Select(e => e.State).ToList());
            SmsState currentState = SmsStates.FirstOrDefault(state => state.Key.Equals(key)); 
            currentState++;

            containerContentLength.Add(Int32.Parse(currentState.Code[0].ToString()));
            if (currentState.Code[1] == '1')
            {
                ++emitterBlockedTakts;
            }
        }

        private IList<SmsElementState> BuildStateKey(ICollection<ISmsElement> elements)
        {
            IList<SmsElementState> key = new List<SmsElementState>();
            foreach (ISmsElement se in elements)
            {
                key.Add(se.State);
            }
            return key;
        }

        public IDictionary<String, Double> StateProbabilities()
        {
            IDictionary<String, Double> result = new Dictionary<String, Double>();
            foreach( SmsState state in SmsStates)
            {
                result.Add(state.Code, state.Probability);
            }
            return result;
        }
    }
}


// j t0 t1 t2
// j - container
// t0 - emitter
// t1 - 1st channel
// t2 - 2nd channel

