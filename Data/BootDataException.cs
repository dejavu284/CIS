﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BootDataException : Exception
    {
        public BootDataException(string message, string pathError) : base(message)
        {
            PathError = pathError;
        }
        public string PathError { get; }
    }
}
