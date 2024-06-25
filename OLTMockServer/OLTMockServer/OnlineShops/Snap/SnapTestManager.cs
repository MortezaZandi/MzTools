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
            orderControl.Order = (SnapOrder)this.Server.CreateNewOrder(this.TestProject.OrderPattern);

            bool saveOrderGeneratorData = true;
            if (saveOrderGeneratorData)
            {
                var tempTest = ImportTestProject(TestProject.SaveFilePath);
                tempTest.OrderPattern = TestProject.OrderPattern;
                SaveTestProject(tempTest, tempTest.SaveFilePath);
            }

            dataDialog.ClientSize = new Size(700, 460);

            if (dataDialog.ShowDialog() == DialogResult.OK)
            {
                return orderControl.Order;
            }

            return null;
        }

        public override object EditOrderUnigUI(Order selectedOrder)
        {
            var orderControl = new SnapOrderDetailsControl(null, this);
            var dataDialog = new DataDialog(orderControl);
            orderControl.ParentDialog = dataDialog;
            orderControl.Order = (SnapOrder)selectedOrder.Clone();
            dataDialog.ClientSize = new Size(700, 460);
            if (dataDialog.ShowDialog() == DialogResult.OK)
            {
                selectedOrder.Vendor = orderControl.Order.Vendor;
                selectedOrder.CreateDate = orderControl.Order.CreateDate;
                selectedOrder.StatusDescription = orderControl.Order.StatusDescription;
                selectedOrder.Customer = orderControl.Order.Customer;
                selectedOrder.AckTime = orderControl.Order.AckTime;
                selectedOrder.AcceptTime = orderControl.Order.AcceptTime;
                selectedOrder.Code = orderControl.Order.Code;
                selectedOrder.Items = orderControl.Order.Items;
                selectedOrder.PickTime = orderControl.Order.PickTime;
                selectedOrder.RejectTime = orderControl.Order.RejectTime;
                selectedOrder.StatusCode = orderControl.Order.StatusCode;
                selectedOrder.DeliveryMode = orderControl.Order.DeliveryMode;

                //send order with edit status code
                //...

                return selectedOrder;
            }

            return null;
        }
    }
}
