using System;
using System.Text;
using Ris;

namespace MsgGen
{
    //******************************************************************************
    //******************************************************************************
    //******************************************************************************
    // File reader

    public class Writer
    {
        //**************************************************************************
        //**************************************************************************
        //**************************************************************************
        // Read from settings file

        public static void writeToFilePath(OutputFileBase aOutputFile, FileData aFileData, String aFilePath)
        {
            // Open outputfile
            if (!aOutputFile.open(aFilePath))
            {
                Console.WriteLine("MsgGen.Writer.writeToFilePath FAIL {0}\n", aFilePath);
                return;
            }
            else
            {
                Console.WriteLine("MsgGen.Writer.writeToFilePath PASS {0}\n", aFilePath);
            }

            // Write the file data to the output file
            aOutputFile.write(aFileData);

            // Close output file
            aOutputFile.close();
        }
    };
}
