using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace hostWin.dataHandling
{
    class WindowsFileHandler: IFileHandler 
    {
        private string fileName = "";
        private string path = "";
        private string environmentVariable = "";
        private ArrayList content = null;
        private StreamReader reader = null;
        private StreamWriter writer = null;

        public WindowsFileHandler()
        {
        }

        /*
         * This method can be used to load a file and reads it content.
         */
        public void loadFile(string environmentVariable, string path, string fileName)
        {
            //Check the input for errors.
            if(environmentVariable == null || environmentVariable == "")
                throw new ArgumentException("The given environmentVariable is empty.");
            if (path == null || path == "")
                throw new ArgumentException("The given path is empty.");
            if (fileName == null || fileName == "")
                throw new ArgumentException("The given filename is empty.");

            //Release the previous file in a correct way.
            if(reader != null)
                reader.Close();
            if(writer != null){
                writer.Flush();
                writer.Close();
            }

            //Open the new file and read its content
            this.path = path;
            this.fileName = fileName;
            this.environmentVariable = environmentVariable;
            try{
                this.content = new ArrayList();
                reader = new StreamReader(Environment.ExpandEnvironmentVariables(environmentVariable)+this.path + this.fileName, Encoding.UTF8);
                string line = reader.ReadLine();
                
                while (line != null)
                {
                    this.content.Add(line);
                    line = reader.ReadLine();
                }
            }
            catch(Exception e){
                this.path = null;
                this.fileName = null;
                this.environmentVariable = null;
                this.reader = null;
                this.writer = null;
                throw new FileNotFoundException("exception: "+e.ToString());
            }

        }

        /*
         * This method can be used to close and opened file correctly. 
         */
        public void closeFile()
        {
            if(reader != null){
                reader.Close();
                this.reader = null;
            }

            if (writer != null)
            {
                writer.Flush();
                this.writer = null;
            }
            this.content = null;
        }

        /*
         * This method can be used to retrieve the content of a file we opened with the loadFile(String path, String fileName);
         * or the content we wrote to that file throught the writeFile(ArrayList content);
         * Returns an ArrayList containing the content line per line.
         */
        public ArrayList getFileContent()
        {
            //We check if there is content to be returned. This can be the content loaded from a file,
            //or the new content we wrote to the file through the writeFile(ArrayList content);
            if (this.content != null)
                return this.content;
            else
                throw new Exception("No file loaded.");
        }


        /*
         * This method can be used to make a backup of the excisting file.
         * The new file name is fileName_day-month-year_hour-minute-second.bak.
         * It is stored in the given path.
         */ 
        public void backupFile()
        {
            //Checks if we opened a file already
            if (this.content != null)
            {
                String date = DateTime.Now.Date.ToString("dd'-'MM'-'yyyy")+"_"+DateTime.Now.ToString("HH'-'mm'-'ss");
                try
                {
                    String newPath = this.path + "hostwin_backups\\";
                    System.IO.Directory.CreateDirectory(Environment.ExpandEnvironmentVariables(environmentVariable)+newPath);
                    StreamWriter tempWriter = new StreamWriter(Environment.ExpandEnvironmentVariables(environmentVariable)+newPath + this.fileName + "_" + date + ".bak", false, Encoding.UTF8);
                    foreach(String line in this.content){
                        System.Console.WriteLine(line);
                        tempWriter.WriteLine(line);
                    }
                    tempWriter.Flush();
                }
                catch (Exception e)
                {
                    throw new Exception("Exception: "+e.ToString());
                }
            }
            else
            {
                throw new Exception("File already modified.");
            }
        }

        public void writeFile(ArrayList content)
        {
            if (content == null)
                throw new Exception("We cannot write this content.");
            if (this.content == null)
                throw new Exception("No file opened, therefor we cannot write to it.");
            if (this.path == null || this.path == "" || this.fileName == null || this.fileName == "" || this.environmentVariable == null || this.environmentVariable == "")
                throw new Exception("No valid environment variable, path and/or filename.");

            this.content = content;
            if (this.reader != null)
            {
                this.reader.Close();
                this.reader = null;
            }
            try
            {
                writer = new StreamWriter(Environment.ExpandEnvironmentVariables(environmentVariable)+this.path + this.fileName, false, Encoding.UTF8);
                foreach (String line in this.content)
                {
                    writer.WriteLine(line);
                }
                writer.Flush();
            }
            catch (Exception e)
            {
                throw new Exception("Could not write to the file: "+e.ToString());
            }
        }
    }
}
