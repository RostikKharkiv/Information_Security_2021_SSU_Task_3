using System;
using System.Linq;
using System.Text;

namespace EncryptorVigenere
{
    public class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string directoryName = @"C:\TEST"; // директория
            string key = "князь"; // ключевое слово

            Encryptor.EncryptorVigenere(directoryName, key);
            Decryptor.DecryptorVigenere("FileWithEncryptedDirectory.txt", key);
        }
    }
}
