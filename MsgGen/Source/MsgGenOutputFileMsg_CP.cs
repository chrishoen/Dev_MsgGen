using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MsgGen
{
    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public class OutputFileMsg_CP : OutputFileBase
    {
        //**********************************************************************
        //**********************************************************************
        //**********************************************************************
        // Members

        public MyStreamWriter mWCP;
        public FileData mFileData;

        public int mNumNameSpace;
        public int mIndent;

        //**********************************************************************
        // Constructor

        public OutputFileMsg_CP()
        {
            mNumNameSpace = 0;
            mWCP = null;
            mIndent=0;
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************
        // Open stream writer

        public override bool open (String aFilePath)
        {
            mWCP = null;

            try
            {   
                mWCP = new MyStreamWriter(aFilePath,3);
            }
            catch
            {
                Console.WriteLine("Error opening {0}", aFilePath);
            }

            return mWCP  != null;
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************
        // Close stream writer

        public override void close ()
        {
            mWCP.Close();
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************
        // Write

        public override void write(FileData aFileData)
        {
            mFileData = aFileData;

            writeFileBegin();
            writeMessageCreator();

            mFileData.mBlockList.ForEach(delegate(BlockData tBlock)
            {
                writeBlock(tBlock);
            });

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
            mFileData.mFileHeaderData.mIncludeCPList.ForEach(delegate(String tString)
            {
                if (tString.StartsWith("<"))
                {
                    mWCP.WriteLine(0, "#include {0}", tString);
                }
                else
                {
                    mWCP.WriteLine(0, "#include \"{0}\"", tString);
                }
            });
            mWCP.WriteSkip ();

            
            mWCP.WriteLine ( 0, "namespace {0}",mFileData.mFileHeaderData.mNameSpace);
            mWCP.WriteLine ( 0, "{");
            mWCP.WriteSkip ();
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void writeFileEnd()
        {
            mWCP.WriteLine ( 0, "}");
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void writeMessageCreator()
        {
            mWCP.WriteBar  (1,3);
            mWCP.WriteLine (1, "// Message Creator");
            mWCP.WriteSkip ();
            mWCP.WriteLine (1, "BaseMsg* MessageCreator::createMessage(int aMessageType)");
            mWCP.WriteLine (1, "{");
            mWCP.WriteLine (2, "BaseMsg* tMsg = 0;");
            mWCP.WriteSkip ();
            mWCP.WriteLine (2, "switch (aMessageType)");
            mWCP.WriteLine (2, "{");

            mFileData.mBlockList.ForEach(delegate(BlockData tBlock)
            {
                if (tBlock.mBlockType == Defs.cBlockT_Message)
                {
                    mWCP.WriteLine (3, "case MsgIdT::c{0} :", tBlock.mName);
                    mWCP.WriteLine (4, "tMsg = new {0}();", tBlock.mName);
                    mWCP.WriteLine (4, "break;");
                }
            });

            mWCP.WriteLine (3, "default :");
            mWCP.WriteLine (4, "break;");

            mWCP.WriteLine (2, "}");
            mWCP.WriteLine (2, "return tMsg;");
            mWCP.WriteLine (1, "}");
            mWCP.WriteSkip ();
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

            mWCP.WriteBar  (1,3);
            mWCP.WriteLine (1, "// {0}", aBlock.mName);
            mWCP.WriteSkip ();

            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Constructor

            mWCP.WriteLine (1, "{0}::{0}()",aBlock.mName);
            mWCP.WriteLine (1, "{");

            if (aBlock.mBlockType == Defs.cBlockT_Message)
            {
                mWCP.WriteLine (2, "{0} = MsgIdT::c{1};",stringExtend("mMessageType",aBlock.mNameMaxSize), aBlock.mName);
                mWCP.WriteSkip ();
            }

            int  tMemberCount = 0;
            bool tLastMember  = false;

            aBlock.mMemberList.ForEach(delegate(MemberData tMember)
            {
                tLastMember = (++tMemberCount == aBlock.mMemberList.Count);

                if (tMember.mIsArray == false)
                {
                    if (tMember.mMemberType == Defs.cMemberT_String)
                    {
                        mWCP.WriteLine (2, "{0}[0]=0;", tMember.mName);
                    }
                    else if (tMember.mMemberType != Defs.cMemberT_Record)
                    {
                        mWCP.WriteLine (2, "{0} = {1};", stringExtend(tMember.mName,aBlock.mNameMaxSize), tMember.mInitialValue);
                    }
                 }
            });

            mWCP.WriteLine (1, "}");
            mWCP.WriteSkip ();

            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Copy

            mWCP.WriteLine (1, "void {0}::copyToFrom (Ris::ByteBuffer* aBuffer)",aBlock.mName);
            mWCP.WriteLine (1, "{");

            if (aBlock.mBlockType == Defs.cBlockT_Message)
            {
                mWCP.WriteLine (2, "mHeader.headerCopyToFrom(aBuffer,this);");
                mWCP.WriteSkip ();
            }

            aBlock.mMemberList.ForEach(delegate(MemberData tMember)
            {
                if (tMember.mIsArray == false)
                {
                    if (tMember.mIsArrayIndex)
                    {
                        mWCP.WriteSkip ();
                    }
                    if (tMember.mMemberType == Defs.cMemberT_String)
                    {
                        mWCP.WriteLine (2, "aBuffer->copyS(  {0} );", stringExtend(tMember.mName,aBlock.mNameMaxSize));
                    }
                    else if (tMember.mMemberType == Defs.cMemberT_Record)
                    {
                        mWCP.WriteLine (2, "aBuffer->copy ( &{0} );", stringExtend(tMember.mName,aBlock.mNameMaxSize));
                    }
                    else
                    {
                        mWCP.WriteLine (2, "aBuffer->copy ( &{0} );", stringExtend(tMember.mName,aBlock.mNameMaxSize));
                    }
                }
                else
                {
                    mWCP.WriteLine (2, "for (int i=0;  i<{0}Loop; i++)", tMember.mName);
                    mWCP.WriteLine (2, "{");
                    if (tMember.mMemberType == Defs.cMemberT_String)
                    {
                        mWCP.WriteLine(2, "aBuffer->copyS(  {0} );", stringExtend(tMember.mName + "[i]",aBlock.mNameMaxSize));
                    }
                    else if (tMember.mMemberType == Defs.cMemberT_Record)
                    {
                        mWCP.WriteLine(2, "aBuffer->copy ( &{0} );", stringExtend(tMember.mName + "[i]",aBlock.mNameMaxSize));
                    }
                    else
                    {
                        mWCP.WriteLine(2, "aBuffer->copy ( &{0} );", stringExtend(tMember.mName + "[i]",aBlock.mNameMaxSize));
                    }
                    mWCP.WriteLine(2, "}");
                    mWCP.WriteSkip ();
                }

            });
            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Class End

            if (aBlock.mBlockType == Defs.cBlockT_Message)
            {
                mWCP.WriteSkip ();
                mWCP.WriteLine(2, "mHeader.headerReCopyToFrom(aBuffer,this);");
            }
            mWCP.WriteLine(1, "}");
            mWCP.WriteSkip ();
        }

    };

}//namespace
