using OLTMockServer.DataStructures;
using OLTMockServer.DataStructures.Snap;
using OLTMockServer.MockServers;
using OLTMockServer.UI;
using OLTMockServer.UI.Snap;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLTMockServer
{
    public class SnapTestManager : TestManager
    {
        public SnapTestManager(SnapMockServer server) : base(server)
        {

        }

        public override DataWizardSelectCustomerControl CreateDataWizardSelectCustomerControl(DataWizardDialog dataWizardDialog)
        {
            return new DataWizardSelectCustomerControl(dataWizardDialog);
        }

        public override DataWizardSelectItemControl CreateDataWizardSelectItemControl(DataWizardDialog dataWizardDialog)
        {
            return new DataWizardSelectItemControl(dataWizardDialog);
        }

        public override DataWizardSelectOrderPatternControl CreateDataWizardSelectOrderPatternControl(DataWizardDialog dataWizardDialog)
        {
            return new DataWizardSelectOrderPatternControl(dataWizardDialog, typeof(SnapOrder));
        }

        public override DataWizardSelectVendorControl CreateDataWizardSelectVendorControl(DataWizardDialog dataWizardDialog)
        {
            return new DataWizardSelectVendorControl(dataWizardDialog);
        }

        public override DataWizardTestOptionsControl CreateDataWizardTestOptionsControl(DataWizardDialog dataWizardDialog)
        {
            return new DataWizardTestOptionsControl(dataWizardDialog);
        }

        public override CustomerDetailsControl CreateCustomerDetailsControl(IConfirmableDialog parentDialog)
        {
            return new CustomerDetailsControl(parentDialog);
        }

        public override ItemDetailsControl CreateItemDetailsControl(IConfirmableDialog parentDialog)
        {
            return new ItemDetailsControl(parentDialog);
        }

        //public override IDataControl CreateOrderDetailsControl(IConfirmableDialog parentDialog)
        //{
        //    return new UI.Snap.SnapOrderDetailsControl(parentDialog, this);
        //}

        public override VendorDetailsControl CreateVendorDetailsControl(IConfirmableDialog parentDialog)
        {
            return new VendorDetailsControl(parentDialog);
        }

        public override ItemSelectControl CreateItemSelectControl(IConfirmableDialog parentDialog)
        {
            return new ItemSelectControl(this, parentDialog) { Items = TestProject.Items };
        }

        public override Order CreateNewOrderUsingUI()
        {
            var orderControl = new SnapOrderDetailsControl(null, this);
            var dataDialog = new DataDialog(orderControl);
            orderControl.ParentDialog = dataDialog;
            orderControl.Order = (SnapOrder)this.Server.CreateNewOrder(this.TestProject, false);

            bool saveOrderGeneratorData = true;
            if (saveOrderGeneratorData)
            {
                var path = TestProject.IsTemp ? TestProject.TempFilePath : TestProject.SaveFilePath;
                var tempTest = ImportTestProject(path);
                tempTest.OrderPattern = TestProject.OrderPattern;
                SaveTestProject(tempTest, path);
            }

            dataDialog.ClientSize = new Size(700, 460);

            if (dataDialog.ShowDialog() == DialogResult.OK)
            {
                return orderControl.Order;
            }

            return null;
        }

        public override Order EditOrderUnigUI(Order selectedOrder)
        {
            var orderControl = new SnapOrderDetailsControl(null, this);
            var dataDialog = new DataDialog(orderControl);
            orderControl.ParentDialog = dataDialog;
            orderControl.Order = (SnapOrder)selectedOrder.LightClone();
            dataDialog.ClientSize = new Size(700, 460);
            if (dataDialog.ShowDialog() == DialogResult.OK)
            {
                var editedOrde = orderControl.Order;

                //send order with edit status code
                editedOrde.AddActivity(Definitions.OrderActivityTypes.Edit, false);
                
                //ToDo: Should i reset create-date in edited version if order edited?

                var mainOrderPositionInList = TestProject.Orders.FindLastIndex(o => o.Code == selectedOrder.Code);

                if (mainOrderPositionInList == TestProject.Orders.Count - 1)
                {
                    TestProject.Orders.Add(editedOrde);
                }
                else
                {
                    TestProject.Orders.Insert(mainOrderPositionInList + 1, editedOrde);
                }

                editedOrde.AddLog($"Order created by EDIT(Manual), Main order: '{selectedOrder.Code}' UID:{selectedOrder.UId}");

                return editedOrde;
            }

            return null;
        }

        protected override Vendor GetTargetVendorOfOrder(Order order)
        {
            var snapOrder = (SnapOrder)order;

            foreach (var vendor in TestProject.Vendors)
            {
                if (vendor.Code == snapOrder.VendorCode)
                {
                    return vendor;
                }
            }

            throw new ApplicationException($"Vendor '{snapOrder.VendorCode}' not found in the list of vendors. Go to test options and define this vendor.");
        }
    }
}
