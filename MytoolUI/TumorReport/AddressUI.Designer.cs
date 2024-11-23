namespace MytoolUI
{
    partial class AddressUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uiTableLayoutPanel1 = new Sunny.UI.UITableLayoutPanel();
            this.uiLabelProvince = new Sunny.UI.UILabel();
            this.uiComboboxCity = new Sunny.UI.UIComboboxEx();
            this.uiComboboxDistrict = new Sunny.UI.UIComboboxEx();
            this.uiComboboxTown = new Sunny.UI.UIComboboxEx();
            this.uiTextBoxHome = new Sunny.UI.UITextBox();
            this.uiComboboxProvince = new Sunny.UI.UIComboboxEx();
            this.uiLabelCity = new Sunny.UI.UILabel();
            this.uiLabelDistrict = new Sunny.UI.UILabel();
            this.uiLabelTwon = new Sunny.UI.UILabel();
            this.uiLabelHome = new Sunny.UI.UILabel();
            this.uiTableLayoutPanel2 = new Sunny.UI.UITableLayoutPanel();
            this.uiLabelName = new Sunny.UI.UILabel();
            this.uiLabelCurrentAddress = new Sunny.UI.UILabel();
            this.uiLine1 = new Sunny.UI.UILine();
            this.uiPanelDesktop = new Sunny.UI.UIPanel();
            this.uiLine2 = new Sunny.UI.UILine();
            this.tableLayoutPanelButtons = new System.Windows.Forms.TableLayoutPanel();
            this.uBtnClose = new Sunny.UI.UISymbolButton();
            this.uBtnOk = new Sunny.UI.UISymbolButton();
            this.uiTableLayoutPanel1.SuspendLayout();
            this.uiTableLayoutPanel2.SuspendLayout();
            this.uiPanelDesktop.SuspendLayout();
            this.tableLayoutPanelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiTableLayoutPanel1
            // 
            this.uiTableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.uiTableLayoutPanel1.ColumnCount = 5;
            this.uiTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.34025F));
            this.uiTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.34025F));
            this.uiTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.34025F));
            this.uiTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.74395F));
            this.uiTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.2353F));
            this.uiTableLayoutPanel1.Controls.Add(this.uiLabelProvince, 0, 0);
            this.uiTableLayoutPanel1.Controls.Add(this.uiComboboxCity, 1, 1);
            this.uiTableLayoutPanel1.Controls.Add(this.uiComboboxDistrict, 2, 1);
            this.uiTableLayoutPanel1.Controls.Add(this.uiComboboxTown, 3, 1);
            this.uiTableLayoutPanel1.Controls.Add(this.uiTextBoxHome, 4, 1);
            this.uiTableLayoutPanel1.Controls.Add(this.uiComboboxProvince, 0, 1);
            this.uiTableLayoutPanel1.Controls.Add(this.uiLabelCity, 1, 0);
            this.uiTableLayoutPanel1.Controls.Add(this.uiLabelDistrict, 2, 0);
            this.uiTableLayoutPanel1.Controls.Add(this.uiLabelTwon, 3, 0);
            this.uiTableLayoutPanel1.Controls.Add(this.uiLabelHome, 4, 0);
            this.uiTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiTableLayoutPanel1.Location = new System.Drawing.Point(10, 103);
            this.uiTableLayoutPanel1.Name = "uiTableLayoutPanel1";
            this.uiTableLayoutPanel1.RowCount = 2;
            this.uiTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uiTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uiTableLayoutPanel1.Size = new System.Drawing.Size(773, 76);
            this.uiTableLayoutPanel1.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiTableLayoutPanel1.TabIndex = 0;
            this.uiTableLayoutPanel1.TagString = null;
            // 
            // uiLabelProvince
            // 
            this.uiLabelProvince.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLabelProvince.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabelProvince.Location = new System.Drawing.Point(3, 0);
            this.uiLabelProvince.Name = "uiLabelProvince";
            this.uiLabelProvince.Size = new System.Drawing.Size(112, 38);
            this.uiLabelProvince.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiLabelProvince.TabIndex = 4;
            this.uiLabelProvince.Text = "省";
            this.uiLabelProvince.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // uiComboboxCity
            // 
            this.uiComboboxCity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.uiComboboxCity.BackColor = System.Drawing.Color.White;
            this.uiComboboxCity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.uiComboboxCity.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiComboboxCity.FormattingEnabled = true;
            this.uiComboboxCity.ItemHeight = 26;
            this.uiComboboxCity.ItemSelectBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiComboboxCity.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.uiComboboxCity.Location = new System.Drawing.Point(121, 41);
            this.uiComboboxCity.Name = "uiComboboxCity";
            this.uiComboboxCity.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiComboboxCity.Size = new System.Drawing.Size(112, 32);
            this.uiComboboxCity.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiComboboxCity.StyleCustomMode = true;
            this.uiComboboxCity.TabIndex = 1;
            this.uiComboboxCity.TextUpdate += new System.EventHandler(this.uiComboboxCity_TextUpdate);
            this.uiComboboxCity.SelectedValueChanged += new System.EventHandler(this.uiComboboxCity_SelectedValueChanged);
            // 
            // uiComboboxDistrict
            // 
            this.uiComboboxDistrict.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.uiComboboxDistrict.BackColor = System.Drawing.Color.White;
            this.uiComboboxDistrict.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.uiComboboxDistrict.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiComboboxDistrict.FormattingEnabled = true;
            this.uiComboboxDistrict.ItemHeight = 26;
            this.uiComboboxDistrict.ItemSelectBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiComboboxDistrict.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.uiComboboxDistrict.Location = new System.Drawing.Point(239, 41);
            this.uiComboboxDistrict.Name = "uiComboboxDistrict";
            this.uiComboboxDistrict.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiComboboxDistrict.Size = new System.Drawing.Size(112, 32);
            this.uiComboboxDistrict.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiComboboxDistrict.StyleCustomMode = true;
            this.uiComboboxDistrict.TabIndex = 2;
            this.uiComboboxDistrict.TextUpdate += new System.EventHandler(this.uiComboboxDistrict_TextUpdate);
            this.uiComboboxDistrict.SelectedValueChanged += new System.EventHandler(this.uiComboboxDistrict_SelectedValueChanged);
            // 
            // uiComboboxTown
            // 
            this.uiComboboxTown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.uiComboboxTown.BackColor = System.Drawing.Color.White;
            this.uiComboboxTown.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.uiComboboxTown.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiComboboxTown.FormattingEnabled = true;
            this.uiComboboxTown.ItemHeight = 26;
            this.uiComboboxTown.ItemSelectBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiComboboxTown.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.uiComboboxTown.Location = new System.Drawing.Point(357, 41);
            this.uiComboboxTown.Name = "uiComboboxTown";
            this.uiComboboxTown.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiComboboxTown.Size = new System.Drawing.Size(115, 32);
            this.uiComboboxTown.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiComboboxTown.StyleCustomMode = true;
            this.uiComboboxTown.TabIndex = 3;
            this.uiComboboxTown.TextUpdate += new System.EventHandler(this.uiComboboxTown_TextUpdate);
            // 
            // uiTextBoxHome
            // 
            this.uiTextBoxHome.ButtonSymbol = 61761;
            this.uiTextBoxHome.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBoxHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTextBoxHome.FillColor = System.Drawing.Color.White;
            this.uiTextBoxHome.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTextBoxHome.Location = new System.Drawing.Point(479, 43);
            this.uiTextBoxHome.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTextBoxHome.Maximum = 2147483647D;
            this.uiTextBoxHome.Minimum = -2147483648D;
            this.uiTextBoxHome.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTextBoxHome.Name = "uiTextBoxHome";
            this.uiTextBoxHome.Radius = 0;
            this.uiTextBoxHome.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiTextBoxHome.Size = new System.Drawing.Size(290, 31);
            this.uiTextBoxHome.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiTextBoxHome.StyleCustomMode = true;
            this.uiTextBoxHome.TabIndex = 4;
            this.uiTextBoxHome.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiComboboxProvince
            // 
            this.uiComboboxProvince.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.uiComboboxProvince.BackColor = System.Drawing.Color.White;
            this.uiComboboxProvince.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.uiComboboxProvince.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiComboboxProvince.FormattingEnabled = true;
            this.uiComboboxProvince.ItemHeight = 26;
            this.uiComboboxProvince.ItemSelectBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiComboboxProvince.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.uiComboboxProvince.Location = new System.Drawing.Point(3, 41);
            this.uiComboboxProvince.Name = "uiComboboxProvince";
            this.uiComboboxProvince.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiComboboxProvince.Size = new System.Drawing.Size(112, 32);
            this.uiComboboxProvince.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiComboboxProvince.StyleCustomMode = true;
            this.uiComboboxProvince.TabIndex = 0;
            this.uiComboboxProvince.TextUpdate += new System.EventHandler(this.uiComboboxProvice_TextUpdate);
            this.uiComboboxProvince.SelectedValueChanged += new System.EventHandler(this.uiComboboxProvince_SelectedValueChanged);
            // 
            // uiLabelCity
            // 
            this.uiLabelCity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLabelCity.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabelCity.Location = new System.Drawing.Point(121, 0);
            this.uiLabelCity.Name = "uiLabelCity";
            this.uiLabelCity.Size = new System.Drawing.Size(112, 38);
            this.uiLabelCity.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiLabelCity.TabIndex = 4;
            this.uiLabelCity.Text = "市";
            this.uiLabelCity.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // uiLabelDistrict
            // 
            this.uiLabelDistrict.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLabelDistrict.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabelDistrict.Location = new System.Drawing.Point(239, 0);
            this.uiLabelDistrict.Name = "uiLabelDistrict";
            this.uiLabelDistrict.Size = new System.Drawing.Size(112, 38);
            this.uiLabelDistrict.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiLabelDistrict.TabIndex = 4;
            this.uiLabelDistrict.Text = "区";
            this.uiLabelDistrict.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // uiLabelTwon
            // 
            this.uiLabelTwon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLabelTwon.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabelTwon.Location = new System.Drawing.Point(357, 0);
            this.uiLabelTwon.Name = "uiLabelTwon";
            this.uiLabelTwon.Size = new System.Drawing.Size(115, 38);
            this.uiLabelTwon.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiLabelTwon.TabIndex = 4;
            this.uiLabelTwon.Text = "街道";
            this.uiLabelTwon.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // uiLabelHome
            // 
            this.uiLabelHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLabelHome.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabelHome.Location = new System.Drawing.Point(478, 0);
            this.uiLabelHome.Name = "uiLabelHome";
            this.uiLabelHome.Size = new System.Drawing.Size(292, 38);
            this.uiLabelHome.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiLabelHome.TabIndex = 4;
            this.uiLabelHome.Text = "门牌号";
            this.uiLabelHome.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // uiTableLayoutPanel2
            // 
            this.uiTableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.uiTableLayoutPanel2.ColumnCount = 2;
            this.uiTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uiTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uiTableLayoutPanel2.Controls.Add(this.uiLabelName, 0, 0);
            this.uiTableLayoutPanel2.Controls.Add(this.uiLabelCurrentAddress, 0, 1);
            this.uiTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiTableLayoutPanel2.Location = new System.Drawing.Point(10, 10);
            this.uiTableLayoutPanel2.Name = "uiTableLayoutPanel2";
            this.uiTableLayoutPanel2.RowCount = 2;
            this.uiTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uiTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uiTableLayoutPanel2.Size = new System.Drawing.Size(773, 57);
            this.uiTableLayoutPanel2.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiTableLayoutPanel2.TabIndex = 4;
            this.uiTableLayoutPanel2.TagString = null;
            // 
            // uiLabelName
            // 
            this.uiLabelName.BackColor = System.Drawing.Color.Transparent;
            this.uiTableLayoutPanel2.SetColumnSpan(this.uiLabelName, 2);
            this.uiLabelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLabelName.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabelName.Location = new System.Drawing.Point(3, 0);
            this.uiLabelName.Name = "uiLabelName";
            this.uiLabelName.Size = new System.Drawing.Size(767, 28);
            this.uiLabelName.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiLabelName.TabIndex = 0;
            this.uiLabelName.Text = "姓名:";
            this.uiLabelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabelCurrentAddress
            // 
            this.uiLabelCurrentAddress.BackColor = System.Drawing.Color.Transparent;
            this.uiTableLayoutPanel2.SetColumnSpan(this.uiLabelCurrentAddress, 2);
            this.uiLabelCurrentAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLabelCurrentAddress.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabelCurrentAddress.Location = new System.Drawing.Point(3, 28);
            this.uiLabelCurrentAddress.Name = "uiLabelCurrentAddress";
            this.uiLabelCurrentAddress.Size = new System.Drawing.Size(767, 29);
            this.uiLabelCurrentAddress.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiLabelCurrentAddress.TabIndex = 0;
            this.uiLabelCurrentAddress.Text = "当前地址:";
            this.uiLabelCurrentAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLine1
            // 
            this.uiLine1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiLine1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.uiLine1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLine1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiLine1.Location = new System.Drawing.Point(10, 67);
            this.uiLine1.MinimumSize = new System.Drawing.Size(2, 2);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.uiLine1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uiLine1.Size = new System.Drawing.Size(773, 36);
            this.uiLine1.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiLine1.TabIndex = 5;
            this.uiLine1.Text = "更新地址";
            // 
            // uiPanelDesktop
            // 
            this.uiPanelDesktop.Controls.Add(this.uiLine2);
            this.uiPanelDesktop.Controls.Add(this.tableLayoutPanelButtons);
            this.uiPanelDesktop.Controls.Add(this.uiTableLayoutPanel1);
            this.uiPanelDesktop.Controls.Add(this.uiLine1);
            this.uiPanelDesktop.Controls.Add(this.uiTableLayoutPanel2);
            this.uiPanelDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanelDesktop.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.uiPanelDesktop.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanelDesktop.Location = new System.Drawing.Point(0, 35);
            this.uiPanelDesktop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanelDesktop.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanelDesktop.Name = "uiPanelDesktop";
            this.uiPanelDesktop.Padding = new System.Windows.Forms.Padding(10);
            this.uiPanelDesktop.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiPanelDesktop.Size = new System.Drawing.Size(793, 297);
            this.uiPanelDesktop.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiPanelDesktop.StyleCustomMode = true;
            this.uiPanelDesktop.TabIndex = 0;
            this.uiPanelDesktop.Text = null;
            this.uiPanelDesktop.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiLine2
            // 
            this.uiLine2.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiLine2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.uiLine2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLine2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiLine2.Location = new System.Drawing.Point(10, 179);
            this.uiLine2.MinimumSize = new System.Drawing.Size(2, 2);
            this.uiLine2.Name = "uiLine2";
            this.uiLine2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.uiLine2.Size = new System.Drawing.Size(773, 39);
            this.uiLine2.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiLine2.TabIndex = 7;
            // 
            // tableLayoutPanelButtons
            // 
            this.tableLayoutPanelButtons.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelButtons.ColumnCount = 2;
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelButtons.Controls.Add(this.uBtnClose, 1, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.uBtnOk, 0, 0);
            this.tableLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanelButtons.Location = new System.Drawing.Point(10, 248);
            this.tableLayoutPanelButtons.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            this.tableLayoutPanelButtons.RowCount = 1;
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelButtons.Size = new System.Drawing.Size(773, 39);
            this.tableLayoutPanelButtons.TabIndex = 6;
            // 
            // uBtnClose
            // 
            this.uBtnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uBtnClose.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(249)))));
            this.uBtnClose.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(226)))), ((int)(((byte)(137)))));
            this.uBtnClose.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.uBtnClose.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.uBtnClose.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uBtnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uBtnClose.ForeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uBtnClose.ForePressColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uBtnClose.ForeSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uBtnClose.Location = new System.Drawing.Point(396, 0);
            this.uBtnClose.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.uBtnClose.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnClose.Name = "uBtnClose";
            this.uBtnClose.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uBtnClose.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(201)))), ((int)(((byte)(88)))));
            this.uBtnClose.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.uBtnClose.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.uBtnClose.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uBtnClose.Size = new System.Drawing.Size(377, 39);
            this.uBtnClose.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uBtnClose.StyleCustomMode = true;
            this.uBtnClose.Symbol = 61453;
            this.uBtnClose.SymbolSize = 20;
            this.uBtnClose.TabIndex = 1;
            this.uBtnClose.Text = "取消";
            this.uBtnClose.Click += new System.EventHandler(this.uBtnClose_Click);
            // 
            // uBtnOk
            // 
            this.uBtnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uBtnOk.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(249)))));
            this.uBtnOk.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(226)))), ((int)(((byte)(137)))));
            this.uBtnOk.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.uBtnOk.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.uBtnOk.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uBtnOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uBtnOk.ForeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uBtnOk.ForePressColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uBtnOk.ForeSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uBtnOk.Location = new System.Drawing.Point(0, 0);
            this.uBtnOk.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.uBtnOk.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnOk.Name = "uBtnOk";
            this.uBtnOk.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uBtnOk.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(201)))), ((int)(((byte)(88)))));
            this.uBtnOk.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.uBtnOk.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.uBtnOk.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uBtnOk.Size = new System.Drawing.Size(376, 39);
            this.uBtnOk.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uBtnOk.StyleCustomMode = true;
            this.uBtnOk.SymbolSize = 20;
            this.uBtnOk.TabIndex = 0;
            this.uBtnOk.Text = "确定";
            this.uBtnOk.Click += new System.EventHandler(this.uBtnOk_Click);
            // 
            // AddressUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.ClientSize = new System.Drawing.Size(793, 332);
            this.Controls.Add(this.uiPanelDesktop);
            this.Name = "AddressUI";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "地址选择器";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.Load += new System.EventHandler(this.AddressUI_Load);
            this.uiTableLayoutPanel1.ResumeLayout(false);
            this.uiTableLayoutPanel2.ResumeLayout(false);
            this.uiPanelDesktop.ResumeLayout(false);
            this.tableLayoutPanelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITableLayoutPanel uiTableLayoutPanel1;
        private Sunny.UI.UIComboboxEx uiComboboxCity;
        private Sunny.UI.UIComboboxEx uiComboboxDistrict;
        private Sunny.UI.UIComboboxEx uiComboboxTown;
        private Sunny.UI.UITextBox uiTextBoxHome;
        private Sunny.UI.UIComboboxEx uiComboboxProvince;
        private Sunny.UI.UILabel uiLabelProvince;
        private Sunny.UI.UILabel uiLabelCity;
        private Sunny.UI.UILabel uiLabelDistrict;
        private Sunny.UI.UILabel uiLabelTwon;
        private Sunny.UI.UILabel uiLabelHome;
        private Sunny.UI.UITableLayoutPanel uiTableLayoutPanel2;
        private Sunny.UI.UILabel uiLabelName;
        private Sunny.UI.UILabel uiLabelCurrentAddress;
        private Sunny.UI.UILine uiLine1;
        private Sunny.UI.UIPanel uiPanelDesktop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
        private Sunny.UI.UISymbolButton uBtnClose;
        private Sunny.UI.UISymbolButton uBtnOk;
        private Sunny.UI.UILine uiLine2;
    }
}