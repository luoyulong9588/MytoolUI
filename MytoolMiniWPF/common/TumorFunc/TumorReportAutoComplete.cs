using FlaUI.Core.AutomationElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MytoolMiniWPF.views
{
    public partial class TumorReportWindow
    {
        private ObservableCollection<ComboBoxDiagnoseNameItemViewModel> DiagnoseNameitems;
        private ObservableCollection<ComboBoxPathologyDiagnoseNameItemViewModel> PathologyDiagnoseNameitems;
        private ObservableCollection<ComboBoxICD10ItemViewModel> ICD10items;


        private async void comboboxDiagnoseName_KeyUpAsync(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down || e.Key== Key.Left || e.Key == Key.Right || e.Key == Key.Space || e.Key == Key.Enter)
            {
                return;
            }


            DiagnoseNameitems.Clear(); // 清空现有项  
            var uniqueNames = new HashSet<string>(); // 用于跟踪唯一名称的HashSet  

            string searchText = comboboxDiagnoseName.Text;
            if (string.IsNullOrWhiteSpace(searchText))
                return;
            
            bool isAbc = Regex.IsMatch(searchText, @"^[A-Za-z]+$");
            
            // SQLite连接字符串，根据你的数据库位置进行修改  
            string connectionString = @"Data Source=.\config\data.db;Version=3;";

            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = null;
                if (searchText != comboboxDiagnoseName.Text.Trim() || searchText.Length < 1)
                {
                    return;
                }
                if (isAbc)
                {
                    query = $"select hospitalDiagnose from icdo3 where  pinyinHospital like @SearchText";
                }
                else
                {
                    query=$"select hospitalDiagnose from icdo3 where  hospitalDiagnose like @SearchText";

                }

                using (var command = new SQLiteCommand(query, connection))
                {
                    // 使用参数化查询来防止SQL注入  
                    command.Parameters.AddWithValue("@SearchText", $"%{searchText}%");

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                      
                        while (await reader.ReadAsync())
                        {
                            string name = reader.GetString(0);

                            if (uniqueNames.Add(name))
                            { 
                                // 创建一个ViewModel或直接在UI中使用DTO（数据传输对象）  
                                DiagnoseNameitems.Add(new ComboBoxDiagnoseNameItemViewModel { DisplayValue = name });
                            }
                        }
                    }
                }
                // 更新UI的_items  
                Application.Current.Dispatcher.Invoke(() =>
                {


                    // 数据更新后，设置ComboBox的IsDropDownOpen属性为true  
                    // 注意：这里假设你的ComboBox有一个名为MyComboBox的x:Name  
                    if (comboboxDiagnoseName.IsDropDownOpen == false)
                    {
                        comboboxDiagnoseName.IsDropDownOpen = true;
                    }
                    
                });
            }
        }

       

        private async void comboboxPathologyDiagnoseName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Space || e.Key == Key.Enter)
            {
                return;
            }

            PathologyDiagnoseNameitems.Clear(); // 清空现有项  
            var uniqueNames = new HashSet<string>(); // 用于跟踪唯一名称的HashSet  

            string searchText = comboboxPathologyDiagnoseName.Text;
            if (string.IsNullOrWhiteSpace(searchText))
                return;

            bool isAbc = Regex.IsMatch(searchText, @"^[A-Za-z]+$");

            // SQLite连接字符串，根据你的数据库位置进行修改  
            string connectionString = @"Data Source=.\config\data.db;Version=3;";

            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = null;
                if (searchText != comboboxPathologyDiagnoseName.Text.Trim() || searchText.Length < 1)
                {
                    return;
                }
                if (isAbc)
                {
                    query = $"select pathologicalDiagnose from icdo3 where  pinyinHospital like @SearchText";
                }
                else
                {
                    query = $"select pathologicalDiagnose from icdo3 where  hospitalDiagnose like @SearchText";

                }

                using (var command = new SQLiteCommand(query, connection))
                {
                    // 使用参数化查询来防止SQL注入  
                    command.Parameters.AddWithValue("@SearchText", $"%{searchText}%");

                    using (var reader = await command.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            string name = reader.GetString(0);

                            if (uniqueNames.Add(name))
                            {
                                // 创建一个ViewModel或直接在UI中使用DTO（数据传输对象）  
                                PathologyDiagnoseNameitems.Add(new ComboBoxPathologyDiagnoseNameItemViewModel { DisplayValue = name });
                            }
                        }
                    }
                }
                // 更新UI的_items  
                Application.Current.Dispatcher.Invoke(() =>
                {


                    // 数据更新后，设置ComboBox的IsDropDownOpen属性为true  
                    // 注意：这里假设你的ComboBox有一个名为MyComboBox的x:Name  
                    if (comboboxPathologyDiagnoseName.IsDropDownOpen == false)
                    {
                        comboboxPathologyDiagnoseName.IsDropDownOpen = true;
                    }

                });
            }


        }



        private async  void comboboxICD10_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Space || e.Key == Key.Enter)
            {
                return;
            }

            ICD10items.Clear(); // 清空现有项  
            var uniqueNames = new HashSet<string>(); // 用于跟踪唯一名称的HashSet  

            string searchText = comboboxICD10.Text;
            if (string.IsNullOrWhiteSpace(searchText))
                return;

            bool isAbc = Regex.IsMatch(searchText, @"^[A-Za-z]+$");

            // SQLite连接字符串，根据你的数据库位置进行修改  
            string connectionString = @"Data Source=.\config\data.db;Version=3;";

            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                connection.EnableExtensions(true);
                connection.LoadExtension("SQLite.Interop.dll", "sqlite3_fts5_init");
                connection.LoadExtension("./simple/simple.dll");

                string query = null;

                if (searchText != comboboxICD10.Text.Trim() || searchText.Length < 1)
                {
                    return;
                }
                if (isAbc)
                {
                    query = $"select name from icd10 where name_pinyin like '%{searchText}%'";
                }
                else
                {
                    query = $"select name from icd10 where name match '{searchText}'";

                }

                using (var command = new SQLiteCommand(query, connection))
                {

                    
                    using (var reader = await command.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            string name = reader.GetString(0);
                            if (uniqueNames.Add(name))
                            {
                                // 创建一个ViewModel或直接在UI中使用DTO（数据传输对象）  
                                ICD10items.Add(new ComboBoxICD10ItemViewModel { DisplayValue = name });
                            }
                        }
                    }
                }
                // 更新UI的_items  
                Application.Current.Dispatcher.Invoke(() =>
                {


                    // 数据更新后，设置ComboBox的IsDropDownOpen属性为true  
                    // 注意：这里假设你的ComboBox有一个名为MyComboBox的x:Name  
                    if (comboboxICD10.IsDropDownOpen == false)
                    {
                        comboboxICD10.IsDropDownOpen = true;
                    }

                });
            }
        }
        











        // ViewModel类，用于存储ComboBox的项  
        public class ComboBoxDiagnoseNameItemViewModel
        {
            public string DisplayValue { get; set; }
        }

        public class ComboBoxPathologyDiagnoseNameItemViewModel
        {
            public string DisplayValue { get; set; }
        }
        public class ComboBoxICD10ItemViewModel
        {
            public string DisplayValue { get; set; }
        }

    }
}
