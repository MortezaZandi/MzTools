using System.Drawing;
using System.Windows.Forms;
partial class MainForm
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

    private CheckedListBox checkedListServers;
    private Button btnSelectAll;
    private Button btnDeselectAll;
    private TextBox txtQuery;
    private Button btnExecute;
    private Button btnCancel;
    private ProgressBar progressBar;
    private ListView listResults;
    private Label lblServers;
    private Label lblQuery;
    private GroupBox groupBoxControls;
    private GroupBox groupBoxResults;

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        // Form configuration
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 600);
        this.MinimumSize = new System.Drawing.Size(816, 639);
        this.Text = "SQL Query Executor";
        this.StartPosition = FormStartPosition.CenterScreen;

        // Labels
        this.lblServers = new Label();
        this.lblServers.AutoSize = true;
        this.lblServers.Location = new Point(12, 9);
        this.lblServers.Size = new Size(100, 15);
        this.lblServers.Text = "Available Servers:";

        this.lblQuery = new Label();
        this.lblQuery.AutoSize = true;
        this.lblQuery.Location = new Point(280, 9);
        this.lblQuery.Size = new Size(100, 15);
        this.lblQuery.Text = "SQL Query:";

        // CheckedListBox for servers
        this.checkedListServers = new CheckedListBox();
        this.checkedListServers.Location = new Point(12, 27);
        this.checkedListServers.Size = new Size(250, 274);
        this.checkedListServers.FormattingEnabled = true;
        this.checkedListServers.CheckOnClick = true;

        // Select/Deselect buttons
        this.btnSelectAll = new Button();
        this.btnSelectAll.Location = new Point(12, 307);
        this.btnSelectAll.Size = new Size(120, 23);
        this.btnSelectAll.Text = "Select All";
        this.btnSelectAll.UseVisualStyleBackColor = true;

        this.btnDeselectAll = new Button();
        this.btnDeselectAll.Location = new Point(142, 307);
        this.btnDeselectAll.Size = new Size(120, 23);
        this.btnDeselectAll.Text = "Deselect All";
        this.btnDeselectAll.UseVisualStyleBackColor = true;

        // Query TextBox
        this.txtQuery = new TextBox();
        this.txtQuery.Location = new Point(280, 27);
        this.txtQuery.Multiline = true;
        this.txtQuery.Size = new Size(490, 60);
        this.txtQuery.ScrollBars = ScrollBars.Vertical;

        // Controls GroupBox
        this.groupBoxControls = new GroupBox();
        this.groupBoxControls.Text = "Controls";
        this.groupBoxControls.Location = new Point(280, 93);
        this.groupBoxControls.Size = new Size(490, 85);

        // Execute and Cancel buttons
        this.btnExecute = new Button();
        this.btnExecute.Location = new Point(10, 22);
        this.btnExecute.Size = new Size(75, 23);
        this.btnExecute.Text = "Execute";
        this.btnExecute.UseVisualStyleBackColor = true;

        this.btnCancel = new Button();
        this.btnCancel.Location = new Point(91, 22);
        this.btnCancel.Size = new Size(75, 23);
        this.btnCancel.Text = "Cancel";
        this.btnCancel.Enabled = false;
        this.btnCancel.UseVisualStyleBackColor = true;

        // Progress Bar
        this.progressBar = new ProgressBar();
        this.progressBar.Location = new Point(10, 51);
        this.progressBar.Size = new Size(470, 23);

        // Add controls to groupBoxControls
        this.groupBoxControls.Controls.Add(this.btnExecute);
        this.groupBoxControls.Controls.Add(this.btnCancel);
        this.groupBoxControls.Controls.Add(this.progressBar);

        // Results GroupBox
        this.groupBoxResults = new GroupBox();
        this.groupBoxResults.Text = "Results";
        this.groupBoxResults.Location = new Point(280, 184);
        this.groupBoxResults.Size = new Size(490, 366);

        // Results ListView
        this.listResults = new ListView();
        this.listResults.Location = new Point(10, 22);
        this.listResults.Size = new Size(470, 334);
        this.listResults.View = View.Details;
        this.listResults.FullRowSelect = true;
        this.listResults.GridLines = true;
        this.listResults.Columns.Add("Server", 150);
        this.listResults.Columns.Add("Version", 100);
        this.listResults.Columns.Add("Message", 160);
        this.listResults.Columns.Add("Time", 80);

        // Add ListView to groupBoxResults
        this.groupBoxResults.Controls.Add(this.listResults);

        // Add all controls to form
        this.Controls.AddRange(new Control[] {
                this.lblServers,
                this.lblQuery,
                this.checkedListServers,
                this.btnSelectAll,
                this.btnDeselectAll,
                this.txtQuery,
                this.groupBoxControls,
                this.groupBoxResults
            });

        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion
}