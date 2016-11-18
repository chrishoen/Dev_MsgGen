﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MsgGen
{
    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public class OutputFileMsg_CS_Message : OutputFileBase
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

        public OutputFileMsg_CS_Message()
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
               mWCS.WriteLine (1, "public partial class {0} : BaseMsg", aBlock.mName);
            }
            else
            {
               mWCS.WriteLine (1, "public partial class {0} : ByteContent", aBlock.mName);
            }

            mWCS.WriteLine (1, "{");

            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Members

            mWCS.WriteBar  (2,1);
            mWCS.WriteLine (2,"// Members:");
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
                    mWCS.WriteLine (2, "public {0} {1};", stringExtend(tMember.mTypeNameCS,aBlock.mTypeMaxSize), tMember.mName);
                }
                else
                {
                    mWCS.WriteLine (2, "public {0} {1};", stringExtend(tMember.mTypeNameCS + "[]",aBlock.mTypeMaxSize), tMember.mName);
                }

            });
            mWCS.WriteSkip ();


            //******************************************************************
            //******************************************************************
            //******************************************************************
            // Class End

            mWCS.WriteLine(1, "};");
            mWCS.WriteSkip ();
        }

    };

}//namespace
