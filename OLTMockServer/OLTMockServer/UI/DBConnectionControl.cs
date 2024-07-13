using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OLTMockServer.DataStructures;
using System.Data.SqlClient;

namespace OLTMockServer.UI
{
    public partial class DBConnectionControl : DataWizardBaseControl, IDataControl
    {
        private readonly UIOperation closeOperation = new UIOperation("Close");
        private readonly UIOperation testOperation = new UIOperation("Test Connection");
        private readonly UIOperation okOperation = new UIOperation("OK");

        public DBConnectionControl(IConfirmableDialog parent) : base()
        {
            this.parentDialog = parent;
            InitializeComponent();
            InitOperations();
        }

        private IConfirmableDialog parentDialog;

        public IConfirmableDialog ParentDialog
        {
            get
            {
                return parentDialog;
            }
            set
            {
                parentDialog = value;
            }
        }

        private void InitOperations()
        {
            okOperation.OnSelected += OnOperationSelected;
            testOperation.OnSelected += OnOperationSelected;
            closeOperation.OnSelected += OnOperationSelected;

            SetOperationButtons(testOperation, closeOperation, okOperation);
        }

        private void OnOperationSelected(object sender, UIOperation uIOperation)
        {
            if (uIOperation.Id == okOperation.Id)
            {
                parentDialog?.OK();
            }

            if (uIOperation.Id == closeOperation.Id)
            {
                parentDialog?.Cancel();
            }

            if (uIOperation.Id == testOperation.Id)
            {
                TestDBConnection();
            }
        }

        public string ConnectionString
        {
            get
            {
                var sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
                {
                    InitialCatalog = txtCatalogName.Text,
                    DataSource = txtServerName.Text,
                    UserID = txtUserName.Text,
                    Password = txtPassword.Text,
                };

                return sqlConnectionStringBuilder.ToString();
            }
            set
            {
                if (value != null)
                {
                    var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(value);
                    txtCatalogName.Text = sqlConnectionStringBuilder.InitialCatalog;
                    txtServerName.Text = sqlConnectionStringBuilder.DataSource;
                    txtUserName.Text = sqlConnectionStringBuilder.UserID;
                    txtPassword.Text = sqlConnectionStringBuilder.Password;
                }
            }
        }

        private void TestDBConnection()
        {
            if (string.IsNullOrWhiteSpace(txtServerName.Text))
            {
                Utils.ShowError("Fill server name");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCatalogName.Text))
            {
                Utils.ShowError("Fill catalog name");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                Utils.ShowError("Fill user name");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                Utils.ShowError("Fill password");
                return;
            }

            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                InitialCatalog = txtCatalogName.Text,
                DataSource = txtServerName.Text,
                UserID = txtUserName.Text,
                Password = txtPassword.Text,
            };

            var connectionString = sqlConnectionStringBuilder.ToString();

            var dataProvider = new DataProvider(connectionString, true);

            var connectionError = string.Empty;

            if (dataProvider.TestConnection(out connectionError))
            {
                Utils.ShowInfo("Connected successfully.");
            }
            else
            {
                Utils.ShowError(connectionError);
            }
        }
    }
}
