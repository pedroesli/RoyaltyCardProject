using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolaltyCardProject.Models
{
    public class VoucherCode
    {
        public string Pattern { get; set; }
        public char[] Charset { get; set; }

        public VoucherCode()
        {
            Pattern = "####-####-####";
            Charset = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        }

        public VoucherCode(string pattern, char[] charset)
        {
            Pattern = pattern;
            Charset = charset;
        }

        public string Generate()
        {
            Random random = new Random();
            StringBuilder pattern = new StringBuilder(Pattern);
            while (pattern.ToString().Contains('#'))
            {
                int randomValue = random.Next(0, Charset.Length);
                char randomChar = Charset[randomValue];
                if (pattern.ToString().Contains('#'))
                {
                    pattern[pattern.ToString().IndexOf('#')] = randomChar;
                }
            }
            return pattern.ToString();
        }
    }
}
