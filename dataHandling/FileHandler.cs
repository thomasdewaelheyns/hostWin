using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace hostWin.dataHandling
{
    interface FileHandler
    {
        void loadFile(string path, string fileName);
        void closeFile();
        ArrayList getFileContent();
        void backupFile();
        void writeFile(ArrayList content);
    }
}
