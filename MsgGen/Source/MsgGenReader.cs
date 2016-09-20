using System;
using System.Text;
using Ris;

namespace MsgGen
{
    //******************************************************************************
    //******************************************************************************
    //******************************************************************************
    // File reader

    public class Reader
    {
        //**************************************************************************
        //**************************************************************************
        //**************************************************************************
        // Read from settings file

        public static void readFromFilePath(FileData tFileData, String aFilePath)
        {
            MsgGen.InputFile tInputFile = new MsgGen.InputFile(tFileData);

            CmdLineFile tCmdLineFile = new CmdLineFile();

            // Open command line file
            if (!tCmdLineFile.open(aFilePath))
            {
                Console.WriteLine("MsgGen.Reader.readFromFilePath FAIL {0}\n", aFilePath);
                return;
            }
            else
            {
                Console.WriteLine("MsgGen.Reader.readFromFilePath PASS {0}\n", aFilePath);
            }

            // Read command line file, execute commands for each line in the file,
            // using this command line executive
            tCmdLineFile.execute(tInputFile);

            // Close command line file
            tCmdLineFile.close();
        }
    };
}
