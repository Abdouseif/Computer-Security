using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;


namespace SecuritySender
{
    class Reciever
    {
        public static bool CheckMAC(string cipherText, int Tlen)
        {
            string MAC = cipherText.Substring(cipherText.Length - Tlen -2, Tlen);
            if (MAC == CMAC(cipherText.Substring(0, cipherText.Length - Tlen-2), Tlen))
                return true;
            else return false;
        }
        //----------------------------------------------------------------------------------------------------------------//
        public static string getCipher()
        {

            return System.IO.File.ReadAllText(@"C:\Users\Abdo\source\repos\SecurityProject\Cipher.txt");
        }
        //----------------------------------------------------------------------------------------------------------------//
        public static string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }
        //----------------------------------------------------------------------------------------------------------------//

        public static string XOR(byte[] str1, string str2)
        {
            string XORed = "", XORedBin = "", str1Bin = "", str2Bin = "";
            foreach (char c in str1)
                str1Bin += (Convert.ToString(c, 2).PadLeft(8, '0'));
            foreach (char c in str2)
                str2Bin += (Convert.ToString(c, 2).PadLeft(8, '0'));
            for (int i = 0; i < str1Bin.Length; i++)
            {
                if (str1Bin[i] == str2Bin[i])
                    XORedBin += "0";
                else XORedBin += "1";
            }

            return BinaryToString(XORedBin);
        }
        //----------------------------------------------------------------------------------------------------------------//
        public static string XORString(string str1, string str2)
        {
            string XORed = "", XORedBin = "", str1Bin = "", str2Bin = "";
            foreach (char c in str1)
                str1Bin += (Convert.ToString(c, 2).PadLeft(8, '0'));
            foreach (char c in str2)
                str2Bin += (Convert.ToString(c, 2).PadLeft(8, '0'));

            for (int i = 0; i < str1Bin.Length; i++)
            {
                if (str1Bin[i] == str2Bin[i])
                    XORedBin += "0";
                else XORedBin += "1";
            }
            return BinaryToString(XORedBin);
        }
        //----------------------------------------------------------------------------------------------------------------//
        private static string IVdes
        {
            get { return @"L%n67}G\Mk@k%:~Y"; }
        }
        //----------------------------------------------------------------------------------------------------------------//
        private static byte[] IVaes
        {
            get { return Encoding.ASCII.GetBytes("1234567890123456"); }
        }
        //----------------------------------------------------------------------------------------------------------------//
        private static byte[] AESkey
        {
            get { return Encoding.ASCII.GetBytes("12345678123456781234567812345678"); }
        }
        //----------------------------------------------------------------------------------------------------------------//
        private static string CMACkey
        {
            get { return "abcdefghijklmnop1234567812345678"; }
        }
        //----------------------------------------------------------------------------------------------------------------//
        private static int CMACBlock
        {
            get { return 8; }
        }
        //----------------------------------------------------------------------------------------------------------------//
        private static string deskey
        {
            get { return @"P@+#wG+Z"; }
        }
        //----------------------------------------------------------------------------------------------------------------//


        #region Methods
        // Print encrypted string    
        //System.Text.Encoding.UTF8.GetString(encrypted

        //----------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------//

        static byte[] AesEncrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create a new AesManaged.    
            using (AesManaged aes = new AesManaged())
            {
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                // Create MemoryStream    
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data    
            //return System.Text.Encoding.UTF8.GetString(encrypted);
            return encrypted;
        }

        //----------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------//

