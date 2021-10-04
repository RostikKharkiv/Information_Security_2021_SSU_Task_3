using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptorVigenere
{
    public static class Decryptor
    {
        private static Encoding encoding = Encoding.GetEncoding("windows-1251");
        private static string symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
            "abcdefghijklmnopqrstuvwxyz" +
            "0123456789 :\\" +
            "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ" +
            "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        //private static string symbols = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private static char[,] table = new char[symbols.Length, symbols.Length];

        private static void CreateTable()
        {
            for (int i = 0; i < symbols.Length; i++)
            {
                for (int j = 0; j < symbols.Length; j++)
                {
                    table[i, j] = symbols[(j + i) % symbols.Length];
                }
            }
        }

        public static void DecryptorVigenere(string encryptedFile, string key)
        {
            CreateTable();

            StringBuilder sb = new StringBuilder();

            string encryptedText;

            using (StreamReader sr = new StreamReader(encryptedFile, encoding))
            {
                encryptedText = sr.ReadToEnd();
            }

            for (int i = 0, j = 0; i < encryptedText.Length; i++, j++)
            {
                if (j == key.Length)
                    j = 0;
                sb.Append(key[j]);
            }

            string fullKey = sb.ToString();
            Console.WriteLine(fullKey);
            sb.Clear();

            for (int i = 0; i < encryptedText.Length; i++)
            {
                for (int j = 0; j < symbols.Length; j++)
                {
                    if (table[symbols.IndexOf(fullKey[i]), j].Equals(encryptedText[i]))
                        sb.Append(table[0,j]);
                }
            }

            Console.WriteLine(sb.ToString());

            Directory.CreateDirectory(sb.ToString());

        }
    }
}
