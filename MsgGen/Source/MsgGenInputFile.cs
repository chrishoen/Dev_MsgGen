using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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

        public InputData      mInputData;
        public BlockData      mBlockData;
        public bool           mBlockValid;
        public List<String>   mPreCommentList;

        //---------------------------------------------------------------------------
        // Constructor

        public InputFile(InputData aInputData)
        {
            mInputData   = aInputData;
            mBlockValid = false;
            mPreCommentList = new List<String>();
        }

        public void show()
        {
            Console.WriteLine("mInputData.mBlockList.Count {0}", mInputData.mBlockList.Count);
        }

        //---------------------------------------------------------------------------
        // Execute

        public override void execute(CmdLineCmd aCmd)
        {
            // Read Members
            if (aCmd.isCmd("WriteFilePathCSMessage"))  mInputData.mWriteFilePathCSMessage = aCmd.argString(1);
            if (aCmd.isCmd("WriteFilePathCSBody"))     mInputData.mWriteFilePathCSBody = aCmd.argString(1);
            if (aCmd.isCmd("WriteFilePathCH"))         mInputData.mWriteFilePathCH = aCmd.argString(1);
            if (aCmd.isCmd("WriteFilePathCP"))         mInputData.mWriteFilePathCP = aCmd.argString(1);

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

            if (aCmd.isCmd("uchar"))        processMember     (Defs.cMemberT_UChar,aCmd);
            if (aCmd.isCmd("ushort"))       processMember     (Defs.cMemberT_UInt16,aCmd);
            if (aCmd.isCmd("uint"))         processMember     (Defs.cMemberT_UInt32,aCmd);
            if (aCmd.isCmd("uint64"))       processMember     (Defs.cMemberT_UInt64,aCmd);
            if (aCmd.isCmd("char"))         processMember     (Defs.cMemberT_Char,aCmd);
            if (aCmd.isCmd("short"))        processMember     (Defs.cMemberT_Int16,aCmd);
            if (aCmd.isCmd("int"))          processMember     (Defs.cMemberT_Int32,aCmd);
            if (aCmd.isCmd("int64"))        processMember     (Defs.cMemberT_Int64,aCmd);
            if (aCmd.isCmd("bool"))         processMember     (Defs.cMemberT_Bool,aCmd);
            if (aCmd.isCmd("float"))        processMember     (Defs.cMemberT_Float,aCmd);
            if (aCmd.isCmd("double"))       processMember     (Defs.cMemberT_Double,aCmd);
            if (aCmd.isCmd("string"))       processString     (aCmd);

            if (aCmd.isComment())           processPreComment (aCmd);

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

            mInputData.mFileHeaderData.mUsingList.Add (aCmd.argString(1));
        }

        public void processDefineCH (CmdLineCmd aCmd)
        {
            if (mBlockValid == true) return;
            if (aCmd.NumArg < 1) return;

            mInputData.mFileHeaderData.mDefineCH = aCmd.argString(1);
        }

        public void processIncludeCH (CmdLineCmd aCmd)
        {
            if (mBlockValid == true) return;
            if (aCmd.NumArg < 1) return;

            mInputData.mFileHeaderData.mIncludeCHList.Add (aCmd.argString(1));
        }

        public void processIncludeCP (CmdLineCmd aCmd)
        {
            if (mBlockValid == true) return;
            if (aCmd.NumArg < 1) return;

            mInputData.mFileHeaderData.mIncludeCPList.Add (aCmd.argString(1));
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void processNameSpace (CmdLineCmd aCmd)
        {
            if (mBlockValid == true) return;
            if (aCmd.NumArg < 1) return;

            mInputData.mFileHeaderData.mNameSpace = aCmd.argString(1);

            if (mPreCommentList.Count > 0)
            {
               mInputData.mFileHeaderData.addPreCommentList(mPreCommentList);
               mPreCommentList = new List<String>();
            }
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

            if (mPreCommentList.Count > 0)
            {
               mBlockData.addPreCommentList(mPreCommentList);
               mPreCommentList = new List<String>();
            }
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

            mInputData.addBlock(mBlockData);
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

            tConst.addPreCommentList(mPreCommentList);
            mPreCommentList = new List<String>();

            tConst.addPostComment(aCmd.comment());

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

            if (mPreCommentList.Count > 0)
            {
                tMember.addPreCommentList(mPreCommentList);
                mPreCommentList = new List<String>();
            }

            tMember.addPostComment(aCmd.comment());

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

            if (mPreCommentList.Count > 0)
            {
                tMember1.addPreCommentList(mPreCommentList);
                mPreCommentList = new List<String>();
            }

            tMember1.addPostComment(aCmd.comment());

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

            tMember.mTypeNameCP = aCmd.argString(0);
            tMember.mTypeNameCS = aCmd.argString(0);
            tMember.mName = aCmd.argString(1);

            if (mPreCommentList.Count > 0)
            {
                tMember.addPreCommentList(mPreCommentList);
                mPreCommentList = new List<String>();
            }

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

            if (mPreCommentList.Count > 0)
            {
                tMember1.addPreCommentList(mPreCommentList);
                mPreCommentList = new List<String>();
            }

            mBlockData.addMember(tMember1);

            //******************************************************************
            MemberData tMember2 = new MemberData(Defs.cMemberT_Record);
            tMember2.mTypeNameCP = aCmd.argString(0);
            tMember2.mTypeNameCS = aCmd.argString(0);
            tMember2.mName = aCmd.argString(1);

            tMember2.mName = aCmd.argString(1);
            tMember2.mIsArray = true;
            tMember2.mArraySize = tArraySize;
            
            mBlockData.addMember(tMember2);
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void processPreComment (CmdLineCmd aCmd)
        {
            String tComment = aCmd.comment();
            if (tComment.Length < 3) return;
            if (tComment[2].Equals('*')) return;

            mPreCommentList.Add(tComment);

        }
    };
}
