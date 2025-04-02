using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Validator
{
    public static class ValidatorSys
    {
        public static void ValidaStringEmpty(string strValidator)
        {
            if (strValidator == null)
            {
                throw new ArgumentNullException(nameof(strValidator));
            }
        }
        public static void ValidaRg(string strValidator)
        {
            string pattern = @"^\d{2}\.\d{3}\.\d{3}-\d$";

            if (!Regex.IsMatch(strValidator, pattern)) {

                throw new ArgumentException("RG Inválido.");
            };
        }


        public static void ValidaCpfVazio(string strValidator)
        {
            if (strValidator == null)
            {
                throw new ArgumentNullException(nameof(strValidator));
            }
        }
        public static void ValidaTelefone(string strValidator)
        {
            string pattern = @"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$";

            if (!Regex.IsMatch(strValidator ,pattern)) {

                throw new ArgumentException("Telefone inválido");
            };
        }
        public static void ValidaEmail(string strValidator)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            if (!Regex.IsMatch(strValidator, pattern)) {

                throw new ArgumentException("E-mail inválido.");
            };
        }
    }
}

