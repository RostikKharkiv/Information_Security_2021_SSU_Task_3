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
            "0123456789 —:\\\n\t\r\b;@#$%^*()-+{}<>.!?№" +
            "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ" +
            "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
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

        public static void EncryptorVigenere(string mainDirectory, string key)
        {
            CreateTable();

            StringBuilder encryptedKey = new StringBuilder();
            StringBuilder encryptedPart = new StringBuilder();
            StringBuilder encryptedText = new StringBuilder();
            string fileText;

            string[] directories = Directory.GetDirectories(mainDirectory, "*", SearchOption.AllDirectories);
            string[] files = Directory.GetFiles(mainDirectory, "*.*", SearchOption.AllDirectories);

            for (int i = 0, j = 0; i < mainDirectory.Length; i++, j++)
            {
                if (j == key.Length)
                    j = 0;
                encryptedKey.Append(key[j]);
                encryptedPart.Append(table[symbols.IndexOf(encryptedKey[i]), symbols.IndexOf(mainDirectory[i])]);
            }
            encryptedText.Append(encryptedPart.ToString());
            encryptedText.Append("_");
            encryptedPart.Clear();
            encryptedKey.Clear();

            foreach (var directory in directories)
            {
                for (int i = 0, j = 0; i < directory.Length; i++, j++)
                {
                    if (j == key.Length)
                        j = 0;
                    encryptedKey.Append(key[j]);
                    encryptedPart.Append(table[symbols.IndexOf(encryptedKey[i]), symbols.IndexOf(directory[i])]);
                }
                encryptedText.Append(encryptedPart.ToString());
                encryptedText.Append("_");
                encryptedPart.Clear();
                encryptedKey.Clear();
            }

            encryptedText.Append("[");

            foreach (var file in files)
            {
                for (int i = 0, j = 0; i < file.Length; i++, j++)
                {
                    if (j == key.Length)
                        j = 0;
                    encryptedKey.Append(key[j]);
                    encryptedPart.Append(table[symbols.IndexOf(encryptedKey[i]), symbols.IndexOf(file[i])]);
                }
                encryptedText.Append(encryptedPart.ToString());
                encryptedText.Append("|");
                encryptedPart.Clear();
                encryptedKey.Clear();

                using (StreamReader sr = new StreamReader(file, encoding))
                {
                    fileText = sr.ReadToEnd();
                }

                for (int i = 0, j = 0; i < fileText.Length; i++, j++)
                {
                    if (j == key.Length)
                        j = 0;
                    encryptedKey.Append(key[j]);
                   
                    string checkFile = file;
                    int check = symbols.IndexOf(encryptedKey[i]);
                    char check2 = fileText[i];
                    int check3 = symbols.IndexOf(fileText[i]);
                    encryptedPart.Append(table[symbols.IndexOf(encryptedKey[i]), symbols.IndexOf(fileText[i])]);
                }
                encryptedText.Append(encryptedPart.ToString());
                encryptedText.Append("]");
                encryptedPart.Clear();
                encryptedKey.Clear();
            }
            encryptedText.Append("_");


            using (StreamWriter sw = new StreamWriter(@"FileWithEncryptedDirectory.txt", false, encoding))
            {
                sw.Write(encryptedText.ToString());
            }

            Directory.Delete(mainDirectory, true);

        }
    }
}
