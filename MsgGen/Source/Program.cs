#pragma warning disable CS0162 

using System;
using System.Collections.Generic;
using System.Text;

using Ris;

namespace MainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Prn.initializeForConsole();
            if (false)
            {
                MyCmdLineExec tCmdLineExec = new MyCmdLineExec();
                CmdLineConsole.execute(tCmdLineExec);
            }
            else
            {
                MyLaunch.generate(args);
            }
        }
    }
}
