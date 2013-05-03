using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modeling.LabThree.SmsElements;

namespace Modeling.LabThree
{
    /// <summary>
    /// Collects statistics during sms run.
    /// </summary>
    public class StatisticResults
    {

        private UInt32 emitterBlockedTakts = 0;

        private ICollection<Int32> containerContentLength = new List<Int32>();

        private ISet<SmsState> SmsStates = new HashSet<SmsState>();

        private SmsState previousState;
        
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
                return (Double)emitterBlockedTakts / Sms.TotalTaktsCount;
            }
        }

        /// <summary>
        /// Records data about current state.
        /// </summary>
        /// <param name="elements">Array of sms elements.</param>
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
            RecordTargetStatistics(currentState);
            previousState = currentState;
        }

        /// <summary>
        /// Record statistics for specific variant. Now it's for #22's.
        /// </summary>
        /// <param name="currentState">Current state.</param>
        private void RecordTargetStatistics(SmsState currentState)
        {
            // hardcode:
            // container index = 0
            // emitter index = 1
            containerContentLength.Add(currentState.Key.ElementsStates[0].Code);
            if (currentState.Key.ElementsStates[1].CurrentState == SmsElementStateCode.Blocked)
            {
                ++emitterBlockedTakts;
            }
        }        

        /// <summary>
        /// Builds StateKey object from collection of sms elements.
        /// </summary>
        /// <param name="elements">Sms elements collection.</param>
        /// <returns>Appropriate state key.</returns>
        private IList<SmsElementStateCode> BuildStateKey(ICollection<IStateElement> elements)
        {
            IList<SmsElementStateCode> key = new List<SmsElementStateCode>();
            foreach (IStateElement se in elements)
            {
                key.Add(se.State);
            }
            return key;
        }

        /// <summary>
        /// Returns probabilities for each state.
        /// </summary>
        /// <returns>Dictionary with state codes and probabilities.</returns>
        public IDictionary<String, Double> StateProbabilities()
        {
            IDictionary<String, Double> result = new Dictionary<String, Double>();
            foreach( SmsState state in SmsStates)
            {
                result.Add(state.Code, state.Probability);
            }
            return result;
        }

        #region Transitions

        /// <summary>
        /// Possible transitions.
        /// </summary>
        private ISet<Transition> possibleTransitions = new HashSet<Transition>();

        
        /// <summary>
        /// Writes transition rable to file.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
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
        #endregion
        
    }
}


// j t0 t1 t2
// j - container
// t0 - emitter
// t1 - 1st channel
// t2 - 2nd channel