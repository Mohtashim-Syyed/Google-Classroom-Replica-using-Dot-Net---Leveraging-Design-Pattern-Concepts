using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI.WinForms;

namespace Classroom.Com
{
    class Code
    {
        public static List<char> source = new List<char>();
        public static void Upper()
        {
            for (char i = 'A'; i <= 'Z'; i++)
            {
                source.Add(i);
            }
        }
        public static void Lower()
        {
            for (char i = 'a'; i <= 'z'; i++)
            {
                source.Add(i);
            }
        }
        public static void num()
        {
            for (char i = '0'; i <= '9'; i++)
            {
                source.Add(i);
            }
        }
        public static string genrate(GunaTextBox l)
        {
            Upper();
            Lower();
            num();
            //source.Add('@');
            //source.Add('#');
            //source.Add('$');
            //source.Add('%');
            //source.Add('^');
            //source.Add('*');
            //source.Add('+');

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();
            int j = 0;
            while (j < 6)
            {
                sb.Append(source[rnd.Next(0, source.Count)]);
                j++;
            }

            return l.Text = sb.ToString();
        }

    }
}
