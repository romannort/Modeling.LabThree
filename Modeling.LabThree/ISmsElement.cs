using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    public interface ISmsElement
    {
        SmsElementState State { get; set; }
    }

    public enum SmsElementState
    {
        Free,
        Blocked,
        Busy,
        // For Container elements
        Empty,
        Full,
        NotFull
    }
}
