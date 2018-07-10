using System;
using System.IO;
using System.Linq;

namespace Infrastructure.Logging.Utils
{
    internal static class SystemDriveUtils
    {
        public static string GetFilePath(string applicationName)
        {
            var fileName = $@"Logs\{applicationName}\{applicationName}.log";

            var sysDrive = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));

            var allDriveInfos = DriveInfo.GetDrives();

            if (Directory.Exists(@"F:\Logs"))
                return $@"F:\{fileName}";

            if (Directory.Exists(@"C:\Logs"))
                return $@"C:\{fileName}";

            if (Directory.Exists(@"D:\Logs"))
                return $@"D:\{fileName}";

            foreach (var drive in allDriveInfos)
            {
                if (drive.DriveType == DriveType.Fixed && drive.Name != sysDrive)
                {
                    if (Directory.Exists($"{drive.Name}Logs"))
                        return $"{drive.Name}{fileName}";
                }
            }

            var availableDrive =
                allDriveInfos
                    .Where(d => d.DriveType == DriveType.Fixed && d.Name != sysDrive)
                    .Select(d => d.Name)
                    .FirstOrDefault();

            if (string.IsNullOrEmpty(availableDrive))
            {
                availableDrive =
                    allDriveInfos
                        .Where(d => d.DriveType == DriveType.Fixed)
                        .Select(d => d.Name)
                        .FirstOrDefault();
            }

            if (string.IsNullOrEmpty(availableDrive))
                throw new ArgumentNullException(nameof(availableDrive));

            return $"{availableDrive}{fileName}";
        }
    }
}
