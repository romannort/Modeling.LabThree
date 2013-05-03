using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    /// <summary>
    /// Describe transition from one SmsState to another
    /// </summary>
    public class Transition: IEquatable<Transition>
    {

        public SmsState From { get; set; }

        public SmsState To { get; set; }


        #region IEquatable Implementation
        public bool Equals(Transition other)
        {
            return other.From.Code == this.From.Code &&
                other.To.Code == this.To.Code;
        }

        public override int GetHashCode()
        {
            int hashCode = (this.From.Code.GetHashCode() * 397) ^ this.To.Code.GetHashCode();
            return hashCode;
        }
        #endregion
    }
}
