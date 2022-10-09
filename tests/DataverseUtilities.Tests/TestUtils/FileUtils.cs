using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malaker.DataverseUtilities.Tests.TestUtils
{
    public class FileUtils
    {
        public static void SaveContent(string content, string fileDestination)
        {
            File.WriteAllText(fileDestination, content);
        }
    }
}
