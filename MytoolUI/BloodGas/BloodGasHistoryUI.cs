using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MytoolUI
{
    public partial class BloodGasHistoryUI : Form
    {
        private SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=config\data.db;Version=3;");
        private DataSet ds = new DataSet();
        private SQLiteDataAdapter adapter;
        public BloodGasHistoryUI()
        {
            InitializeComponent();
            ShowData();
        }
        private void ShowData()
        {
            DataTable dt = new DataTable();
            m_dbConnection.Open();
            adapter = new SQLiteDataAdapter("select * from bloodgas", m_dbConnection);
            adapter.Fill(ds, "ST");
            uiDataGridViewDesktop.DataSource = ds.Tables[0];
            uiDataGridViewDesktop.Columns[0].Width = 50;
            uiDataGridViewDesktop.Columns[1].Width = 150;
            m_dbConnection.Close();

        }

    }
}
