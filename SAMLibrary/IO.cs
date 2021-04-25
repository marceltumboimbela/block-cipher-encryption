using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMLibrary
{
    public interface IO
    {
        /// <summary>
        /// Fungsi untuk mengembalikan Block berikutnya dari stream data
        /// </summary>
        /// <returns></returns>
        Block getNextBlock();

        /// <summary>
        /// Fungsi untuk mengembalikan byte berikutnya dari stream data
        /// </summary>
        /// <returns></returns>
        byte? getNextByte();

        /// <summary>
        /// Fungsi untuk menuliskan block ke stream output yang dipilih
        /// </summary>
        void writeBlock(Block outBlock);

        /// <summary>
        /// Fungsi untuk menuliskan byte ke stream output yang dipilih
        /// </summary>
        /// <param name="b"></param>
        void writeByte(byte b);

    }
}
