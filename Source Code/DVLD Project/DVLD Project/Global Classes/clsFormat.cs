using System;

namespace DVLD.Classes
{
    public class clsFormat
    {
        public static string DateToShort(DateTime Dt1)
        {

            return Dt1.ToString("dd/MMM/yyyy");
        }


        public static string DateToShort2(DateTime Dt1)
        {

            return Dt1.ToString("dd/MM/yyyy");
        }

    }
}
