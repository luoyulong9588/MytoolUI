using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace MytoolUI
{
    public partial class DeathInformationUI : UIForm
    {
        private Pain currentPain;
        private SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=.\config\data.db;Version=3;");
        private UIRadioButton uradioBtnDead;
        private UIRadioButton uradioBtnunDead;
        public DeathInformationUI(ref Pain pain, ref UIRadioButton dead, ref UIRadioButton undead)
        {
            this.currentPain = pain;
            this.uradioBtnDead = dead;
            this.uradioBtnunDead = undead;
            InitializeComponent();
            SetDefaultMessage();
            m_dbConnection.Open();
            m_dbConnection.EnableExtensions(true);
            m_dbConnection.LoadExtension("SQLite.Interop.dll", "sqlite3_fts5_init");
            m_dbConnection.LoadExtension("simple\\simple.dll");
        }

        private void SetDefaultMessage()
        {
            this.uiComboboxName.Text = currentPain.Name;
            this.uiDatetimePickerDeathTime.Text = "";
        }

        private void uiComboboxDeathReason_SelectedValueChanged(object sender, EventArgs e)
        {
            if (uiComboboxDeathReason.Text != null && uiComboboxDeathReason.Text == "2.其他疾病" || uiComboboxDeathReason.Text == "3.不详")
            {
                this.uiComboboxDeathIcd10Num.Enabled = true;
                this.uiComboboxDeathIcd10.Enabled = true;
                this.uiComboboxDeathIcd10Num.Text = "键入死亡ICD编码";
                this.uiComboboxDeathIcd10.Text = "键入死亡ICD名称";
                this.uiLabelDeathIcd10.ForeColor = Color.IndianRed;
                this.uiComboboxDeathIcd10.RectColor = Color.IndianRed;
            }
            else
            {
                this.uiComboboxDeathIcd10Num.Enabled = false;
                this.uiComboboxDeathIcd10.Enabled = false;
                this.uiComboboxDeathIcd10Num.Text = "死于肿瘤无需填写";
                this.uiComboboxDeathIcd10.Text = "死于肿瘤无需填写";
                this.uiLabelDeathIcd10.Style = UIStyle.Office2010Silver;
                this.uiComboboxDeathIcd10.Style = UIStyle.Office2010Silver;
            }
        }

        private void uBtnOk_Click(object sender, EventArgs e)
        {
            currentPain.IsDeath = true;
            if (CheckBlanks())
            {
                currentPain.DeathTime = uiDatetimePickerDeathTime.Text;
                currentPain.DeathReason = uiComboboxDeathReason.Text;
                currentPain.DeathIcd10Number = uiComboboxDeathIcd10Num.Text;
                currentPain.DeathIcd10Name = uiComboboxDeathIcd10.Text;
                ChangeChecked(true);
                this.Close();
            }
        }

        private void ChangeChecked(bool dead)
        {
            if (dead)
            {
                this.uradioBtnDead.Checked = true;
            }
            else
            {
                this.uradioBtnunDead.Checked = true;
            }

        }


        private bool CheckBlanks()
        {
            try
            {
                DateTime deathDate = DateTime.Parse(uiDatetimePickerDeathTime.Text);
            }
            catch (Exception)
            {
                UIMessageDialog.ShowInfoDialog(this,"提示","值未填写完！",UIStyle.LightRed);
                return false;
            }
            List<UIComboboxEx> comboboxs = new List<UIComboboxEx>() { uiComboboxDeathIcd10, uiComboboxDeathIcd10Num, uiComboboxDeathReason };

            foreach (var item in comboboxs)
            {
                if (item.Text == null || item.Text == "键入死亡ICD名称" || item.Text == "键入死亡ICD编码" || item.Text == "选择死亡原因")
                {
                    return false;
                }
            }
            return true;
        }

        private void uBtnClose_Click(object sender, EventArgs e)
        {
            ChangeChecked(false);
            this.Close();
        }

        private DataSet SearchDb(string sql)
        {

            DataSet ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, m_dbConnection);
            adapter.Fill(ds, "hospitalDiagnose");
            return ds;
        }

        /// <summary>
        /// 获取对固定列不重复的新DataTable
        /// </summary>
        /// <param name="dt">含有重复数据的DataTable</param>
        /// <param name="colName">需要验证重复的列名</param>
        /// <returns>新的DataSet，colName列不重复，表格式保持不变</returns>
        private DataSet GetDistinctTable(DataTable dt, string colName)
        {
            DataView dv = dt.DefaultView;
            DataTable dtCardNo = dv.ToTable(true, colName);
            DataTable Pointdt = dv.ToTable();
            Pointdt.Clear();
            for (int i = 0; i < dtCardNo.Rows.Count; i++)
            {
                DataRow dr = dt.Select(colName + "='" + dtCardNo.Rows[i][0].ToString() + "'")[0];
                Pointdt.Rows.Add(dr.ItemArray);
            }
            DataSet newds = new DataSet();
            newds.Tables.Add(Pointdt);
            return newds;
        }

        /// <summary>
        /// 通过委托线程重新绑定数据;
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="ds"></param>
        public static void BindDataSource(ComboBox ctrl, DataSet ds)
        {
            try
            {
                ctrl.BeginUpdate();
                ctrl.DroppedDown = false;
                string oldText = ctrl.Text;
                ctrl.DataSource = ds.Tables[0];
                ctrl.DisplayMember = ds.Tables[0].Columns[0].ColumnName;
                ctrl.Text = oldText;
                ctrl.Focus();
                ctrl.Select(oldText.Length, oldText.Length);
                ctrl.MaxDropDownItems = 12;
                ctrl.AutoCompleteMode = AutoCompleteMode.None;
                if (ctrl.Items.Count > 0)
                {
                    ctrl.DroppedDown = true;
                }

                ctrl.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                ctrl.EndUpdate();
            }

        }

        private void uiComboboxDeathIcd10_SelectedValueChanged(object sender, EventArgs e)
        {
            string searchName = uiComboboxDeathIcd10.Text;

            try
            {
                SQLiteCommand command = new SQLiteCommand($"select num from icd10 where name match '{searchName}'", m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    uiComboboxDeathIcd10Num.Text = reader[0].ToString();
                    reader.Close();
                    break;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }

        private void uiComboboxDeathIcd10_TextUpdate(object sender, EventArgs e)
        {
            Thread th = new Thread(UpdateDeathIcd10Autocomplete);
            th.Start();

            void UpdateDeathIcd10Autocomplete()
            {
                DataSet ds = new DataSet();
                string abbr = uiComboboxDeathIcd10.Text.Trim();
                bool isAbc = Regex.IsMatch(abbr, @"^[A-Za-z]+$");
                Thread.Sleep(300);
                if (abbr != uiComboboxDeathIcd10.Text.Trim() || abbr.Length < 1)
                {
                    return;
                }
                if (isAbc)
                {
                    ds = SearchDb($"select name,num from icd10 where name_pinyin like '%{abbr}%'");
                }
                else
                {
                    ds = SearchDb($"select name,num from icd10 where name match '{abbr}'");
                }
                ds = GetDistinctTable(ds.Tables[0], "name");
                // 重新邦定  
                uiComboboxDeathIcd10.BeginInvoke(new ReBindDataSource(BindDataSource), uiComboboxDeathIcd10, ds);
                Cursor = Cursors.Default;
            }
        }


    }
}
