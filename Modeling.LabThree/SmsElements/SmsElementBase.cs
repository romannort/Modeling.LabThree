using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree.SmsElements
{
    /// <summary>
    /// 
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
        /// 
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
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private Boolean IsAvailable(SmsElementStateCode state)
        {
            return state != SmsElementStateCode.Blocked &&
                state != SmsElementStateCode.Busy &&
                state != SmsElementStateCode.Full;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void TakeRequest()
        {
            State = SmsElementStateCode.Busy;
        }

        /// <summary>
        /// 
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
