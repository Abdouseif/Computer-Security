using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;

namespace SecuritySender
{
    static class Program
    {
        public static void SendCipher(string ciphertext)
        {

            System.IO.File.WriteAllText(@"C:\Users\Abdo\source\repos\SecurityProject\Cipher.txt", ciphertext+ "\r\n");
        }

        //----------------------------------------------------------------------------------------------------------------//
        public static void SendTimeECB(string ciphertext)
        {
            System.IO.File.AppendAllText(@"C:\Users\Abdo\source\repos\SecurityProject\ECB.txt", ciphertext+ "\r\n");
        }
        //----------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------//
        public static void SendTimeCBC(string ciphertext)
        {
            System.IO.File.AppendAllText(@"C:\Users\Abdo\source\repos\SecurityProject\CBC.txt", ciphertext+ "\r\n");
        }
        //----------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------//
        public static void SendTimeCFB(string ciphertext)
        {
            System.IO.File.AppendAllText(@"C:\Users\Abdo\source\repos\SecurityProject\CFB.txt", ciphertext+ "\r\n");
        }
        //----------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------//
        public static void SendTimeOFB(string ciphertext)
        {
            System.IO.File.AppendAllText(@"C:\Users\Abdo\source\repos\SecurityProject\OFB.txt", ciphertext+ "\r\n");
        }
        //----------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------------------//
        public static void SendTimeCTR(string ciphertext)
        {
            System.IO.File.AppendAllText(@"C:\Users\Abdo\source\repos\SecurityProject\CTR.txt", ciphertext+ "\r\n");
        }
        //----------------------------------------------------------------------------------------------------------------//

        public static string getCipher()
        {

            return System.IO.File.ReadAllText(@"C:\Users\Abdo\source\repos\SecurityProject\Cipher.txt");
        }

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


        #region Methods
        // Print encrypted string    
        //System.Text.Encoding.UTF8.GetString(encrypted)
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

        //----------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------------------//


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

        //---------------------------------------------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------------------------//

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

        //----------------------------------------------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------------------------//

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
        //--------------------------------------------------------------------------------------------------------------//
        //--------------------------------------------------------------------------------------------------------------//


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

        //-------------------------------------------------------------------------------------------------------------//
        //------------------------------------------------------------------------------------------------------------//


        public static long ECB(string plainText, int blockSize, int Tlen)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            long elapsedMs=0;

            int plainTextSize = plainText.Length;
            string cipherText = "";
            //   string cipherBlocks = "";
            int k = 0;
            while (plainTextSize >= blockSize)
            {
                plainTextSize -= blockSize;
                string block = plainText.Substring(k, blockSize);
                k += blockSize;
                string recvBlock = DesEncrypt(block, deskey);
                cipherText += recvBlock;
            }


            if (plainTextSize > 0)
            {
                string lastBlock = "";
                lastBlock += plainText.Substring(k, plainTextSize);
                while (lastBlock.Length < blockSize)               //Padding with zeroes
                    lastBlock += "0";
                string recvBlock = DesEncrypt(lastBlock, deskey);
                cipherText += recvBlock;

            }

            string messageMac = CMAC(cipherText, Tlen);
            string sendMessage = cipherText + messageMac;
            //Send mail code here
            SendCipher(sendMessage);
           
            watch.Stop();
            
            elapsedMs = watch.ElapsedMilliseconds;

            return elapsedMs;
        }

        //--------------------------------------------------------------------------------------------------------------------//
        //--------------------------------------------------------------------------------------------------------------------//


