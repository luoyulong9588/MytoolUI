namespace MytoolUI
{
    partial class DeathInformationUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeathInformationUI));
            this.uiTableLayoutPanelDesktop = new Sunny.UI.UITableLayoutPanel();
            this.uiLabelName = new Sunny.UI.UILabel();
            this.uiLabelDeadTime = new Sunny.UI.UILabel();
            this.uiLabelDeadReason = new Sunny.UI.UILabel();
            this.tableLayoutPanelButtons = new System.Windows.Forms.TableLayoutPanel();
            this.uBtnClose = new Sunny.UI.UISymbolButton();
            this.uBtnOk = new Sunny.UI.UISymbolButton();
            this.uiDatetimePickerDeathTime = new Sunny.UI.UIDatetimePicker();
            this.uiComboboxDeathReason = new Sunny.UI.UIComboboxEx();
            this.uiComboboxName = new Sunny.UI.UIComboboxEx();
            this.uiComboboxDeathIcd10 = new Sunny.UI.UIComboboxEx();
            this.uiComboboxDeathIcd10Num = new Sunny.UI.UIComboboxEx();
            this.uiLabelDeathIcd10Num = new Sunny.UI.UILabel();
            this.uiLabelDeathIcd10 = new Sunny.UI.UILabel();
            this.uiTableLayoutPanelDesktop.SuspendLayout();
            this.tableLayoutPanelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiTableLayoutPanelDesktop
            // 
            this.uiTableLayoutPanelDesktop.ColumnCount = 4;
            this.uiTableLayoutPanelDesktop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.97368F));
            this.uiTableLayoutPanelDesktop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.89474F));
            this.uiTableLayoutPanelDesktop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.10526F));
            this.uiTableLayoutPanelDesktop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.28947F));
            this.uiTableLayoutPanelDesktop.Controls.Add(this.uiLabelName, 0, 0);
            this.uiTableLayoutPanelDesktop.Controls.Add(this.uiLabelDeadTime, 0, 1);
            this.uiTableLayoutPanelDesktop.Controls.Add(this.uiLabelDeadReason, 2, 1);
            this.uiTableLayoutPanelDesktop.Controls.Add(this.tableLayoutPanelButtons, 0, 3);
            this.uiTableLayoutPanelDesktop.Controls.Add(this.uiDatetimePickerDeathTime, 1, 1);
            this.uiTableLayoutPanelDesktop.Controls.Add(this.uiComboboxDeathReason, 3, 1);
            this.uiTableLayoutPanelDesktop.Controls.Add(this.uiComboboxName, 1, 0);
            this.uiTableLayoutPanelDesktop.Controls.Add(this.uiComboboxDeathIcd10, 1, 2);
            this.uiTableLayoutPanelDesktop.Controls.Add(this.uiComboboxDeathIcd10Num, 3, 2);
            this.uiTableLayoutPanelDesktop.Controls.Add(this.uiLabelDeathIcd10Num, 2, 2);
            this.uiTableLayoutPanelDesktop.Controls.Add(this.uiLabelDeathIcd10, 0, 2);
            this.uiTableLayoutPanelDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTableLayoutPanelDesktop.Location = new System.Drawing.Point(0, 35);
            this.uiTableLayoutPanelDesktop.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.uiTableLayoutPanelDesktop.Name = "uiTableLayoutPanelDesktop";
            this.uiTableLayoutPanelDesktop.Padding = new System.Windows.Forms.Padding(20, 20, 20, 0);
            this.uiTableLayoutPanelDesktop.RowCount = 4;
            this.uiTableLayoutPanelDesktop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.uiTableLayoutPanelDesktop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.uiTableLayoutPanelDesktop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.uiTableLayoutPanelDesktop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.uiTableLayoutPanelDesktop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.uiTableLayoutPanelDesktop.Size = new System.Drawing.Size(793, 297);
            this.uiTableLayoutPanelDesktop.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiTableLayoutPanelDesktop.TabIndex = 0;
            this.uiTableLayoutPanelDesktop.TagString = null;
            // 
            // uiLabelName
            // 
            this.uiLabelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLabelName.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabelName.Location = new System.Drawing.Point(23, 20);
            this.uiLabelName.Name = "uiLabelName";
            this.uiLabelName.Size = new System.Drawing.Size(121, 69);
            this.uiLabelName.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiLabelName.TabIndex = 0;
            this.uiLabelName.Text = "姓名:";
            this.uiLabelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiLabelDeadTime
            // 
            this.uiLabelDeadTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLabelDeadTime.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabelDeadTime.Location = new System.Drawing.Point(23, 89);
            this.uiLabelDeadTime.Name = "uiLabelDeadTime";
            this.uiLabelDeadTime.Size = new System.Drawing.Size(121, 69);
            this.uiLabelDeadTime.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiLabelDeadTime.TabIndex = 0;
            this.uiLabelDeadTime.Text = "死亡时间:";
            this.uiLabelDeadTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiLabelDeadReason
            // 
            this.uiLabelDeadReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLabelDeadReason.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabelDeadReason.Location = new System.Drawing.Point(397, 89);
            this.uiLabelDeadReason.Name = "uiLabelDeadReason";
            this.uiLabelDeadReason.Size = new System.Drawing.Size(122, 69);
            this.uiLabelDeadReason.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiLabelDeadReason.TabIndex = 0;
            this.uiLabelDeadReason.Text = "死亡原因:";
            this.uiLabelDeadReason.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanelButtons
            // 
            this.tableLayoutPanelButtons.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelButtons.ColumnCount = 2;
            this.uiTableLayoutPanelDesktop.SetColumnSpan(this.tableLayoutPanelButtons, 4);
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelButtons.Controls.Add(this.uBtnClose, 1, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.uBtnOk, 0, 0);
            this.tableLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelButtons.Location = new System.Drawing.Point(23, 229);
            this.tableLayoutPanelButtons.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            this.tableLayoutPanelButtons.RowCount = 1;
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelButtons.Size = new System.Drawing.Size(747, 66);
            this.tableLayoutPanelButtons.TabIndex = 7;
            // 
            // uBtnClose
            // 
            this.uBtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.uBtnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnClose.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(249)))));
            this.uBtnClose.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(226)))), ((int)(((byte)(137)))));
            this.uBtnClose.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.uBtnClose.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.uBtnClose.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uBtnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uBtnClose.ForeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uBtnClose.ForePressColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uBtnClose.ForeSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uBtnClose.Location = new System.Drawing.Point(383, 13);
            this.uBtnClose.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.uBtnClose.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnClose.Name = "uBtnClose";
            this.uBtnClose.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uBtnClose.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(201)))), ((int)(((byte)(88)))));
            this.uBtnClose.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.uBtnClose.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.uBtnClose.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uBtnClose.Size = new System.Drawing.Size(364, 39);
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
            this.uBtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.uBtnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnOk.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(249)))));
            this.uBtnOk.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(226)))), ((int)(((byte)(137)))));
            this.uBtnOk.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.uBtnOk.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.uBtnOk.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uBtnOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uBtnOk.ForeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uBtnOk.ForePressColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uBtnOk.ForeSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uBtnOk.Location = new System.Drawing.Point(0, 13);
            this.uBtnOk.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.uBtnOk.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnOk.Name = "uBtnOk";
            this.uBtnOk.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uBtnOk.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(201)))), ((int)(((byte)(88)))));
            this.uBtnOk.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.uBtnOk.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.uBtnOk.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uBtnOk.Size = new System.Drawing.Size(363, 39);
            this.uBtnOk.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uBtnOk.StyleCustomMode = true;
            this.uBtnOk.SymbolSize = 20;
            this.uBtnOk.TabIndex = 0;
            this.uBtnOk.Text = "确定";
            this.uBtnOk.Click += new System.EventHandler(this.uBtnOk_Click);
            // 
            // uiDatetimePickerDeathTime
            // 
            this.uiDatetimePickerDeathTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.uiDatetimePickerDeathTime.DateFormat = "yyyy-MM-dd";
            this.uiDatetimePickerDeathTime.FillColor = System.Drawing.Color.White;
            this.uiDatetimePickerDeathTime.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiDatetimePickerDeathTime.Location = new System.Drawing.Point(151, 108);
            this.uiDatetimePickerDeathTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiDatetimePickerDeathTime.MaxLength = 10;
            this.uiDatetimePickerDeathTime.MinimumSize = new System.Drawing.Size(63, 0);
            this.uiDatetimePickerDeathTime.Name = "uiDatetimePickerDeathTime";
            this.uiDatetimePickerDeathTime.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.uiDatetimePickerDeathTime.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiDatetimePickerDeathTime.Size = new System.Drawing.Size(239, 31);
            this.uiDatetimePickerDeathTime.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiDatetimePickerDeathTime.SymbolDropDown = 61555;
            this.uiDatetimePickerDeathTime.SymbolNormal = 61555;
            this.uiDatetimePickerDeathTime.TabIndex = 0;
            this.uiDatetimePickerDeathTime.Text = "2021-12-18";
            this.uiDatetimePickerDeathTime.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiDatetimePickerDeathTime.Value = new System.DateTime(2021, 12, 18, 0, 0, 0, 0);
            // 
            // uiComboboxDeathReason
            // 
            this.uiComboboxDeathReason.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.uiComboboxDeathReason.BackColor = System.Drawing.Color.White;
            this.uiComboboxDeathReason.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.uiComboboxDeathReason.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiComboboxDeathReason.FormattingEnabled = true;
            this.uiComboboxDeathReason.Items.AddRange(new object[] {
            "1.肿瘤",
            "2.其他疾病",
            "3.不详"});
            this.uiComboboxDeathReason.ItemSelectBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiComboboxDeathReason.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.uiComboboxDeathReason.Location = new System.Drawing.Point(525, 107);
            this.uiComboboxDeathReason.Name = "uiComboboxDeathReason";
            this.uiComboboxDeathReason.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiComboboxDeathReason.Size = new System.Drawing.Size(245, 32);
            this.uiComboboxDeathReason.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiComboboxDeathReason.TabIndex = 1;
            this.uiComboboxDeathReason.Text = "选择死亡原因";
            this.uiComboboxDeathReason.SelectedValueChanged += new System.EventHandler(this.uiComboboxDeathReason_SelectedValueChanged);
            // 
            // uiComboboxName
            // 
            this.uiComboboxName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.uiComboboxName.BackColor = System.Drawing.Color.White;
            this.uiComboboxName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.uiComboboxName.Enabled = false;
            this.uiComboboxName.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiComboboxName.FormattingEnabled = true;
            this.uiComboboxName.ItemSelectBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiComboboxName.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.uiComboboxName.Location = new System.Drawing.Point(150, 38);
            this.uiComboboxName.Name = "uiComboboxName";
            this.uiComboboxName.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiComboboxName.Size = new System.Drawing.Size(241, 32);
            this.uiComboboxName.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiComboboxName.TabIndex = 10;
            // 
            // uiComboboxDeathIcd10
            // 
            this.uiComboboxDeathIcd10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.uiComboboxDeathIcd10.BackColor = System.Drawing.Color.White;
            this.uiComboboxDeathIcd10.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.uiComboboxDeathIcd10.Enabled = false;
            this.uiComboboxDeathIcd10.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiComboboxDeathIcd10.FormattingEnabled = true;
            this.uiComboboxDeathIcd10.ItemSelectBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiComboboxDeathIcd10.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.uiComboboxDeathIcd10.Location = new System.Drawing.Point(150, 176);
            this.uiComboboxDeathIcd10.Name = "uiComboboxDeathIcd10";
            this.uiComboboxDeathIcd10.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiComboboxDeathIcd10.Size = new System.Drawing.Size(241, 32);
            this.uiComboboxDeathIcd10.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiComboboxDeathIcd10.TabIndex = 2;
            this.uiComboboxDeathIcd10.TextUpdate += new System.EventHandler(this.uiComboboxDeathIcd10_TextUpdate);
            this.uiComboboxDeathIcd10.SelectedValueChanged += new System.EventHandler(this.uiComboboxDeathIcd10_SelectedValueChanged);
            // 
            // uiComboboxDeathIcd10Num
            // 
            this.uiComboboxDeathIcd10Num.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.uiComboboxDeathIcd10Num.BackColor = System.Drawing.Color.White;
            this.uiComboboxDeathIcd10Num.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.uiComboboxDeathIcd10Num.Enabled = false;
            this.uiComboboxDeathIcd10Num.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiComboboxDeathIcd10Num.FormattingEnabled = true;
            this.uiComboboxDeathIcd10Num.ItemSelectBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiComboboxDeathIcd10Num.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.uiComboboxDeathIcd10Num.Location = new System.Drawing.Point(525, 176);
            this.uiComboboxDeathIcd10Num.Name = "uiComboboxDeathIcd10Num";
            this.uiComboboxDeathIcd10Num.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiComboboxDeathIcd10Num.Size = new System.Drawing.Size(245, 32);
            this.uiComboboxDeathIcd10Num.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiComboboxDeathIcd10Num.TabIndex = 3;
            // 
            // uiLabelDeathIcd10Num
            // 
            this.uiLabelDeathIcd10Num.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabelDeathIcd10Num.Location = new System.Drawing.Point(397, 158);
            this.uiLabelDeathIcd10Num.Name = "uiLabelDeathIcd10Num";
            this.uiLabelDeathIcd10Num.Size = new System.Drawing.Size(122, 63);
            this.uiLabelDeathIcd10Num.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiLabelDeathIcd10Num.TabIndex = 0;
            this.uiLabelDeathIcd10Num.Text = "死亡ICD-10:";
            this.uiLabelDeathIcd10Num.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiLabelDeathIcd10
            // 
            this.uiLabelDeathIcd10.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabelDeathIcd10.Location = new System.Drawing.Point(23, 158);
            this.uiLabelDeathIcd10.Name = "uiLabelDeathIcd10";
            this.uiLabelDeathIcd10.Size = new System.Drawing.Size(121, 63);
            this.uiLabelDeathIcd10.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.uiLabelDeathIcd10.TabIndex = 0;
            this.uiLabelDeathIcd10.Text = "死亡ICD名称:";
            this.uiLabelDeathIcd10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DeathInformationUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(793, 332);
            this.Controls.Add(this.uiTableLayoutPanelDesktop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DeathInformationUI";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.ShowIcon = true;
            this.ShowRadius = false;
            this.ShowShadow = true;
            this.Style = Sunny.UI.UIStyle.Office2010Silver;
            this.Text = "死亡原因更正";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.TopMost = true;
            this.uiTableLayoutPanelDesktop.ResumeLayout(false);
            this.tableLayoutPanelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITableLayoutPanel uiTableLayoutPanelDesktop;
        private Sunny.UI.UILabel uiLabelName;
        private Sunny.UI.UILabel uiLabelDeadTime;
        private Sunny.UI.UILabel uiLabelDeadReason;
        private Sunny.UI.UILabel uiLabelDeathIcd10Num;
        private Sunny.UI.UILabel uiLabelDeathIcd10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
        private Sunny.UI.UISymbolButton uBtnClose;
        private Sunny.UI.UISymbolButton uBtnOk;
        private Sunny.UI.UIDatetimePicker uiDatetimePickerDeathTime;
        private Sunny.UI.UIComboboxEx uiComboboxDeathIcd10Num;
        private Sunny.UI.UIComboboxEx uiComboboxDeathIcd10;
        private Sunny.UI.UIComboboxEx uiComboboxDeathReason;
        private Sunny.UI.UIComboboxEx uiComboboxName;
    }
}