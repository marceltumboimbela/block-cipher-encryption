using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMLibrary
{
    public class Block : List<byte>
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Count; i++)
            {
                sb.Append(String.Format("{0:D3}", this[i]) + " ");
            }

            return sb.ToString();
        }
    }
}
