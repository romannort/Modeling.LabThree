using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    public enum SmsElementState
    {
        Blocked = -4,
        Busy,
        Full,
        NotFull,

        Free, // = 0
        One,
        Two,
        Three,
        Four,
        Five
    }
}
