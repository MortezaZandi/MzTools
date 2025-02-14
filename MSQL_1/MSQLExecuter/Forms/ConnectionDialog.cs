using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

public class ConnectionDialog : Form
{
    private TextBox _serverTextBox;
    private TextBox _databaseTextBox;
    private TextBox _usernameTextBox;
    private TextBox _passwordTextBox;
    private CheckBox _integratedSecurityCheckBox;
    
    public ConnectionInfo ConnectionInfo { get; private set; }
    
    public ConnectionDialog()
    {
        InitializeComponents();
    }
    
    private void InitializeComponents()
    {
        this.Text = "Connect to Database";
        this.Size = new Size(400, 250);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.StartPosition = FormStartPosition.CenterParent;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(10),
            RowCount = 6,
            ColumnCount = 2
        };
        
        layout.Controls.Add(new Label { Text = "Server:" }, 0, 0);
        _serverTextBox = new TextBox { Width = 250 };
        layout.Controls.Add(_serverTextBox, 1, 0);
        
        layout.Controls.Add(new Label { Text = "Database:" }, 0, 1);
        _databaseTextBox = new TextBox { Width = 250 };
        layout.Controls.Add(_databaseTextBox, 1, 1);
        
        _integratedSecurityCheckBox = new CheckBox 
        { 
            Text = "Use Windows Authentication",
            AutoSize = true,
            Checked = true
        };
        layout.Controls.Add(_integratedSecurityCheckBox, 1, 2);
        
        layout.Controls.Add(new Label { Text = "Username:" }, 0, 3);
        _usernameTextBox = new TextBox 
        { 
            Width = 250,
            Enabled = false
        };
        layout.Controls.Add(_usernameTextBox, 1, 3);
        
        layout.Controls.Add(new Label { Text = "Password:" }, 0, 4);
        _passwordTextBox = new TextBox 
        { 
            Width = 250,
            UseSystemPasswordChar = true,
            Enabled = false
        };
        layout.Controls.Add(_passwordTextBox, 1, 4);
        
        var buttonPanel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.RightToLeft,
            Dock = DockStyle.Fill
        };
        
        var connectButton = new Button 
        { 
            Text = "Connect",
            DialogResult = DialogResult.OK
        };
        var cancelButton = new Button 
        { 
            Text = "Cancel",
            DialogResult = DialogResult.Cancel
        };
        
        buttonPanel.Controls.AddRange(new Control[] { cancelButton, connectButton });
        layout.Controls.Add(buttonPanel, 1, 5);
        
        this.Controls.Add(layout);
        this.AcceptButton = connectButton;
        this.CancelButton = cancelButton;
        
        _integratedSecurityCheckBox.CheckedChanged += (s, e) =>
        {
            _usernameTextBox.Enabled = !_integratedSecurityCheckBox.Checked;
            _passwordTextBox.Enabled = !_integratedSecurityCheckBox.Checked;
        };
        
        connectButton.Click += (s, e) =>
        {
            if (!ValidateAndCreateConnection())
            {
                ((CancelEventArgs)e).Cancel = true;
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        };
    }
    
    private bool ValidateAndCreateConnection()
    {
        if (!ValidateInput())
            return false;

        try
        {
            ConnectionInfo = new ConnectionInfo
            {
                Server = _serverTextBox.Text,
                Database = _databaseTextBox.Text,
                IntegratedSecurity = _integratedSecurityCheckBox.Checked,
                Username = _usernameTextBox.Text,
                Password = _passwordTextBox.Text
            };

            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error creating connection: {ex.Message}",
                "Connection Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return false;
        }
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(_serverTextBox.Text))
        {
            MessageBox.Show("Please enter a server name.", "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            _serverTextBox.Focus();
            return false;
        }
        
        if (string.IsNullOrWhiteSpace(_databaseTextBox.Text))
        {
            MessageBox.Show("Please enter a database name.", "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            _databaseTextBox.Focus();
            return false;
        }
        
        if (!_integratedSecurityCheckBox.Checked)
        {
            if (string.IsNullOrWhiteSpace(_usernameTextBox.Text))
            {
                MessageBox.Show("Please enter a username.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _usernameTextBox.Focus();
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(_passwordTextBox.Text))
            {
                MessageBox.Show("Please enter a password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _passwordTextBox.Focus();
                return false;
            }
        }
        
        return true;
    }
}