﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace DllArchShortcut
{
    public partial class Form1 : Form
    {

        internal const string AppName = "Assembly Info";

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
           (
               int nLeftRect,     // x-coordinate of upper-left corner
               int nTopRect,      // y-coordinate of upper-left corner
               int nRightRect,    // x-coordinate of lower-right corner
               int nBottomRect,   // y-coordinate of lower-right corner
               int nWidthEllipse, // width of ellipse
               int nHeightEllipse // height of ellipse
           );

        public Form1()
        {
            InitializeComponent();

            CreateRoundForm(10);
        }

        private void CreateRoundForm(int radius)
        {
            this.FormBorderStyle = FormBorderStyle.None;

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, radius, radius));

            lblTitle.moveOtherWithMouse(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            RegisterAppShortCutIfNotRegistered();

            ShowAssemblyInfo();

            ShowDialogInCurrentScreen();
        }

        private void RegisterAppShortCutIfNotRegistered()
        {
            try
            {
                var name = AppName;

                var executer = Application.ExecutablePath;

                if (!ShortcutHelper.IsShortcutCreated(name, ".dll", executer))
                {
                    ShortcutHelper.CreateShortcut(name, ".dll", executer);
                }

                if (!ShortcutHelper.IsShortcutCreated(name, ".exe", executer))
                {
                    ShortcutHelper.CreateShortcut(name, ".exe", executer);
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error in registering shortcut item in registry.\n{ex.Message}");
            }
        }

        private void ShowAssemblyInfo()
        {
            var args = Environment.GetCommandLineArgs();
            string assemblyFilePath = null;

            if (args.Length > 1)
            {
                try
                {
                    assemblyFilePath = string.Join(" ", args.Skip(1).ToArray());

                    var assembyName = Path.GetFileName(assemblyFilePath);

                    var assemblyPath = Path.GetDirectoryName(assemblyFilePath);

                    lblDllName.Text = assembyName;
                    lblDllPath.Text = assemblyPath;
                    lblDllArc.Text = DllHelper.PrintDllInfo(assemblyFilePath);

                    assemblyFilePath = DllHelper.GetShortPath(assemblyFilePath);

                    try
                    {
                        var asm = AssemblyName.GetAssemblyName(assemblyFilePath);
                        lblAssemblyVersion.Text = asm.Version.ToString();
                    }
                    catch (Exception ex)
                    {
                        lblAssemblyVersion.Text = "Unable to load assembly.";
                    }

                    try
                    {
                        var asm = Assembly.LoadFrom(assemblyFilePath).GetName();

                        if (asm.GetPublicKey().Length > 0)
                        {
                            lblStronglyNamed.Text = "Sigend";
                            lblStronglyNamed.ForeColor = Color.Green;
                        }
                        else
                        {
                            lblStronglyNamed.Text = "Not signed";
                            lblStronglyNamed.ForeColor = Color.Red;
                        }
                    }
                    catch (Exception)
                    {
                        lblStronglyNamed.Text = "?";
                    }

                    try
                    {
                        lblAssemblyFile.Text = System.Diagnostics.FileVersionInfo.GetVersionInfo(assemblyFilePath).FileVersion;
                        var fileInfo = new FileInfo(assemblyFilePath);
                        lblAssemblyCreateDate.Text = fileInfo.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
                        lblAssemblyModifyDate.Text = fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    catch (Exception exx)
                    {
                    }
                }
                catch (Exception ex)
                {
                    ShowError($"Error in reading assembly info: {ex.Message}{Environment.NewLine}AssemblyFilePath: {assemblyFilePath}");
                    Close();
                }
            }
            else
            {
                ShowInfo($"Right click on dll files or exe files and click {AppName} to see their architecture");
                Close();
            }
        }

        private void ShowDialogInCurrentScreen()
        {
            var screen = Screen.FromPoint(Cursor.Position);
            this.StartPosition = FormStartPosition.Manual;
            this.Left = screen.Bounds.Left + screen.Bounds.Width / 2 - this.Width / 2;
            this.Top = screen.Bounds.Top + screen.Bounds.Height / 2 - this.Height / 2;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void ShowError(string message, string title = null)
        {
            MessageBox.Show(
                message,
                title ?? AppName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }


        private void ShowInfo(string message, string title = null)
        {
            MessageBox.Show(
                message,
                title ?? AppName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
