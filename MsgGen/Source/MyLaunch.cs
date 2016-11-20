#pragma warning disable CS0162 
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ris;

namespace MainApp
{
    //**************************************************************************
    //**************************************************************************
    //**************************************************************************
    public class MyLaunch
    {
        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public static void generate(string[] args)
        {
            //******************************************************************
            // Command line arguments:

            String tReadFilePath;

            if (args.Length==0)
            {
                tReadFilePath = @"C:\Prime\DevelopComm\Dev_MsgGen\Files\MassiveMsg.msg";
               
            }
            else
            {
                tReadFilePath = args[0];
            }

            Console.WriteLine("MSG GENERATE {0}",tReadFilePath);

            //******************************************************************
            // Generate:

            MsgGen.InputData tInputData = new MsgGen.InputData();
            MsgGen.Reader.readFromFilePath(tInputData, tReadFilePath);

            MsgGen.Writer.writeToFilePath(
                new MsgGen.OutputFileMsg_CS_Message(),
                tInputData,
                tInputData.mWriteFilePathCSMessage);

            MsgGen.Writer.writeToFilePath(
                new MsgGen.OutputFileMsg_CS_Body(),
                tInputData,
                tInputData.mWriteFilePathCSBody);

            MsgGen.Writer.writeToFilePath(
                new MsgGen.OutputFileMsg_CH(),
                tInputData,
                tInputData.mWriteFilePathCH);

            MsgGen.Writer.writeToFilePath(
                new MsgGen.OutputFileMsg_CP(),
                tInputData,
                tInputData.mWriteFilePathCP);
        }
    };
}
