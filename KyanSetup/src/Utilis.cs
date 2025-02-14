using System.IO;
using System.Windows.Forms;

namespace KyanSetup
{
    public static class Utilis
    {
        public static string FormatSizeString(long sizeMB)
        {
            if (sizeMB >= 1000)
            {
                var gig = sizeMB / 1024f;
                return $"{gig:N1} GB";
            }
            else
            {
                return $"{sizeMB:N0} MB";
            }
        }

        public static long GetTotalDriveFreeSpace(string path)
        {
            var driveName = Path.GetPathRoot(path);

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                {
                    return drive.TotalFreeSpace;
                }
            }

            return -1;
        }

        private static string PrepareMessageTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                title = Application.ProductName;
            }

            return title;
        }

        public static void ShowErrorMessage(string message, string title = null)
        {
            title = PrepareMessageTitle(title);
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowInformationMessage(string message, string title = null)
        {
            title = PrepareMessageTitle(title);
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowWarningMessage(string message, string title = null)
        {
            title = PrepareMessageTitle(title);
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowQuestionMessage(string message, string title = null)
        {
            title = PrepareMessageTitle(title);
            return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}
