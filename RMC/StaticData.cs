﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC
{
    public class StaticData
    {
        public static Dictionary<string, int> DataItemType = new Dictionary<string, int>() { { "Medicine", 1 }, { "Products", 2 }, { "Equipment", 3 } };
    }
}