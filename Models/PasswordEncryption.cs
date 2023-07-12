using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daily_Test_Management_project.Models
{
    public class PasswordEncryption
    {
      public static String Encode(string password)
        {
            try
            {
                byte[] EnDateByte = new byte[password.Length];
                EnDateByte = System.Text.Encoding.UTF8.GetBytes(password);
                string Encryptdata = Convert.ToBase64String(EnDateByte);
                return Encryptdata;
            }
            catch(Exception ex)
            {
                throw new Exception("Error in Encode" + ex.Message);
            }
        }

        public static string Decode(string EncryptData)
        {
            try
            {
                System.Text.UTF8Encoding encode = new System.Text.UTF8Encoding();
                System.Text.Decoder UTF8decode = encode.GetDecoder();
                byte[] decodeByte = Convert.FromBase64String(EncryptData);
                int charcount = UTF8decode.GetCharCount(decodeByte, 0, decodeByte.Length);
                char[] DecodeChar = new char[charcount];
                UTF8decode.GetChars(decodeByte, 0, decodeByte.Length, DecodeChar, 0);
                string DecryptedData = new string(DecodeChar);
                return DecryptedData;
            }
            catch(Exception ex)
            {

                throw new Exception("Erroi=r in Encode" + ex.Message);

            }
        }
    }
}