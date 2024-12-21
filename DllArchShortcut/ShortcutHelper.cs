using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DllArchShortcut
{
    internal static class ShortcutHelper
    {
        public static bool CreateShortcut(string shortcutName, string extesion, string executer)
        {
            try
            {
                var reg = Registry.ClassesRoot.OpenSubKey($"SystemFileAssociations\\{extesion}\\shell\\{shortcutName}\\Command", true);

                if (reg == null)
                {
                    reg = Registry.ClassesRoot.CreateSubKey($"SystemFileAssociations\\{extesion}\\shell\\{shortcutName}\\Command");
                }

                var shortcutKey = Registry.ClassesRoot.OpenSubKey($"SystemFileAssociations\\{extesion}\\shell\\{shortcutName}", true);

                shortcutKey.SetValue("", shortcutName);

                shortcutKey.SetValue("Icon", "%SystemRoot%\\system32\\shell32{extesion},171");

                var commandKey = Registry.ClassesRoot.OpenSubKey($"SystemFileAssociations\\{extesion}\\shell\\{shortcutName}\\Command", true);

                commandKey.SetValue("", $"{executer} %1");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsShortcutCreated(string shortcutName, string extesion, string executablePath)
        {
            try
            {
                var reg = Registry.ClassesRoot.OpenSubKey($"SystemFileAssociations\\{extesion}\\shell\\{shortcutName}\\Command", true);

                if (reg != null)
                {
                    var exec = reg.GetValue("");

                    if (exec != null)
                    {
                        return exec.ToString().ToLower() == $"{executablePath} %1".ToLower();
                    }
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
