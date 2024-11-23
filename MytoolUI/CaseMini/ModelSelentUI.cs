using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace MytoolUI.CaseMini
{
    public partial class ModelSelentUI : UIForm
    {
        public bool SingleSelect
        {
            get {
                return singleSelect;
            }
            set {
            singleSelect = value;}
        }
        public bool MultiSelect
        {
            get {
            return multiSelect;}
            set {
            multiSelect= value;}
        }

        private bool singleSelect = false;
        private bool multiSelect = true;

        public ModelSelentUI()
        {
            InitializeComponent();
        }

        private void ModelSelentUI_Load(object sender, EventArgs e)
        {

        }

        private void uiRadioButton_CheckedChanged(object sender, EventArgs e)
        {   
            if (uiRadioButtonSingle.Checked) {
             singleSelect = true;
                multiSelect= false;
            }
            else
            {
                singleSelect= false;
                multiSelect= true;
            }

        }

        private void uiSymbolButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
            this.Close();
        }

        private void uiSymbolButtonEnsure_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.OK;

            this.Close();
        }
    }
}
