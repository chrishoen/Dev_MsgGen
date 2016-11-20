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
        // Write to output file.

        public static void writeToFilePath(OutputFileBase aOutputFile, InputData aInputData, String aFilePath)
        {
            if (String.IsNullOrEmpty(aFilePath))
            {
                Console.WriteLine("MsgGen.Writer.writeToFilePath EMPTY\n");
                return;
            }

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
            aOutputFile.write(aInputData);

            // Close output file
            aOutputFile.close();
        }
    };
}
