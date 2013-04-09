﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    public class SmsStateKey: IEquatable<SmsStateKey>
    {
        public IList<SmsElementState> ElementsStates { get; private set; }

        public SmsStateKey(ICollection<SmsElementState> elements)
        {
            ElementsStates = new List<SmsElementState>();
            foreach (SmsElementState e in elements)
            {
                ElementsStates.Add(e);
            }
        }


        public bool Equals(SmsStateKey other)
        {
            if (other == null)
            {
                return false;
            }
            SmsStateKey otherKey = (SmsStateKey)other;
            if (otherKey == null || this.ElementsStates.Count != otherKey.ElementsStates.Count)
            {
                return false;
            }
            for (Int32 i = 0; i < other.ElementsStates.Count; ++i)
            {
                if (other.ElementsStates[i] != this.ElementsStates[i])
                {
                    return false;
                }
            }
                return true;
        }
    }
}