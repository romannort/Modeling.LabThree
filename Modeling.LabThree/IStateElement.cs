﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    internal interface IStateElement
    {
        SmsElementState State { get; set; }
    }
}
