using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MsgGen
{
    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public class OutputFileTMsgB_CS : OutputFileBase
    {
        //**********************************************************************
        //**********************************************************************
        //**********************************************************************
        // Members

        public MyStreamWriter mWCS;
        public FileData mFileData;

        public int mNumNameSpace;
        public int mIndent;

        //**********************************************************************
        // Constructor

        public OutputFileTMsgB_CS()
        {
            mNumNameSpace = 0;
            mWCS = null;
            mIndent=0;
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************
        // Open stream writer

        public override bool open (String aFilePath)
        {
            mWCS = null;

            try
            {   
                mWCS = new MyStreamWriter(aFilePath,4);
            }
            catch
            {
                Console.WriteLine("Error opening {0}", aFilePath);
            }

            return mWCS  != null;
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************
        // Close stream writer

        public override void close ()
        {
            mWCS.Close();
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************
        // Write

        public override void write(FileData aFileData)
        {
            mFileData = aFileData;

            writeFileBegin();
            writeIdentifiers();

            mFileData.mBlockList.ForEach(delegate(BlockData tBlock)
            {
                writeBlock(tBlock);
            });

            writeMessageCopier();
            writeFileEnd();
        }
        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public String stringExtend(String aString, int aMaxLength)
        {
            String tString = String.Copy(aString);
            for (int i = aString.Length; i < aMaxLength; i++)
            {
                tString = tString + " ";
            }
            return tString;
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void writeFileBegin()
        {
            mFileData.mFileHeaderData.mUsingList.ForEach(delegate(String name)
            {
                mWCS.WriteLine (0, "using {0};",name);
            });
            mWCS.WriteSkip ();

            
            mWCS.WriteLine (0, "namespace {0}",mFileData.mFileHeaderData.mNameSpace);
            mWCS.WriteLine (0, "{");
            mWCS.WriteSkip ();
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void writeFileEnd()
        {
            mWCS.WriteLine ( 0, "}");
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void writeIdentifiers()
        {
            mWCS.WriteBar  (1,3);
            mWCS.WriteLine (1,"// Message Types");
            mWCS.WriteSkip ();
            mWCS.WriteLine (1,"{0}","public class MsgIdT");
            mWCS.WriteLine (1,"{0}","{");
            mWCS.WriteLine (2,"{0}","public const int cUnspecified = 0;");

            int tIdent = 1;
            mFileData.mBlockList.ForEach(delegate(BlockData tBlock)
            {
                if (tBlock.mBlockType == Defs.cBlockT_Message)
                {
                    mWCS.WriteLine (2,"public const int c{0} = {1,3};", stringExtend(tBlock.mName,mFileData.mNameMaxSize), tIdent++);
                }
            });

            mWCS.WriteLine (1,"{0}","};");
            mWCS.WriteSkip ();
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void writeBlock(BlockData aBlock)
        {
            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Class Begin

            mWCS.WriteBar  (1,3);
            mWCS.WriteSkip ();

            if (aBlock.mBlockType == Defs.cBlockT_Message)
            {
               mWCS.WriteLine (1, "public class {0} : ByteMsgB", aBlock.mName);
            }
            else
            {
               mWCS.WriteLine (1, "public class {0} : ByteContent", aBlock.mName);
            }

            mWCS.WriteLine (1, "{");

            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Members

            mWCS.WriteBar  (2,1);
            mWCS.WriteLine (2,"// Members");
            mWCS.WriteSkip ();

            if (aBlock.mConstList.Count > 0)
            {
                aBlock.mConstList.ForEach(delegate (ConstData tConst)
                {
                    mWCS.WriteLine(2, "public const int {0} = {1};", stringExtend(tConst.mName, aBlock.mConstMaxSize), tConst.mInitialValue);
                });
                mWCS.WriteSkip();
            }

            aBlock.mMemberList.ForEach(delegate(MemberData tMember)
            {
                if (!tMember.mIsArray)
                {
                    mWCS.WriteLine (2, "public {0} {1};", stringExtend(tMember.mTypeName,aBlock.mTypeMaxSize), tMember.mName);
                }
                else
                {
                    mWCS.WriteLine (2, "public {0} {1};", stringExtend(tMember.mTypeName + "[]",aBlock.mTypeMaxSize), tMember.mName);
                }

            });
            mWCS.WriteSkip ();

            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Constructor

            mWCS.WriteBar  (2,1);
            mWCS.WriteLine (2,"// Constructor");
            mWCS.WriteSkip ();

            mWCS.WriteLine (2, "public {0}()",aBlock.mName);
            mWCS.WriteLine (2, "{");

            if (aBlock.mBlockType == Defs.cBlockT_Message)
            {
                mWCS.WriteLine (3, "{0} = MsgIdT.c{1};",stringExtend("mMessageType",aBlock.mNameMaxSize), aBlock.mName);
                mWCS.WriteSkip ();
            }

            int  tMemberCount = 0;
            bool tLastMember  = false;

            aBlock.mMemberList.ForEach(delegate(MemberData tMember)
            {
                tLastMember = (++tMemberCount == aBlock.mMemberList.Count);

                if (tMember.mIsArrayIndex)
                {
                    mWCS.WriteSkip ();
                }

                if (tMember.mIsArray == false)
                {
                    if (tMember.mMemberType != Defs.cMemberT_Record)
                    {
                        mWCS.WriteLine (3, "{0} = {1};", stringExtend(tMember.mName,aBlock.mNameMaxSize), tMember.mInitialValue);
                    }
                    else
                    {
                        mWCS.WriteLine (3, "{0} = new {1}();", stringExtend(tMember.mName,aBlock.mNameMaxSize), tMember.mTypeName);
                    }
                 }
                else
                {
                    if (tMember.mMemberType != Defs.cMemberT_Record)
                    {
                        mWCS.WriteLine (3, "{0} = new {1}[{2}];", stringExtend(tMember.mName,aBlock.mNameMaxSize), tMember.mTypeName,tMember.mArraySize);
                        mWCS.WriteSkip ();
                    }
                    else
                    {
                        mWCS.WriteLine (3, "{0} = new {1}[{2}];", stringExtend(tMember.mName,aBlock.mNameMaxSize), tMember.mTypeName,tMember.mArraySize);
                        mWCS.WriteLine (3, "for (int i=0; i<{0}; i++)",tMember.mArraySize);
                        mWCS.WriteLine (3, "{");
                        mWCS.WriteLine (3, "{0} = new {1}();",stringExtend(tMember.mName+"[i]",aBlock.mNameMaxSize),tMember.mTypeName);
                        mWCS.WriteLine (3, "}");
                        if (tLastMember == false)
                        {
                            mWCS.WriteSkip();
                        }
                    }
                }

            });

            mWCS.WriteLine(2, "}");

            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Class End

            mWCS.WriteLine(1, "};");
            mWCS.WriteSkip ();
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void writeMessageCopier()
        {
            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Class Begin

            mWCS.WriteBar  (1,3);
            mWCS.WriteLine (1, "// Copier");
            mWCS.WriteSkip ();
            mWCS.WriteLine (1, "public class MsgBCopier : BaseMsgBCopier");
            mWCS.WriteLine (1, "{");
            mWCS.WriteSkip ();

            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Create

            mWCS.WriteBar  (2,3);
            mWCS.WriteLine (2, "// Create");
            mWCS.WriteSkip ();
            mWCS.WriteLine (2, "public override ByteMsgB createMessage (int aMessageType)");
            mWCS.WriteLine (2, "{");
            mWCS.WriteLine (3, "ByteMsgB tMsg = null;");
            mWCS.WriteSkip ();
            mWCS.WriteLine (3, "switch (aMessageType)");
            mWCS.WriteLine (3, "{");

            mFileData.mBlockList.ForEach(delegate(BlockData tBlock)
            {
                if (tBlock.mBlockType == Defs.cBlockT_Message)
                {
                    mWCS.WriteLine (4, "case MsgIdT.c{0} :", tBlock.mName);
                    mWCS.WriteLine (5, "tMsg = new {0}();", tBlock.mName);
                    mWCS.WriteLine (5, "break;");
                }
            });

            mWCS.WriteLine (3, "}");
            mWCS.WriteSkip ();
            mWCS.WriteLine (3, "return tMsg;");
            mWCS.WriteLine (2, "}");
            mWCS.WriteSkip ();

            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Copy

            mWCS.WriteBar  (2,3);
            mWCS.WriteLine (2, "// Copy");
            mWCS.WriteSkip ();
            mWCS.WriteLine (2, "public override void copyToFrom( ByteBuffer aBuffer, ByteMsgB aMsg)");
            mWCS.WriteLine (2, "{");
            mWCS.WriteLine (3, "switch (aMsg.mMessageType)");
            mWCS.WriteLine (3, "{");

            mFileData.mBlockList.ForEach(delegate(BlockData tBlock)
            {
                if (tBlock.mBlockType == Defs.cBlockT_Message)
                {
                    mWCS.WriteLine (4, "case MsgIdT.c{0} :", tBlock.mName);
                    mWCS.WriteLine (5, "copyToFrom(aBuffer, ({0})aMsg);", tBlock.mName);
                    mWCS.WriteLine (5, "break;");
                }
            });

            mWCS.WriteLine (3, "}");
            mWCS.WriteLine (2, "}");
            mWCS.WriteSkip ();

            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Copy 

            mWCS.WriteBar  (2,2);

            mFileData.mBlockList.ForEach(delegate(BlockData tBlock)
            {
                mWCS.WriteBar  (2,1);
                mWCS.WriteLine (2,"// Copy {0}",tBlock.mName);
                mWCS.WriteSkip ();

                mWCS.WriteLine (2, "public void copyToFrom( ByteBuffer aBuffer, {0} aMsg)",tBlock.mName);
                mWCS.WriteLine (2, "{");

                tBlock.mMemberList.ForEach(delegate(MemberData tMember)
                {
                    if (tMember.mIsArray == false)
                    {
                        if (tMember.mMemberType == Defs.cMemberT_String)
                        {
                            mWCS.WriteLine (3, "aBuffer.copyS( ref aMsg.{0} );", tMember.mName);
                        }
                        else if (tMember.mMemberType == Defs.cMemberT_Record)
                        {
                            mWCS.WriteLine (3, "copyToFrom(aBuffer,aMsg.{0} );", tMember.mName);
                        }
                        else
                        {
                            mWCS.WriteLine (3, "aBuffer.copy ( ref aMsg.{0} );", tMember.mName);
                        }
                    }
                    else
                    {
                        mWCS.WriteLine (3, "for (int i = 0;  i<aMsg.{0}Loop; i++ )", tMember.mName);
                        mWCS.WriteLine (3, "{");
                        if (tMember.mMemberType == Defs.cMemberT_String)
                        {
                        }
                        else if (tMember.mMemberType == Defs.cMemberT_Record)
                        {
                            mWCS.WriteLine (3, "copyToFrom(aBuffer,aMsg.{0}[i]);", tMember.mName);
                        }
                        else
                        {
                            mWCS.WriteLine (3, "aBuffer.copy ( ref aMsg.{0}[i] );", tMember.mName);
                        }
                        mWCS.WriteLine(3, "}");
                    }
                });
                mWCS.WriteLine (2, "}");
                mWCS.WriteSkip ();
                });
                mWCS.WriteLine (1, "}");
        }

    };

}//namespace
