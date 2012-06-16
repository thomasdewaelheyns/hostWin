using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hostWin.dataHandling
{
    interface FileHandler
    {
        void loadFile(string path, string fileName);
        string[] getFile();
        void backupFile();
        void writeFile(string[] content);
    }
}
