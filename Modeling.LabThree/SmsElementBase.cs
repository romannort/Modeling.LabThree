using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class SmsElementBase: IStateElement
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual SmsElementState State
        {
            get;
            set;
        }

        public SmsElementBase()
        {
            State = SmsElementState.Free;
        }

        /// <summary>
        /// Transfers request to the next element.
        /// </summary>
        /// <param name="target">Target element to transfer.</param>
        /// <returns>True if transfer succeed.</returns>
        //public abstract Boolean TransferRequest(SmsElementBase target);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private Boolean IsAvailable(SmsElementState state)
        {
            return state != SmsElementState.Blocked &&
                state != SmsElementState.Busy &&
                state != SmsElementState.Full;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void TakeRequest()
        {
            State = SmsElementState.Busy;
        }
    }


}
