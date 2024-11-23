using System;
using System.Collections;
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
    public delegate void ReBindDataSource(ComboBox ctrl, DataSet ds);

    public partial class AddressUI : UIForm
    {
        private string provinceId;
        private string cityId;
        private string districtId;
        private Pain currentSelectedPain;

        SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=.\config\address.db;Version=3;");
        public AddressUI(ref Pain pain)
        {
            
            InitializeComponent();
            m_dbConnection.Open();
            SedProvinceData();
            this.currentSelectedPain = pain;
            this.uiLabelName.Text += pain.Name;
            this.uiLabelCurrentAddress.Text += pain.HomeAddr;
        }
        /// <summary>
        /// 设置省会默认值
        /// </summary>
        private void SedProvinceData()
        {
  
            
            SQLiteCommand command = new SQLiteCommand("select id,name from province", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                list.Add(new DictionaryEntry(reader.GetString(0),reader.GetString(1)));
            }
            //m_dbConnection.Close();
            uiComboboxProvince.DataSource = list;
            uiComboboxProvince.DisplayMember = "Value";
            uiComboboxProvince.ValueMember = "Key";
 
        }


        public static void BindDataSource(ComboBox ctrl, DataSet ds)
        {
            try
            {
                ctrl.BeginUpdate();

                // make sure change it to false, or there will be exception if the droppedDownList is empty  

                ctrl.DroppedDown = false;

                string oldText = ctrl.Text;

                ctrl.DataSource = ds.Tables[0];
                ctrl.DisplayMember = ds.Tables[0].Columns[1].ColumnName;
                ctrl.ValueMember = ds.Tables[0].Columns[0].ColumnName;

                // set the text, so user can input continuely  
                ctrl.Text = oldText;

                // set the cursor at the end of the text  
                ctrl.Focus();

                ctrl.Select(oldText.Length, oldText.Length);
                ctrl.IntegralHeight = false;
                ctrl.MaxDropDownItems = 10;
                //do not drop down if it is empty, or there will be exception
                if (ctrl.Items.Count > 0)
                {
                    ctrl.DroppedDown = true;
                }

                ctrl.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                //statusLabel.Text = ex.Message;  
            }
            finally
            {
                ctrl.EndUpdate();
            }

        }



        private DataSet SearchDb(string sql)
        {
            DataSet ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, m_dbConnection);
            adapter.Fill(ds, "ST");
            return ds;
        }
        public static void BindChildDataSource(ComboBox ctrl, DataSet ds)
        {
            try
            {
                ctrl.BeginUpdate();

                // make sure change it to false, or there will be exception if the droppedDownList is empty  

                ctrl.DroppedDown = false;
                ctrl.DataSource = ds.Tables[0];
                ctrl.DisplayMember = ds.Tables[0].Columns[1].ColumnName;
                ctrl.ValueMember = ds.Tables[0].Columns[0].ColumnName;
                ctrl.IntegralHeight = false;
                ctrl.MaxDropDownItems = 10;
            }
            catch (Exception ex)
            {
                //statusLabel.Text = ex.Message;  
            }
            finally
            {
                ctrl.EndUpdate();
            }

        }
        #region 选项改变时候，更新下级combox数据;
        private void uiComboboxProvince_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.IsHandleCreated) //判断控件已经创建线程;
            {
                if (uiComboboxProvince.SelectedValue != null)  // 必须要有选中项
                {
                    this.provinceId = uiComboboxProvince.SelectedValue.ToString();
                }
              
                DataSet ds = new DataSet();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter($"select id,name from city where pid='{provinceId}' ", m_dbConnection);
                adapter.Fill(ds, "city");
              
                uiComboboxCity.BeginInvoke(new ReBindDataSource(BindChildDataSource), uiComboboxCity, ds);
                
            }
        }

        private void uiComboboxDistrict_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.IsHandleCreated)
            {
                if (uiComboboxDistrict.SelectedValue != null)
                {
                    this.districtId = uiComboboxDistrict.SelectedValue.ToString();
                }
           
                DataSet ds = new DataSet();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter($"select id,name from town where pid='{districtId}' ", m_dbConnection);
                adapter.Fill(ds, "district");
       
                uiComboboxTown.BeginInvoke(new ReBindDataSource(BindChildDataSource), uiComboboxTown, ds);
            }
        }

        private void uiComboboxCity_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.IsHandleCreated)
            {
                if (uiComboboxCity.SelectedValue != null)
                {
                    this.cityId = uiComboboxCity.SelectedValue.ToString();
                }
       
                DataSet ds = new DataSet();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter($"select id,name from district where pid='{cityId}' ", m_dbConnection);
                adapter.Fill(ds, "district");
    
                uiComboboxDistrict.BeginInvoke(new ReBindDataSource(BindChildDataSource), uiComboboxDistrict, ds);
            }
        }
        #endregion

        #region  动态更新填充数据

        private void uiComboboxProvice_TextUpdate(object sender, EventArgs e)
        {
            Thread thread = new Thread(UpdateProviceAutoComplete);
            thread.Start();
            // 通过子线程减慢查询速度，避免闪屏;
            void UpdateProviceAutoComplete()
            {
                DataSet ds = new DataSet();
                // 获得输入的拼音  
                string abbr = uiComboboxProvince.Text.Trim();
                Thread.Sleep(200);
                if (abbr!= uiComboboxProvince.Text.Trim()||abbr.Length<1)
                {
                    return;
                }
                bool isAbc = Regex.IsMatch(abbr, @"^[A-Za-z]+$");
                if (isAbc)
                {
                    ds = SearchDb($"select id,name from province where pinyin_prefix like '%{abbr}%'");
                }
                else
                {
                    ds = SearchDb($"select id,name from province where name like '%{abbr}%'");
                }
                // 重新邦定  
                uiComboboxProvince.BeginInvoke(new ReBindDataSource(BindDataSource), uiComboboxProvince, ds);
                this.Cursor = Cursors.Default;
            }
        }



        private void uiComboboxCity_TextUpdate(object sender, EventArgs e)
        {
            Thread thread=new Thread(UpdateCityAutoComplete);
            thread.Start();

            void UpdateCityAutoComplete()
            {
                DataSet ds = new DataSet();
                // 获得输入的拼音  
                string abbr = uiComboboxCity.Text.Trim();
                Thread.Sleep(200);
                if (abbr!= uiComboboxCity.Text.Trim()||abbr.Length<1)
                {
                    return;
                }
                bool isAbc = Regex.IsMatch(abbr, @"^[A-Za-z]+$");
                if (isAbc)
                {
                    ds = SearchDb($"select id,name from city where  pid = '{provinceId}' and pinyin_prefix like '%{abbr}%'");
                }
                else
                {
                    ds = SearchDb($"select id,name from city where  pid = '{provinceId}' and name like '%{abbr}%'");
                }
                // 重新邦定  
                uiComboboxCity.BeginInvoke(new ReBindDataSource(BindDataSource), uiComboboxCity, ds);
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 动态更新填充列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiComboboxDistrict_TextUpdate(object sender, EventArgs e)
        {
            Thread thread = new Thread(UpdateDistrictAutoComplete);
            thread.Start();

            void UpdateDistrictAutoComplete()
            {
                DataSet ds = new DataSet();
                // 获得输入的拼音  
                string abbr = uiComboboxDistrict.Text.Trim();
                Thread.Sleep(200);
                if (abbr!=uiComboboxDistrict.Text.Trim() || abbr.Length < 1)
                {
                    return;
                }
                bool isAbc = Regex.IsMatch(abbr, @"^[A-Za-z]+$");
                if (isAbc)
                {
                    ds = SearchDb($"select id,name from district where pid = '{cityId}' and pinyin_prefix like '%{abbr}%'");
                }
                else
                {
                    ds = SearchDb($"select id,name from district where pid = '{cityId}' and name like '%{abbr}%'");
                }
                // 重新邦定  
                uiComboboxDistrict.BeginInvoke(new ReBindDataSource(BindDataSource), uiComboboxDistrict, ds);
                this.Cursor = Cursors.Default;
            }
        }

        private void uiComboboxTown_TextUpdate(object sender, EventArgs e)
        {
            Thread thread = new Thread(UpdateTownAutoComplete);
            thread.Start();

            void UpdateTownAutoComplete()          
            {
                DataSet ds = new DataSet();
                string abbr = uiComboboxTown.Text.Trim();
                Thread.Sleep(200);
                if (abbr!= uiComboboxTown.Text.Trim() || abbr.Length < 1)
                {
                    return;
                }
                bool isAbc = Regex.IsMatch(abbr, @"^[A-Za-z]+$");
                if (isAbc)
                {
                    ds = SearchDb($"select id,name from town where pid = '{districtId}' and pinyin_prefix like '%{abbr}%'");
                }
                else
                {
                    ds = SearchDb($"select id,name from town where pid = '{districtId}' and name like '%{abbr}%'");

                }
                // 重新邦定  
                uiComboboxTown.BeginInvoke(new ReBindDataSource(BindDataSource), uiComboboxTown, ds);
                this.Cursor = Cursors.Default;
            }
        }

        #endregion
        private void uBtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void uBtnOk_Click(object sender, EventArgs e)
        {
            string newAddress = "";
            if (uiComboboxProvince.SelectedValue != null)
            {
                newAddress += uiComboboxProvince.Text;
            }
            if (uiComboboxCity.SelectedValue != null)
            {
                newAddress += uiComboboxCity.Text;
            }
            if (uiComboboxDistrict.SelectedValue != null)
            {
                newAddress += uiComboboxDistrict.Text;
            }
            if (uiComboboxTown.SelectedValue != null)
            {
                newAddress += uiComboboxTown.Text;
            }
            if (uiTextBoxHome.Text != null)
            {
                newAddress += uiTextBoxHome.Text;
            }
           
            if (uiTextBoxHome.Text!=null && uiTextBoxHome.Text.Length<2)
            {
                bool result = UIMessageDialog.ShowAskDialog(this,"确定要继续吗？", "检测到详细地址处未键入任何字符,这将会导致患者具体地址缺失镇/街道以下的数据!\r\n是：保存所有修改,以新地址覆盖旧地址,并退出!\r\n否：不保存任何修改,并退出。\r\n取消：返回地址更新界面。");
                if (result) {
                    SaveHistory(newAddress);
                    this.currentSelectedPain.HomeAddr = newAddress;
                }
                else
                {
                    return;
                }
            }
            else if (uiTextBoxHome.Text.Length > 0)
            {
                SaveHistory(newAddress);
                this.currentSelectedPain.HomeAddr = newAddress;
            }
            
            this.Close();
        }

        private void SaveHistory(string newAddress)
        {
            if (uiTextBoxHome.Text!=null&&uiTextBoxHome.Text.Length>1)
            {
                string history = uiTextBoxHome.Text;
                SQLiteCommand command = new SQLiteCommand($"insert into history values('{history}','{newAddress}')", m_dbConnection);
                command.ExecuteNonQuery();
            }
        }
        private void AddressUI_Load(object sender, EventArgs e)
        {
            uiComboboxProvince.Text = "重庆市";
            ReadHistory();
        }
        private void ReadHistory()
        {
            SQLiteCommand command = new SQLiteCommand($"select home from history", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                this.uiTextBoxHome.AutoCompleteCustomSource.Add(reader.GetString(0));
            }
            reader.Close();
            this.uiTextBoxHome.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.uiTextBoxHome.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
    }
}
