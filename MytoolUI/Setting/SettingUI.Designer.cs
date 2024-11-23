
namespace MytoolUI
{
    partial class SettingUI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tablePanelUser = new System.Windows.Forms.TableLayoutPanel();
            this.labelUserName = new System.Windows.Forms.Label();
            this.textBoxEditName = new Sunny.UI.UITextBox();
            this.ubtnEditName = new Sunny.UI.UISymbolButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uiDataGridViewDesktop = new Sunny.UI.UIDataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.uBtnFactory = new Sunny.UI.UISymbolButton();
            this.uBtnHelp = new Sunny.UI.UISymbolButton();
            this.uBtnSaveChange = new Sunny.UI.UISymbolButton();
            this.tablePanelUser.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiDataGridViewDesktop)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tablePanelUser
            // 
            this.tablePanelUser.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tablePanelUser.ColumnCount = 3;
            this.tablePanelUser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.14286F));
            this.tablePanelUser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.42857F));
            this.tablePanelUser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.42857F));
            this.tablePanelUser.Controls.Add(this.labelUserName, 0, 0);
            this.tablePanelUser.Controls.Add(this.textBoxEditName, 1, 0);
            this.tablePanelUser.Controls.Add(this.ubtnEditName, 2, 0);
            this.tablePanelUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.tablePanelUser.Location = new System.Drawing.Point(0, 0);
            this.tablePanelUser.Margin = new System.Windows.Forms.Padding(3, 2, 0, 2);
            this.tablePanelUser.Name = "tablePanelUser";
            this.tablePanelUser.RowCount = 1;
            this.tablePanelUser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelUser.Size = new System.Drawing.Size(1348, 58);
            this.tablePanelUser.TabIndex = 0;
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.labelUserName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelUserName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelUserName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelUserName.ForeColor = System.Drawing.Color.Black;
            this.labelUserName.Location = new System.Drawing.Point(3, 0);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(764, 58);
            this.labelUserName.TabIndex = 0;
            this.labelUserName.Text = "UserName（Click to Edit）";
            this.labelUserName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelUserName.Click += new System.EventHandler(this.labelUserName_Click);
            // 
            // textBoxEditName
            // 
            this.textBoxEditName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEditName.ButtonSymbol = 61761;
            this.textBoxEditName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxEditName.FillColor = System.Drawing.Color.White;
            this.textBoxEditName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxEditName.Location = new System.Drawing.Point(774, 5);
            this.textBoxEditName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxEditName.Maximum = 2147483647D;
            this.textBoxEditName.Minimum = -2147483648D;
            this.textBoxEditName.MinimumSize = new System.Drawing.Size(1, 1);
            this.textBoxEditName.Name = "textBoxEditName";
            this.textBoxEditName.RectColor = System.Drawing.Color.Gray;
            this.textBoxEditName.Size = new System.Drawing.Size(280, 48);
            this.textBoxEditName.Style = Sunny.UI.UIStyle.Custom;
            this.textBoxEditName.TabIndex = 3;
            this.textBoxEditName.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ubtnEditName
            // 
            this.ubtnEditName.BackColor = System.Drawing.Color.White;
            this.ubtnEditName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ubtnEditName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ubtnEditName.FillColor = System.Drawing.Color.Transparent;
            this.ubtnEditName.FillHoverColor = System.Drawing.Color.Silver;
            this.ubtnEditName.FillPressColor = System.Drawing.Color.Silver;
            this.ubtnEditName.FillSelectedColor = System.Drawing.Color.Silver;
            this.ubtnEditName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ubtnEditName.ForeColor = System.Drawing.Color.Black;
            this.ubtnEditName.ForeHoverColor = System.Drawing.Color.OrangeRed;
            this.ubtnEditName.Location = new System.Drawing.Point(1061, 2);
            this.ubtnEditName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ubtnEditName.MinimumSize = new System.Drawing.Size(1, 1);
            this.ubtnEditName.Name = "ubtnEditName";
            this.ubtnEditName.RectColor = System.Drawing.Color.Gray;
            this.ubtnEditName.RectHoverColor = System.Drawing.Color.Gray;
            this.ubtnEditName.RectPressColor = System.Drawing.Color.Gray;
            this.ubtnEditName.RectSelectedColor = System.Drawing.Color.Gray;
            this.ubtnEditName.Size = new System.Drawing.Size(284, 54);
            this.ubtnEditName.Style = Sunny.UI.UIStyle.Custom;
            this.ubtnEditName.TabIndex = 4;
            this.ubtnEditName.Text = "确定";
            this.ubtnEditName.TipsForeColor = System.Drawing.Color.Red;
            this.ubtnEditName.Click += new System.EventHandler(this.ubtnEditName_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uiDataGridViewDesktop);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.tablePanelUser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1348, 721);
            this.panel1.TabIndex = 2;
            // 
            // uiDataGridViewDesktop
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.uiDataGridViewDesktop.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.uiDataGridViewDesktop.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
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
            this.uiDataGridViewDesktop.ColumnHeadersVisible = false;
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
            this.uiDataGridViewDesktop.Location = new System.Drawing.Point(0, 58);
            this.uiDataGridViewDesktop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.uiDataGridViewDesktop.RowHeadersVisible = false;
            this.uiDataGridViewDesktop.RowHeadersWidth = 51;
            this.uiDataGridViewDesktop.RowHeight = 27;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            this.uiDataGridViewDesktop.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.uiDataGridViewDesktop.RowTemplate.Height = 27;
            this.uiDataGridViewDesktop.SelectedIndex = -1;
            this.uiDataGridViewDesktop.ShowGridLine = true;
            this.uiDataGridViewDesktop.ShowRect = false;
            this.uiDataGridViewDesktop.Size = new System.Drawing.Size(1348, 605);
            this.uiDataGridViewDesktop.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.uiDataGridViewDesktop.Style = Sunny.UI.UIStyle.DarkBlue;
            this.uiDataGridViewDesktop.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.4F));
            this.tableLayoutPanel1.Controls.Add(this.uBtnFactory, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.uBtnHelp, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.uBtnSaveChange, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 663);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1348, 58);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // uBtnFactory
            // 
            this.uBtnFactory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnFactory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uBtnFactory.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnFactory.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnFactory.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnFactory.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnFactory.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnFactory.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uBtnFactory.ForeDisableColor = System.Drawing.Color.White;
            this.uBtnFactory.Location = new System.Drawing.Point(899, 2);
            this.uBtnFactory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.uBtnFactory.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnFactory.Name = "uBtnFactory";
            this.uBtnFactory.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnFactory.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnFactory.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnFactory.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnFactory.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnFactory.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uBtnFactory.Size = new System.Drawing.Size(446, 54);
            this.uBtnFactory.Style = Sunny.UI.UIStyle.Custom;
            this.uBtnFactory.StyleCustomMode = true;
            this.uBtnFactory.Symbol = 362750;
            this.uBtnFactory.TabIndex = 9;
            this.uBtnFactory.Text = "工厂";
            this.uBtnFactory.Click += new System.EventHandler(this.uBtnFactory_Click);
            // 
            // uBtnHelp
            // 
            this.uBtnHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uBtnHelp.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnHelp.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnHelp.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnHelp.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnHelp.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnHelp.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uBtnHelp.ForeDisableColor = System.Drawing.Color.White;
            this.uBtnHelp.Location = new System.Drawing.Point(451, 2);
            this.uBtnHelp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.uBtnHelp.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnHelp.Name = "uBtnHelp";
            this.uBtnHelp.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnHelp.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnHelp.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnHelp.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnHelp.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnHelp.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uBtnHelp.Size = new System.Drawing.Size(442, 54);
            this.uBtnHelp.Style = Sunny.UI.UIStyle.Custom;
            this.uBtnHelp.StyleCustomMode = true;
            this.uBtnHelp.Symbol = 61736;
            this.uBtnHelp.TabIndex = 8;
            this.uBtnHelp.Text = "帮助";
            this.uBtnHelp.Click += new System.EventHandler(this.uBtnHelp_Click);
            // 
            // uBtnSaveChange
            // 
            this.uBtnSaveChange.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnSaveChange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uBtnSaveChange.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnSaveChange.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uBtnSaveChange.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnSaveChange.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnSaveChange.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnSaveChange.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uBtnSaveChange.ForeDisableColor = System.Drawing.Color.White;
            this.uBtnSaveChange.Location = new System.Drawing.Point(3, 2);
            this.uBtnSaveChange.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.uBtnSaveChange.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnSaveChange.Name = "uBtnSaveChange";
            this.uBtnSaveChange.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnSaveChange.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnSaveChange.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnSaveChange.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnSaveChange.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.uBtnSaveChange.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uBtnSaveChange.Size = new System.Drawing.Size(442, 54);
            this.uBtnSaveChange.Style = Sunny.UI.UIStyle.Custom;
            this.uBtnSaveChange.StyleCustomMode = true;
            this.uBtnSaveChange.Symbol = 61639;
            this.uBtnSaveChange.TabIndex = 7;
            this.uBtnSaveChange.Text = "保存修改";
            this.uBtnSaveChange.Click += new System.EventHandler(this.uBtnSaveChange_Click);
            // 
            // SettingUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1348, 721);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SettingUI";
            this.Text = "设置";
            this.Load += new System.EventHandler(this.SettingUI_Load);
            this.tablePanelUser.ResumeLayout(false);
            this.tablePanelUser.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiDataGridViewDesktop)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tablePanelUser;
        private System.Windows.Forms.Label labelUserName;
        private Sunny.UI.UITextBox textBoxEditName;
        private Sunny.UI.UISymbolButton ubtnEditName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Sunny.UI.UISymbolButton uBtnFactory;
        private Sunny.UI.UISymbolButton uBtnHelp;
        private Sunny.UI.UISymbolButton uBtnSaveChange;
        private Sunny.UI.UIDataGridView uiDataGridViewDesktop;
    }
}