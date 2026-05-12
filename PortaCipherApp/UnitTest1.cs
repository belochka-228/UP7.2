using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace PortaCipherApp
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// TC‑R05: Проверка шифрования слова из таблицы кодов (позитивный тест)
        /// </summary>
        [TestMethod]
        public void Encrypt_WordInTable_ReturnsCode()
        {
            var table = new Dictionary<string, string> { { "привет", "alpha" } };
            string input = "привет";
            string result = PortaCipher.Encrypt(input, table);

            Assert.AreEqual("alpha", result);
        }

        /// <summary>
        /// TC‑R06: Проверка дешифрования кода из таблицы кодов (позитивный тест)
        /// </summary>
        [TestMethod]
        public void Decrypt_CodeInTable_ReturnsWord()
        {
            var table = new Dictionary<string, string> { { "привет", "alpha" } };
            string input = "alpha";
            string result = PortaCipher.Decrypt(input, table);

            Assert.AreEqual("привет", result);
        }

        /// <summary>
        /// TC‑R07: Проверка обработки слова, отсутствующего в таблице кодов
        /// </summary>
        [TestMethod]
        public void Encrypt_WordNotFound_ReturnsNull()
        {
            var table = new Dictionary<string, string>();
            string input = "абв";
            string result = PortaCipher.Encrypt(input, table);

            Assert.IsNull(result, "Для отсутствующего слова ожидается null.");
        }

        /// <summary>
        /// TC‑R12: Проверка обработки отсутствующего кода при дешифровании
        /// </summary>
        [TestMethod]
        public void Decrypt_CodeNotFound_ReturnsNull()
        {
            var table = new Dictionary<string, string>();
            string input = "xyz";
            string result = PortaCipher.Decrypt(input, table);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Проверка валидации при шифровании
        /// </summary>
        [TestMethod]
        public void Encrypt_EmptyString_ThrowsArgumentException()
        {
            var table = new Dictionary<string, string>();
            Assert.ThrowsException<ArgumentException>(() => PortaCipher.Encrypt("", table));
        }

        /// <summary>
        /// Проверка валидации: пустая строка при дешифровании
        /// </summary>
        [TestMethod]
        public void Decrypt_EmptyString_ThrowsArgumentException()
        {
            var table = new Dictionary<string, string>();
            Assert.ThrowsException<ArgumentException>(() => PortaCipher.Decrypt("", table));
        }

        /// <summary>
        /// TC‑R10: Проверка ввода строки из одних пробелов
        /// </summary>
        [TestMethod]
        public void Encrypt_WhiteSpaceString_ThrowsArgumentException()
        {
            var table = new Dictionary<string, string>();
            Assert.ThrowsException<ArgumentException>(() => PortaCipher.Encrypt("   ", table));
        }

        /// <summary>
        /// TC‑R10 (дешифрование): Проверка, что строка из пробелов при дешифровании вызывает ArgumentException
        /// </summary>
        [TestMethod]
        public void Decrypt_WhiteSpaceString_ThrowsArgumentException()
        {
            var table = new Dictionary<string, string>();
            Assert.ThrowsException<ArgumentException>(() => PortaCipher.Decrypt("   ", table));
        }

        /// <summary>
        /// Проверка, что передача null в метод Encrypt вызывает ArgumentNullException
        /// </summary>
        [TestMethod]
        public void Encrypt_Null_ThrowsArgumentNullException()
        {
            var table = new Dictionary<string, string>();
            Assert.ThrowsException<ArgumentNullException>(() => PortaCipher.Encrypt(null, table));
        }

        /// <summary>
        /// Проверка, что передача null в метод Decrypt вызывает ArgumentNullException
        /// </summary>
        [TestMethod]
        public void Decrypt_Null_ThrowsArgumentNullException()
        {
            var table = new Dictionary<string, string>();
            Assert.ThrowsException<ArgumentNullException>(() => PortaCipher.Decrypt(null, table));
        }

        /// <summary>
        /// TC‑R11: Проверка работы с изменённой таблицей кодов
        /// </summary>
        [TestMethod]
        public void Encrypt_AfterAddingPairToTable_UsesUpdatedTable()
        {
            var table = new Dictionary<string, string> { { "привет", "alpha" } };
            string newWord = "мир";
            string newCode = "world";

            table[newWord] = newCode;

            string encrypted = PortaCipher.Encrypt(newWord, table);
            string decrypted = PortaCipher.Decrypt(encrypted, table);

            Assert.AreEqual(newCode, encrypted);
            Assert.AreEqual(newWord, decrypted);
        }

        /// <summary>
        /// проверка обратимости шифрования
        /// </summary>
        [TestMethod]
        public void RoundTrip_EncryptDecrypt_ReturnsOriginalWord()
        {
            var table = new Dictionary<string, string> { { "тест", "gamma" } };
            string original = "тест";
            string encrypted = PortaCipher.Encrypt(original, table);
            string decrypted = PortaCipher.Decrypt(encrypted, table);

            Assert.AreEqual(original, decrypted);
        }
    }
}