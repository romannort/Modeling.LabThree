using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    public abstract class SmsElementBase: IStateElement
    {

        public SmsElementState State
        {
            get;
            set;
        }

        public SmsElementBase()
        {
            State = SmsElementState.Free;
        }

        /// <summary>
        /// Transfers request to the next element/
        /// </summary>
        /// <param name="target">Target element to transfer.</param>
        /// <returns>True if transfer succeed.</returns>
        public virtual Boolean TransferRequest(SmsElementBase target)
        {
            Boolean result = false;
            if (target.State != SmsElementState.Blocked &&
                target.State != SmsElementState.Busy &&
                target.State != SmsElementState.Full)
            {
                target.TakeRequest();
                result = true;
            }
            return result;
        }


        protected virtual void TakeRequest()
        {
            this.State = SmsElementState.Busy;
        }
    }


}