        static string AesDecrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            // Create AesManaged    
            using (AesManaged aes = new AesManaged())
            {
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                // Create the streams used for decryption.    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }

        //----------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------//

        public static string DesDecrypt(string encryptStr, string Key)  //Key format : @"Key_val" ... Key= 8 characters string= 64 bit
        {
            byte[] bKey = Encoding.UTF8.GetBytes(Key);
            byte[] bIV = Encoding.UTF8.GetBytes(IVdes);
            encryptStr = encryptStr.Replace(" ", "+");
            byte[] byteArray = Convert.FromBase64String(encryptStr);

            string decrypt = null;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, des.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(byteArray, 0, byteArray.Length);
                        cStream.FlushFinalBlock();
                        decrypt = Encoding.UTF8.GetString(mStream.ToArray());
                    }
                }
            }
            catch { }
            des.Clear();

            return decrypt;
        }

        //----------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------//

        public static string DesEncrypt(string plainStr, string Key)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(Key);
            byte[] bIV = Encoding.UTF8.GetBytes(IVdes);
            byte[] byteArray = Encoding.UTF8.GetBytes(plainStr);

            string encrypt = null;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(byteArray, 0, byteArray.Length);
                        cStream.FlushFinalBlock();
                        encrypt = Convert.ToBase64String(mStream.ToArray());
                    }
                }
            }
            catch { }
            des.Clear();

            return encrypt;
        }

        #endregion Methods

        //----------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------//

        public static string CMAC(string Message, int Tlen)
        {

            int msgSize = Message.Length;


            if (msgSize < CMACBlock)
            {
                Message += "1";
                while (Message.Length < CMACBlock)
                    Message += "0";
                return DesDecrypt(XORString(Message, CMACkey.Substring(0, CMACBlock)), deskey).Substring(0, Tlen);
            }
            string prevBlock = DesEncrypt(Message.Substring(0, CMACBlock), deskey);

            int k = CMACBlock;
            msgSize -= CMACBlock;
            while (msgSize >= CMACBlock)
            {
                prevBlock = DesEncrypt(XORString(prevBlock.Substring(0, CMACBlock), Message.Substring(k, CMACBlock)), deskey);
                k += CMACBlock;
                msgSize -= CMACBlock;
            }
            if (msgSize > 0)
            {
                string lastBlock = Message.Substring(k, msgSize);
                lastBlock += "1";
                while (lastBlock.Length < CMACBlock)
                {
                    lastBlock += "0";
                }

                return DesEncrypt(XORString(CMACkey.Substring(0, CMACBlock), XORString(lastBlock.Substring(0, CMACBlock), lastBlock)), deskey).Substring(0, Tlen);
            }
            else
            {
                return DesEncrypt(XORString(CMACkey.Substring(0, CMACBlock), prevBlock.Substring(0, CMACBlock)), deskey).Substring(0, Tlen);
            }
        }

        //----------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------//

        public static string ECB(string cipherText, int blockSize)
        {
            blockSize = CT_char_cout(blockSize);

            int cipherTextSize = cipherText.Length;
            string plainText = "";

            int k = 0;
            while (cipherTextSize >= blockSize)
            {
                cipherTextSize -= blockSize;
                string block = cipherText.Substring(k, blockSize);
                k += blockSize;
                string recvBlock = DesDecrypt(block, deskey);
                plainText += recvBlock;

            }
            return plainText;

        }

        //----------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------//

        public static string CBC(string cipherText, int blockSize, string initVector)
        {
            int actualBlockSize = blockSize;
            blockSize = CT_char_cout(blockSize);
            int cipherTextSize = cipherText.Length;

            string plainText = "";




            cipherTextSize -= blockSize;

            string firstBlock = cipherText.Substring(0, blockSize);
            int k = blockSize;

            string Decrypted = DesDecrypt(firstBlock, deskey);

            //XORING part
            string XORedFirst = XORString(Decrypted, initVector.Substring(0, actualBlockSize));

            string prevBlock = firstBlock;


            plainText += XORedFirst;



            while (cipherTextSize >= blockSize)
            {
                cipherTextSize -= blockSize;
                string block = cipherText.Substring(k, blockSize);
                k += blockSize;
                plainText += XORString(DesDecrypt(block, deskey), prevBlock.Substring(0, actualBlockSize));
                prevBlock = block;
            }
            return plainText;


        }

        //----------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------//


        public static string CFB(string cipherText, int blockSize, string IV, int shamt)
        {
            int actualBlockSize = blockSize;
            blockSize = CT_char_cout(blockSize);

            string plainText = "";

            while (shamt > blockSize)
                shamt -= blockSize;



            int k = 0;

            string shiftReg = DesEncrypt(IV, deskey).Substring(0, actualBlockSize);

            int cipherTextSize = cipherText.Length;

            string prevCipher = cipherText.Substring(0, shamt);

            string PTblock = XORString(prevCipher, shiftReg.Substring(0, shamt));

            plainText += PTblock;

            cipherTextSize -= shamt;

            k += shamt;

            while (cipherTextSize >= shamt)
            {
                shiftReg = shiftReg.Substring(shamt, actualBlockSize - shamt) + prevCipher.Substring(0, shamt);

                string encryptedReg = DesEncrypt(shiftReg, deskey).Substring(0, actualBlockSize);

                prevCipher = cipherText.Substring(k, shamt);

                PTblock = XORString(prevCipher, encryptedReg.Substring(0, shamt));

                plainText += PTblock;

                cipherTextSize -= shamt;

                k += shamt;

            }
            return plainText;

        }

        //----------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------//

        public static string OFB(string cipherText, int blockSize, string nonce)
        {

            int cipherTextSize = cipherText.Length;

            int k = 0;

            string plainText = "";

            string firstBlock = "";



            firstBlock += cipherText.Substring(0, blockSize);

            string nextBlock = DesEncrypt(nonce, deskey);

            string XORedFirst = XORString(nextBlock.Substring(0, blockSize), firstBlock);

            plainText += XORedFirst;

            k += blockSize;

            cipherTextSize -= blockSize;
            while (cipherTextSize >= blockSize)
            {
                cipherTextSize -= blockSize;
                string sendBlock = XORString(cipherText.Substring(k, blockSize), nextBlock.Substring(0, blockSize));
                k += blockSize;
                plainText += sendBlock;
                nextBlock = DesEncrypt(nextBlock, deskey);

            }
            return plainText;

        }

        //----------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------//

        public static string CTR(string cipherText, int blockSize, int startValue, int increment)
        {

            string Counter = startValue.ToString();

            while (Counter.Length < blockSize)
            {
                Counter = "0" + Counter;
            }

            int plainTextSize = cipherText.Length;
            string plainText = "";


            int k = 0;



            while (plainTextSize >= blockSize)
            {
                plainTextSize -= blockSize;
                string block = cipherText.Substring(k, blockSize);
                k += blockSize;
                string sendBlock = XORString(DesEncrypt(Counter, deskey).Substring(0, blockSize), block);
                Counter = (int.Parse(Counter) + increment).ToString();
                while (Counter.Length < blockSize)
                { Counter = "0" + Counter; }
                plainText += sendBlock;
            }
            return plainText;

        }
        //----------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------//

        public static int CT_char_cout(int blockSize)
        {


            if (blockSize >= 0 && blockSize <= 7)
                return 12;
            else if (blockSize >= 8 && blockSize <= 15)
                return 24;
            else if (blockSize >= 16 && blockSize <= 23)
                return 32;
            else if (blockSize >= 24 && blockSize <= 31)
                return 44;
            else if (blockSize >= 32 && blockSize <= 39)
                return 56;
            else return -1;
        }

        //----------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------//

        public static string block_modes_en(string plainText, int mode, int blockSize, string initVector, int shamt, int CTRstart, int CTRincr, string nonce)
        {
            string plaintext = "";
            switch (mode)
            {
                case 1:
                    plaintext = ECB(plainText, blockSize);
                    break;
                case 2:
                    plaintext = CBC(plainText, blockSize, initVector);
                    break;
                case 3:
                    plaintext = CFB(plainText, blockSize, initVector, shamt);
                    break;
                case 4:
                    plaintext = OFB(plainText, blockSize, nonce);
                    break;
                case 5:
                    plaintext = CTR(plainText, blockSize, CTRstart, CTRincr);
                    break;
            }
            return plaintext;
        }

    }
}
