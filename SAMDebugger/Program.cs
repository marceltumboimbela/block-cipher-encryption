using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMLibrary;
using System.IO;

namespace SAMDebugger
{
    class Program
    {
        static void Main(string[] args)
        {
            Block b = new Block();
            Block key = new Block();
            for (int i = 1; i <= 10; i++)
            {
                b.Add((byte)(i + 0x2F));
            }
            for (int i = 0; i < 10; i++)
            {
                key.Add((byte)i);
            }
            FileIO fio = new FileIO(@"D:\input.txt", @"D:\output.txt", 10);
            new OFB(fio, key).StartEncrypt();
            fio.Close();
            fio = new FileIO(@"D:\output.txt", @"D:\decrypt_output.txt", 10);
            new OFB(fio, key).StartDecrypt();
            fio.Close();
            //Console.WriteLine("Plainteks  : " + b);
            //b = Kripsi.Encrypt(b, key);
            //Console.WriteLine("Cipherteks : " + b);
            //b = Kripsi.Decrypt(b, key);
            //Console.WriteLine("Plainteks  : " + b);
            Console.ReadKey();
        }
    }

    class FileIO : IO
    {
        BinaryReader fi;
        BinaryWriter fo;
        int blocksize;

        public FileIO(String inputFilename, String outputFilename, int blocksize)
        {
            fi = new BinaryReader(new FileStream(inputFilename, FileMode.Open));
            fo = new BinaryWriter(new FileStream(outputFilename, FileMode.Create));
            this.blocksize = blocksize;
        }

        #region IO Members

        public Block getNextBlock()
        {
            byte[] read = fi.ReadBytes(blocksize);
            if (read.Count() == 0)
            {
                return null;
            }
            else
            {
                Block b = new Block();
                b.AddRange(read);
                for (int i = read.Count(); i < blocksize; i++)
                {
                    b.Add((byte)0);
                }
                Console.WriteLine("Block read: " + b.Count);
                return b;
            }
        }

        public byte? getNextByte()
        {
            try
            {
                return fi.ReadByte();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void writeBlock(Block outBlock)
        {
            fo.Write(outBlock.ToArray());
            fo.Flush();
        }

        public void writeByte(byte b)
        {
            fo.Write(b);
            fo.Flush();
        }

        #endregion

        public void Close()
        {
            fi.Close();
            fo.Close();
        }
    }
}
 