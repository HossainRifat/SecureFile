using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace SecureFile
{
     public class Crypto
    {
        //  Call this function to remove the key from memory after use for security
        [DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern bool ZeroMemory(IntPtr Destination, int Length);

        /// <summary>
        /// Creates a random salt that will be used to encrypt your file. This method is required on FileEncrypt.
        /// </summary>
        /// <returns></returns>
        public static byte[] GenerateRandomSalt()
        {
            byte[] data = new byte[32];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    // Fille the buffer with the generated data
                    rng.GetBytes(data);
                }
            }

            return data;
        }

        /// <summary>
        /// Encrypts a file from its path and a plain password.
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="password"></param>
        public static bool FileEncrypt(string inputFile, string outputFile, string password)
        {
            //http://stackoverflow.com/questions/27645527/aes-encryption-on-large-files

            process.processInstance.PrecessStatus = "Initializing the file.";
            process.processInstance.p2.Width = 30;

            //generate random salt
            byte[] salt = GenerateRandomSalt();
            string fname = Path.GetFileName(inputFile);

            //create output file name
            FileStream fsCrypt = new FileStream(outputFile+"//"+fname + ".SFile", FileMode.Create);

            //convert password string to byte arrray
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            process.processInstance.PrecessStatus = "output file created.";
            process.processInstance.p2.Width = 60;
            
            //Set Rijndael symmetric encryption algorithm
            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Padding = PaddingMode.PKCS7;

            process.processInstance.PrecessStatus = "Creating encryptor.";
            process.processInstance.p2.Width = 90;

            //http://stackoverflow.com/questions/2659214/why-do-i-need-to-use-the-rfc2898derivebytes-class-in-net-instead-of-directly
            //"What it does is repeatedly hash the user password along with the salt." High iteration counts.
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);

            //Cipher modes: http://security.stackexchange.com/questions/52665/which-is-the-best-cipher-mode-and-padding-mode-for-aes-encryption
            AES.Mode = CipherMode.CFB;

            // write salt to the begining of the output file, so in this case can be random every time
            fsCrypt.Write(salt, 0, salt.Length);

            process.processInstance.PrecessStatus = "Getting input file ready.";
            process.processInstance.p2.Width = 120;

            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);

            FileStream fsIn = new FileStream(inputFile, FileMode.Open);

            //create a buffer (1mb) so only this amount will allocate in the memory and not the whole file
            byte[] buffer = new byte[1048576];
            int read;

            process.processInstance.PrecessStatus = "Encrypting file.";
            process.processInstance.p2.Width = 180;

            try
            {
                while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                {
                    Application.DoEvents(); // -> for responsive GUI, using Task will be better!
                    cs.Write(buffer, 0, read);
                }
                process.processInstance.PrecessStatus = "Encryption complete.";
                process.processInstance.p2.Width = 421;
                // Close up
                fsIn.Close();

                return true;
            }
            
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

                return false;
            }
            finally
            {
                cs.Close();
                fsCrypt.Close();          
            }

        }

        /// <summary>
        /// Decrypts an encrypted file with the FileEncrypt method through its path and the plain password.
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="outputFile"></param>
        /// <param name="password"></param>
        public static bool FileDecrypt(string inputFile, string outputFile, string password)
        {
            process.processInstance.PrecessStatus = "Initializing the file.";
            process.processInstance.p2.Width = 30;

            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] salt = new byte[32];

            process.processInstance.PrecessStatus = "Encoding started.";
            process.processInstance.p2.Width = 60;

            FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);
            fsCrypt.Read(salt, 0, salt.Length);

            process.processInstance.PrecessStatus = "Getting input file.";
            process.processInstance.p2.Width = 90;

            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.CFB;

            process.processInstance.PrecessStatus = "Creating decryptor file.";
            process.processInstance.p2.Width = 120;

            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read);

            FileStream fsOut = new FileStream(outputFile, FileMode.Create);

            process.processInstance.PrecessStatus = "Decrypting file.";
            process.processInstance.p2.Width = 180;

            int read;
            byte[] buffer = new byte[1048576];

            try
            {
                while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    Application.DoEvents();
                    fsOut.Write(buffer, 0, read);
                }
                
            }
            catch (CryptographicException ex_CryptographicException)
            {
                Console.WriteLine("CryptographicException error: " + ex_CryptographicException.Message);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                
            }

            process.processInstance.PrecessStatus = "Decryption complete.";
            process.processInstance.p2.Width = 380;

            try
            {
                cs.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error by closing CryptoStream: " + ex.Message);
                return false;
            }
            finally
            {
                fsOut.Close();
                fsCrypt.Close();
            }
        }
    }
}
