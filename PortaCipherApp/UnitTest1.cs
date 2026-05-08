using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace PortaCipherApp
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// TC‑R06: Проверка шифрования слова, присутствующего в таблице кодов
        /// Ожидается, что метод Encrypt вернёт соответствующий код
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
        /// TC‑R07: Проверка дешифрования кода, присутствующего в таблице кодов
        /// Ожидается, что метод Decrypt вернёт исходное слово
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
        /// TC‑R08: Проверка поведения при шифровании слова, отсутствующего в таблице
        /// Ожидается возврат null
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
        /// TC‑R13: Проверка поведения при дешифровании кода, отсутствующего в таблице
        /// Ожидается возврат null
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
        /// Проверка валидации: пустая строка при шифровании должна вызывать ArgumentException
        /// Соответствует требованиям TC‑01 и TC‑R10
        /// </summary>
        [TestMethod]
        public void Encrypt_EmptyString_ThrowsArgumentException()
        {
            var table = new Dictionary<string, string>();
            Assert.ThrowsException<ArgumentException>(() => PortaCipher.Encrypt("", table));
        }

        /// <summary>
        /// Проверка валидации: пустая строка при дешифровании должна вызывать ArgumentException
        /// </summary>
        [TestMethod]
        public void Decrypt_EmptyString_ThrowsArgumentException()
        {
            var table = new Dictionary<string, string>();
            Assert.ThrowsException<ArgumentException>(() => PortaCipher.Decrypt("", table));
        }

        /// <summary>
        /// TC‑R11: Проверка, что строка из одних пробелов при шифровании вызывает ArgumentException
        /// </summary>
        [TestMethod]
        public void Encrypt_WhiteSpaceString_ThrowsArgumentException()
        {
            var table = new Dictionary<string, string>();
            Assert.ThrowsException<ArgumentException>(() => PortaCipher.Encrypt("   ", table));
        }

        /// <summary>
        /// TC‑R11 (дешифрование): Проверка, что строка из пробелов при дешифровании вызывает ArgumentException
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
        /// TC‑R12: Проверка конфигурируемости таблицы кодов
        /// После добавления новой пары шифрование должно использовать обновлённую таблицу
        /// </summary>
        [TestMethod]
        public void Encrypt_AfterAddingPairToTable_UsesUpdatedTable()
        {
            var table = new Dictionary<string, string> { { "привет", "alpha" } };
            string newWord = "мир";
            string newCode = "world";

            // Имитация настройки: добавляем новую пару
            table[newWord] = newCode;

            string encrypted = PortaCipher.Encrypt(newWord, table);
            string decrypted = PortaCipher.Decrypt(encrypted, table);

            Assert.AreEqual(newCode, encrypted);
            Assert.AreEqual(newWord, decrypted);
        }

        /// <summary>
        /// Дополнительный тест: проверка обратимости шифрования (полный цикл Encrypt → Decrypt).
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