﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Classes.Screen
{
    internal interface Screen
    {
        string type { get; }
        double coefficient { get; }
    }
}