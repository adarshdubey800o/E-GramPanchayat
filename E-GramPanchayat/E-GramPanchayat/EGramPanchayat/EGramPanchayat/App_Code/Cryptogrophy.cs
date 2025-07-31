using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EGramPanchayat.App_Code
{
    class Cryptography
    {
        internal string EncryptMyData(string PlainText)
        {
            byte[] EncryptedBytes = UTF8Encoding.UTF8.GetBytes(PlainText);
            string CipherText = Convert.ToBase64String(EncryptedBytes);
            return CipherText;
        }
        internal string DecryptMyData(string CipherText)
        {
            UTF8Encoding encode = new UTF8Encoding();
            Decoder decode = encode.GetDecoder();
            byte[] EncryptBytes = Convert.FromBase64String(CipherText);
            char[] DecryptedChars = new char[EncryptBytes.Length];
            decode.GetChars(EncryptBytes, 0, EncryptBytes.Length, DecryptedChars, 0);
            string PlainText = new string(DecryptedChars);
            return PlainText;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string PlainText, CipherText;
            Console.Write("Enter plain Text:");
            PlainText = Console.ReadLine();
            Cryptography cg = new Cryptography();
            CipherText = cg.EncryptMyData(PlainText);
            Console.WriteLine("Encrypted Text :" + CipherText);
            PlainText = cg.DecryptMyData(CipherText);
            Console.WriteLine("Plain Text: " + PlainText);
            Console.ReadKey();
        }

    }
}