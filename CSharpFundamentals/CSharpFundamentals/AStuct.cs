using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentals
{
    public struct AStuct
    {
        public AStuct(string strValue, int intValue)
        {
            AStringValue = strValue;
            AnIntValue = intValue;
        }
        public string AStringValue;
        public int AnIntValue;
    }
}
