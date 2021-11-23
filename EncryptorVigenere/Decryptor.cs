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

        public static void DecryptorVigenere(string encryptedFile, string key)
        {
            CreateTable();

            List<string> directories = new List<string>();
            List<string> files = new List<string>();
            List<string> filesInfo = new List<string>();
            StringBuilder directoriesSB = new StringBuilder();
            StringBuilder encryptedDirectoriesSB = new StringBuilder();
            StringBuilder filesSB = new StringBuilder();
            StringBuilder encryptedFilesSB = new StringBuilder();
            StringBuilder filesInfoSB = new StringBuilder();
            StringBuilder encryptedFilesInfoSB = new StringBuilder();
            StringBuilder encryptedKey = new StringBuilder();
            string encryptedText;
            int n = 0;


            using (StreamReader sr = new StreamReader(encryptedFile, encoding))
            {
                encryptedText = sr.ReadToEnd();
            }

            while (!encryptedText[n].Equals('[') && n < encryptedText.Length)
            {
                while (!encryptedText[n].Equals('_') && n < encryptedText.Length)
                {
                    encryptedDirectoriesSB.Append(encryptedText[n]);
                    n++;
                }

                if (encryptedText[n].Equals('_') && n < encryptedText.Length) n++;

                for (int i = 0, j = 0; i < encryptedDirectoriesSB.Length; i++, j++)
                {
                    if (j == key.Length)
                        j = 0;
                    encryptedKey.Append(key[j]);
                }

                for (int i = 0; i < encryptedDirectoriesSB.Length; i++)
                {
                    for (int j = 0; j < symbols.Length; j++)
                    {
                        if (table[symbols.IndexOf(encryptedKey[i]), j].Equals(encryptedDirectoriesSB[i]))
                            directoriesSB.Append(table[0, j]);
                    }
                }

                directories.Add(directoriesSB.ToString());
                encryptedDirectoriesSB.Clear();
                encryptedKey.Clear();
                directoriesSB.Clear();
            }
            n++;

            while (!encryptedText[n].Equals('_') && n < encryptedText.Length)
            {
                while (!encryptedText[n].Equals('|') && n < encryptedText.Length)
                {
                    encryptedFilesSB.Append(encryptedText[n]);
                    n++;
                }

                if (encryptedText[n].Equals('|') && n < encryptedText.Length)
                {
                    n++;
                    while (!encryptedText[n].Equals(']') && n < encryptedText.Length)
                    {
                        encryptedFilesInfoSB.Append(encryptedText[n]);
                        n++;
                    }
                }

                if (encryptedText[n].Equals(']')) n++;

                for (int i = 0, j = 0; i < encryptedFilesSB.Length; i++, j++)
                {
                    if (j == key.Length)
                        j = 0;
                    encryptedKey.Append(key[j]);
                }

                for (int i = 0; i < encryptedFilesSB.Length; i++)
                {
                    for (int j = 0; j < symbols.Length; j++)
                    {
                        if (table[symbols.IndexOf(encryptedKey[i]), j].Equals(encryptedFilesSB[i]))
                            filesSB.Append(table[0, j]);
                    }
                }

                files.Add(filesSB.ToString());
                encryptedFilesSB.Clear();
                encryptedKey.Clear();
                filesSB.Clear();

                for (int i = 0, j = 0; i < encryptedFilesInfoSB.Length; i++, j++)
                {
                    if (j == key.Length)
                        j = 0;
                    encryptedKey.Append(key[j]);
                }

                for (int i = 0; i < encryptedFilesInfoSB.Length; i++)
                {
                    for (int j = 0; j < symbols.Length; j++)
                    {
                        if (table[symbols.IndexOf(encryptedKey[i]), j].Equals(encryptedFilesInfoSB[i]))
                            filesInfoSB.Append(table[0, j]);
                    }
                }

                encryptedFilesInfoSB.Clear();
                encryptedKey.Clear();
                filesInfo.Add(filesInfoSB.ToString());
                filesInfoSB.Clear();
            }

            foreach (var directory in directories)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(directory);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
            }

            int filesInfoCount = 0;

            foreach (var file in files)
            {
                using (StreamWriter sw = new StreamWriter(file, false, encoding))
                {
                    sw.Write(filesInfo[filesInfoCount]);
                }
                filesInfoCount++;
            }



            foreach (var item in directories)
            {
                Console.WriteLine(item);
            }

            foreach (var item in files)
            {
                Console.WriteLine(item);
            }

            //Directory.CreateDirectory(directories.ToString());

        }
    }
}
