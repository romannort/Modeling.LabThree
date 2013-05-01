using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    public class Transition: IEquatable<Transition>
    {
        public SmsState From { get; set; }

        public SmsState To { get; set; }

        public bool Equals(Transition other)
        {
            return other.From.Code == this.From.Code &&
                other.To.Code == this.To.Code;
        }

        public override int GetHashCode()
        {
            return (this.From.Code.GetHashCode() / 37) * (this.To.Code.GetHashCode() / 59);
        }
    }
}
