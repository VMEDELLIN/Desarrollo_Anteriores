﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugTest
{
    public class CDebug
    {
        public static bool IsDebug
        {
            get
            {
                bool debug = false;
#if DEBUG
                debug = true;
#endif
                return debug;
            }
        }
    }
}
