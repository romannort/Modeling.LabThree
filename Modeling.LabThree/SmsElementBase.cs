using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    public class SmsElementBase: ISmsElement
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
