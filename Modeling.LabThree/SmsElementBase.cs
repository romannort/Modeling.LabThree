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
    }


}
