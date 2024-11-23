
namespace MytoolUI
{
    partial class DischargeFollowUpUI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tablePanelOpenFile = new System.Windows.Forms.TableLayoutPanel();
            this.uBtnBrowse = new Sunny.UI.UISymbolButton();
            this.textBoxFilepath = new Sunny.UI.UITextBox();
            this.labelFilename = new System.Windows.Forms.Label();
            this.tablePanelSelect = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxSelectUser = new Sunny.UI.UIComboboxEx();
            this.labelSheetname = new System.Windows.Forms.Label();
            this.labelSelectUser = new System.Windows.Forms.Label();
            this.comboBoxSheets = new Sunny.UI.UIComboboxEx();
            this.tablePanelBtn = new System.Windows.Forms.TableLayoutPanel();
            this.uBtnOpendir = new Sunny.UI.UISymbolButton();
            this.uBtnStart = new Sunny.UI.UISymbolButton();
            this.uiDataGridViewTable = new Sunny.UI.UIDataGridView();
            this.uiToolTipDischarge = new Sunny.UI.UIToolTip(this.components);
            this.uiProcessBar = new Sunny.UI.UIProcessBar();
            this.tablePanelOpenFile.SuspendLayout();
            this.tablePanelSelect.SuspendLayout();
            this.tablePanelBtn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiDataGridViewTable)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePanelOpenFile
            // 
            this.tablePanelOpenFile.ColumnCount = 3;
            this.tablePanelOpenFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tablePanelOpenFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tablePanelOpenFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tablePanelOpenFile.Controls.Add(this.uBtnBrowse, 2, 0);
            this.tablePanelOpenFile.Controls.Add(this.textBoxFilepath, 1, 0);
            this.tablePanelOpenFile.Controls.Add(this.labelFilename, 0, 0);
            this.tablePanelOpenFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.tablePanelOpenFile.Location = new System.Drawing.Point(0, 0);
            this.tablePanelOpenFile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tablePanelOpenFile.Name = "tablePanelOpenFile";
            this.tablePanelOpenFile.RowCount = 1;
            this.tablePanelOpenFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanelOpenFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tablePanelOpenFile.Size = new System.Drawing.Size(862, 35);
            this.tablePanelOpenFile.TabIndex = 2;
            // 
            // uBtnBrowse
            // 
            this.uBtnBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnBrowse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uBtnBrowse.FillColor = System.Drawing.Color.White;
            this.uBtnBrowse.FillDisableColor = System.Drawing.Color.White;
            this.uBtnBrowse.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(211)))), ((int)(((byte)(248)))));
            this.uBtnBrowse.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(191)))), ((int)(((byte)(253)))));
            this.uBtnBrowse.FillSelectedColor = System.Drawing.Color.White;
            this.uBtnBrowse.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.uBtnBrowse.ForeColor = System.Drawing.Color.Black;
            this.uBtnBrowse.ForeHoverColor = System.Drawing.Color.Black;
            this.uBtnBrowse.ForePressColor = System.Drawing.Color.Black;
            this.uBtnBrowse.ForeSelectedColor = System.Drawing.Color.Black;
            this.uBtnBrowse.Location = new System.Drawing.Point(734, 2);
            this.uBtnBrowse.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.uBtnBrowse.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnBrowse.Name = "uBtnBrowse";
            this.uBtnBrowse.RectColor = System.Drawing.Color.White;
            this.uBtnBrowse.RectDisableColor = System.Drawing.Color.White;
            this.uBtnBrowse.RectHoverColor = System.Drawing.Color.White;
            this.uBtnBrowse.RectPressColor = System.Drawing.Color.White;
            this.uBtnBrowse.RectSelectedColor = System.Drawing.Color.White;
            this.uBtnBrowse.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uBtnBrowse.Size = new System.Drawing.Size(126, 31);
            this.uBtnBrowse.Style = Sunny.UI.UIStyle.Custom;
            this.uBtnBrowse.StyleCustomMode = true;
            this.uBtnBrowse.Symbol = 61717;
            this.uBtnBrowse.TabIndex = 5;
            this.uBtnBrowse.Text = "浏览";
            this.uiToolTipDischarge.SetToolTip(this.uBtnBrowse, "点击打开文件");
            this.uBtnBrowse.Click += new System.EventHandler(this.uBtnBrowse_Click);
            // 
            // textBoxFilepath
            // 
            this.textBoxFilepath.AllowDrop = true;
            this.textBoxFilepath.ButtonSymbol = 61761;
            this.textBoxFilepath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxFilepath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFilepath.FillColor = System.Drawing.Color.White;
            this.textBoxFilepath.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.textBoxFilepath.IconSize = 0;
            this.textBoxFilepath.Location = new System.Drawing.Point(132, 4);
            this.textBoxFilepath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxFilepath.Maximum = 2147483647D;
            this.textBoxFilepath.Minimum = -2147483648D;
            this.textBoxFilepath.MinimumSize = new System.Drawing.Size(1, 1);
            this.textBoxFilepath.Name = "textBoxFilepath";
            this.textBoxFilepath.Radius = 0;
            this.textBoxFilepath.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxFilepath.Size = new System.Drawing.Size(597, 27);
            this.textBoxFilepath.Style = Sunny.UI.UIStyle.Custom;
            this.textBoxFilepath.TabIndex = 6;
            this.textBoxFilepath.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelFilename
            // 
            this.labelFilename.AutoSize = true;
            this.labelFilename.BackColor = System.Drawing.Color.White;
            this.labelFilename.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelFilename.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelFilename.ForeColor = System.Drawing.Color.Black;
            this.labelFilename.Location = new System.Drawing.Point(73, 0);
            this.labelFilename.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFilename.Name = "labelFilename";
            this.labelFilename.Size = new System.Drawing.Size(54, 35);
            this.labelFilename.TabIndex = 0;
            this.labelFilename.Text = "文件名:";
            this.labelFilename.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tablePanelSelect
            // 
            this.tablePanelSelect.ColumnCount = 5;
            this.tablePanelSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tablePanelSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tablePanelSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tablePanelSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tablePanelSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tablePanelSelect.Controls.Add(this.comboBoxSelectUser, 1, 0);
            this.tablePanelSelect.Controls.Add(this.labelSheetname, 2, 0);
            this.tablePanelSelect.Controls.Add(this.labelSelectUser, 0, 0);
            this.tablePanelSelect.Controls.Add(this.comboBoxSheets, 3, 0);
            this.tablePanelSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.tablePanelSelect.Location = new System.Drawing.Point(0, 35);
            this.tablePanelSelect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tablePanelSelect.Name = "tablePanelSelect";
            this.tablePanelSelect.RowCount = 1;
            this.tablePanelSelect.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelSelect.Size = new System.Drawing.Size(862, 35);
            this.tablePanelSelect.TabIndex = 3;
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
            "刘益宏",
            "彭育欢",
            "李小琴",
            "朱庆霞",
            "张   李"});
            this.comboBoxSelectUser.ItemSelectBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.comboBoxSelectUser.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.comboBoxSelectUser.Location = new System.Drawing.Point(132, 4);
            this.comboBoxSelectUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxSelectUser.Name = "comboBoxSelectUser";
            this.comboBoxSelectUser.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboBoxSelectUser.Size = new System.Drawing.Size(166, 28);
            this.comboBoxSelectUser.Style = Sunny.UI.UIStyle.Custom;
            this.comboBoxSelectUser.StyleCustomMode = true;
            this.comboBoxSelectUser.TabIndex = 6;
            // 
            // labelSheetname
            // 
            this.labelSheetname.AutoSize = true;
            this.labelSheetname.BackColor = System.Drawing.Color.White;
            this.labelSheetname.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelSheetname.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelSheetname.ForeColor = System.Drawing.Color.Black;
            this.labelSheetname.Location = new System.Drawing.Point(421, 0);
            this.labelSheetname.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSheetname.Name = "labelSheetname";
            this.labelSheetname.Size = new System.Drawing.Size(50, 35);
            this.labelSheetname.TabIndex = 4;
            this.labelSheetname.Text = "Sheet:";
            this.labelSheetname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSelectUser
            // 
            this.labelSelectUser.AutoSize = true;
            this.labelSelectUser.BackColor = System.Drawing.Color.White;
            this.labelSelectUser.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelSelectUser.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelSelectUser.ForeColor = System.Drawing.Color.Black;
            this.labelSelectUser.Location = new System.Drawing.Point(73, 0);
            this.labelSelectUser.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSelectUser.Name = "labelSelectUser";
            this.labelSelectUser.Size = new System.Drawing.Size(54, 35);
            this.labelSelectUser.TabIndex = 0;
            this.labelSelectUser.Text = "用户名:";
            this.labelSelectUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxSheets
            // 
            this.comboBoxSheets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSheets.BackColor = System.Drawing.Color.White;
            this.comboBoxSheets.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxSheets.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.comboBoxSheets.FormattingEnabled = true;
            this.comboBoxSheets.ItemHeight = 22;
            this.comboBoxSheets.ItemSelectBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.comboBoxSheets.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.comboBoxSheets.Location = new System.Drawing.Point(476, 4);
            this.comboBoxSheets.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxSheets.Name = "comboBoxSheets";
            this.comboBoxSheets.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboBoxSheets.Size = new System.Drawing.Size(166, 28);
            this.comboBoxSheets.Style = Sunny.UI.UIStyle.Custom;
            this.comboBoxSheets.StyleCustomMode = true;
            this.comboBoxSheets.TabIndex = 7;
            this.comboBoxSheets.SelectedIndexChanged += new System.EventHandler(this.comboBoxSheets_SelectedIndexChanged);
            // 
            // tablePanelBtn
            // 
            this.tablePanelBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.tablePanelBtn.ColumnCount = 2;
            this.tablePanelBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanelBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanelBtn.Controls.Add(this.uBtnOpendir, 0, 0);
            this.tablePanelBtn.Controls.Add(this.uBtnStart, 0, 0);
            this.tablePanelBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tablePanelBtn.Location = new System.Drawing.Point(0, 509);
            this.tablePanelBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tablePanelBtn.Name = "tablePanelBtn";
            this.tablePanelBtn.RowCount = 1;
            this.tablePanelBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanelBtn.Size = new System.Drawing.Size(862, 46);
            this.tablePanelBtn.TabIndex = 4;
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
            this.uBtnOpendir.Location = new System.Drawing.Point(433, 2);
            this.uBtnOpendir.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.uBtnOpendir.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnOpendir.Name = "uBtnOpendir";
            this.uBtnOpendir.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnOpendir.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnOpendir.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnOpendir.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnOpendir.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnOpendir.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uBtnOpendir.Size = new System.Drawing.Size(427, 42);
            this.uBtnOpendir.Style = Sunny.UI.UIStyle.Custom;
            this.uBtnOpendir.StyleCustomMode = true;
            this.uBtnOpendir.Symbol = 61564;
            this.uBtnOpendir.TabIndex = 4;
            this.uBtnOpendir.Text = "打开目录";
            this.uiToolTipDischarge.SetToolTip(this.uBtnOpendir, "打开所在目录");
            this.uBtnOpendir.Click += new System.EventHandler(this.uBtnOpendir_Click);
            // 
            // uBtnStart
            // 
            this.uBtnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uBtnStart.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnStart.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnStart.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnStart.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnStart.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uBtnStart.Location = new System.Drawing.Point(2, 2);
            this.uBtnStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.uBtnStart.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnStart.Name = "uBtnStart";
            this.uBtnStart.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnStart.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnStart.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnStart.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnStart.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnStart.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uBtnStart.Size = new System.Drawing.Size(427, 42);
            this.uBtnStart.Style = Sunny.UI.UIStyle.Custom;
            this.uBtnStart.StyleCustomMode = true;
            this.uBtnStart.Symbol = 61515;
            this.uBtnStart.TabIndex = 3;
            this.uBtnStart.Text = "开始";
            this.uiToolTipDischarge.SetToolTip(this.uBtnStart, "开始格式化门诊日志");
            this.uBtnStart.Click += new System.EventHandler(this.uBtnStart_Click);
            // 
            // uiDataGridViewTable
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.uiDataGridViewTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.uiDataGridViewTable.BackgroundColor = System.Drawing.Color.White;
            this.uiDataGridViewTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.uiDataGridViewTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiDataGridViewTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.uiDataGridViewTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.uiDataGridViewTable.DefaultCellStyle = dataGridViewCellStyle3;
            this.uiDataGridViewTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiDataGridViewTable.EnableHeadersVisualStyles = false;
            this.uiDataGridViewTable.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiDataGridViewTable.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.uiDataGridViewTable.Location = new System.Drawing.Point(0, 70);
            this.uiDataGridViewTable.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.uiDataGridViewTable.Name = "uiDataGridViewTable";
            this.uiDataGridViewTable.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiDataGridViewTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.uiDataGridViewTable.RowHeadersWidth = 51;
            this.uiDataGridViewTable.RowHeight = 27;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            this.uiDataGridViewTable.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.uiDataGridViewTable.RowTemplate.Height = 27;
            this.uiDataGridViewTable.SelectedIndex = -1;
            this.uiDataGridViewTable.ShowGridLine = true;
            this.uiDataGridViewTable.ShowRect = false;
            this.uiDataGridViewTable.Size = new System.Drawing.Size(862, 429);
            this.uiDataGridViewTable.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.uiDataGridViewTable.Style = Sunny.UI.UIStyle.DarkBlue;
            this.uiDataGridViewTable.StyleCustomMode = true;
            this.uiDataGridViewTable.TabIndex = 5;
            // 
            // uiToolTipDischarge
            // 
            this.uiToolTipDischarge.BackColor = System.Drawing.Color.WhiteSmoke;
            this.uiToolTipDischarge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uiToolTipDischarge.OwnerDraw = true;
            // 
            // uiProcessBar
            // 
            this.uiProcessBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiProcessBar.FillColor = System.Drawing.Color.White;
            this.uiProcessBar.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiProcessBar.Location = new System.Drawing.Point(0, 499);
            this.uiProcessBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.uiProcessBar.MinimumSize = new System.Drawing.Size(52, 4);
            this.uiProcessBar.Name = "uiProcessBar";
            this.uiProcessBar.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            this.uiProcessBar.Size = new System.Drawing.Size(862, 10);
            this.uiProcessBar.Style = Sunny.UI.UIStyle.Custom;
            this.uiProcessBar.TabIndex = 6;
            // 
            // DischargeFollowUpUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(862, 555);
            this.Controls.Add(this.uiDataGridViewTable);
            this.Controls.Add(this.uiProcessBar);
            this.Controls.Add(this.tablePanelBtn);
            this.Controls.Add(this.tablePanelSelect);
            this.Controls.Add(this.tablePanelOpenFile);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "DischargeFollowUpUI";
            this.Text = "出院随访格式化";
            this.tablePanelOpenFile.ResumeLayout(false);
            this.tablePanelOpenFile.PerformLayout();
            this.tablePanelSelect.ResumeLayout(false);
            this.tablePanelSelect.PerformLayout();
            this.tablePanelBtn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiDataGridViewTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tablePanelOpenFile;
        private System.Windows.Forms.Label labelFilename;
        private System.Windows.Forms.TableLayoutPanel tablePanelSelect;
        private System.Windows.Forms.Label labelSelectUser;
        private System.Windows.Forms.Label labelSheetname;
        private System.Windows.Forms.TableLayoutPanel tablePanelBtn;
        private Sunny.UI.UIComboboxEx comboBoxSelectUser;
        private Sunny.UI.UIComboboxEx comboBoxSheets;
        private Sunny.UI.UITextBox textBoxFilepath;
        private Sunny.UI.UISymbolButton uBtnStart;
        private Sunny.UI.UISymbolButton uBtnOpendir;
        private Sunny.UI.UIDataGridView uiDataGridViewTable;
        private Sunny.UI.UISymbolButton uBtnBrowse;
        private Sunny.UI.UIToolTip uiToolTipDischarge;
        private Sunny.UI.UIProcessBar uiProcessBar;
    }
}