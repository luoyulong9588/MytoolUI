
namespace MytoolUI
{
    partial class ChronicDiseasesUI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChronicDiseasesUI));
            this.tablePanelOpenFile = new System.Windows.Forms.TableLayoutPanel();
            this.labelFilename = new System.Windows.Forms.Label();
            this.textBoxFilepath = new Sunny.UI.UITextBox();
            this.uBtnBrowse = new Sunny.UI.UISymbolButton();
            this.tablePanelSelect = new System.Windows.Forms.TableLayoutPanel();
            this.labelSelectUser = new System.Windows.Forms.Label();
            this.comboBoxSelectUser = new Sunny.UI.UIComboboxEx();
            this.uBtnStart = new Sunny.UI.UISymbolButton();
            this.uiCheckBoxCheckAMI = new Sunny.UI.UICheckBox();
            this.ucheckBoxAlignstr = new Sunny.UI.UICheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.uBtnPrint = new Sunny.UI.UISymbolButton();
            this.uBtnClear = new Sunny.UI.UISymbolButton();
            this.uBtnOpendir = new Sunny.UI.UISymbolButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar1 = new Sunny.UI.UIProcessBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelOutMessage = new System.Windows.Forms.TableLayoutPanel();
            this.UIlabelCopd = new Sunny.UI.UISymbolLabel();
            this.UIlabelAMI = new Sunny.UI.UISymbolLabel();
            this.UIlabelApoplexy = new Sunny.UI.UISymbolLabel();
            this.uiListBoxCopd = new Sunny.UI.UIListBox();
            this.uiContextMenuStripChronic = new Sunny.UI.UIContextMenuStrip();
            this.Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.uiListBoxAmi = new Sunny.UI.UIListBox();
            this.uiListBoxApoplexy = new Sunny.UI.UIListBox();
            this.uiToolTipChronic = new Sunny.UI.UIToolTip(this.components);
            this.uiPanelDesktop = new Sunny.UI.UIPanel();
            this.tablePanelOpenFile.SuspendLayout();
            this.tablePanelSelect.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelOutMessage.SuspendLayout();
            this.uiContextMenuStripChronic.SuspendLayout();
            this.uiPanelDesktop.SuspendLayout();
            this.SuspendLayout();
            // 
            // tablePanelOpenFile
            // 
            this.tablePanelOpenFile.ColumnCount = 3;
            this.tablePanelOpenFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tablePanelOpenFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tablePanelOpenFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tablePanelOpenFile.Controls.Add(this.labelFilename, 0, 0);
            this.tablePanelOpenFile.Controls.Add(this.textBoxFilepath, 1, 0);
            this.tablePanelOpenFile.Controls.Add(this.uBtnBrowse, 2, 0);
            this.tablePanelOpenFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.tablePanelOpenFile.Location = new System.Drawing.Point(0, 0);
            this.tablePanelOpenFile.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tablePanelOpenFile.Name = "tablePanelOpenFile";
            this.tablePanelOpenFile.RowCount = 1;
            this.tablePanelOpenFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanelOpenFile.Size = new System.Drawing.Size(1794, 44);
            this.tablePanelOpenFile.TabIndex = 1;
            // 
            // labelFilename
            // 
            this.labelFilename.AutoSize = true;
            this.labelFilename.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelFilename.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelFilename.ForeColor = System.Drawing.Color.Black;
            this.labelFilename.Location = new System.Drawing.Point(201, 0);
            this.labelFilename.Name = "labelFilename";
            this.labelFilename.Size = new System.Drawing.Size(65, 44);
            this.labelFilename.TabIndex = 0;
            this.labelFilename.Text = "文件名:";
            this.labelFilename.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxFilepath
            // 
            this.textBoxFilepath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFilepath.ButtonSymbol = 61761;
            this.textBoxFilepath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxFilepath.FillColor = System.Drawing.Color.White;
            this.textBoxFilepath.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.textBoxFilepath.IconSize = 0;
            this.textBoxFilepath.Location = new System.Drawing.Point(273, 5);
            this.textBoxFilepath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxFilepath.Maximum = 2147483647D;
            this.textBoxFilepath.Minimum = -2147483648D;
            this.textBoxFilepath.MinimumSize = new System.Drawing.Size(1, 1);
            this.textBoxFilepath.Name = "textBoxFilepath";
            this.textBoxFilepath.Radius = 0;
            this.textBoxFilepath.Size = new System.Drawing.Size(1247, 34);
            this.textBoxFilepath.TabIndex = 3;
            this.textBoxFilepath.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uBtnBrowse
            // 
            this.uBtnBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnBrowse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uBtnBrowse.FillColor = System.Drawing.Color.White;
            this.uBtnBrowse.FillDisableColor = System.Drawing.Color.White;
            this.uBtnBrowse.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(236)))));
            this.uBtnBrowse.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(184)))), ((int)(((byte)(217)))));
            this.uBtnBrowse.FillSelectedColor = System.Drawing.Color.White;
            this.uBtnBrowse.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.uBtnBrowse.ForeColor = System.Drawing.Color.Black;
            this.uBtnBrowse.ForeHoverColor = System.Drawing.Color.Black;
            this.uBtnBrowse.ForePressColor = System.Drawing.Color.Black;
            this.uBtnBrowse.ForeSelectedColor = System.Drawing.Color.Black;
            this.uBtnBrowse.Location = new System.Drawing.Point(1527, 2);
            this.uBtnBrowse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.uBtnBrowse.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnBrowse.Name = "uBtnBrowse";
            this.uBtnBrowse.RectColor = System.Drawing.Color.White;
            this.uBtnBrowse.RectDisableColor = System.Drawing.Color.White;
            this.uBtnBrowse.RectHoverColor = System.Drawing.Color.White;
            this.uBtnBrowse.RectPressColor = System.Drawing.Color.White;
            this.uBtnBrowse.RectSelectedColor = System.Drawing.Color.White;
            this.uBtnBrowse.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uBtnBrowse.Size = new System.Drawing.Size(264, 40);
            this.uBtnBrowse.Style = Sunny.UI.UIStyle.Custom;
            this.uBtnBrowse.StyleCustomMode = true;
            this.uBtnBrowse.Symbol = 61717;
            this.uBtnBrowse.TabIndex = 4;
            this.uBtnBrowse.Text = "浏览";
            this.uiToolTipChronic.SetToolTip(this.uBtnBrowse, "点击打开文件");
            this.uBtnBrowse.Click += new System.EventHandler(this.uBtnBrowse_Click);
            // 
            // tablePanelSelect
            // 
            this.tablePanelSelect.ColumnCount = 6;
            this.tablePanelSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tablePanelSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tablePanelSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tablePanelSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tablePanelSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.39825F));
            this.tablePanelSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.98906F));
            this.tablePanelSelect.Controls.Add(this.labelSelectUser, 0, 0);
            this.tablePanelSelect.Controls.Add(this.comboBoxSelectUser, 1, 0);
            this.tablePanelSelect.Controls.Add(this.uBtnStart, 5, 0);
            this.tablePanelSelect.Controls.Add(this.uiCheckBoxCheckAMI, 3, 0);
            this.tablePanelSelect.Controls.Add(this.ucheckBoxAlignstr, 4, 0);
            this.tablePanelSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.tablePanelSelect.Location = new System.Drawing.Point(0, 44);
            this.tablePanelSelect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tablePanelSelect.Name = "tablePanelSelect";
            this.tablePanelSelect.RowCount = 1;
            this.tablePanelSelect.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelSelect.Size = new System.Drawing.Size(1794, 44);
            this.tablePanelSelect.TabIndex = 2;
            // 
            // labelSelectUser
            // 
            this.labelSelectUser.AutoSize = true;
            this.labelSelectUser.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelSelectUser.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelSelectUser.ForeColor = System.Drawing.Color.Black;
            this.labelSelectUser.Location = new System.Drawing.Point(200, 0);
            this.labelSelectUser.Name = "labelSelectUser";
            this.labelSelectUser.Size = new System.Drawing.Size(65, 44);
            this.labelSelectUser.TabIndex = 0;
            this.labelSelectUser.Text = "用户名:";
            this.labelSelectUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxSelectUser
            // 
            this.comboBoxSelectUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSelectUser.BackColor = System.Drawing.Color.White;
            this.comboBoxSelectUser.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxSelectUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectUser.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.comboBoxSelectUser.FormattingEnabled = true;
            this.comboBoxSelectUser.ItemHeight = 22;
            this.comboBoxSelectUser.Items.AddRange(new object[] {
            "罗玉龙",
            "李小琴",
            "彭育欢",
            "刘益宏",
            "朱庆霞",
            "张    李"});
            this.comboBoxSelectUser.Location = new System.Drawing.Point(272, 8);
            this.comboBoxSelectUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxSelectUser.Name = "comboBoxSelectUser";
            this.comboBoxSelectUser.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboBoxSelectUser.Size = new System.Drawing.Size(242, 28);
            this.comboBoxSelectUser.Style = Sunny.UI.UIStyle.Custom;
            this.comboBoxSelectUser.StyleCustomMode = true;
            this.comboBoxSelectUser.TabIndex = 6;
            // 
            // uBtnStart
            // 
            this.uBtnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uBtnStart.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.uBtnStart.Location = new System.Drawing.Point(1528, 2);
            this.uBtnStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.uBtnStart.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnStart.Name = "uBtnStart";
            this.uBtnStart.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uBtnStart.Size = new System.Drawing.Size(263, 40);
            this.uBtnStart.Symbol = 61515;
            this.uBtnStart.TabIndex = 7;
            this.uBtnStart.Text = "开始";
            this.uiToolTipChronic.SetToolTip(this.uBtnStart, resources.GetString("uBtnStart.ToolTip"));
            this.uBtnStart.Click += new System.EventHandler(this.uBtnStart_Click);
            // 
            // uiCheckBoxCheckAMI
            // 
            this.uiCheckBoxCheckAMI.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiCheckBoxCheckAMI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiCheckBoxCheckAMI.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.uiCheckBoxCheckAMI.Location = new System.Drawing.Point(699, 2);
            this.uiCheckBoxCheckAMI.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.uiCheckBoxCheckAMI.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiCheckBoxCheckAMI.Name = "uiCheckBoxCheckAMI";
            this.uiCheckBoxCheckAMI.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.uiCheckBoxCheckAMI.Size = new System.Drawing.Size(387, 40);
            this.uiCheckBoxCheckAMI.TabIndex = 8;
            this.uiCheckBoxCheckAMI.Text = "检查心血管事件";
            this.uiToolTipChronic.SetToolTip(this.uiCheckBoxCheckAMI, "包括心肌梗死、不稳定性心绞痛");
            // 
            // ucheckBoxAlignstr
            // 
            this.ucheckBoxAlignstr.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucheckBoxAlignstr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucheckBoxAlignstr.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ucheckBoxAlignstr.Location = new System.Drawing.Point(1092, 2);
            this.ucheckBoxAlignstr.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ucheckBoxAlignstr.MinimumSize = new System.Drawing.Size(1, 1);
            this.ucheckBoxAlignstr.Name = "ucheckBoxAlignstr";
            this.ucheckBoxAlignstr.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.ucheckBoxAlignstr.Size = new System.Drawing.Size(430, 40);
            this.ucheckBoxAlignstr.TabIndex = 8;
            this.ucheckBoxAlignstr.Text = "检查脑卒中";
            this.uiToolTipChronic.SetToolTip(this.ucheckBoxAlignstr, "包括脑梗死、脑出血");
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.4F));
            this.tableLayoutPanel1.Controls.Add(this.uBtnPrint, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.uBtnClear, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.uBtnOpendir, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 782);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1797, 72);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // uBtnPrint
            // 
            this.uBtnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uBtnPrint.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnPrint.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnPrint.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnPrint.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnPrint.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnPrint.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uBtnPrint.ForeDisableColor = System.Drawing.Color.White;
            this.uBtnPrint.Location = new System.Drawing.Point(1200, 2);
            this.uBtnPrint.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.uBtnPrint.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnPrint.Name = "uBtnPrint";
            this.uBtnPrint.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnPrint.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnPrint.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnPrint.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnPrint.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnPrint.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uBtnPrint.Size = new System.Drawing.Size(593, 68);
            this.uBtnPrint.Style = Sunny.UI.UIStyle.Custom;
            this.uBtnPrint.StyleCustomMode = true;
            this.uBtnPrint.Symbol = 61487;
            this.uBtnPrint.TabIndex = 9;
            this.uBtnPrint.Text = "打印";
            this.uiToolTipChronic.SetToolTip(this.uBtnPrint, "进一步调用打印信息");
            this.uBtnPrint.Click += new System.EventHandler(this.uBtnPrint_Click);
            // 
            // uBtnClear
            // 
            this.uBtnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uBtnClear.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnClear.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnClear.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnClear.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnClear.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnClear.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uBtnClear.ForeDisableColor = System.Drawing.Color.White;
            this.uBtnClear.Location = new System.Drawing.Point(602, 2);
            this.uBtnClear.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.uBtnClear.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnClear.Name = "uBtnClear";
            this.uBtnClear.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnClear.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnClear.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnClear.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnClear.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnClear.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uBtnClear.Size = new System.Drawing.Size(590, 68);
            this.uBtnClear.Style = Sunny.UI.UIStyle.Custom;
            this.uBtnClear.StyleCustomMode = true;
            this.uBtnClear.Symbol = 57386;
            this.uBtnClear.TabIndex = 8;
            this.uBtnClear.Text = "清除";
            this.uiToolTipChronic.SetToolTip(this.uBtnClear, "仅清除当前窗口信息\r\n不会删除已保存的文件");
            this.uBtnClear.Click += new System.EventHandler(this.uBtnClear_Click);
            // 
            // uBtnOpendir
            // 
            this.uBtnOpendir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnOpendir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uBtnOpendir.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnOpendir.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnOpendir.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnOpendir.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnOpendir.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnOpendir.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uBtnOpendir.ForeDisableColor = System.Drawing.Color.White;
            this.uBtnOpendir.Location = new System.Drawing.Point(4, 2);
            this.uBtnOpendir.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.uBtnOpendir.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnOpendir.Name = "uBtnOpendir";
            this.uBtnOpendir.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnOpendir.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnOpendir.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnOpendir.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnOpendir.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnOpendir.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uBtnOpendir.Size = new System.Drawing.Size(590, 68);
            this.uBtnOpendir.Style = Sunny.UI.UIStyle.Custom;
            this.uBtnOpendir.StyleCustomMode = true;
            this.uBtnOpendir.Symbol = 61564;
            this.uBtnOpendir.TabIndex = 7;
            this.uBtnOpendir.Text = "打开目录";
            this.uiToolTipChronic.SetToolTip(this.uBtnOpendir, "打开报卡文件的保存目录");
            this.uBtnOpendir.Click += new System.EventHandler(this.uBtnOpendir_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 769);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1794, 13);
            this.panel1.TabIndex = 4;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.progressBar1.Location = new System.Drawing.Point(0, 3);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(0);
            this.progressBar1.MinimumSize = new System.Drawing.Size(69, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Radius = 0;
            this.progressBar1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.progressBar1.ShowFocusColor = true;
            this.progressBar1.Size = new System.Drawing.Size(1794, 10);
            this.progressBar1.Style = Sunny.UI.UIStyle.Custom;
            this.progressBar1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 88);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1794, 27);
            this.panel2.TabIndex = 5;
            // 
            // panelOutMessage
            // 
            this.panelOutMessage.ColumnCount = 7;
            this.panelOutMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.005005F));
            this.panelOutMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.63F));
            this.panelOutMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.panelOutMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.63F));
            this.panelOutMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.panelOutMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.73F));
            this.panelOutMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.005005F));
            this.panelOutMessage.Controls.Add(this.UIlabelCopd, 1, 0);
            this.panelOutMessage.Controls.Add(this.UIlabelAMI, 3, 0);
            this.panelOutMessage.Controls.Add(this.UIlabelApoplexy, 5, 0);
            this.panelOutMessage.Controls.Add(this.uiListBoxCopd, 1, 1);
            this.panelOutMessage.Controls.Add(this.uiListBoxAmi, 3, 1);
            this.panelOutMessage.Controls.Add(this.uiListBoxApoplexy, 5, 1);
            this.panelOutMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOutMessage.Location = new System.Drawing.Point(0, 115);
            this.panelOutMessage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelOutMessage.Name = "panelOutMessage";
            this.panelOutMessage.RowCount = 2;
            this.panelOutMessage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.panelOutMessage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.panelOutMessage.Size = new System.Drawing.Size(1794, 654);
            this.panelOutMessage.TabIndex = 6;
            // 
            // UIlabelCopd
            // 
            this.UIlabelCopd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UIlabelCopd.Dock = System.Windows.Forms.DockStyle.Top;
            this.UIlabelCopd.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UIlabelCopd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.UIlabelCopd.Location = new System.Drawing.Point(92, 2);
            this.UIlabelCopd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UIlabelCopd.MinimumSize = new System.Drawing.Size(1, 1);
            this.UIlabelCopd.Name = "UIlabelCopd";
            this.UIlabelCopd.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.UIlabelCopd.Size = new System.Drawing.Size(507, 29);
            this.UIlabelCopd.Style = Sunny.UI.UIStyle.Custom;
            this.UIlabelCopd.StyleCustomMode = true;
            this.UIlabelCopd.Symbol = 61568;
            this.UIlabelCopd.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(118)))), ((int)(((byte)(176)))));
            this.UIlabelCopd.TabIndex = 0;
            this.UIlabelCopd.Text = "慢性肺病";
            this.uiToolTipChronic.SetToolTip(this.UIlabelCopd, "对所选表格中的\"慢性阻塞性肺疾病\", \"慢性支气管炎\", \r\n\"哮喘\", \"肺气肿\", \"支气管扩张\", \"急性支气管炎\"\r\n进行自动化报卡操作；\r\n身份证信息为" +
        "空的会弹出提示，需结束后手动填写。\r\n");
            this.UIlabelCopd.MouseEnter += new System.EventHandler(this.LabelMouseEnter);
            this.UIlabelCopd.MouseLeave += new System.EventHandler(this.LabelMouseLeave);
            // 
            // UIlabelAMI
            // 
            this.UIlabelAMI.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UIlabelAMI.Dock = System.Windows.Forms.DockStyle.Top;
            this.UIlabelAMI.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UIlabelAMI.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.UIlabelAMI.Location = new System.Drawing.Point(640, 2);
            this.UIlabelAMI.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UIlabelAMI.MinimumSize = new System.Drawing.Size(1, 1);
            this.UIlabelAMI.Name = "UIlabelAMI";
            this.UIlabelAMI.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.UIlabelAMI.Size = new System.Drawing.Size(507, 29);
            this.UIlabelAMI.Style = Sunny.UI.UIStyle.Custom;
            this.UIlabelAMI.StyleCustomMode = true;
            this.UIlabelAMI.Symbol = 61568;
            this.UIlabelAMI.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(77)))), ((int)(((byte)(221)))));
            this.UIlabelAMI.TabIndex = 1;
            this.UIlabelAMI.Text = "心血管疾病";
            this.uiToolTipChronic.SetToolTip(this.UIlabelAMI, "对所选表格中的\"心肌梗死\",\"急性冠脉综合征\"\r\n进行自动化报卡操作；\r\n诊断依据的随机种子:\r\n\"①⑨\", \"①⑨\", \"①⑨\",\r\n \"①⑨\", \"①⑨\", " +
        "\"⑨\", \"①\"\r\n");
            this.UIlabelAMI.MouseEnter += new System.EventHandler(this.LabelMouseEnter);
            this.UIlabelAMI.MouseLeave += new System.EventHandler(this.LabelMouseLeave);
            // 
            // UIlabelApoplexy
            // 
            this.UIlabelApoplexy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UIlabelApoplexy.Dock = System.Windows.Forms.DockStyle.Top;
            this.UIlabelApoplexy.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UIlabelApoplexy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.UIlabelApoplexy.Location = new System.Drawing.Point(1188, 2);
            this.UIlabelApoplexy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UIlabelApoplexy.MinimumSize = new System.Drawing.Size(1, 1);
            this.UIlabelApoplexy.Name = "UIlabelApoplexy";
            this.UIlabelApoplexy.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.UIlabelApoplexy.Size = new System.Drawing.Size(509, 29);
            this.UIlabelApoplexy.Style = Sunny.UI.UIStyle.Custom;
            this.UIlabelApoplexy.StyleCustomMode = true;
            this.UIlabelApoplexy.Symbol = 61568;
            this.UIlabelApoplexy.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(161)))), ((int)(((byte)(251)))));
            this.UIlabelApoplexy.TabIndex = 1;
            this.UIlabelApoplexy.Text = "脑卒中";
            this.uiToolTipChronic.SetToolTip(this.UIlabelApoplexy, "对所选表格中的\"脑梗死\"<.不区分大面积或腔隙性>,\"脑出血\"\r\n进行自动化报卡操作；\r\n诊断依据的随机种子:\r\n\"①②⑨\", \"①②⑨\", \"①②⑨\", \"①②" +
        "⑨\"\r\n \"①②⑨\", \"①②⑨\", \"①②⑤⑨\", \"①⑤\"");
            this.UIlabelApoplexy.MouseEnter += new System.EventHandler(this.LabelMouseEnter);
            this.UIlabelApoplexy.MouseLeave += new System.EventHandler(this.LabelMouseLeave);
            // 
            // uiListBoxCopd
            // 
            this.uiListBoxCopd.ContextMenuStrip = this.uiContextMenuStripChronic;
            this.uiListBoxCopd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiListBoxCopd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.uiListBoxCopd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiListBoxCopd.ForeColor = System.Drawing.Color.Silver;
            this.uiListBoxCopd.FormatString = "";
            this.uiListBoxCopd.HoverColor = System.Drawing.Color.Silver;
            this.uiListBoxCopd.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiListBoxCopd.Location = new System.Drawing.Point(93, 70);
            this.uiListBoxCopd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiListBoxCopd.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiListBoxCopd.Name = "uiListBoxCopd";
            this.uiListBoxCopd.Padding = new System.Windows.Forms.Padding(2);
            this.uiListBoxCopd.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.uiListBoxCopd.Size = new System.Drawing.Size(505, 579);
            this.uiListBoxCopd.Style = Sunny.UI.UIStyle.Custom;
            this.uiListBoxCopd.StyleCustomMode = true;
            this.uiListBoxCopd.TabIndex = 4;
            this.uiListBoxCopd.Text = null;
            // 
            // uiContextMenuStripChronic
            // 
            this.uiContextMenuStripChronic.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiContextMenuStripChronic.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.uiContextMenuStripChronic.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Delete});
            this.uiContextMenuStripChronic.Name = "uiContextMenuStripChronic";
            this.uiContextMenuStripChronic.Size = new System.Drawing.Size(171, 32);
            // 
            // Delete
            // 
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(170, 28);
            this.Delete.Text = "删除选中项";
            // 
            // uiListBoxAmi
            // 
            this.uiListBoxAmi.ContextMenuStrip = this.uiContextMenuStripChronic;
            this.uiListBoxAmi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiListBoxAmi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.uiListBoxAmi.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiListBoxAmi.ForeColor = System.Drawing.Color.Silver;
            this.uiListBoxAmi.FormatString = "";
            this.uiListBoxAmi.HoverColor = System.Drawing.Color.Silver;
            this.uiListBoxAmi.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiListBoxAmi.Location = new System.Drawing.Point(641, 70);
            this.uiListBoxAmi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiListBoxAmi.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiListBoxAmi.Name = "uiListBoxAmi";
            this.uiListBoxAmi.Padding = new System.Windows.Forms.Padding(2);
            this.uiListBoxAmi.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.uiListBoxAmi.Size = new System.Drawing.Size(505, 579);
            this.uiListBoxAmi.Style = Sunny.UI.UIStyle.Custom;
            this.uiListBoxAmi.StyleCustomMode = true;
            this.uiListBoxAmi.TabIndex = 4;
            this.uiListBoxAmi.Text = null;
            // 
            // uiListBoxApoplexy
            // 
            this.uiListBoxApoplexy.ContextMenuStrip = this.uiContextMenuStripChronic;
            this.uiListBoxApoplexy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiListBoxApoplexy.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.uiListBoxApoplexy.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiListBoxApoplexy.ForeColor = System.Drawing.Color.Silver;
            this.uiListBoxApoplexy.FormatString = "";
            this.uiListBoxApoplexy.HoverColor = System.Drawing.Color.Silver;
            this.uiListBoxApoplexy.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiListBoxApoplexy.Location = new System.Drawing.Point(1189, 70);
            this.uiListBoxApoplexy.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiListBoxApoplexy.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiListBoxApoplexy.Name = "uiListBoxApoplexy";
            this.uiListBoxApoplexy.Padding = new System.Windows.Forms.Padding(2);
            this.uiListBoxApoplexy.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.uiListBoxApoplexy.Size = new System.Drawing.Size(507, 579);
            this.uiListBoxApoplexy.Style = Sunny.UI.UIStyle.Custom;
            this.uiListBoxApoplexy.StyleCustomMode = true;
            this.uiListBoxApoplexy.TabIndex = 4;
            this.uiListBoxApoplexy.Text = null;
            // 
            // uiToolTipChronic
            // 
            this.uiToolTipChronic.BackColor = System.Drawing.Color.WhiteSmoke;
            this.uiToolTipChronic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uiToolTipChronic.OwnerDraw = true;
            // 
            // uiPanelDesktop
            // 
            this.uiPanelDesktop.Controls.Add(this.panelOutMessage);
            this.uiPanelDesktop.Controls.Add(this.panel2);
            this.uiPanelDesktop.Controls.Add(this.tablePanelSelect);
            this.uiPanelDesktop.Controls.Add(this.panel1);
            this.uiPanelDesktop.Controls.Add(this.tablePanelOpenFile);
            this.uiPanelDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanelDesktop.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.uiPanelDesktop.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanelDesktop.Location = new System.Drawing.Point(0, 0);
            this.uiPanelDesktop.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.uiPanelDesktop.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanelDesktop.Name = "uiPanelDesktop";
            this.uiPanelDesktop.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.uiPanelDesktop.Radius = 0;
            this.uiPanelDesktop.RectColor = System.Drawing.Color.Silver;
            this.uiPanelDesktop.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.uiPanelDesktop.Size = new System.Drawing.Size(1797, 782);
            this.uiPanelDesktop.Style = Sunny.UI.UIStyle.Custom;
            this.uiPanelDesktop.TabIndex = 7;
            this.uiPanelDesktop.Text = "uiPanel1";
            this.uiPanelDesktop.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChronicDiseasesUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1797, 854);
            this.Controls.Add(this.uiPanelDesktop);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.MediumPurple;
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "ChronicDiseasesUI";
            this.Text = "重庆市居民慢病报卡";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChronicDiseasesUI_FormClosed);
            this.tablePanelOpenFile.ResumeLayout(false);
            this.tablePanelOpenFile.PerformLayout();
            this.tablePanelSelect.ResumeLayout(false);
            this.tablePanelSelect.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelOutMessage.ResumeLayout(false);
            this.uiContextMenuStripChronic.ResumeLayout(false);
            this.uiPanelDesktop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tablePanelOpenFile;
        private System.Windows.Forms.Label labelFilename;
        private System.Windows.Forms.TableLayoutPanel tablePanelSelect;
        private System.Windows.Forms.Label labelSelectUser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel panelOutMessage;
        private Sunny.UI.UISymbolLabel UIlabelCopd;
        private Sunny.UI.UISymbolLabel UIlabelAMI;
        private Sunny.UI.UISymbolLabel UIlabelApoplexy;
        private Sunny.UI.UITextBox textBoxFilepath;
        private Sunny.UI.UIComboboxEx comboBoxSelectUser;
        private Sunny.UI.UIProcessBar progressBar1;
        private Sunny.UI.UISymbolButton uBtnPrint;
        private Sunny.UI.UISymbolButton uBtnClear;
        private Sunny.UI.UISymbolButton uBtnOpendir;
        private Sunny.UI.UISymbolButton uBtnStart;
        private Sunny.UI.UICheckBox uiCheckBoxCheckAMI;
        private Sunny.UI.UICheckBox ucheckBoxAlignstr;
        private Sunny.UI.UISymbolButton uBtnBrowse;
        private Sunny.UI.UIToolTip uiToolTipChronic;
        private Sunny.UI.UIListBox uiListBoxCopd;
        private Sunny.UI.UIListBox uiListBoxAmi;
        private Sunny.UI.UIListBox uiListBoxApoplexy;
        private Sunny.UI.UIContextMenuStrip uiContextMenuStripChronic;
        private System.Windows.Forms.ToolStripMenuItem Delete;
        private Sunny.UI.UIPanel uiPanelDesktop;
    }
}