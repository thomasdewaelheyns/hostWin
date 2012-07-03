using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hostWin.dataHandling
{
    interface IFilePathReader
    {
        String getPath();
        String getFileName();
        String getEnvironmentVariable();
    }
}