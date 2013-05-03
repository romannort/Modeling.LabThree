using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree.SmsElements
{
    public interface ICapacityElement
    {
        Int32 MaxCapacity { get; set; }

        Int32 CurrentCapacity { get; set; }
    }
}
