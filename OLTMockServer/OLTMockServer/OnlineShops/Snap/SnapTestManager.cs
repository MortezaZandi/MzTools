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
            orderControl.Order = (SnapOrder)this.Server.CreateNewOrder(this.TestProject.OrderPattern, false);

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
            orderControl.Order = (SnapOrder)selectedOrder.LightClone();
            dataDialog.ClientSize = new Size(700, 460);
            if (dataDialog.ShowDialog() == DialogResult.OK)
            {
                var snapOrder = (SnapOrder)selectedOrder;
                //selectedOrder.Vendor = orderControl.Order.Vendor;
                //snapOrder.CreateDate = orderControl.Order.CreateDate;
                //snapOrder.StatusDescription = orderControl.Order.StatusDescription;
                //snapOrder.Customer = orderControl.Order.Customer;
                //snapOrder.AckTime = orderControl.Order.AckTime;
                //snapOrder.AcceptTime = orderControl.Order.AcceptTime;
                //snapOrder.Code = orderControl.Order.Code;
                //snapOrder.Items = orderControl.Order.Items;
                //snapOrder.PickTime = orderControl.Order.PickTime;
                //snapOrder.RejectTime = orderControl.Order.RejectTime;
                //snapOrder.StatusCode = orderControl.Order.StatusCode;
                //snapOrder.DeliveryMode = orderControl.Order.DeliveryMode;
                //snapOrder.VendorCode= orderControl.Order.VendorCode;
                //snapOrder.Vat= orderControl.Order.Vat;
                //snapOrder.Tax= orderControl.Order.Tax;
                //snapOrder.Price = orderControl.Order.Price;
                //snapOrder.Comment = orderControl.Order.Comment;
                Utils.SwapObjects(orderControl.Order, snapOrder, nameof(snapOrder.Items), nameof(snapOrder.Vendor), nameof(snapOrder.Customer), nameof(snapOrder.Activities));

                //send order with edit status code
                selectedOrder.AddActivity(Definitions.OrderActivityTypes.Edit, false);

                return selectedOrder;
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
