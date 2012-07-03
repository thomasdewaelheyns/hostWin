using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace hostWin.dataHandling
{
    class WindowsFilePathReader: IFilePathReader
    {
        string path = "";
        string fileName = "";
        string environmentVariable = "";

        public WindowsFilePathReader()
        {
            this.readXmlContent();
        }

        private void readXmlContent()
        {
            XmlTextReader reader = new XmlTextReader(".\\configurationFiles\\FilePaths.xml");
            String osName = externalCode.OSVersionInfo.Name.Split(new char[]{' '})[0].ToLower();
            while (reader.ReadToFollowing("os"))
            {
                reader.MoveToFirstAttribute();
                if (reader.Value.Equals(osName))
                {
                    reader.ReadToFollowing("fileName");
                    this.fileName = reader.ReadElementContentAsString();
                    reader.ReadToFollowing("environmentVariable");
                    this.environmentVariable = reader.ReadElementContentAsString();
                    reader.ReadToFollowing("path");
                    this.path = reader.ReadElementContentAsString();
                    break;
                }
            }
        }


        public String getPath()
        {
            return this.path;
        }

        public String getFileName()
        {
            return this.fileName;
        }

        public String getEnvironmentVariable()
        {
            return this.environmentVariable;
        }
    }
}
