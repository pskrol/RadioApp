using System.IO;
using System.Reflection;

namespace RadioApp
{
    public static class ProgramPaths
    {
        public static string ProgramLaunchDirectoryPath
        {
            get
            {
                var assemblyLocation = Assembly.GetExecutingAssembly().Location;

                return Path.GetDirectoryName(assemblyLocation);
            }
        }

        public static string DatabaseFilepath
        {
            get
            {
                return Path.Combine(ProgramLaunchDirectoryPath, "radioapp.db");
            }
        }
    }
}
