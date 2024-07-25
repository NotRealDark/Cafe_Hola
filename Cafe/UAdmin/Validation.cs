using Cafe.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Cafe.UAdmin
{
    internal class Validation
    {
        public string ValidateUsername(string username, List<User> userlist)
        {
            if (username.Length < 8 ||  username.Length > 16)
            {
                return "OutOfLength";
            }
            foreach (char c in username)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return "ContainSpecial";
                }
            }
            foreach (User u in userlist)
            {
                if (u.Username == username)
                {
                    return "Duplicate";
                }
            }
            return "Pass";
        }

        public string ValidatePassword(string password)
        {
            if (password.Length < 8 || password.Length > 16)
            {
                return "OutOfLength";
            }
            bool UpperCase = false, Digital = false, Special = false;
            foreach (char c in password)
            {
                if (char.IsUpper(c) && !UpperCase)
                {
                    UpperCase = true;
                }
                if (char.IsDigit(c) && !Digital)
                {
                    Digital = true;
                }
                if (!char.IsLetterOrDigit(c))
                {
                    Special = true;
                }
                if (UpperCase &&  Digital && Special)
                {
                    return "Pass";
                }
            }
            return "WrongForm";
        }
    }
}
