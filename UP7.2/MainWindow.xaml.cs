using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UP7._2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Таблица кодов, используемая при шифровании/дешифровании.
        /// </summary>
        private Dictionary<string, string> _codeTable;

        public MainWindow()
        {
            InitializeComponent();
            // Начальное наполнение таблицы
            _codeTable = new Dictionary<string, string>
            {
                { "привет", "alpha" },
                { "тест", "gamma" },
                { "защита", "delta" }
            };
        }

        /// <summary>
        /// Обработчик нажатия кнопки «Зашифровать».
        /// Выполняет шифрование текста из InputTextBox, результат помещает в ResultTextBox.
        /// </summary>
        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            ExecuteOperation(isEncrypt: true);
        }

        /// <summary>
        /// Обработчик нажатия кнопки «Расшифровать».
        /// Выполняет дешифрование текста из InputTextBox, результат помещает в ResultTextBox.
        /// </summary>
        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            ExecuteOperation(isEncrypt: false);
        }

        /// <summary>
        /// Общая логика выполнения операции шифрования/дешифрования с обработкой ошибок.
        /// </summary>
        /// <param name="isEncrypt">True для шифрования, false для дешифрования.</param>
        private void ExecuteOperation(bool isEncrypt)
        {
            string input = InputTextBox.Text;

            try
            {
                string result = isEncrypt
                    ? PortaCipher.Encrypt(input, _codeTable)
                    : PortaCipher.Decrypt(input, _codeTable);

                if (result == null)
                {
                    MessageBox.Show(
                        isEncrypt ? "Слово не найдено в таблице." : "Код не найден в таблице.",
                        "Информация",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    ResultTextBox.Clear();
                }
                else
                {
                    ResultTextBox.Text = result;
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Поле не может быть пустым.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Обработчик кнопки настройки таблицы кодов.
        /// Открывает окно редактора и обновляет локальную таблицу.
        /// </summary>
        private void EditTableButton_Click(object sender, RoutedEventArgs e)
        {
            var editorWindow = new TableEditorWindow(_codeTable);
            editorWindow.Owner = this;
            bool? dialogResult = editorWindow.ShowDialog();

            if (dialogResult == true)
            {
                // Заменяем таблицу на отредактированную версию
                _codeTable = editorWindow.EditedTable;
            }
        }
    }
}