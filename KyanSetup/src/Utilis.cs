using System.IO;

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
    }
}
