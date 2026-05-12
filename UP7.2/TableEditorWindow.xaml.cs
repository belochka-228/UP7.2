using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace UP7._2
{
    /// <summary>
    /// Окно редактирования таблицы кодов
    /// </summary>
    public partial class TableEditorWindow : Window
    {
        /// <summary>
        /// Временная таблица для редактирования
        /// </summary>
        public Dictionary<string, string> EditedTable { get; private set; }
        public TableEditorWindow(Dictionary<string, string> currentTable)
        {
            InitializeComponent();

            EditedTable = new Dictionary<string, string>(currentTable);

            var dataTable = new DataTable();
            dataTable.Columns.Add("Слово", typeof(string));
            dataTable.Columns.Add("Код", typeof(string));

            foreach (var pair in EditedTable)
            {
                dataTable.Rows.Add(pair.Key, pair.Value);
            }

            CodesDataGrid.ItemsSource = dataTable.DefaultView;
        }
        /// <summary>
        /// Обработчик кнопки «Сохранить»
        /// Преобразует данные из DataGrid обратно в словарь
        /// </summary>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (CodesDataGrid.ItemsSource is DataView dataView)
            {
                var newTable = new Dictionary<string, string>();
                foreach (DataRowView rowView in dataView)
                {
                    string word = rowView[0]?.ToString() ?? string.Empty;
                    string code = rowView[1]?.ToString() ?? string.Empty;

                    if (string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(code))
                        continue;

                    newTable[word] = code;
                }

                EditedTable = newTable;
                DialogResult = true;
                Close();
            }
        }
        /// <summary>
        /// Обработчик кнопки «Отмена»
        /// </summary>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}