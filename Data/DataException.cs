using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataException : Exception
    {
        public DataException(string message, string pathError) : base(message)
        {
            PathError = pathError;
        }
        public string PathError { get; }
    }
}
