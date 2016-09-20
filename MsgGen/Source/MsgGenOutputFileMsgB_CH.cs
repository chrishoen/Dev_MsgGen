using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MsgGen
{
    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public class OutputFileMsgB_CH : OutputFileBase
    {
        //**********************************************************************
        //**********************************************************************
        //**********************************************************************
        // Members

        public MyStreamWriter mWCH;
        public FileData mFileData;

        public int mNumNameSpace;
        public int mIndent;

        //**********************************************************************
        // Constructor

        public OutputFileMsgB_CH()
        {
            mNumNameSpace = 0;
            mWCH = null;
            mIndent=0;
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************
        // Open stream writer

        public override bool open (String aFilePath)
        {
            mWCH = null;

            try
            {   
                mWCH = new MyStreamWriter(aFilePath,4);
            }
            catch
            {
                Console.WriteLine("Error opening {0}", aFilePath);
            }

            return mWCH  != null;
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************
        // Close stream writer

        public override void close ()
        {
            mWCH.Close();
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
            mWCH.WriteLine (0, "#ifndef {0}", mFileData.mFileHeaderData.mDefineCH);
            mWCH.WriteLine (0, "#define {0}", mFileData.mFileHeaderData.mDefineCH);
            mWCH.WriteSkip ();

            mFileData.mFileHeaderData.mIncludeCHList.ForEach(delegate(String tString)
            {
                if (tString.StartsWith("<"))
                {
                    mWCH.WriteLine(0, "#include {0}", tString);
                }
                else
                {
                    mWCH.WriteLine(0, "#include \"{0}\"", tString);
                }
            });
            mWCH.WriteSkip ();

            
            mWCH.WriteLine ( 0, "namespace {0}",mFileData.mFileHeaderData.mNameSpace);
            mWCH.WriteLine ( 0, "{");
            mWCH.WriteSkip ();
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void writeFileEnd()
        {
            mWCH.WriteLine (0, "}");
            mWCH.WriteLine (0, "#endif");
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void writeIdentifiers()
        {
            mWCH.WriteBar  (1,3);
            mWCH.WriteLine (1, "// Message Types");
            mWCH.WriteSkip ();
            mWCH.WriteLine (1, "class MsgIdT");
            mWCH.WriteLine (1, "{");
            mWCH.WriteLine (1, "public:");
            mWCH.WriteSkip ();
            mWCH.WriteLine (2, "static const int cUnspecified = 0;");

            int ident=1;
            mFileData.mBlockList.ForEach(delegate(BlockData tBlock)
            {
                if (tBlock.mBlockType == Defs.cBlockT_Message)
                {
                    mWCH.WriteLine (2, "static const int c{0} = {1,3};", stringExtend(tBlock.mName,mFileData.mNameMaxSize), ident++);
                }
            });

            mWCH.WriteLine (1, "{0}","};");
            mWCH.WriteSkip ();
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

            mWCH.WriteBar  (1,3);
            mWCH.WriteSkip ();

            mWCH.WriteLine (1, "class {0}  : public Ris::ByteMsgB", aBlock.mName);

            mWCH.WriteLine (1, "{");
            mWCH.WriteLine (1, "public:");
            mWCH.WriteSkip ();

            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Members

            mWCH.WriteBar  (2,1);
            mWCH.WriteLine (2,"// Members");
            mWCH.WriteSkip ();

            if (aBlock.mConstList.Count > 0)
            {
                aBlock.mConstList.ForEach(delegate (ConstData tConst)
                {
                    mWCH.WriteLine(2, "static const int {0} = {1};", stringExtend(tConst.mName, aBlock.mConstMaxSize), tConst.mInitialValue);
                });
                mWCH.WriteSkip();
            }

            aBlock.mMemberList.ForEach(delegate(MemberData tMember)
            {
                if (tMember.mMemberType == Defs.cMemberT_String)
                {
                    mWCH.WriteLine(2, "{0} {1} [{2}];", stringExtend("char", aBlock.mTypeMaxSize), tMember.mName, tMember.mArraySize);
                }
                else if (!tMember.mIsArray)
                {
                    mWCH.WriteLine(2, "{0} {1};", stringExtend(tMember.mTypeName, aBlock.mTypeMaxSize), tMember.mName);
                }
                else
                {
                    mWCH.WriteLine(2, "{0} {1} [{2}];", stringExtend(tMember.mTypeName, aBlock.mTypeMaxSize), tMember.mName, tMember.mArraySize);
                }

            });
            mWCH.WriteSkip ();

            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Constructor

            mWCH.WriteBar  (2,1);
            mWCH.WriteLine (2,"// Constructor");
            mWCH.WriteSkip ();

            mWCH.WriteLine (2, "{0}();",aBlock.mName);
            mWCH.WriteSkip ();

            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Class End

            mWCH.WriteLine(1, "};");
            mWCH.WriteSkip ();
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void writeMessageCopier()
        {
            mWCH.WriteBar  (1,3);
            mWCH.WriteLine (1, "// Message Creator");
            mWCH.WriteSkip ();
            mWCH.WriteLine (1, "class MsgBCopier : public Ris::BaseMsgBCopier");
            mWCH.WriteLine (1, "{");
            mWCH.WriteLine (1, "public:");
            mWCH.WriteSkip ();
            mWCH.WriteBar  (2,1);
            mWCH.WriteLine (2,"// Create");
            mWCH.WriteSkip ();
            mWCH.WriteLine (2, "Ris::ByteMsgB* createMessage(int aMsgType);");
            mWCH.WriteSkip ();
            mWCH.WriteBar  (2,1);
            mWCH.WriteLine (2,"// Copy");
            mWCH.WriteSkip ();
            mWCH.WriteLine (2, "void copyToFrom (Ris::ByteBuffer* aBuffer, Ris::ByteMsgB* aMsg);");
            mWCH.WriteSkip ();
            mWCH.WriteBar  (2,1);
            mWCH.WriteLine (2,"// Copy");
            mWCH.WriteSkip ();

            mFileData.mBlockList.ForEach(delegate(BlockData tBlock)
            {
                if (tBlock.mBlockType == Defs.cBlockT_Message)
                {
                    mWCH.WriteLine (2, "void copyToFrom (Ris::ByteBuffer* aBuffer, {0}aMsg);", stringExtend(tBlock.mName+"*",mFileData.mNameMaxSize));
                }
            });
            mWCH.WriteSkip ();

            mWCH.WriteLine (1, "};");
            mWCH.WriteSkip ();
        }

    };

}//namespace
