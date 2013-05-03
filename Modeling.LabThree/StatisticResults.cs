using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modeling.LabThree.SmsElements;

namespace Modeling.LabThree
{
    public class StatisticResults
    {
        private UInt32 emitterBlockedTakts = 0;

        private ICollection<Int32> containerContentLength = new List<Int32>();

        private ISet<SmsState> SmsStates = new HashSet<SmsState>()
        {
            //new SmsState("0000", SmsElementStateCode.Free, SmsElementStateCode.Free, SmsElementStateCode.Free, SmsElementStateCode.Free),
            //new SmsState("0010", SmsElementStateCode.Free, SmsElementStateCode.Free, SmsElementStateCode.Busy, SmsElementStateCode.Free),
            //new SmsState("0001", SmsElementStateCode.Free, SmsElementStateCode.Free, SmsElementStateCode.Free, SmsElementStateCode.Busy),
            //new SmsState("0011", SmsElementStateCode.Free, SmsElementStateCode.Free, SmsElementStateCode.Busy, SmsElementStateCode.Busy),
            //new SmsState("0110", SmsElementStateCode.Free, SmsElementStateCode.Blocked, SmsElementStateCode.Busy, SmsElementStateCode.Free),
            //new SmsState("1001", SmsElementStateCode.Full, SmsElementStateCode.Free, SmsElementStateCode.Free, SmsElementStateCode.Busy),
            //new SmsState("1021", SmsElementStateCode.Full, SmsElementStateCode.Free, SmsElementStateCode.Blocked, SmsElementStateCode.Busy),
            //new SmsState("0111", SmsElementStateCode.Free, SmsElementStateCode.Blocked, SmsElementStateCode.Busy, SmsElementStateCode.Busy),
            //new SmsState("1011", SmsElementStateCode.Full, SmsElementStateCode.Free, SmsElementStateCode.Busy, SmsElementStateCode.Busy),
            //new SmsState("1111", SmsElementStateCode.Full, SmsElementStateCode.Blocked, SmsElementStateCode.Busy, SmsElementStateCode.Busy),
            //new SmsState("1121", SmsElementStateCode.Full, SmsElementStateCode.Blocked, SmsElementStateCode.Blocked, SmsElementStateCode.Busy)
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
            SmsStateKey key = new SmsStateKey( elements.Select(e => e.GetState()).ToList() );
            SmsState currentState = SmsStates.FirstOrDefault(state => state.Key.Equals(key));
            if (currentState == null)
            {
                SmsState newState = new SmsState(key);
                currentState = newState;
                SmsStates.Add(newState);
            }
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
            // container index = 2
            containerContentLength.Add(currentState.Key.ElementsStates[2].Code);
            if (currentState.Key.ElementsStates[0].CurrentState == SmsElementStateCode.Blocked)
            {
                ++emitterBlockedTakts;
            }
        }        

        private IList<SmsElementStateCode> BuildStateKey(ICollection<IStateElement> elements)
        {
            IList<SmsElementStateCode> key = new List<SmsElementStateCode>();
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