using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAuto
{
    partial class Product
    {
        public string Color
        {
            get
            {
                return IsActive ? "White" : "Gray";

            }
        }
        public string IsActiven
        {
            get
            {
                return IsActive ? "Активен" : "Неактивен";
            }
        }
    }
}
