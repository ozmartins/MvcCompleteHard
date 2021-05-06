using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hard.Business.Models.Validations
{
    public class CpfValidator
    {
        public const int CpfLength = 11;

        public static bool Validate(string cpf)
        {
            var cpfDigits = Utils.OnlyNumbes(cpf);

            if (!ValidSize(cpfDigits)) return false;
            return !HasRepeatedDigits(cpfDigits) && HasValidDigits(cpfDigits);
        }

        private static bool ValidSize(string valor)
        {
            return valor.Length == CpfLength;
        }

        private static bool HasRepeatedDigits(string valor)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };

            return invalidNumbers.Contains(valor);
        }

        private static bool HasValidDigits(string valor)
        {
            var number = valor.Substring(0, CpfLength - 2);

            var verifyDigit = new verifyDigit(number).WithMultiplierUntil(2, 11).Replacing("0", 10, 11);
            
            var firstDigit = verifyDigit.CalculateDigit();
            
            verifyDigit.AddDigit(firstDigit);
            
            var secondDigit = verifyDigit.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == valor.Substring(CpfLength - 2, 2);
        }
    }

    public class CnpjValidator
    {
        public const int CnpjLength = 14;

        public static bool Validate(string cpnj)
        {
            var cnpjNumeros = Utils.OnlyNumbes(cpnj);

            if (!HasValideLength(cnpjNumeros)) return false;
            return !HasRepeatedDigits(cnpjNumeros) && HasValidDigits(cnpjNumeros);
        }

        private static bool HasValideLength(string valor)
        {
            return valor.Length == CnpjLength;
        }

        private static bool HasRepeatedDigits(string valor)
        {
            string[] invalidNumbers =
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };
            return invalidNumbers.Contains(valor);
        }

        private static bool HasValidDigits(string valor)
        {
            var number = valor.Substring(0, CnpjLength - 2);

            var verifyDigit = new verifyDigit(number)
                .WithMultiplierUntil(2, 9)
                .Replacing("0", 10, 11);
            var firstDigit = verifyDigit.CalculateDigit();
            verifyDigit.AddDigit(firstDigit);
            var secondDigit = verifyDigit.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == valor.Substring(CnpjLength - 2, 2);
        }
    }

    public class verifyDigit
    {
        private string _numero;

        private const int Modulo = 11;
        
        private readonly List<int> _multiplicadores = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
        
        private readonly IDictionary<int, string> _substituicoes = new Dictionary<int, string>();
        
        private bool _complementarDoModulo = true;

        public verifyDigit(string numero)
        {
            _numero = numero;
        }

        public verifyDigit WithMultiplierUntil(int primeiroMultiplicador, int ultimoMultiplicador)
        {
            _multiplicadores.Clear();
            
            for (var i = primeiroMultiplicador; i <= ultimoMultiplicador; i++)
                _multiplicadores.Add(i);

            return this;
        }

        public verifyDigit Replacing(string substituto, params int[] digitos)
        {
            foreach (var i in digitos)
            {
                _substituicoes[i] = substituto;
            }
            return this;
        }

        public void AddDigit(string digito)
        {
            _numero = string.Concat(_numero, digito);
        }

        public string CalculateDigit()
        {
            return !(_numero.Length > 0) ? "" : GetDigitSum();
        }

        private string GetDigitSum()
        {
            var soma = 0;
            
            for (int i = _numero.Length - 1, m = 0; i >= 0; i--)
            {
                var produto = (int)char.GetNumericValue(_numero[i]) * _multiplicadores[m];
                soma += produto;

                if (++m >= _multiplicadores.Count) m = 0;
            }

            var mod = (soma % Modulo);
            
            var resultado = _complementarDoModulo ? Modulo - mod : mod;

            return _substituicoes.ContainsKey(resultado) ? _substituicoes[resultado] : resultado.ToString();
        }
    }

    public class Utils
    {
        public static string OnlyNumbes(string valor)
        {
            var onlyNumber = "";
            
            foreach (var s in valor)
            {
                if (char.IsDigit(s))
                {
                    onlyNumber += s;
                }
            }
            
            return onlyNumber.Trim();
        }
    }
}
