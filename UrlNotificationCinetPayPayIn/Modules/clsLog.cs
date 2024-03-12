using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrlNotificationCinetPayPayIn.Modules
{
    public static class clsNombreMontant1
    {
        public static bool Between(this int num, int lower, int upper, bool inclusive = false)
        {

            if (num == lower || num == upper)
                return true;

            return inclusive
                ? lower <= num && num <= upper
                : lower < num && num < upper;
        }
    }
}