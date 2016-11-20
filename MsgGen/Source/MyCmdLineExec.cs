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
    class MyCmdLineExec : BaseCmdLineExec
    {
        //**********************************************************************

        public MyCmdLineExec()
        {
            reset();
        }

        public override void reset()
        {
        }

        //**********************************************************************

        public override void execute(CmdLineCmd aCmd)
        {
            if (aCmd.isCmd("RUN1")) OnRun1(aCmd);
            if (aCmd.isCmd("RUN2")) OnRun2(aCmd);
            if (aCmd.isCmd("GO1"))  OnGo1(aCmd);
            if (aCmd.isCmd("GO2"))  OnGo2(aCmd);
            if (aCmd.isCmd("GO3"))  OnGo3(aCmd);
            if (aCmd.isCmd("GO4"))  OnGo4(aCmd);
            if (aCmd.isCmd("GO5"))  OnGo5(aCmd);
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void OnRun1(CmdLineCmd aCmd)
        {
            String tReadFilePath = @"C:\Prime\DevelopComm\Dev_MsgGen\Files\MassiveMsg.msg";
            
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

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void OnRun2(CmdLineCmd aCmd)
        {
            String tReadFilePath = @"C:\Prime\DevelopComm\Dev_MsgGen\Files\MassiveMsg.msg";
            
            String tWriteFilePathCSMessage = @"C:\Prime\DevelopComm\Dev_MsgGen\MassiveCS\Source\MassiveMsgMessage.cs";
            String tWriteFilePathCSBody    = @"C:\Prime\DevelopComm\Dev_MsgGen\MassiveCS\Source\MassiveMsgBody.cs";
            String tWriteFilePathCH = @"C:\Prime\DevelopComm\Dev_MsgGen\MassiveCP\Source\massiveMsg.h";
            String tWriteFilePathCP = @"C:\Prime\DevelopComm\Dev_MsgGen\MassiveCP\Source\massiveMsg.cpp";

            MsgGen.InputData tInputData = new MsgGen.InputData();
            MsgGen.Reader.readFromFilePath(tInputData, tReadFilePath);

            MsgGen.Writer.writeToFilePath(new MsgGen.OutputFileMsg_CS_Message(), tInputData, tWriteFilePathCSMessage);
            MsgGen.Writer.writeToFilePath(new MsgGen.OutputFileMsg_CS_Body(),    tInputData, tWriteFilePathCSBody);
            MsgGen.Writer.writeToFilePath(new MsgGen.OutputFileMsg_CH(), tInputData, tWriteFilePathCH);
            MsgGen.Writer.writeToFilePath(new MsgGen.OutputFileMsg_CP(), tInputData, tWriteFilePathCP);
        }

        //**********************************************************************

        public void OnGo1(CmdLineCmd aCmd)
        {
            String tReadFilePath  = @"C:\Prime\DevelopComm\Dev_MsgGen\Files\MassiveMsg.msg";
            String tWriteFilePath = @"C:\Prime\DevelopComm\Dev_MsgGen\MassiveCP\Source\massiveMsg.h";

            MsgGen.InputData tInputData = new MsgGen.InputData();
            MsgGen.Reader.readFromFilePath(tInputData,tReadFilePath);
            MsgGen.Writer.writeToFilePath(new MsgGen.OutputFileMsg_CH(),tInputData,tWriteFilePath);
        }

        //**********************************************************************

        public void OnGo2(CmdLineCmd aCmd)
        {
            String tReadFilePath  = @"C:\Prime\DevelopComm\Dev_MsgGen\Files\MassiveMsg.msg";
            String tWriteFilePath = @"C:\Prime\DevelopComm\Dev_MsgGen\MassiveCP\Source\massiveMsg.cpp";

            MsgGen.InputData tInputData = new MsgGen.InputData();
            MsgGen.Reader.readFromFilePath(tInputData,tReadFilePath);
            MsgGen.Writer.writeToFilePath(new MsgGen.OutputFileMsg_CP(),tInputData,tWriteFilePath);
        }

        //**********************************************************************

        public void OnGo3(CmdLineCmd aCmd)
        {
            String tReadFilePath  = @"C:\Prime\DevelopComm\Dev_MsgGen\Files\MassiveMsg.msg";
            String tWriteFilePath = @"C:\Prime\DevelopComm\Dev_MsgGen\\MassiveCS\Source\MassiveMsgMessage.cs";

            MsgGen.InputData tInputData = new MsgGen.InputData();
            MsgGen.Reader.readFromFilePath(tInputData,tReadFilePath);
            MsgGen.Writer.writeToFilePath(new MsgGen.OutputFileMsg_CS_Message(),tInputData,tWriteFilePath);
        }

        //**********************************************************************

        public void OnGo4(CmdLineCmd aCmd)
        {
            String tReadFilePath  = @"C:\Prime\DevelopComm\Dev_MsgGen\Files\MassiveMsg.msg";
            String tWriteFilePath = @"C:\Prime\DevelopComm\Dev_MsgGen\\MassiveCS\Source\MassiveMsgBody.cs";

            MsgGen.InputData tInputData = new MsgGen.InputData();
            MsgGen.Reader.readFromFilePath(tInputData,tReadFilePath);
            MsgGen.Writer.writeToFilePath(new MsgGen.OutputFileMsg_CS_Body(),tInputData,tWriteFilePath);
        }

        //**********************************************************************

        public void OnGo5(CmdLineCmd aCmd)
        {
            aCmd.setArgDefault(1, 0x10);

            UInt64 tN = aCmd.argUInt64(1);

            Console.WriteLine("0x{0,16:X}", tN);
            
        }


    }
}
