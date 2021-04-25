using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMLibrary
{
    public class CFB
    {
        /// <summary>
        /// Interface IO untuk menerima atau meng-output-kan hasil enkripsi atau dekripsi
        /// </summary>
        private IO io;
        private Block key;
        private Block IV;

        public CFB(IO io, Block key)
        {
            this.io = io;
            this.key = key;
            this.IV = GenerateIV(key);
        }

        /// <summary>
        /// Generate Initialization Vector dengan menggunakan random seed = checksum kunci + panjang kunci
        /// </summary>
        /// <param name="key">Block key</param>
        /// <returns>Initialization Vector</returns>
        private static Block GenerateIV(Block key)
        {
            Block result = new Block();
            int randomseed;
            int sum = 0;
            Random rnd;

            for (int i = 0; i < key.Count; i++)
            {
                sum += key[i];
            }
            randomseed = sum + key.Count;

            rnd = new Random(randomseed);
            // Generate IV sebesar block key
            for (int i = 0; i < key.Count; i++)
            {
                result.Add((byte)rnd.Next(byte.MaxValue));
            }

            return result;
        }

        public void StartEncrypt()
        {
            Block antrian;
            Block k;
            byte? Pstream;
            byte P;
            byte C;

            antrian = GenerateIV(key);
            while ((Pstream = io.getNextByte()) != null)
            {
                k = Kripsi.Encrypt(antrian, key);

                P = Pstream.Value;
                C = (byte)(P ^ k[0]);
                antrian.RemoveAt(0);
                antrian.Add(C);
                io.writeByte(C);
            }
        }

        public void StartDecrypt()
        {
            Block antrian;
            Block k;
            byte? Cstream;
            byte C;
            byte P;

            antrian = GenerateIV(key);
            while ((Cstream = io.getNextByte()) != null)
            {
                k = Kripsi.Encrypt(antrian, key);

                C = Cstream.Value;
                P = (byte)(C ^ k[0]);
                antrian.RemoveAt(0);
                antrian.Add(C);
                io.writeByte(P);
            }
        }
    }
}
