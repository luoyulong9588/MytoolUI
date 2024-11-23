
namespace MytoolUI
{
    partial class OutpatientUI
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelFilename = new System.Windows.Forms.Label();
            this.labelSheetname = new System.Windows.Forms.Label();
            this.textBoxFilepath = new Sunny.UI.UITextBox();
            this.comboBoxSheets = new Sunny.UI.UIComboboxEx();
            this.uBtnBrowse = new Sunny.UI.UISymbolButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.uBtnOpendir = new Sunny.UI.UISymbolButton();
            this.uBtnStart = new Sunny.UI.UISymbolButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uiDataGridViewDesktop = new Sunny.UI.UIDataGridView();
            this.uiToolTipOutpatient = new Sunny.UI.UIToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiDataGridViewDesktop)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.labelFilename, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelSheetname, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxFilepath, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxSheets, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.uBtnBrowse, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1011, 70);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelFilename
            // 
            this.labelFilename.AutoSize = true;
            this.labelFilename.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelFilename.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelFilename.ForeColor = System.Drawing.Color.Black;
            this.labelFilename.Location = new System.Drawing.Point(95, 0);
            this.labelFilename.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFilename.Name = "labelFilename";
            this.labelFilename.Size = new System.Drawing.Size(54, 35);
            this.labelFilename.TabIndex = 0;
            this.labelFilename.Text = "文件名:";
            this.labelFilename.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSheetname
            // 
            this.labelSheetname.AutoSize = true;
            this.labelSheetname.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelSheetname.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelSheetname.ForeColor = System.Drawing.Color.Black;
            this.labelSheetname.Location = new System.Drawing.Point(99, 35);
            this.labelSheetname.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSheetname.Name = "labelSheetname";
            this.labelSheetname.Size = new System.Drawing.Size(50, 35);
            this.labelSheetname.TabIndex = 3;
            this.labelSheetname.Text = "Sheet:";
            this.labelSheetname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxFilepath
            // 
            this.textBoxFilepath.ButtonSymbol = 61761;
            this.textBoxFilepath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxFilepath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFilepath.FillColor = System.Drawing.Color.White;
            this.textBoxFilepath.Font = new System.Drawing.Font("宋体", 10F);
            this.textBoxFilepath.IconSize = 0;
            this.textBoxFilepath.Location = new System.Drawing.Point(154, 4);
            this.textBoxFilepath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxFilepath.Maximum = 2147483647D;
            this.textBoxFilepath.Minimum = -2147483648D;
            this.textBoxFilepath.MinimumSize = new System.Drawing.Size(1, 1);
            this.textBoxFilepath.Name = "textBoxFilepath";
            this.textBoxFilepath.Radius = 0;
            this.textBoxFilepath.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxFilepath.Size = new System.Drawing.Size(701, 27);
            this.textBoxFilepath.Style = Sunny.UI.UIStyle.Custom;
            this.textBoxFilepath.SymbolSize = 0;
            this.textBoxFilepath.TabIndex = 5;
            this.textBoxFilepath.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxSheets
            // 
            this.comboBoxSheets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSheets.BackColor = System.Drawing.Color.White;
            this.comboBoxSheets.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxSheets.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.comboBoxSheets.FormattingEnabled = true;
            this.comboBoxSheets.ItemHeight = 22;
            this.comboBoxSheets.ItemSelectBackColor = System.Drawing.Color.Gray;
            this.comboBoxSheets.Location = new System.Drawing.Point(154, 39);
            this.comboBoxSheets.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxSheets.Name = "comboBoxSheets";
            this.comboBoxSheets.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboBoxSheets.Size = new System.Drawing.Size(701, 28);
            this.comboBoxSheets.Style = Sunny.UI.UIStyle.Custom;
            this.comboBoxSheets.TabIndex = 7;
            this.comboBoxSheets.SelectedIndexChanged += new System.EventHandler(this.comboBoxSheets_SelectedIndexChanged_1);
            // 
            // uBtnBrowse
            // 
            this.uBtnBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnBrowse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uBtnBrowse.FillColor = System.Drawing.Color.White;
            this.uBtnBrowse.FillDisableColor = System.Drawing.Color.White;
            this.uBtnBrowse.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(217)))), ((int)(((byte)(212)))));
            this.uBtnBrowse.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(194)))), ((int)(((byte)(186)))));
            this.uBtnBrowse.FillSelectedColor = System.Drawing.Color.White;
            this.uBtnBrowse.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.uBtnBrowse.ForeColor = System.Drawing.Color.Black;
            this.uBtnBrowse.ForeHoverColor = System.Drawing.Color.Black;
            this.uBtnBrowse.ForePressColor = System.Drawing.Color.Black;
            this.uBtnBrowse.ForeSelectedColor = System.Drawing.Color.Black;
            this.uBtnBrowse.Location = new System.Drawing.Point(860, 2);
            this.uBtnBrowse.Margin = new System.Windows.Forms.Padding(2);
            this.uBtnBrowse.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnBrowse.Name = "uBtnBrowse";
            this.uBtnBrowse.RectColor = System.Drawing.Color.White;
            this.uBtnBrowse.RectDisableColor = System.Drawing.Color.White;
            this.uBtnBrowse.RectHoverColor = System.Drawing.Color.White;
            this.uBtnBrowse.RectPressColor = System.Drawing.Color.White;
            this.uBtnBrowse.RectSelectedColor = System.Drawing.Color.White;
            this.uBtnBrowse.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uBtnBrowse.Size = new System.Drawing.Size(149, 31);
            this.uBtnBrowse.Style = Sunny.UI.UIStyle.Custom;
            this.uBtnBrowse.StyleCustomMode = true;
            this.uBtnBrowse.Symbol = 61717;
            this.uBtnBrowse.TabIndex = 8;
            this.uBtnBrowse.Text = "浏览";
            this.uiToolTipOutpatient.SetToolTip(this.uBtnBrowse, "点击打开文件");
            this.uBtnBrowse.Click += new System.EventHandler(this.uBtnBrowse_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.uBtnOpendir, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.uBtnStart, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 531);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1011, 46);
            this.tableLayoutPanel2.TabIndex = 1;
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
            this.uBtnOpendir.Location = new System.Drawing.Point(507, 2);
            this.uBtnOpendir.Margin = new System.Windows.Forms.Padding(2);
            this.uBtnOpendir.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnOpendir.Name = "uBtnOpendir";
            this.uBtnOpendir.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnOpendir.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnOpendir.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnOpendir.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnOpendir.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnOpendir.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uBtnOpendir.Size = new System.Drawing.Size(502, 42);
            this.uBtnOpendir.Style = Sunny.UI.UIStyle.Custom;
            this.uBtnOpendir.StyleCustomMode = true;
            this.uBtnOpendir.Symbol = 61564;
            this.uBtnOpendir.TabIndex = 3;
            this.uBtnOpendir.Text = "打开目录";
            this.uiToolTipOutpatient.SetToolTip(this.uBtnOpendir, "打开文件保存目录");
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
            this.uBtnStart.Margin = new System.Windows.Forms.Padding(2);
            this.uBtnStart.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnStart.Name = "uBtnStart";
            this.uBtnStart.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnStart.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnStart.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnStart.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnStart.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnStart.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uBtnStart.Size = new System.Drawing.Size(501, 42);
            this.uBtnStart.Style = Sunny.UI.UIStyle.Custom;
            this.uBtnStart.StyleCustomMode = true;
            this.uBtnStart.Symbol = 61515;
            this.uBtnStart.TabIndex = 2;
            this.uBtnStart.Text = "开始";
            this.uiToolTipOutpatient.SetToolTip(this.uBtnStart, "开始门诊日志格式化");
            this.uBtnStart.Click += new System.EventHandler(this.uBtnStart_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uiDataGridViewDesktop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 70);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.panel1.Size = new System.Drawing.Size(1011, 461);
            this.panel1.TabIndex = 2;
            // 
            // uiDataGridViewDesktop
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.uiDataGridViewDesktop.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.uiDataGridViewDesktop.BackgroundColor = System.Drawing.Color.White;
            this.uiDataGridViewDesktop.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.uiDataGridViewDesktop.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiDataGridViewDesktop.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.uiDataGridViewDesktop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.uiDataGridViewDesktop.DefaultCellStyle = dataGridViewCellStyle3;
            this.uiDataGridViewDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiDataGridViewDesktop.EnableHeadersVisualStyles = false;
            this.uiDataGridViewDesktop.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiDataGridViewDesktop.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.uiDataGridViewDesktop.Location = new System.Drawing.Point(1, 0);
            this.uiDataGridViewDesktop.Margin = new System.Windows.Forms.Padding(2);
            this.uiDataGridViewDesktop.Name = "uiDataGridViewDesktop";
            this.uiDataGridViewDesktop.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiDataGridViewDesktop.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.uiDataGridViewDesktop.RowHeadersWidth = 51;
            this.uiDataGridViewDesktop.RowHeight = 27;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            this.uiDataGridViewDesktop.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.uiDataGridViewDesktop.RowTemplate.Height = 27;
            this.uiDataGridViewDesktop.SelectedIndex = -1;
            this.uiDataGridViewDesktop.ShowGridLine = true;
            this.uiDataGridViewDesktop.ShowRect = false;
            this.uiDataGridViewDesktop.Size = new System.Drawing.Size(1010, 461);
            this.uiDataGridViewDesktop.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.uiDataGridViewDesktop.Style = Sunny.UI.UIStyle.DarkBlue;
            this.uiDataGridViewDesktop.TabIndex = 0;
            // 
            // uiToolTipOutpatient
            // 
            this.uiToolTipOutpatient.BackColor = System.Drawing.Color.WhiteSmoke;
            this.uiToolTipOutpatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uiToolTipOutpatient.OwnerDraw = true;
            // 
            // OutpatientUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1011, 577);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "OutpatientUI";
            this.Text = "门诊日志格式化";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OutpatientUI_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiDataGridViewDesktop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelFilename;
        private System.Windows.Forms.Label labelSheetname;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private Sunny.UI.UITextBox textBoxFilepath;
        private Sunny.UI.UIComboboxEx comboBoxSheets;
        private Sunny.UI.UISymbolButton uBtnOpendir;
        private Sunny.UI.UISymbolButton uBtnStart;
        private Sunny.UI.UIDataGridView uiDataGridViewDesktop;
        private Sunny.UI.UISymbolButton uBtnBrowse;
        private Sunny.UI.UIToolTip uiToolTipOutpatient;
    }
}