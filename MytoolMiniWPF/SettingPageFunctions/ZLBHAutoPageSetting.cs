using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MytoolMiniWPF.common;

namespace MytoolMiniWPF.views
{
    public partial class Settings
    {
       void SetPersonInfos()
        {
          string userName =  new DatabaseUnit().GetuserName();
          Dictionary<string,string> infoDict =   new DatabaseForZLBHPageAuto().QueryInfo(userName);
            
            
            try
            {
                ResidentPhysician.Value = infoDict["residentPhysician"];
                AttendingPhysician.Value = infoDict["attendingPhysician"];
                AssociateChiefPhysician.Value = infoDict["associateChiefPhysician"];
                QualityControlDoctor.Value = infoDict["qualityControlDoctor"];
                QualityControlNurse.Value = infoDict["qualityControlNurse"];
                HeadOfDepartment.Value = infoDict["headOfDepartment"];
            }
            catch (Exception ex)
            {
                
            }
        }

        private void LabelInput_TextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

          //  SavePersonInfos();
        }
        void SavePersonInfos()
        {
            new DatabaseForZLBHPageAuto().SaveInfoToDb(ResidentPhysician.Value.Trim(), AttendingPhysician.Value.Trim(), AssociateChiefPhysician.Value.Trim(), QualityControlDoctor.Value.Trim(), QualityControlNurse.Value.Trim(), HeadOfDepartment.Value.Trim());
        }
    }
}
