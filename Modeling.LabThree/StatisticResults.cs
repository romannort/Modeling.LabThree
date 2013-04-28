using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    public class StatisticResults
    {
        private UInt32 emitterBlockedTakts = 0;

        private ICollection<Int32> containerContentLength = new List<Int32>();

        private ISet<Transition> possibleTransitions = new HashSet<Transition>();

        private ISet<SmsState> SmsStates = new HashSet<SmsState>()
        {
            new SmsState("0000", SmsElementState.Free, SmsElementState.Free, SmsElementState.Free, SmsElementState.Free),
            //new SmsState("0100", SmsElementState.Free, SmsElementState.Blocked, SmsElementState.Free, SmsElementState.Free),
            new SmsState("0010", SmsElementState.Free, SmsElementState.Free, SmsElementState.Busy, SmsElementState.Free),
            new SmsState("0001", SmsElementState.Free, SmsElementState.Free, SmsElementState.Free, SmsElementState.Busy),
            //new SmsState("0101", SmsElementState.Free, SmsElementState.Blocked, SmsElementState.Free, SmsElementState.Busy),
            new SmsState("0011", SmsElementState.Free, SmsElementState.Free, SmsElementState.Busy, SmsElementState.Busy),
            new SmsState("0110", SmsElementState.Free, SmsElementState.Blocked, SmsElementState.Busy, SmsElementState.Free),
//            new SmsState("0120", SmsElementState.Free, SmsElementState.Blocked, SmsElementState.Blocked, SmsElementState.Free),
            new SmsState("1001", SmsElementState.Full, SmsElementState.Free, SmsElementState.Free, SmsElementState.Busy),
            //new SmsState("1101", SmsElementState.Full, SmsElementState.Blocked, SmsElementState.Free, SmsElementState.Busy),
            new SmsState("0111", SmsElementState.Free, SmsElementState.Blocked, SmsElementState.Busy, SmsElementState.Busy),
            new SmsState("1011", SmsElementState.Full, SmsElementState.Free, SmsElementState.Busy, SmsElementState.Busy),
            new SmsState("1111", SmsElementState.Full, SmsElementState.Blocked, SmsElementState.Busy, SmsElementState.Busy),
//            new SmsState("0021", SmsElementState.Free, SmsElementState.Free, SmsElementState.Blocked, SmsElementState.Busy),
            new SmsState("1021", SmsElementState.Full, SmsElementState.Free, SmsElementState.Blocked, SmsElementState.Busy),
//            new SmsState("0121", SmsElementState.Free, SmsElementState.Blocked, SmsElementState.Blocked, SmsElementState.Busy),
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


        private SmsState previousState;

        internal void Add(params IStateElement[] elements)
        {
            SmsStateKey key = new SmsStateKey(elements.Select(e => e.State).ToList());
            SmsState currentState = SmsStates.FirstOrDefault(state => state.Key.Equals(key)); 
            currentState++;
            AddTransition(currentState);
            previousState = currentState;
            RecordEmitterBlocks(currentState);
        }


        private void RecordEmitterBlocks(SmsState currentState)
        {
            containerContentLength.Add(Int32.Parse(currentState.Code[0].ToString()));
            if (currentState.Code[1] == '1')
            {
                ++emitterBlockedTakts;
            }
        }

        
        private void AddTransition(SmsState currentState)
        {
            possibleTransitions.Add(new Transition() 
                {  
                    From = previousState ?? currentState,
                    To = currentState
                }
            );
        }

        private IList<SmsElementState> BuildStateKey(ICollection<IStateElement> elements)
        {
            IList<SmsElementState> key = new List<SmsElementState>();
            foreach (IStateElement se in elements)
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


        public void GenerateTransitionsTable(String filePath)
        {
            IList<String> output = new List<String>();
            foreach (SmsState state in SmsStates)
            {
                IEnumerable<String> targetCodes = possibleTransitions
                    .Where(t => t.From.Code.Equals(state.Code)).Select(t => t.To.Code);
                String sourceLine = state.Code + " => " + String.Join(",", targetCodes.ToArray());
                output.Add(sourceLine);
            }
            File.WriteAllLines(filePath, output.AsEnumerable());
        }
    }
}


// j t0 t1 t2
// j - container
// t0 - emitter
// t1 - 1st channel
// t2 - 2nd channel