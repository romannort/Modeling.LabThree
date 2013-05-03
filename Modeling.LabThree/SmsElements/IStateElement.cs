using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree.SmsElements
{
    internal interface IStateElement
    {
        SmsElementStateCode State { get; set; }

        SmsElementState GetState();
    }
}
