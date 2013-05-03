using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modeling.LabThree.SmsElements;

namespace Modeling.LabThree
{
    /// <summary>
    /// Key that describes sms state by states of it elements.
    /// </summary>
    public class SmsStateKey: IEquatable<SmsStateKey>
    {
        /// <summary>
        /// States of sms elements.
        /// </summary>
        public IList<SmsElementState> ElementsStates { get; private set; }


        public SmsStateKey(ICollection<SmsElementState> elements)
        {
            ElementsStates = new List<SmsElementState>();
            foreach (SmsElementState e in elements)
            {
                ElementsStates.Add(e);
            }
        }


        #region IEquatable Implementation

        public bool Equals(SmsStateKey other)
        {
            if (other == null)
            {
                return false;
            }
            SmsStateKey otherKey = (SmsStateKey)other;
            if (otherKey == null || this.ElementsStates.Count != otherKey.ElementsStates.Count)
            {
                return false;
            }
            for (Int32 i = 0; i < other.ElementsStates.Count; ++i)
            {
                if (!other.ElementsStates[i].Equals(this.ElementsStates[i]))
                {
                    return false;
                }
            }
            return true;
        }


        public override int GetHashCode()
        {
            int accum = 17;

            foreach (var item in ElementsStates)
            {
                accum = accum * 397 + item.GetHashCode();
            }

            return accum;
        }

        #endregion

    }
}
