using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioBackEndApi.Utils
{
    public class CPFValidation
    {
        public static bool CPFValido(string CPF)
        {
            int[] validador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] validador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            if (CPF.Length != 11)
            {
                return false;
            }

            string tempCpf = CPF.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * validador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito1 = resto.ToString();
            tempCpf = tempCpf + resto;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * validador2[i];

            int digito2 = soma % 11;
            if (digito2 < 2)
                digito2 = 0;
            else
                digito2 = 11 - digito2;


            string resultado = digito1 + digito2;
            return CPF.EndsWith(resultado);
        }
    }
}
