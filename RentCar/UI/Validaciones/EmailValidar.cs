using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace RentCar.UI.Validaciones {
   public  class EmailValidar : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string cadena = value as string;

            if (cadena != null)
            {
                String expresion;
                expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                if (Regex.IsMatch(cadena, expresion))
                {
                    if (Regex.Replace(cadena, expresion, String.Empty).Length == 0)
                    {
                        return ValidationResult.ValidResult;
                    }
                    else
                    {
                        return new ValidationResult(false, "Email no valido");
                    }
                }
                else
                    return new ValidationResult(false, "Email no valido");



            }
            return new ValidationResult(false, "Debes poner un email");
        }
    }
}
