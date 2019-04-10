using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

namespace WpfApp1
{
    
    class SValidator : ValidationRule

    {

        public int MinSalary
        {
            get;
            set;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int val = 0;

            if (int.TryParse(value.ToString(), out val))
            {
                if (val >= MinSalary)
                    return new ValidationResult(true, "");
                else
                    return new ValidationResult(false, "Salary can not be less than 5000!");
            }
            else
                return new ValidationResult(false, "Value should be integer!");
        }
    }
}
