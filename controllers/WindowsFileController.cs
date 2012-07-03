using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using hostWin.dataHandling;

namespace hostWin.controllers
{
    class WindowsFileController: IFileController
    {

        IFileHandler fileHandler;
        IFilePathReader pathReader;

        public WindowsFileController()
        {
            fileHandler = new WindowsFileHandler();
            pathReader = new WindowsFilePathReader();
        }

        public void openFile()
        {
            fileHandler.loadFile(pathReader.getEnvironmentVariable(), pathReader.getPath(), pathReader.getFileName());
        }

        public void closeFile()
        {
            fileHandler.closeFile();
        }

        public ArrayList getFileContent()
        {
            return fileHandler.getFileContent();
        }

        public void backupFile()
        {
            fileHandler.backupFile();
        }

        public void writeFile(ArrayList newContent)
        {
            fileHandler.writeFile(newContent);
        }
    }
}
