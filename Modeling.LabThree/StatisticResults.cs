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

        private ISet<SmsState> SmsStates = new HashSet<SmsState>()
        {
            new SmsState("0000", SmsElementState.Free, SmsElementState.Free, SmsElementState.Free, SmsElementState.Free),
            new SmsState("0010", SmsElementState.Free, SmsElementState.Free, SmsElementState.Busy, SmsElementState.Free),
            new SmsState("0001", SmsElementState.Free, SmsElementState.Free, SmsElementState.Free, SmsElementState.Busy),
            new SmsState("0011", SmsElementState.Free, SmsElementState.Free, SmsElementState.Busy, SmsElementState.Busy),
            new SmsState("0110", SmsElementState.Free, SmsElementState.Blocked, SmsElementState.Busy, SmsElementState.Free),
            new SmsState("1001", SmsElementState.Full, SmsElementState.Free, SmsElementState.Free, SmsElementState.Busy),
            new SmsState("1021", SmsElementState.Full, SmsElementState.Free, SmsElementState.Blocked, SmsElementState.Busy),
            new SmsState("0111", SmsElementState.Free, SmsElementState.Blocked, SmsElementState.Busy, SmsElementState.Busy),
            new SmsState("1011", SmsElementState.Full, SmsElementState.Free, SmsElementState.Busy, SmsElementState.Busy),
            new SmsState("1111", SmsElementState.Full, SmsElementState.Blocked, SmsElementState.Busy, SmsElementState.Busy),
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
            //if (currentState == null)
            //{
            //    SmsState newState = new SmsState(key);
            //    currentState = newState;
            //    SmsStates.Add(newState);
            //}
            ++currentState;
            AddTransition(currentState);
            previousState = currentState;
            RecordEmitterBlocks(currentState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentState"></param>
        private void RecordEmitterBlocks(SmsState currentState)
        {
            int contContent = (Int32)currentState.Key.ElementsStates[0];
            containerContentLength.Add(contContent < 0 ? 1 : 0);
            if (currentState.Key.ElementsStates[1] == SmsElementState.Blocked)
            {
                ++emitterBlockedTakts;
            }
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

        private ISet<Transition> possibleTransitions = new HashSet<Transition>();

        public void GenerateTransitionsTable(String filePath)
        {
            IList<String> output = new List<String>();
            foreach (SmsState state in SmsStates)
            {
                IEnumerable<String> targetCodes = possibleTransitions
                    .Where(t => t.From.Code.Equals(state.Code)).Select(t => t.To.Code);
                String sourceLine = state.Code + " => " + targetCodes.First();
                output.Add(sourceLine);
                foreach (String transitionEnd in targetCodes.Skip(1))
                {
                    String line = @"        " + transitionEnd;
                    output.Add(line);
                }
                output.Add(@"-------------");
            }
            File.WriteAllLines(filePath, output.AsEnumerable());
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
    }
}


// j t0 t1 t2
// j - container
// t0 - emitter
// t1 - 1st channel
// t2 - 2nd channel