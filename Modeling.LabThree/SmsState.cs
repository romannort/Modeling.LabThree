using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modeling.LabThree.SmsElements;

namespace Modeling.LabThree
{
    /// <summary>
    /// Encapsulate data about state of sms.
    /// </summary>
    public class SmsState
    {

        /// <summary>
        /// Number of takts sms stayed in this state.
        /// </summary>
        private UInt32 taktsCount;

        /// <summary>
        /// State key with states of sms elements.
        /// </summary>
        public SmsStateKey Key { get; private set; }

        /// <summary>
        /// Display code.
        /// </summary>
        /// <example>0010</example>
        public String Code { get; private set; }

        /// <summary>
        /// State probability
        /// </summary>
        public Double Probability
        {
            get
            {
                return (Double)taktsCount / Sms.TotalTaktsCount;
            }
        }


        public SmsState(params SmsElementState[] elements)
        {
            this.Key = new SmsStateKey(elements);
            this.Code = CodeBuilder();
        }

        public SmsState(SmsStateKey key)
        {
            this.Key = key;
            this.Code = CodeBuilder();
        }


        public static SmsState operator++(SmsState o)
        {
            o.taktsCount++;
            return o;
        }

        /// <summary>
        /// Builds display code for current state.
        /// </summary>
        /// <returns>Display code string.</returns>
        private String CodeBuilder()
        {
            StringBuilder builder = new StringBuilder();
            foreach (SmsElementState state in Key.ElementsStates)
            {
                builder.Append(state.Code);
            }
            return builder.ToString();
        }


        public override bool Equals(object obj)
        {
            if (obj != null )
            {
                SmsState objAsState = obj as SmsState;
                return objAsState.Code.Equals(Code);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

    }
}
