using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP7._2
{
    /// <summary>
    /// Реализует шифрование и дешифрование по методу Порта с использованием
    /// предустановленной таблицы кодов.
    /// </summary>
    public static class PortaCipher
    {
        /// <summary>
        /// Шифрует указанное слово
        /// Если слово найдено в таблице, возвращается его код
        /// В противном случае возвращается null
        /// </summary>
        public static string Encrypt(string plainText, Dictionary<string, string> codeTable)
        {
            if (plainText == null)
                throw new ArgumentNullException(nameof(plainText));
            if (string.IsNullOrWhiteSpace(plainText))
                throw new ArgumentException("Поле не может быть пустым.", nameof(plainText));

            if (codeTable.TryGetValue(plainText, out string code))
                return code;

            return null;
        }

        /// <summary>
        /// Дешифрует указанный код
        /// Если код найден в таблице, возвращается исходное слово
        /// В противном случае возвращается null
        /// </summary>
        public static string Decrypt(string cipherText, Dictionary<string, string> codeTable)
        {
            if (cipherText == null)
                throw new ArgumentNullException(nameof(cipherText));
            if (string.IsNullOrWhiteSpace(cipherText))
                throw new ArgumentException("Поле не может быть пустым.", nameof(cipherText));

            var reverseTable = codeTable.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
            if (reverseTable.TryGetValue(cipherText, out string original))
                return original;

            return null;
        }
    }
}

