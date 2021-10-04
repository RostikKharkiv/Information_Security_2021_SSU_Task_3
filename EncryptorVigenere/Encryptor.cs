using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EncryptorVigenere
{
    public static class Encryptor
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

        public static void EncryptorVigenere(string directoryName, string key)
        {
            CreateTable();

            StringBuilder sb = new StringBuilder();

            for (int i = 0, j = 0; i < directoryName.Length; i++, j++)
            {
                if (j == key.Length)
                    j = 0;
                sb.Append(key[j]);
            }

            string fullKey = sb.ToString();
            sb.Clear();

            Console.WriteLine(fullKey);

            for (int i = 0; i < directoryName.Length; i++)
            {
                sb.Append(table[symbols.IndexOf(fullKey[i]), symbols.IndexOf(directoryName[i])]);
            }

            using (StreamWriter sw = new StreamWriter(@"FileWithEncryptedDirectory.txt", false, encoding))
            {
                sw.Write(sb.ToString());
            }

            Directory.Delete(directoryName, true);

        }
    }
}