        public static long CBC(string plainText, int blockSize, string initVector, int Tlen)
        {
            int plainTextSize = plainText.Length;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            long elapsedMs = 0;



            string firstBlock = "";
            string messageMac;
            string sendMessage;
            if (plainTextSize <= blockSize)
            {
                firstBlock += plainText;
                while (firstBlock.Length < blockSize)  //Padding with zeroes
                    firstBlock += "0";
                string sentBlock = DesEncrypt(XORString(firstBlock, initVector), deskey);

                messageMac = CMAC(sentBlock, Tlen);

                sendMessage = sentBlock + messageMac;

                //Send mail code here...
                SendCipher(sendMessage);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                return elapsedMs;
                
            }

            string cipherText = "";

            plainTextSize -= blockSize;

            firstBlock += plainText.Substring(0, blockSize);
            int k = blockSize;

            //XORING part
            string XORedFirst = XORString(firstBlock, initVector.Substring(0, blockSize));

            string prevBlock = DesEncrypt(XORedFirst, deskey);


            cipherText += prevBlock;



            while (plainTextSize >= blockSize)
            {
                plainTextSize -= blockSize;
                string block = plainText.Substring(k, blockSize);
                k += blockSize;
                prevBlock = DesEncrypt(XORString(block, prevBlock.Substring(0, blockSize)), deskey);
                cipherText += prevBlock;
            }


            if (plainTextSize > 0)
            {
                string lastBlock = plainText.Substring(k, plainTextSize);
                while (lastBlock.Length < blockSize)               //Padding with zeroes
                    lastBlock += "0";
                string finalSendBlock = DesEncrypt(XORString(lastBlock, prevBlock.Substring(0, blockSize)), deskey);
                cipherText += finalSendBlock;
            }

            messageMac = CMAC(cipherText, Tlen);
            sendMessage = cipherText + messageMac;
            //Send mail code here
            SendCipher(sendMessage);
        
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            return elapsedMs;
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        //-----------------------------------------------------------------------------------------------------------------------//

        public static long CFB(string plainText, int blockSize, string IV, int shamt, int Tlen)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            long elapsedMs = 0;

            while (shamt > blockSize)
                shamt -= blockSize;

            if (plainText.Length < shamt)
            {
                while (plainText.Length < shamt)
                    plainText += "0";
            }

            string cipherText = "";

            int k = 0;

            string shiftReg = DesEncrypt(IV, deskey).Substring(0, blockSize);

            int plainTextSize = plainText.Length;

            string prevCipher = XORString(plainText.Substring(0, shamt), shiftReg.Substring(0, shamt));

            cipherText += prevCipher;

            plainTextSize -= shamt;

            k += shamt;

            while (plainTextSize >= shamt)
            {
                shiftReg = shiftReg.Substring(shamt, blockSize - shamt) + prevCipher.Substring(0, shamt);

                string encryptedReg = DesEncrypt(shiftReg, deskey).Substring(0, blockSize);

                prevCipher = XORString(plainText.Substring(k, shamt), encryptedReg.Substring(0, shamt));

                cipherText += prevCipher;

                plainTextSize -= shamt;

                k += shamt;

            }

            if (plainTextSize > 0)
            {
                string finalBlock = plainText.Substring(k, plainTextSize);
                while (finalBlock.Length < shamt)
                    finalBlock += "0";
                shiftReg = shiftReg.Substring(shamt, blockSize - shamt) + prevCipher.Substring(0, shamt);
                string encryptedReg = DesEncrypt(shiftReg, deskey).Substring(0, blockSize);
                string finalCipher = XORString(finalBlock, encryptedReg.Substring(0, shamt));

                cipherText += finalCipher;

            }
            string messageMac = CMAC(cipherText, Tlen);
            string sendMessage = cipherText + messageMac;
            //Send mail code here
            SendCipher(sendMessage);
          
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            return elapsedMs;

        }

        //-----------------------------------------------------------------------------------------------------------//
        //-----------------------------------------------------------------------------------------------------------//


        public static long OFB(string plainText, int blockSize, string nonce, int Tlen)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            long elapsedMs = 0;

            string cipherText = "";

            int plainTextSize = plainText.Length;

            int k = 0;

            string firstBlock = "";

            string messageMac;
            string sendMessage;

            if (plainTextSize <= blockSize)
            {
                firstBlock += plainText.Substring(k, plainTextSize);
                while (firstBlock.Length < blockSize)  //Padding with zeroes
                    firstBlock += "0";
                string sentBlock = XORString(nonce.Substring(0, blockSize), firstBlock);

                messageMac = CMAC(sentBlock, Tlen);
                sendMessage = sentBlock + messageMac;
                //Send mail code here
                SendCipher(sendMessage);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                return elapsedMs;
                

            }


            //XORING part

            firstBlock += plainText.Substring(0, blockSize);

            string nextBlock = DesEncrypt(nonce, deskey);

            string XORedFirst = XORString(nextBlock.Substring(0, blockSize), firstBlock);

            cipherText += XORedFirst;

            k += blockSize;

            plainTextSize -= blockSize;
            while (plainTextSize >= blockSize)
            {
                plainTextSize -= blockSize;
                string sendBlock = XORString(plainText.Substring(k, blockSize), nextBlock.Substring(0, blockSize));
                k += blockSize;
                cipherText += sendBlock;
                nextBlock = DesEncrypt(nextBlock, deskey);

            }


            if (plainTextSize > 0)
            {
                string lastBlock = plainText.Substring(k, plainTextSize);
                while (lastBlock.Length < blockSize)                            //Padding with zeroes
                    lastBlock += "0";
                string finalSendBlock = XORString(lastBlock, nextBlock.Substring(0, blockSize));
                cipherText += finalSendBlock;
            }

