using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree.SmsElements
{
    /// <summary>
    /// Base class for all sms elements.
    /// </summary>
    public abstract class SmsElementBase: IStateElement
    {
        private static Int32 lastId = 0;

        public Int32 Id
        {
            get;
            private set;
        }

        /// <summary>
        /// Element state.
        /// </summary>
        public virtual SmsElementStateCode State
        {
            get;
            set;
        }

        public SmsElementBase()
        {
            State = SmsElementStateCode.Free;
            Id = ++lastId;
        }

        /// <summary>
        /// Updates element state after request taken.
        /// </summary>
        public virtual void TakeRequest()
        {
            State = SmsElementStateCode.Busy;
        }

        /// <summary>
        /// Gets information about sms elements.
        /// </summary>
        /// <returns></returns>
        public virtual SmsElementState GetState()
        {
            SmsElementState state = new SmsElementState()
            {
                CurrentState = State,
                ElementId = Id
            };
            return state;
        }

    }


}
