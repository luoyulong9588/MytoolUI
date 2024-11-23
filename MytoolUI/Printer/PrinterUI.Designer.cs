
namespace MytoolUI
{
    partial class PrinterUI
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
            this.uiTitlePanelPrinter = new Sunny.UI.UITitlePanel();
            this.panelText = new Sunny.UI.UIPanel();
            this.textBoxOutMessage = new Sunny.UI.UIRichTextBox();
            this.panelBtns = new Sunny.UI.UITableLayoutPanel();
            this.ubtnCancel = new Sunny.UI.UISymbolButton();
            this.ubtnStart = new Sunny.UI.UISymbolButton();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.comboxSelectPrinter = new Sunny.UI.UIComboboxEx();
            this.uiTitlePanelPrinter.SuspendLayout();
            this.panelText.SuspendLayout();
            this.panelBtns.SuspendLayout();
            this.uiPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiTitlePanelPrinter
            // 
            this.uiTitlePanelPrinter.Controls.Add(this.panelText);
            this.uiTitlePanelPrinter.Controls.Add(this.panelBtns);
            this.uiTitlePanelPrinter.Controls.Add(this.uiPanel1);
            this.uiTitlePanelPrinter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTitlePanelPrinter.FillColor = System.Drawing.Color.Gainsboro;
            this.uiTitlePanelPrinter.FillDisableColor = System.Drawing.Color.Gainsboro;
            this.uiTitlePanelPrinter.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTitlePanelPrinter.Location = new System.Drawing.Point(0, 0);
            this.uiTitlePanelPrinter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTitlePanelPrinter.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanelPrinter.Name = "uiTitlePanelPrinter";
            this.uiTitlePanelPrinter.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.uiTitlePanelPrinter.Radius = 0;
            this.uiTitlePanelPrinter.RectColor = System.Drawing.Color.DimGray;
            this.uiTitlePanelPrinter.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uiTitlePanelPrinter.Size = new System.Drawing.Size(548, 375);
            this.uiTitlePanelPrinter.Style = Sunny.UI.UIStyle.Custom;
            this.uiTitlePanelPrinter.StyleCustomMode = true;
            this.uiTitlePanelPrinter.TabIndex = 0;
            this.uiTitlePanelPrinter.Text = "选择打印机";
            this.uiTitlePanelPrinter.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiTitlePanelPrinter.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uiTitlePanelPrinter.Click += new System.EventHandler(this.uiTitlePanelPrinter_Click);
            // 
            // panelText
            // 
            this.panelText.Controls.Add(this.textBoxOutMessage);
            this.panelText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelText.FillColor = System.Drawing.Color.Gainsboro;
            this.panelText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelText.Location = new System.Drawing.Point(0, 82);
            this.panelText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelText.MinimumSize = new System.Drawing.Size(1, 1);
            this.panelText.Name = "panelText";
            this.panelText.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.panelText.Radius = 0;
            this.panelText.RectColor = System.Drawing.Color.Gainsboro;
            this.panelText.RectDisableColor = System.Drawing.Color.Gainsboro;
            this.panelText.Size = new System.Drawing.Size(548, 246);
            this.panelText.Style = Sunny.UI.UIStyle.Custom;
            this.panelText.StyleCustomMode = true;
            this.panelText.TabIndex = 2;
            this.panelText.Text = "uiPanel2";
            this.panelText.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxOutMessage
            // 
            this.textBoxOutMessage.AutoWordSelection = true;
            this.textBoxOutMessage.BackColor = System.Drawing.Color.White;
            this.textBoxOutMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxOutMessage.FillColor = System.Drawing.Color.White;
            this.textBoxOutMessage.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxOutMessage.Location = new System.Drawing.Point(10, 5);
            this.textBoxOutMessage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxOutMessage.MinimumSize = new System.Drawing.Size(1, 1);
            this.textBoxOutMessage.Name = "textBoxOutMessage";
            this.textBoxOutMessage.Padding = new System.Windows.Forms.Padding(2);
            this.textBoxOutMessage.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.textBoxOutMessage.Size = new System.Drawing.Size(528, 236);
            this.textBoxOutMessage.Style = Sunny.UI.UIStyle.Custom;
            this.textBoxOutMessage.StyleCustomMode = true;
            this.textBoxOutMessage.TabIndex = 0;
            this.textBoxOutMessage.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.textBoxOutMessage.WordWrap = true;
            this.textBoxOutMessage.TextChanged += new System.EventHandler(this.textBoxOutMessage_TextChanged);
            // 
            // panelBtns
            // 
            this.panelBtns.BackColor = System.Drawing.Color.Gainsboro;
            this.panelBtns.ColumnCount = 2;
            this.panelBtns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelBtns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelBtns.Controls.Add(this.ubtnCancel, 1, 0);
            this.panelBtns.Controls.Add(this.ubtnStart, 0, 0);
            this.panelBtns.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBtns.Location = new System.Drawing.Point(0, 328);
            this.panelBtns.Margin = new System.Windows.Forms.Padding(0);
            this.panelBtns.Name = "panelBtns";
            this.panelBtns.RowCount = 1;
            this.panelBtns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelBtns.Size = new System.Drawing.Size(548, 47);
            this.panelBtns.Style = Sunny.UI.UIStyle.Custom;
            this.panelBtns.StyleCustomMode = true;
            this.panelBtns.TabIndex = 1;
            this.panelBtns.TagString = null;
            // 
            // ubtnCancel
            // 
            this.ubtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ubtnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ubtnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.ubtnCancel.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.ubtnCancel.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.ubtnCancel.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.ubtnCancel.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.ubtnCancel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ubtnCancel.ForeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(88)))), ((int)(((byte)(155)))));
            this.ubtnCancel.ForePressColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(88)))), ((int)(((byte)(155)))));
            this.ubtnCancel.ForeSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(88)))), ((int)(((byte)(155)))));
            this.ubtnCancel.Location = new System.Drawing.Point(277, 3);
            this.ubtnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.ubtnCancel.Name = "ubtnCancel";
            this.ubtnCancel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.ubtnCancel.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.ubtnCancel.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.ubtnCancel.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.ubtnCancel.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.ubtnCancel.Size = new System.Drawing.Size(268, 41);
            this.ubtnCancel.Style = Sunny.UI.UIStyle.Custom;
            this.ubtnCancel.StyleCustomMode = true;
            this.ubtnCancel.Symbol = 61453;
            this.ubtnCancel.TabIndex = 1;
            this.ubtnCancel.Text = "取消";
            this.ubtnCancel.Click += new System.EventHandler(this.ubtnCancel_Click);
            // 
            // ubtnStart
            // 
            this.ubtnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ubtnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ubtnStart.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.ubtnStart.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.ubtnStart.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.ubtnStart.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.ubtnStart.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.ubtnStart.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ubtnStart.ForeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(88)))), ((int)(((byte)(155)))));
            this.ubtnStart.ForePressColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(88)))), ((int)(((byte)(155)))));
            this.ubtnStart.ForeSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(88)))), ((int)(((byte)(155)))));
            this.ubtnStart.Location = new System.Drawing.Point(3, 3);
            this.ubtnStart.MinimumSize = new System.Drawing.Size(1, 1);
            this.ubtnStart.Name = "ubtnStart";
            this.ubtnStart.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.ubtnStart.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.ubtnStart.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.ubtnStart.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.ubtnStart.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.ubtnStart.Size = new System.Drawing.Size(268, 41);
            this.ubtnStart.Style = Sunny.UI.UIStyle.Custom;
            this.ubtnStart.StyleCustomMode = true;
            this.ubtnStart.Symbol = 61487;
            this.ubtnStart.TabIndex = 0;
            this.ubtnStart.Text = "打印";
            this.ubtnStart.Click += new System.EventHandler(this.ubtnStart_Click);
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.comboxSelectPrinter);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiPanel1.FillColor = System.Drawing.Color.Gainsboro;
            this.uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel1.Location = new System.Drawing.Point(0, 35);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 5);
            this.uiPanel1.Radius = 0;
            this.uiPanel1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.uiPanel1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uiPanel1.Size = new System.Drawing.Size(548, 47);
            this.uiPanel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiPanel1.StyleCustomMode = true;
            this.uiPanel1.TabIndex = 0;
            this.uiPanel1.Text = "uiPanel1";
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboxSelectPrinter
            // 
            this.comboxSelectPrinter.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboxSelectPrinter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.comboxSelectPrinter.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboxSelectPrinter.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboxSelectPrinter.FormattingEnabled = true;
            this.comboxSelectPrinter.ItemSelectBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.comboxSelectPrinter.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(251)))));
            this.comboxSelectPrinter.Location = new System.Drawing.Point(10, 7);
            this.comboxSelectPrinter.Name = "comboxSelectPrinter";
            this.comboxSelectPrinter.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.comboxSelectPrinter.Size = new System.Drawing.Size(528, 35);
            this.comboxSelectPrinter.Style = Sunny.UI.UIStyle.Custom;
            this.comboxSelectPrinter.StyleCustomMode = true;
            this.comboxSelectPrinter.TabIndex = 0;
            this.comboxSelectPrinter.SelectedIndexChanged += new System.EventHandler(this.comboxSelectPrinter_SelectedIndexChanged);
            // 
            // PrinterUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(548, 375);
            this.Controls.Add(this.uiTitlePanelPrinter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PrinterUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PrinterUI";
            this.Load += new System.EventHandler(this.PrinterUI_Load);
            this.uiTitlePanelPrinter.ResumeLayout(false);
            this.panelText.ResumeLayout(false);
            this.panelBtns.ResumeLayout(false);
            this.uiPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITitlePanel uiTitlePanelPrinter;
        private Sunny.UI.UIPanel panelText;
        private Sunny.UI.UIRichTextBox textBoxOutMessage;
        private Sunny.UI.UITableLayoutPanel panelBtns;
        private Sunny.UI.UISymbolButton ubtnCancel;
        private Sunny.UI.UISymbolButton ubtnStart;
        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UIComboboxEx comboxSelectPrinter;
    }
}