using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Lab2
{
    
    public static class Encryptor
        // class for encrypting printable ASCII characters (32 - 127) with subtitution cyphers
        // the characters:
        // !"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~⌂
    {
        //declaring the interval that can be encrypted
        private static readonly int FirstChar = 32; //SP(space)
        private static readonly int LastChar = 127; //DEL(delete)
        //private static readonly int KeyLength = LastChar - FirstChar + 1;
        
        //creating a static variable for random used for generating random keys
        private static readonly Random rnd = new();

        public static void ShuffleArray<T>(T[] array)
            //function for shuffling an array in a random order using the Fisher-Yates Shuffle algorithm
        {
            for(int i = array.Length - 1; i > 0; i--)
            {
                int j = rnd.Next(0, i + 1);
                T temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }
        
        public static string GenerateRandomKey()
            //returns random key (string) that can be use to encode / decode substitution cyphers
        {
            int[] randomKey = new int[LastChar - FirstChar + 1];
            for (int i = 0; i < randomKey.Length; i++)
            {
                randomKey[i] = i + 32;
            }
            Encryptor.ShuffleArray<int>(randomKey);
            string key = "";
            foreach (int i in randomKey)
            {
                key += (char)i;
            }

            return key;
        }

        public static string Encrypt(string message, string key)
            //retunrs an encrypted message encyphered by substitution acording to the given key 
        {
            string encryptedMessage = "";
            foreach (char character in message)
            {
                if ((int)character < FirstChar ||  (int)character > LastChar)
                    //characters not in range of the Encryptor are not encrypted
                {
                    encryptedMessage += character;
                }
                else
                {
                    encryptedMessage += key[(int)character - FirstChar];
                }
            }
            return encryptedMessage;
        }

        public static string Decrypt(string encryptedMessage, string key)
            //retunrs a dencrypted message decyphered by substitution acording to the given key 
        {
            Dictionary<char, int> decryptionMap = new Dictionary<char, int>();
            int count = 0;
            foreach (char character in key)
            {
                decryptionMap[character] = count++;
            }

            string decryptedMessage = "";
            foreach(char character in encryptedMessage)
            {
                if (!decryptionMap.ContainsKey(character))
                {
                    // characters not in range of the Encryptor are not decrypted
                    decryptedMessage += character;
                }
                else
                {
                    decryptedMessage += (char)(decryptionMap[character] + FirstChar);
                }
            }
            return decryptedMessage;
        }

        public static void EncryptorTest()
        {
            while (true)
            {
                Console.Write("Enter message: ");
                string message = Console.ReadLine();
                string key = Encryptor.GenerateRandomKey();
                string encryptedMessage = Encryptor.Encrypt(message, key);
                Console.WriteLine(encryptedMessage);
                Console.WriteLine(Encryptor.Decrypt(encryptedMessage, key));
            }
        }
    }
}
