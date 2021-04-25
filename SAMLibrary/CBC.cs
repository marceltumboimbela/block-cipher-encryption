using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMLibrary
{
    public class CBC
    {
        /// <summary>
        /// Interface IO untuk menerima atau meng-output-kan hasil enkripsi atau dekripsi
        /// </summary>
        private IO io;
        private Block key;
        private Block IV;

        public CBC(IO io, Block key)
        {
            this.io = io;
            this.key = key;
            this.IV = GenerateIV(key);
            //Console.WriteLine(IV);
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
            Block P;
            Block C;

            P = io.getNextBlock();
            if (P != null)
            {
                C = Kripsi.Encrypt(Kripsi.XOR(P, IV), key);
                //Console.WriteLine("P = " + P);
                //Console.WriteLine("K = " + key);
                //Console.WriteLine("C = " + C);
                io.writeBlock(C);
                while ((P = io.getNextBlock()) != null)
                {
                    C = Kripsi.Encrypt(Kripsi.XOR(P, C), key);
                    io.writeBlock(C);
                    //Console.WriteLine("C = " + C);
                }
            }
        }

        public void StartDecrypt()
        {
            Block P;
            Block C;
            Block CBef;   // Block C sebelumnya

            C = io.getNextBlock();
            if (C != null)
            {
                P = Kripsi.XOR(IV, Kripsi.Decrypt(C, key));
                CBef = C;
                io.writeBlock(P);
                //Console.WriteLine("C = " + C);
                //Console.WriteLine("K = " + key);
                //Console.WriteLine("P = " + P);
                while ((C = io.getNextBlock()) != null)
                {
                    P = Kripsi.XOR(CBef, Kripsi.Decrypt(C, key));
                    CBef = C;
                    io.writeBlock(P);
                    //Console.WriteLine("P = " + P);
                }
            }
        }
    }
}
