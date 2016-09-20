using System;
using System.Text;
using Ris;

namespace MsgGen
{
    //******************************************************************************
    //******************************************************************************
    //******************************************************************************
    // InputFile

    public class InputFile : BaseCmdLineExec
    {
        //---------------------------------------------------------------------------
        // Members

        FileData    mFileData;
        BlockData   mBlockData;
        bool        mBlockValid;

        //---------------------------------------------------------------------------
        // Constructor

        public InputFile(FileData aFileData)
        {
            mFileData   = aFileData;
            mBlockValid = false;
        }

        public void show()
        {
            Console.WriteLine("mFileData.mBlockList.Count {0}", mFileData.mBlockList.Count);
        }

        //---------------------------------------------------------------------------
        // Execute

        public override void execute(CmdLineCmd aCmd)
        {
            // Read Members
            if (aCmd.isCmd("Using"))        processUsing      (aCmd);
            if (aCmd.isCmd("Define_CH"))    processDefineCH   (aCmd);
            if (aCmd.isCmd("Include_CH"))   processIncludeCH  (aCmd);
            if (aCmd.isCmd("Include_CP"))   processIncludeCP  (aCmd);
            if (aCmd.isCmd("NameSpace"))    processNameSpace  (aCmd);

            if (aCmd.isCmd("Message"))      processBlock      (Defs.cBlockT_Message,aCmd);
            if (aCmd.isCmd("Record"))       processBlock      (Defs.cBlockT_Record,aCmd);
            if (aCmd.isCmd("{"))            processBegin      (aCmd);
            if (aCmd.isCmd("}"))            processEnd        (aCmd);

            if (aCmd.isCmd("const"))        processConst      (aCmd);

            if (aCmd.isCmd("byte"))         processMember     (Defs.cMemberT_Byte,aCmd);
            if (aCmd.isCmd("sbyte"))        processMember     (Defs.cMemberT_SByte,aCmd);
            if (aCmd.isCmd("short"))        processMember     (Defs.cMemberT_Int16,aCmd);
            if (aCmd.isCmd("ushort"))       processMember     (Defs.cMemberT_UInt16,aCmd);
            if (aCmd.isCmd("int"))          processMember     (Defs.cMemberT_Int32,aCmd);
            if (aCmd.isCmd("uint"))         processMember     (Defs.cMemberT_UInt32,aCmd);
            if (aCmd.isCmd("int64"))        processMember     (Defs.cMemberT_Int64,aCmd);
            if (aCmd.isCmd("uint64"))       processMember     (Defs.cMemberT_UInt64,aCmd);
            if (aCmd.isCmd("bool"))         processMember     (Defs.cMemberT_Bool,aCmd);
            if (aCmd.isCmd("float"))        processMember     (Defs.cMemberT_Float,aCmd);
            if (aCmd.isCmd("double"))       processMember     (Defs.cMemberT_Double,aCmd);

            if (aCmd.isCmd("string"))       processString     (aCmd);

            if (aCmd.isBadCmd() && mBlockValid)
            {
                processRecord(aCmd);
            }

        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void processUsing (CmdLineCmd aCmd)
        {
            if (mBlockValid == true) return;
            if (aCmd.NumArg < 1) return;

            mFileData.mFileHeaderData.mUsingList.Add (aCmd.argString(1));
        }

        public void processDefineCH (CmdLineCmd aCmd)
        {
            if (mBlockValid == true) return;
            if (aCmd.NumArg < 1) return;

            mFileData.mFileHeaderData.mDefineCH = aCmd.argString(1);
        }

        public void processIncludeCH (CmdLineCmd aCmd)
        {
            if (mBlockValid == true) return;
            if (aCmd.NumArg < 1) return;

            mFileData.mFileHeaderData.mIncludeCHList.Add (aCmd.argString(1));
        }

        public void processIncludeCP (CmdLineCmd aCmd)
        {
            if (mBlockValid == true) return;
            if (aCmd.NumArg < 1) return;

            mFileData.mFileHeaderData.mIncludeCPList.Add (aCmd.argString(1));
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void processNameSpace (CmdLineCmd aCmd)
        {
            if (mBlockValid == true) return;
            if (aCmd.NumArg < 1) return;

            mFileData.mFileHeaderData.mNameSpace = aCmd.argString(1);
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void processBlock (int aBlockType,CmdLineCmd aCmd)
        {
            if (mBlockValid == true) return;

            mBlockData = new BlockData();
            mBlockData.mBlockType = aBlockType;
            mBlockData.mName = aCmd.argString(1);
            mBlockValid = false;
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void processBegin (CmdLineCmd aCmd)
        {
            if (mBlockValid == true) return;

            mBlockValid = true;
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void processEnd (CmdLineCmd aCmd)
        {
            if (mBlockValid == false) return;

            mFileData.addBlock(mBlockData);
            mBlockValid = false;
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void processConst (CmdLineCmd aCmd)
        {
            ConstData tConst = new ConstData();
            tConst.mName = aCmd.argString(1);

            if (aCmd.isArgString(2,"="))
            {
                tConst.mInitialValue = aCmd.argString(3);
            }

            mBlockData.addConst(tConst);
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void processString (CmdLineCmd aCmd)
        {
            //******************************************************************
            String tArraySize = String.Empty;
            if (aCmd.argString(2).StartsWith("["))
            {
                tArraySize = aCmd.argString(2).Trim('[', ']');
            }

            //******************************************************************
            MemberData tMember = new MemberData(Defs.cMemberT_String);
            tMember.mName = aCmd.argString(1);
            tMember.mIsArray = false;
            tMember.mArraySize = tArraySize;
            
            mBlockData.addMember(tMember);
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void processMember (int aMemberType,CmdLineCmd aCmd)
        {
            if (mBlockValid == false) return;

            if (aCmd.argString(2).StartsWith("[")==false)
            {
                processMemberSingle(aMemberType, aCmd);
            }
            else
            {
                processMemberArray(aMemberType, aCmd);
            }
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void processMemberSingle (int aMemberType,CmdLineCmd aCmd)
        {
            MemberData tMember = new MemberData(aMemberType);
            tMember.mName = aCmd.argString(1);

            if (aCmd.isArgString(2,"="))
            {
                if (aMemberType != Defs.cMemberT_String)
                {
                    tMember.mInitialValue = aCmd.argString(3);
                }
                else
                {
                    tMember.mInitialValue = "\"" + aCmd.argString(3) + "\"";
                }
            }

            mBlockData.addMember(tMember);
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void processMemberArray (int aMemberType,CmdLineCmd aCmd)
        {
            //******************************************************************
            String tArraySize = aCmd.argString(2).Trim('[',']');

            //******************************************************************
            MemberData tMember1 = new MemberData(Defs.cMemberT_Int32);
            tMember1.mName = aCmd.argString(1) + "Loop";
            tMember1.mInitialValue = tArraySize;
            tMember1.mIsArrayIndex = true;

            mBlockData.addMember(tMember1);

            //******************************************************************
            MemberData tMember2 = new MemberData(aMemberType);
            tMember2.mName = aCmd.argString(1);
            tMember2.mIsArray = true;
            tMember2.mArraySize = tArraySize;
            
            mBlockData.addMember(tMember2);
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void processRecord (CmdLineCmd aCmd)
        {
            if (mBlockValid == false) return;

            if (aCmd.argString(2).StartsWith("[")==false)
            {
                processRecordSingle(aCmd);
            }
            else
            {
                processRecordArray(aCmd);
            }
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void processRecordSingle (CmdLineCmd aCmd)
        {
            if (mBlockValid == false) return;

            MemberData tMember = new MemberData(Defs.cMemberT_Record);

            tMember.mTypeName = aCmd.argString(0);
            tMember.mName = aCmd.argString(1);

            mBlockData.addMember(tMember);
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void processRecordArray (CmdLineCmd aCmd)
        {
            //******************************************************************
            String tArraySize = aCmd.argString(2).Trim('[',']');

            //******************************************************************
            MemberData tMember1 = new MemberData(Defs.cMemberT_Int32);
            tMember1.mName = aCmd.argString(1) + "Loop";
            tMember1.mInitialValue = tArraySize;
            tMember1.mIsArrayIndex = true;

            mBlockData.addMember(tMember1);

            //******************************************************************
            MemberData tMember2 = new MemberData(Defs.cMemberT_Record);
            tMember2.mTypeName = aCmd.argString(0);
            tMember2.mName = aCmd.argString(1);

            tMember2.mName = aCmd.argString(1);
            tMember2.mIsArray = true;
            tMember2.mArraySize = tArraySize;
            
            mBlockData.addMember(tMember2);
        }
    };
}
