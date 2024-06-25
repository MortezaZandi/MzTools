using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.UI
{
    public interface IUIFactory
    {
        DataWizardSelectItemControl CreateDataWizardSelectItemControl(DataWizardDialog dataWizardDialog);
        DataWizardSelectCustomerControl CreateDataWizardSelectCustomerControl(DataWizardDialog dataWizardDialog);
        DataWizardSelectVendorControl CreateDataWizardSelectVendorControl(DataWizardDialog dataWizardDialog);
        DataWizardTestOptionsControl CreateDataWizardTestOptionsControl(DataWizardDialog dataWizardDialog);
        DataWizardSelectOrderPatternControl CreateDataWizardSelectOrderPatternControl(DataWizardDialog dataWizardDialog);
        ItemDetailsControl CreateItemDetailsControl(IConfirmableDialog parentDialog);
        //IDataControl CreateOrderDetailsControl(IConfirmableDialog parentDialog);
        CustomerDetailsControl CreateCustomerDetailsControl(IConfirmableDialog parentDialog);
        VendorDetailsControl CreateVendorDetailsControl(IConfirmableDialog parentDialog);
        ItemSelectControl CreateItemSelectControl(IConfirmableDialog parentDialog);
    }
}
