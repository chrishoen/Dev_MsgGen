﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MsgGen
{
    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public class OutputFileMsg_CH : OutputFileBase
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

        public OutputFileMsg_CH()
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
            mWCH.WriteBar  (0,3);
            mWCH.WriteLine (0, "// Message Types");
            mWCH.WriteSkip ();
            mWCH.WriteLine (0, "class MsgIdT");
            mWCH.WriteLine (0, "{");
            mWCH.WriteLine (0, "public:");
            mWCH.WriteSkip ();
            mWCH.WriteLine (1, "static const int cUnspecified =   0;");

            int ident=1;
            mFileData.mBlockList.ForEach(delegate(BlockData tBlock)
            {
                if (tBlock.mBlockType == Defs.cBlockT_Message)
                {
                    mWCH.WriteLine (1, "static const int c{0} = {1,3};", stringExtend(tBlock.mName,mFileData.mNameMaxSize), ident++);
                }
            });

            mWCH.WriteLine (0, "{0}","};");
            mWCH.WriteSkip ();
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void writeMessageCreator()
        {
            mWCH.WriteBar  (0,3);
            mWCH.WriteLine (0, "// Message Creator:");
            mWCH.WriteSkip ();
            mWCH.WriteLine (0, "class MsgCreator : public Ris::BaseMsgCreator");
            mWCH.WriteLine (0, "{");
            mWCH.WriteLine (0, "public:");
            mWCH.WriteBar  (1,1);
            mWCH.WriteLine (1, "// Create a new message, based on a message type.");
            mWCH.WriteSkip ();
            mWCH.WriteLine (1, "Ris::ByteContent* createMsg (int aMessageType) override;");
            mWCH.WriteLine (0, "};");
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

            mWCH.WriteBar  (0,3);
            mWCH.WriteSkip ();

            if (aBlock.mBlockType == Defs.cBlockT_Message)
            {
               mWCH.WriteLine (0, "class {0} : public BaseMsg", aBlock.mName);
            }
            else
            {
               mWCH.WriteLine (0, "class {0} : public Ris::ByteContent", aBlock.mName);
            }

            mWCH.WriteLine (0, "{");
            mWCH.WriteLine (0, "public:");
            mWCH.WriteSkip ();

            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Members

            mWCH.WriteBar  (1,1);
            mWCH.WriteLine (1,"// Members:");
            mWCH.WriteSkip ();

            if (aBlock.mConstList.Count > 0)
            {
                aBlock.mConstList.ForEach(delegate (ConstData tConst)
                {
                    mWCH.WriteLine(1, "static const int {0} = {1};", stringExtend(tConst.mName, aBlock.mConstMaxSize), tConst.mInitialValue);
                });
                mWCH.WriteSkip();
            }

            aBlock.mMemberList.ForEach(delegate(MemberData tMember)
            {
                if (tMember.mMemberType == Defs.cMemberT_String)
                {
                    mWCH.WriteLine(1, "{0} {1} [{2}];", stringExtend("char", aBlock.mTypeMaxSize), tMember.mName, tMember.mArraySize);
                }
                else if (!tMember.mIsArray)
                {
                    mWCH.WriteLine(1, "{0} {1};", stringExtend(tMember.mTypeNameCP, aBlock.mTypeMaxSize), tMember.mName);
                }
                else
                {
                    mWCH.WriteLine(1, "{0} {1} [{2}];", stringExtend(tMember.mTypeNameCP, aBlock.mTypeMaxSize), tMember.mName, tMember.mArraySize);
                }

            });
            mWCH.WriteSkip ();

            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Methods

            mWCH.WriteBar  (1,1);
            mWCH.WriteLine (1,"// Methods:");
            mWCH.WriteSkip ();

            mWCH.WriteLine (1, "{0}();",aBlock.mName);
            mWCH.WriteLine (1, "void copyToFrom (Ris::ByteBuffer* aBuffer);");

            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Class End

            mWCH.WriteLine(0, "};");
            mWCH.WriteSkip ();
        }

    };

}//namespace
