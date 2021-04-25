using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMLibrary
{
    public class ECB
    {
        /// <summary>
        /// Interface IO untuk menerima atau meng-output-kan hasil enkripsi atau dekripsi
        /// </summary>
        private IO io;
        private Block key;

        public ECB(IO io, Block key)
        {
            this.io = io;
            this.key = key;
        }

        public void StartEncrypt()
        {
            Block b;

            while ((b = io.getNextBlock()) != null)
            {
                io.writeBlock(Kripsi.Encrypt(b, key));
            }
        }

        public void StartDecrypt()
        {
            Block b;

            while ((b = io.getNextBlock()) != null)
            {
                io.writeBlock(Kripsi.Decrypt(b, key));
            }
        }
    }
}
