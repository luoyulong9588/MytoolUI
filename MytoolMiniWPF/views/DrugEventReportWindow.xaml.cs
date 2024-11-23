using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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

namespace MytoolMiniWPF.views
{
    /// <summary>
    /// DrugEventReportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DrugEventReportWindow : Window
    {
        private SQLiteConnection conn;
        public DrugEventReportWindow()
        {
            InitializeComponent();
            conn = new SQLiteConnection("Data Source=./config/drugEvent.db;Version=3;");  //数据库存到服务器上；
            //LoadData();
        }
        private void LoadData()
        {
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Persons", conn);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable("Persons");
            adapter.Fill(dt);
            dataGrid.ItemsSource = dt.DefaultView;
            conn.Close();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
           // DataView dv = (DataView)dataGrid.ItemsSource;
          //  dv.RowFilter = $"Name LIKE '%{textBoxSearch.Text}%'";
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
