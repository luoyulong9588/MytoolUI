
namespace MytoolUI.OutpatientMainPage
{
    partial class MenuUI
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
            this.uHButtonMainPageBasicInfo = new Sunny.UI.UIHeaderButton();
            this.uHButtonChief = new Sunny.UI.UIHeaderButton();
            this.uiTableLayoutPanel1 = new Sunny.UI.UITableLayoutPanel();
            this.uiLabelChief = new Sunny.UI.UILabel();
            this.uiTextBoxChief = new Sunny.UI.UITextBox();
            this.uiTableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uHButtonMainPageBasicInfo
            // 
            this.uHButtonMainPageBasicInfo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uHButtonMainPageBasicInfo.Location = new System.Drawing.Point(29, 78);
            this.uHButtonMainPageBasicInfo.MinimumSize = new System.Drawing.Size(1, 1);
            this.uHButtonMainPageBasicInfo.Name = "uHButtonMainPageBasicInfo";
            this.uHButtonMainPageBasicInfo.Padding = new System.Windows.Forms.Padding(0, 8, 0, 3);
            this.uHButtonMainPageBasicInfo.Radius = 0;
            this.uHButtonMainPageBasicInfo.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uHButtonMainPageBasicInfo.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uHButtonMainPageBasicInfo.Size = new System.Drawing.Size(100, 88);
            this.uHButtonMainPageBasicInfo.TabIndex = 0;
            this.uHButtonMainPageBasicInfo.Text = "首页-基本信息";
            // 
            // uHButtonChief
            // 
            this.uHButtonChief.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uHButtonChief.Location = new System.Drawing.Point(583, 78);
            this.uHButtonChief.MinimumSize = new System.Drawing.Size(1, 1);
            this.uHButtonChief.Name = "uHButtonChief";
            this.uHButtonChief.Padding = new System.Windows.Forms.Padding(0, 8, 0, 3);
            this.uHButtonChief.Radius = 0;
            this.uHButtonChief.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uHButtonChief.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uHButtonChief.Size = new System.Drawing.Size(100, 88);
            this.uHButtonChief.TabIndex = 1;
            this.uHButtonChief.Text = "首页-就诊信息";
            // 
            // uiTableLayoutPanel1
            // 
            this.uiTableLayoutPanel1.ColumnCount = 2;
            this.uiTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uiTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uiTableLayoutPanel1.Controls.Add(this.uiTextBoxChief, 1, 0);
            this.uiTableLayoutPanel1.Controls.Add(this.uiLabelChief, 0, 0);
            this.uiTableLayoutPanel1.Location = new System.Drawing.Point(161, 78);
            this.uiTableLayoutPanel1.Name = "uiTableLayoutPanel1";
            this.uiTableLayoutPanel1.RowCount = 1;
            this.uiTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uiTableLayoutPanel1.Size = new System.Drawing.Size(377, 88);
            this.uiTableLayoutPanel1.TabIndex = 2;
            this.uiTableLayoutPanel1.TagString = null;
            // 
            // uiLabelChief
            // 
            this.uiLabelChief.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabelChief.Location = new System.Drawing.Point(3, 0);
            this.uiLabelChief.Name = "uiLabelChief";
            this.uiLabelChief.Size = new System.Drawing.Size(100, 23);
            this.uiLabelChief.TabIndex = 0;
            this.uiLabelChief.Text = "主诉选择:";
            this.uiLabelChief.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiTextBoxChief
            // 
            this.uiTextBoxChief.ButtonSymbol = 61761;
            this.uiTextBoxChief.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBoxChief.FillColor = System.Drawing.Color.White;
            this.uiTextBoxChief.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTextBoxChief.Location = new System.Drawing.Point(192, 5);
            this.uiTextBoxChief.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTextBoxChief.Maximum = 2147483647D;
            this.uiTextBoxChief.Minimum = -2147483648D;
            this.uiTextBoxChief.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTextBoxChief.Name = "uiTextBoxChief";
            this.uiTextBoxChief.Size = new System.Drawing.Size(150, 29);
            this.uiTextBoxChief.TabIndex = 1;
            this.uiTextBoxChief.Text = "uiTextBox1";
            this.uiTextBoxChief.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MenuUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.uiTableLayoutPanel1);
            this.Controls.Add(this.uHButtonChief);
            this.Controls.Add(this.uHButtonMainPageBasicInfo);
            this.Name = "MenuUI";
            this.Text = "MenuUI";
            this.uiTableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIHeaderButton uHButtonMainPageBasicInfo;
        private Sunny.UI.UIHeaderButton uHButtonChief;
        private Sunny.UI.UITableLayoutPanel uiTableLayoutPanel1;
        private Sunny.UI.UITextBox uiTextBoxChief;
        private Sunny.UI.UILabel uiLabelChief;
    }
}