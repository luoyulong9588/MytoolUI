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
using Sunny.UI;

namespace MytoolUI
{
    public partial class SettingUI : Form
    {
        private Color color;
        private DataSet ds = new DataSet();
        private SQLiteDataAdapter adapter;
        private SQLiteCommandBuilder builder;
        private Form currentChild;
        private SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=config\data.db;Version=3;");
        private Sunny.UI.UIHeaderButton mainBtnUserName;
        private UIMessageForm message = new UIMessageForm();
        public SettingUI(Sunny.UI.UIHeaderButton uconbtn,Color color)
        {
            this.color = color;
            this.mainBtnUserName = uconbtn;
            InitializeComponent();
            ModifyDataBase();
            SetBtnColor();
        }

        private void SetBtnColor()
        { 
            this.uBtnHelp.ForeHoverColor = color;
            this.uBtnHelp.ForePressColor = color;
            this.uBtnHelp.ForeSelectedColor = color;

            this.uBtnSaveChange.ForeHoverColor = color;
            this.uBtnSaveChange.ForePressColor = color;
            this.uBtnSaveChange.ForeSelectedColor = color;

            this.uBtnFactory.ForeHoverColor = color;
            this.uBtnFactory.ForePressColor = color;
            this.uBtnFactory.ForeSelectedColor = color;

            this.ubtnEditName.ForeHoverColor = color;
            this.ubtnEditName.ForeSelectedColor= color;
            this.ubtnEditName.ForePressColor= color;


        }

        private void textBoxEditName_TextChanged(object sender, EventArgs e)
        {

        }

        private void SettingUI_Load(object sender, EventArgs e)
        {
            SetDeafultUserName();
        }


        private void SetDeafultUserName()
        {
            string userName = new DatabaseUnit().GetuserName();
            if (userName == "未写入userName")
            {
                textBoxEditName.Visible = true;
                ubtnEditName.Visible = true;
                labelUserName.Text = "请输入用户名：";
            }
            else
            {
                textBoxEditName.Visible = false;
                ubtnEditName.Visible = false;
                labelUserName.Text = String.Format("当前用户名：{0}  （单击修改）", userName);
            }
        }

        private void labelUserName_Click(object sender, EventArgs e)
        {
            textBoxEditName.Visible = true;
            ubtnEditName.Visible = true;
        }

        private void ubtnEditName_Click(object sender, EventArgs e)
        {
            string inputUserName = textBoxEditName.Text;
            if (inputUserName.Length > 1&& "罗玉龙王雪玲刘益宏彭育欢朱庆霞李小琴userName张李张  李".Contains(inputUserName))
            {
                new DatabaseUnit().UpdateUserName(inputUserName);
                this.mainBtnUserName.Text = inputUserName;
                message.ShowInfoDialog("提示",$"新用户名“{inputUserName}”已保存!",UIStyle.LightRed,false);
            }
            else
            {
                message.ShowErrorDialog("字符非法!",UIStyle.Red);
            }


            SetDeafultUserName();
        }
        private void ModifyDataBase()
        {
            DataTable dt = new DataTable();
            m_dbConnection.Open();
            adapter = new SQLiteDataAdapter("select id,Key,chief1,chief2,chief3,chief4,chief5,chief6,chief7,chief8,chief9,chief10 from diagnose", m_dbConnection);
            this.builder = new SQLiteCommandBuilder(adapter);
            adapter.Fill(ds, "ST");
            uiDataGridViewDesktop.DataSource = ds.Tables[0];
            uiDataGridViewDesktop.Columns[0].Width = 50;
            uiDataGridViewDesktop.Columns[1].Width = 150;
        }
        private void OpenFactoryrForm(Form childForm)
        {
            if (currentChild != null)
            {
                currentChild.Close();
            }
            currentChild = childForm;
            childForm.TopLevel = true;
            childForm.BringToFront();
            childForm.Show();
        }

        private void uBtnSaveChange_Click(object sender, EventArgs e)
        {
            adapter.Update(ds, "ST");
            //数据刷新
            ds.Tables["ST"].Clear();
            adapter.Fill(ds, "ST");
            message.ShowInfoDialog("提示","修改已成功保存。",UIStyle.LightRed,false);
        }

        private void uBtnHelp_Click(object sender, EventArgs e)
        {
            string message = string.Format("增加随机种子说明:\r1.只需要在下列表格中修改（或增加）,点击保存即可。\r2.每一个关键词限制10个随机主诉种子。");
            message += "\r--------------------------\r";
            message += string.Format("增加关键词说明:\r1.只需要在下列表格中修改（或增加）,点击保存即可。\r2.每一个关键词限制10个随机主诉种子。");
            
            this.message.ShowInfoDialog("提示", message,  UIStyle.LightRed,false);
        }

        private void uBtnFactory_Click(object sender, EventArgs e)
        {
            OpenFactoryrForm(new ChangeDocx(color));
        }
    }
}
