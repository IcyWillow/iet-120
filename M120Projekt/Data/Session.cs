using M120Projekt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace M120Projekt.Data
{
    public static class Session
    {
        public static User User { get; private set; }

        public static void Start(User user)
        {
            User = user;
        }

        public static bool IsAuthenticated()
        {
            return User != null;
        }

        public static void End()
        {
            User = null;
        }
    }
}
