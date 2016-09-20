using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ris;
using Example;

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
            String tReadFilePath = @"C:\Prime\DevelopComm\Dev_MsgGen\Files\DasCommMsg.msg";
            
            String tWriteFilePathCS = @"C:\Prime\DevelopComm\Dev_MsgGen\MassiveCS\Source\DasCommMsg.cs";
            String tWriteFilePathCH = @"C:\Prime\DevelopComm\Dev_MsgGen\MassiveCP\Source\dascommMsg.h";
            String tWriteFilePathCP = @"C:\Prime\DevelopComm\Dev_MsgGen\MassiveCP\Source\dascommMsg.cpp";

            MsgGen.FileData tFileData = new MsgGen.FileData();
            MsgGen.Reader.readFromFilePath(tFileData, tReadFilePath);

            MsgGen.Writer.writeToFilePath(new MsgGen.OutputFileTMessageCS(), tFileData, tWriteFilePathCS);
            MsgGen.Writer.writeToFilePath(new MsgGen.OutputFileTMessageCH(), tFileData, tWriteFilePathCH);
            MsgGen.Writer.writeToFilePath(new MsgGen.OutputFileTMessageCP(), tFileData, tWriteFilePathCP);
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void OnRun2(CmdLineCmd aCmd)
        {
            String tReadFilePath = @"C:\Prime\DevelopComm\Dev_MsgGen\Files\CommTestMsg.msg";
            String tWriteFilePathCS = @"C:\Prime\DevelopComm\Dev_MsgGen\MsgLib_CS\CommTestMsg_Message.cs";
            String tWriteFilePathCH = @"C:\Prime\DevelopComm\Dev_MsgGen\MsgLib_CP\commtestMsg_Message.h";
            String tWriteFilePathCP = @"C:\Prime\DevelopComm\Dev_MsgGen\MsgLib_CP\commtestMsg_Message.cpp";

            MsgGen.FileData tFileData = new MsgGen.FileData();
            MsgGen.Reader.readFromFilePath(tFileData, tReadFilePath);

            MsgGen.Writer.writeToFilePath(new MsgGen.OutputFileMsgCS(), tFileData, tWriteFilePathCS);
            MsgGen.Writer.writeToFilePath(new MsgGen.OutputFileMsgCH(), tFileData, tWriteFilePathCH);
            MsgGen.Writer.writeToFilePath(new MsgGen.OutputFileMsgCP(), tFileData, tWriteFilePathCP);
        }

        //**********************************************************************

        public void OnGo1(CmdLineCmd aCmd)
        {
            String tReadFilePath  = @"C:\Prime\DevelopComm\Dev_MsgGen\Files\MassiveMsg.msg";
            String tWriteFilePath = @"C:\Prime\DevelopComm\Dev_MsgGen\Files\MassiveMsg.cs";

            MsgGen.FileData tFileData = new MsgGen.FileData();
            MsgGen.Reader.readFromFilePath(tFileData,tReadFilePath);
            MsgGen.Writer.writeToFilePath(new MsgGen.OutputFileTMessageCS(),tFileData,tWriteFilePath);
        }

        //**********************************************************************

        public void OnGo2(CmdLineCmd aCmd)
        {
            String tReadFilePath  = @"C:\Prime\DevelopComm\Dev_MsgGen\Files\MassiveMsg.msg";
            String tWriteFilePath = @"C:\Prime\DevelopComm\Dev_MsgGen\Files\massiveMsg.h";

            MsgGen.FileData tFileData = new MsgGen.FileData();
            MsgGen.Reader.readFromFilePath(tFileData,tReadFilePath);
            MsgGen.Writer.writeToFilePath(new MsgGen.OutputFileTMessageCH(),tFileData,tWriteFilePath);
        }

        //**********************************************************************

        public void OnGo3(CmdLineCmd aCmd)
        {
            String tReadFilePath  = @"C:\Prime\DevelopComm\Dev_MsgGen\Files\MassiveMsg.msg";
            String tWriteFilePath = @"C:\Prime\DevelopComm\Dev_MsgGen\Files\massiveMsg.cpp";

            MsgGen.FileData tFileData = new MsgGen.FileData();
            MsgGen.Reader.readFromFilePath(tFileData,tReadFilePath);
            MsgGen.Writer.writeToFilePath(new MsgGen.OutputFileTMessageCP(),tFileData,tWriteFilePath);
        }

        //**********************************************************************

        public void OnGo4(CmdLineCmd aCmd)
        {
            ByteBuffer tBuffer = new ByteBuffer(1000);

            TestMsg tTxMsg = new TestMsg();
            TestMsg tRxMsg = new TestMsg();

            tTxMsg.initialize();

            tBuffer.putToBuffer(tTxMsg);
            Console.WriteLine("Buffer.Length {0}",tBuffer.mWorkingLength);

            tBuffer.rewind();

            tBuffer.getFromBuffer(tRxMsg);
            tRxMsg.show();
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