            messageMac = CMAC(cipherText, Tlen);
            sendMessage = cipherText + messageMac;
            //Send mail code here
            SendCipher(sendMessage);
            
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            return elapsedMs;
        }

        //-------------------------------------------------------------------------------------------------------------------//
        //--------------------------------------------------------------------------------------------------------------------//

        public static long CTR(string plainText, int blockSize, int startValue, int increment, int Tlen)
        {
            long elapsedMs = 0;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string Counter = startValue.ToString();

            while (Counter.Length < blockSize)
            {
                Counter = "0" + Counter;
            }

            int plainTextSize = plainText.Length;
            string ciphertext = "";

            string firstBlock = "";

            string messageMac = "";
            string sendMessage = "";
            if (plainTextSize <= blockSize)
            {
                firstBlock += plainText.Substring(0, plainTextSize);
                while (firstBlock.Length < blockSize)  //Padding with zeroes
                    firstBlock += "0";
                string firstSent = XORString(DesEncrypt(Counter, deskey).Substring(0, blockSize), firstBlock);
                messageMac = CMAC(firstSent, Tlen);
                sendMessage = firstSent + messageMac;
                SendCipher(sendMessage);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                return elapsedMs;
            }



            int k = 0;
            while (plainTextSize >= blockSize)
            {
                plainTextSize -= blockSize;
                string block = plainText.Substring(k, blockSize);
                k += blockSize;
                string sendBlock = XORString(DesEncrypt(Counter, deskey).Substring(0, blockSize), block);
                Counter = (int.Parse(Counter) + increment).ToString();
                while(Counter.Length<blockSize)
                { Counter = "0" + Counter; }
                ciphertext += sendBlock;
            }


            if (plainTextSize > 0)
            {
                string lastBlock = plainText.Substring(k, plainTextSize);
                while (lastBlock.Length < blockSize)               //Padding with zeroes
                    lastBlock += "0";
                string sendBlock = XORString(DesEncrypt(Counter, deskey).Substring(0, blockSize), lastBlock);
                ciphertext += sendBlock;
            }
            messageMac = CMAC(ciphertext, Tlen);
            sendMessage = ciphertext + messageMac;
            SendCipher(sendMessage);
           
           
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            return elapsedMs;
        }
        //------------------------------------------------------------------------------------------------------------------//
        //-----------------------------------------------------------------------------------------------------------------//

      

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
            else if (blockSize >= 40 && blockSize <= 47)
                return 64;
            else if (blockSize >= 48 && blockSize <= 55)
                return 76;
            else if (blockSize >= 56 && blockSize <= 63)
                return 88;
            else if (blockSize >= 64 && blockSize <= 71)
                return 96;
            else if (blockSize >= 72 && blockSize <= 79)
                return 108;
            else if (blockSize >= 80 && blockSize <= 87)
                return 120;
            else if (blockSize > 88 && blockSize <= 95)
                return 128;
            else if (blockSize >= 96 && blockSize <= 103)
                return 140;
            else if (blockSize >= 104 && blockSize <= 111)
                return 152;
            else if (blockSize >= 112 && blockSize <= 119)
                return 160;
            else if (blockSize >= 120 && blockSize <= 127)
                return 172;
            else if (blockSize >= 128 && blockSize <= 135)
                return 184;
            else if (blockSize >= 136 && blockSize <= 143)
                return 192;
            else if (blockSize >= 144 && blockSize <= 151)
                return 204;

            //Max size of block = 151
            else return -1;
        }
        //------------------------------------------------------------------------------------------------------------------//
        //------------------------------------------------------------------------------------------------------------------//

        public static long block_modes_en(string plainText, int mode, int blockSize, string initVector, int shamt, int CTBlockSize, int CTRstart, int CTRincr, string nonce, int Tlen)
        {
            long elapsedMs = 0;
           
            switch (mode)
            {
                case 1:
                    elapsedMs= ECB(plainText, blockSize, Tlen);
                    break;
                case 2:
                    elapsedMs= CBC(plainText, blockSize, initVector, Tlen);
                    break;
                case 3:
                    elapsedMs= CFB(plainText, blockSize, initVector, shamt, Tlen);
                    break;
                case 4:
                    elapsedMs= OFB(plainText, blockSize, nonce, Tlen);
                    break;
                case 5:
                    elapsedMs=CTR(plainText, blockSize, CTRstart, CTRincr, Tlen);
                    break;

            }
            return elapsedMs*1000;


        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
