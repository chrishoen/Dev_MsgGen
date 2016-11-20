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
        public InputData mInputData;

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

        public override void write(InputData aInputData)
        {
            mInputData = aInputData;

            writeFileBegin();

            mInputData.mBlockList.ForEach(delegate(BlockData tBlock)
            {
                writeBlock(tBlock);
            });

            writeMessageCreator();
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
            mInputData.mFileHeaderData.mIncludeCPList.ForEach(delegate(String tString)
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

            
            mWCP.WriteLine ( 0, "namespace {0}",mInputData.mFileHeaderData.mNameSpace);
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
            mWCP.WriteBar  (0,3);
            mWCP.WriteLine (0, "// Create a new message, based on a message type.");
            mWCP.WriteSkip ();
            mWCP.WriteLine (0, "Ris::ByteContent* MsgCreator::createMsg (int aMessageType)");
            mWCP.WriteLine (0, "{");
            mWCP.WriteLine (1, "BaseMsg* tMsg = 0;");
            mWCP.WriteSkip ();
            mWCP.WriteLine (1, "switch (aMessageType)");
            mWCP.WriteLine (1, "{");

            mInputData.mBlockList.ForEach(delegate(BlockData tBlock)
            {
                if (tBlock.mBlockType == Defs.cBlockT_Message)
                {
                    mWCP.WriteLine (2, "case MsgIdT::c{0} :", tBlock.mName);
                    mWCP.WriteLine (3, "tMsg = new {0}();", tBlock.mName);
                    mWCP.WriteLine (3, "break;");
                }
            });

            mWCP.WriteLine (2, "default :");
            mWCP.WriteLine (3, "break;");

            mWCP.WriteLine (1, "}");
            mWCP.WriteLine (1, "return tMsg;");
            mWCP.WriteLine (0, "}");
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

            mWCP.WriteBar  (0,3);
            mWCP.WriteLine (0, "// {0}", aBlock.mName);
            mWCP.WriteSkip ();

            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Constructor

            mWCP.WriteLine (0, "{0}::{0}()",aBlock.mName);
            mWCP.WriteLine (0, "{");

            if (aBlock.mBlockType == Defs.cBlockT_Message)
            {
                mWCP.WriteLine (1, "{0} = MsgIdT::c{1};",stringExtend("mMessageType",aBlock.mNameMaxSize), aBlock.mName);
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
                        mWCP.WriteLine (1, "{0}[0]=0;", tMember.mName);
                    }
                    else if (tMember.mMemberType != Defs.cMemberT_Record)
                    {
                        mWCP.WriteLine (1, "{0} = {1};", stringExtend(tMember.mName,aBlock.mNameMaxSize), tMember.mInitialValue);
                    }
                 }
            });

            mWCP.WriteLine (0, "}");
            mWCP.WriteSkip ();

            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Copy

            mWCP.WriteLine (0, "void {0}::copyToFrom (Ris::ByteBuffer* aBuffer)",aBlock.mName);
            mWCP.WriteLine (0, "{");

            if (aBlock.mBlockType == Defs.cBlockT_Message)
            {
                mWCP.WriteLine (1, "mHeader.headerCopyToFrom(aBuffer,this);");
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
                        mWCP.WriteLine (1, "aBuffer->copyS(  {0} );", stringExtend(tMember.mName,aBlock.mNameMaxSize));
                    }
                    else if (tMember.mMemberType == Defs.cMemberT_Record)
                    {
                        mWCP.WriteLine (1, "aBuffer->copy ( &{0} );", stringExtend(tMember.mName,aBlock.mNameMaxSize));
                    }
                    else
                    {
                        mWCP.WriteLine (1, "aBuffer->copy ( &{0} );", stringExtend(tMember.mName,aBlock.mNameMaxSize));
                    }
                }
                else
                {
                    mWCP.WriteLine (1, "for (int i=0;  i<{0}Loop; i++)", tMember.mName);
                    mWCP.WriteLine (1, "{");
                    if (tMember.mMemberType == Defs.cMemberT_String)
                    {
                        mWCP.WriteLine(1, "aBuffer->copyS(  {0} );", stringExtend(tMember.mName + "[i]",aBlock.mNameMaxSize));
                    }
                    else if (tMember.mMemberType == Defs.cMemberT_Record)
                    {
                        mWCP.WriteLine(1, "aBuffer->copy ( &{0} );", stringExtend(tMember.mName + "[i]",aBlock.mNameMaxSize));
                    }
                    else
                    {
                        mWCP.WriteLine(1, "aBuffer->copy ( &{0} );", stringExtend(tMember.mName + "[i]",aBlock.mNameMaxSize));
                    }
                    mWCP.WriteLine(1, "}");
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
                mWCP.WriteLine(1, "mHeader.headerReCopyToFrom(aBuffer,this);");
            }
            mWCP.WriteLine(0, "}");
            mWCP.WriteSkip ();
        }

    };

}//namespace
