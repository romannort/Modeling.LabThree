using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree.SmsElements
{
    public class SmsElementState
    {
        public Int32 ElementId { get; set; }

        public Int32 Code { get; set; }

        public SmsElementStateCode CurrentState { get; set; }

        public SmsElementState()
        {
            Code = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            SmsElementState objAsType = obj as SmsElementState;
            if (objAsType != null)
            {
                return objAsType.ElementId.Equals(this.ElementId) &&
                    objAsType.CurrentState.Equals(this.CurrentState) &&
                    objAsType.Code.Equals(this.Code);
            }
            return false;
        }

        public override int GetHashCode()
        {
            Int32 accumulator = 0;

            accumulator = this.Code.GetHashCode();
            accumulator = accumulator * 37 + this.ElementId.GetHashCode();
            accumulator = accumulator * 37 + this.CurrentState.GetHashCode();

            return accumulator;
        }
    }
}
