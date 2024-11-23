using Sunny.UI;

namespace MytoolUI
{
    partial class CaseUIMini
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
            this.printDocumentReport = new System.Drawing.Printing.PrintDocument();
            this.printDocumentCase = new System.Drawing.Printing.PrintDocument();
            this.uBtnReportMini = new Sunny.UI.UISymbolButton();
            this.uiTableLayoutPanelDesktop = new Sunny.UI.UITableLayoutPanel();
            this.uBtnOutPatientMini = new Sunny.UI.UISymbolButton();
            this.uBtnSettingMini = new Sunny.UI.UISymbolButton();
            this.uBtnInvestigationMini = new Sunny.UI.UISymbolButton();
            this.uBtnNoteMini = new Sunny.UI.UISymbolButton();
            this.uiSymbolButtonCloseMini = new Sunny.UI.UISymbolButton();
            this.uBtnRestMini = new Sunny.UI.UISymbolButton();
            this.uBtnStrMini = new Sunny.UI.UISymbolButton();
            this.uiToolTipMini = new Sunny.UI.UIToolTip(this.components);
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.uiTableLayoutPanelDesktop.SuspendLayout();
            this.SuspendLayout();
            // 
            // printDocumentReport
            // 
            this.printDocumentReport.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocumentReport_PrintPage);
            // 
            // printDocumentCase
            // 
            this.printDocumentCase.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocumentCase_PrintPage);
            // 
            // uBtnReportMini
            // 
            this.uBtnReportMini.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnReportMini.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uBtnReportMini.Font = new System.Drawing.Font("微软雅黑", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uBtnReportMini.Location = new System.Drawing.Point(2, 2);
            this.uBtnReportMini.Margin = new System.Windows.Forms.Padding(2);
            this.uBtnReportMini.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnReportMini.Name = "uBtnReportMini";
            this.uBtnReportMini.Size = new System.Drawing.Size(29, 26);
            this.uBtnReportMini.Style = Sunny.UI.UIStyle.Custom;
            this.uBtnReportMini.Symbol = 162362;
            this.uBtnReportMini.SymbolOffset = new System.Drawing.Point(1, 1);
            this.uBtnReportMini.SymbolSize = 18;
            this.uBtnReportMini.TabIndex = 0;
            this.uiToolTipMini.SetToolTip(this.uBtnReportMini, "打印感染病例报告卡");
            this.uBtnReportMini.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragWindow_MouseDown);
            // 
            // uiTableLayoutPanelDesktop
            // 
            this.uiTableLayoutPanelDesktop.BackColor = System.Drawing.Color.Transparent;
            this.uiTableLayoutPanelDesktop.ColumnCount = 8;
            this.uiTableLayoutPanelDesktop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.uiTableLayoutPanelDesktop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.uiTableLayoutPanelDesktop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.uiTableLayoutPanelDesktop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.uiTableLayoutPanelDesktop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.uiTableLayoutPanelDesktop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.uiTableLayoutPanelDesktop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.uiTableLayoutPanelDesktop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.uiTableLayoutPanelDesktop.Controls.Add(this.uBtnOutPatientMini, 1, 0);
            this.uiTableLayoutPanelDesktop.Controls.Add(this.uBtnReportMini, 0, 0);
            this.uiTableLayoutPanelDesktop.Controls.Add(this.uBtnSettingMini, 3, 0);
            this.uiTableLayoutPanelDesktop.Controls.Add(this.uBtnInvestigationMini, 2, 0);
            this.uiTableLayoutPanelDesktop.Controls.Add(this.uBtnNoteMini, 4, 0);
            this.uiTableLayoutPanelDesktop.Controls.Add(this.uiSymbolButtonCloseMini, 7, 0);
            this.uiTableLayoutPanelDesktop.Controls.Add(this.uBtnRestMini, 6, 0);
            this.uiTableLayoutPanelDesktop.Controls.Add(this.uBtnStrMini, 5, 0);
            this.uiTableLayoutPanelDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTableLayoutPanelDesktop.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTableLayoutPanelDesktop.Location = new System.Drawing.Point(0, 0);
            this.uiTableLayoutPanelDesktop.Margin = new System.Windows.Forms.Padding(0);
            this.uiTableLayoutPanelDesktop.Name = "uiTableLayoutPanelDesktop";
            this.uiTableLayoutPanelDesktop.RowCount = 1;
            this.uiTableLayoutPanelDesktop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uiTableLayoutPanelDesktop.Size = new System.Drawing.Size(265, 30);
            this.uiTableLayoutPanelDesktop.Style = Sunny.UI.UIStyle.Custom;
            this.uiTableLayoutPanelDesktop.TabIndex = 1;
            this.uiTableLayoutPanelDesktop.TagString = null;
            // 
            // uBtnOutPatientMini
            // 
            this.uBtnOutPatientMini.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnOutPatientMini.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uBtnOutPatientMini.Font = new System.Drawing.Font("微软雅黑", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uBtnOutPatientMini.Location = new System.Drawing.Point(35, 2);
            this.uBtnOutPatientMini.Margin = new System.Windows.Forms.Padding(2);
            this.uBtnOutPatientMini.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnOutPatientMini.Name = "uBtnOutPatientMini";
            this.uBtnOutPatientMini.Size = new System.Drawing.Size(29, 26);
            this.uBtnOutPatientMini.Style = Sunny.UI.UIStyle.Custom;
            this.uBtnOutPatientMini.Symbol = 61530;
            this.uBtnOutPatientMini.SymbolOffset = new System.Drawing.Point(0, 1);
            this.uBtnOutPatientMini.SymbolSize = 18;
            this.uBtnOutPatientMini.TabIndex = 1;
            this.uiToolTipMini.SetToolTip(this.uBtnOutPatientMini, "填写门诊首页");
            this.uBtnOutPatientMini.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragWindow_MouseDown);
            // 
            // uBtnSettingMini
            // 
            this.uBtnSettingMini.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnSettingMini.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uBtnSettingMini.Font = new System.Drawing.Font("微软雅黑", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uBtnSettingMini.Location = new System.Drawing.Point(101, 2);
            this.uBtnSettingMini.Margin = new System.Windows.Forms.Padding(2);
            this.uBtnSettingMini.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnSettingMini.Name = "uBtnSettingMini";
            this.uBtnSettingMini.Size = new System.Drawing.Size(29, 26);
            this.uBtnSettingMini.Style = Sunny.UI.UIStyle.Custom;
            this.uBtnSettingMini.Symbol = 61459;
            this.uBtnSettingMini.SymbolOffset = new System.Drawing.Point(1, 1);
            this.uBtnSettingMini.SymbolSize = 18;
            this.uBtnSettingMini.TabIndex = 2;
            this.uiToolTipMini.SetToolTip(this.uBtnSettingMini, "设置流调表的项目\r\n（单击切换隐藏）");
            this.uBtnSettingMini.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragWindow_MouseDown);
            // 
            // uBtnInvestigationMini
            // 
            this.uBtnInvestigationMini.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnInvestigationMini.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uBtnInvestigationMini.Font = new System.Drawing.Font("微软雅黑", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uBtnInvestigationMini.Location = new System.Drawing.Point(68, 2);
            this.uBtnInvestigationMini.Margin = new System.Windows.Forms.Padding(2);
            this.uBtnInvestigationMini.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnInvestigationMini.Name = "uBtnInvestigationMini";
            this.uBtnInvestigationMini.Size = new System.Drawing.Size(29, 26);
            this.uBtnInvestigationMini.Style = Sunny.UI.UIStyle.Custom;
            this.uBtnInvestigationMini.Symbol = 61632;
            this.uBtnInvestigationMini.SymbolSize = 18;
            this.uBtnInvestigationMini.TabIndex = 4;
            this.uiToolTipMini.SetToolTip(this.uBtnInvestigationMini, "填写住院首页");
            this.uBtnInvestigationMini.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragWindow_MouseDown);
            // 
            // uBtnNoteMini
            // 
            this.uBtnNoteMini.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnNoteMini.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uBtnNoteMini.Font = new System.Drawing.Font("微软雅黑", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uBtnNoteMini.Location = new System.Drawing.Point(134, 2);
            this.uBtnNoteMini.Margin = new System.Windows.Forms.Padding(2);
            this.uBtnNoteMini.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnNoteMini.Name = "uBtnNoteMini";
            this.uBtnNoteMini.Size = new System.Drawing.Size(29, 26);
            this.uBtnNoteMini.Style = Sunny.UI.UIStyle.Custom;
            this.uBtnNoteMini.Symbol = 362213;
            this.uBtnNoteMini.SymbolOffset = new System.Drawing.Point(0, 1);
            this.uBtnNoteMini.SymbolSize = 18;
            this.uBtnNoteMini.TabIndex = 3;
            this.uiToolTipMini.SetToolTip(this.uBtnNoteMini, "记事小本");
            this.uBtnNoteMini.Click += new System.EventHandler(this.uBtnNote_Click);
            this.uBtnNoteMini.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragWindow_MouseDown);
            // 
            // uiSymbolButtonCloseMini
            // 
            this.uiSymbolButtonCloseMini.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButtonCloseMini.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiSymbolButtonCloseMini.Font = new System.Drawing.Font("微软雅黑", 4.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButtonCloseMini.Location = new System.Drawing.Point(233, 2);
            this.uiSymbolButtonCloseMini.Margin = new System.Windows.Forms.Padding(2);
            this.uiSymbolButtonCloseMini.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButtonCloseMini.Name = "uiSymbolButtonCloseMini";
            this.uiSymbolButtonCloseMini.Size = new System.Drawing.Size(30, 26);
            this.uiSymbolButtonCloseMini.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolButtonCloseMini.Symbol = 61457;
            this.uiSymbolButtonCloseMini.SymbolOffset = new System.Drawing.Point(0, 1);
            this.uiSymbolButtonCloseMini.SymbolSize = 18;
            this.uiSymbolButtonCloseMini.TabIndex = 5;
            this.uiToolTipMini.SetToolTip(this.uiSymbolButtonCloseMini, "退出程序");
            this.uiSymbolButtonCloseMini.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragWindow_MouseDown);
            // 
            // uBtnRestMini
            // 
            this.uBtnRestMini.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnRestMini.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uBtnRestMini.Font = new System.Drawing.Font("微软雅黑", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uBtnRestMini.Location = new System.Drawing.Point(200, 2);
            this.uBtnRestMini.Margin = new System.Windows.Forms.Padding(2);
            this.uBtnRestMini.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnRestMini.Name = "uBtnRestMini";
            this.uBtnRestMini.Size = new System.Drawing.Size(29, 26);
            this.uBtnRestMini.Style = Sunny.UI.UIStyle.Custom;
            this.uBtnRestMini.Symbol = 62162;
            this.uBtnRestMini.SymbolOffset = new System.Drawing.Point(0, 1);
            this.uBtnRestMini.SymbolSize = 18;
            this.uBtnRestMini.TabIndex = 3;
            this.uiToolTipMini.SetToolTip(this.uBtnRestMini, "退出mini模式");
            this.uBtnRestMini.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragWindow_MouseDown);
            // 
            // uBtnStrMini
            // 
            this.uBtnStrMini.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnStrMini.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uBtnStrMini.Font = new System.Drawing.Font("微软雅黑", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uBtnStrMini.Location = new System.Drawing.Point(167, 2);
            this.uBtnStrMini.Margin = new System.Windows.Forms.Padding(2);
            this.uBtnStrMini.MinimumSize = new System.Drawing.Size(1, 1);
            this.uBtnStrMini.Name = "uBtnStrMini";
            this.uBtnStrMini.Size = new System.Drawing.Size(29, 26);
            this.uBtnStrMini.Style = Sunny.UI.UIStyle.Custom;
            this.uBtnStrMini.Symbol = 361642;
            this.uBtnStrMini.SymbolOffset = new System.Drawing.Point(0, 1);
            this.uBtnStrMini.SymbolSize = 18;
            this.uBtnStrMini.TabIndex = 3;
            this.uiToolTipMini.SetToolTip(this.uBtnStrMini, "文本格式化");
            this.uBtnStrMini.Click += new System.EventHandler(this.uBtnStrMini_Click);
            this.uBtnStrMini.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragWindow_MouseDown);
            // 
            // uiToolTipMini
            // 
            this.uiToolTipMini.AutoPopDelay = 5000;
            this.uiToolTipMini.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.uiToolTipMini.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.uiToolTipMini.InitialDelay = 100;
            this.uiToolTipMini.OwnerDraw = true;
            this.uiToolTipMini.RectColor = System.Drawing.Color.Transparent;
            this.uiToolTipMini.ReshowDelay = 150;
            // 
            // CaseUIMini
            // 
            this.AllowShowTitle = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.ClientSize = new System.Drawing.Size(265, 30);
            this.Controls.Add(this.uiTableLayoutPanelDesktop);
            this.ExtendSymbolSize = 18;
            this.Font = new System.Drawing.Font("微软雅黑", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1493, 818);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1, 1);
            this.Name = "CaseUIMini";
            this.Opacity = 0.86D;
            this.Padding = new System.Windows.Forms.Padding(0);
            this.RectColor = System.Drawing.Color.Gray;
            this.ShowInTaskbar = false;
            this.ShowTitle = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "CaseUIMini";
            this.TitleColor = System.Drawing.Color.Gray;
            this.TitleFont = new System.Drawing.Font("微软雅黑", 4.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TitleHeight = 31;
            this.TopMost = true;
            this.uiTableLayoutPanelDesktop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UISymbolButton uBtnReportMini;
        private Sunny.UI.UITableLayoutPanel uiTableLayoutPanelDesktop;
        private Sunny.UI.UISymbolButton uBtnRestMini;
        private Sunny.UI.UISymbolButton uBtnSettingMini;
        private Sunny.UI.UISymbolButton uBtnOutPatientMini;
        private Sunny.UI.UISymbolButton uBtnInvestigationMini;
        private System.Drawing.Printing.PrintDocument printDocumentReport;
        private System.Drawing.Printing.PrintDocument printDocumentCase;
        private Sunny.UI.UIToolTip uiToolTipMini;
        private Sunny.UI.UISymbolButton uiSymbolButtonCloseMini;
        private Sunny.UI.UISymbolButton uBtnNoteMini;
        private System.Windows.Forms.FontDialog fontDialog;
        private Sunny.UI.UISymbolButton uBtnStrMini;

        //  create Control for myself;
        private UICheckBox uiCheckBoxfill = new UICheckBox();
        private UICheckBox uiCheckcBoxContact = new UICheckBox();
        private UIButton ubtnFontUpper = new UIButton();
        private UIButton ubtnFontDown = new UIButton();
        private UIButton ubtnFontSelect = new UIButton();
        private UIButton ubtnFontBold = new UIButton();
        private UIButton ubtnClean = new UIButton();
        private UIButton ubtnUnderLine = new UIButton();
        private UIButton ubtnItalic = new UIButton();
        private UIButton ubtnBuildCase = new UIButton();
        private UIButton ubtnCopyCase = new UIButton();
        private UIButton ubtnClearCase = new UIButton();
        private UIButton ubtnLaboratory = new UIButton();
        private UIButton ubtnDrug = new UIButton();
        private UIRichTextBox uTextBox = new UIRichTextBox();
        private UIRichTextBox uTextBoxCase = new UIRichTextBox();
        private UIRichTextBox uTextBoxResult = new UIRichTextBox();
        private UICheckBox uiCheckcBoxSingleLine = new UICheckBox();
        private UIComboboxEx uCbtn = new UIComboboxEx();
    }
}