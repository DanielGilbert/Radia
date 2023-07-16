using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radia.Tests
{
    [TestClass]
    public static class TestDataInitializer
    {
        [AssemblyInitialize()]
        public static void TestAssemblyInitialize(TestContext testContext)
        {
            string testDataSourceDirPath = testContext.Properties["TestDataDirectory"].ToString();

            DirectoryInfo sourceDir = new DirectoryInfo(testDataSourceDirPath);
            Trace.WriteLine($"Test Data 2 Source Path: {sourceDir}");

            foreach (var file in Directory.GetFiles(sourceDir.FullName))
            {
                Console.WriteLine($"File: {file}");
                Trace.WriteLine($"File: {file}");
            }

            string testDataDestDirPath = testContext.DeploymentDirectory;
            DirectoryInfo destDir = new DirectoryInfo(testDataDestDirPath);
            Console.WriteLine($"Test Data Dest Path: {destDir}");

            Directory.CreateDirectory(destDir.FullName + Path.DirectorySeparatorChar + "GitRepository");

            DirectoryCopy(sourceDir.FullName, destDir.FullName + Path.DirectorySeparatorChar + "GitRepository", true);
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                Console.WriteLine($"Copy file: {temppath}");
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    Console.WriteLine($"Copy dir: {temppath}");
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
