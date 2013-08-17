using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace skyrimProfileManager
{
    class profiler
    {
        public DirectoryInfo currentDir, savesDir, newDir;
        public DirectoryInfo[] profiles;
        public string activeProfile;
        

        public profiler()
        {
            currentDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            savesDir = new DirectoryInfo(currentDir + ".\\saves");
            profiles = currentDir.GetDirectories("_*");
            activeProfile = "";
        }

        public int createProfile(string name)
        {
            DirectoryInfo [] subDirs = currentDir.GetDirectories("_" + name);
            if (subDirs.Length == 0)
            {
                currentDir.CreateSubdirectory("_" + name);
                newDir = new DirectoryInfo(currentDir + ".\\_" + name);
                File.Create(newDir.Name + "\\_" + name);
                profiles = currentDir.GetDirectories("_*");
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int deleteProfile(string name)
        {
            DirectoryInfo profToDel = new DirectoryInfo(currentDir + ".\\"+name);
            profToDel.Delete(true);
            profiles = currentDir.GetDirectories("_*");
            return 1;
        }

    }
}
