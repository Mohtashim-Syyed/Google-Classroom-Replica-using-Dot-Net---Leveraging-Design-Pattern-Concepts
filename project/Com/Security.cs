using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classroom.Com
{
    class Security
    {
        public static string Encryption(string p)
        {
            char ch1;
            string enc = "";
            char[] ch2 = p.ToCharArray();
            for (int i = 0; i < ch2.Length; i++)
            {
                ch1 = (char)(ch2[i] + 3);
                enc += ch1.ToString();

            }
            return enc;
        }
        public static string Decryption(string p)
        {

            char ch1;
            string dec = "";
            char[] ch2 = p.ToCharArray();
            for (int i = 0; i < ch2.Length; i++)
            {
                ch1 = (char)(ch2[i] - 3);
                dec += ch1.ToString();

            }
            return dec;

        }
    }
}
