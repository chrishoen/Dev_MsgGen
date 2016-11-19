using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MsgGen
{
    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public class MyStreamWriter : StreamWriter 
    {
        //**********************************************************************
        //**********************************************************************
        //**********************************************************************
        public bool mLastWasNotEmpty;
        public int mSpacePerTab;

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public MyStreamWriter(string aPath,int aSpacePerTab)
            : base(aPath)
        {
            mSpacePerTab = aSpacePerTab;

        }
        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void Write (int aTab,string aString)
        {
            int tSpace = aTab*mSpacePerTab;
            for (int i=0; i<tSpace; i++) Write(" ");

            Write(aString);
        }

        public void Write (int aTab,string aFormat,params object[] aArgs)
        {
            int tSpace = aTab*mSpacePerTab;
            for (int i=0; i<tSpace; i++) Write(" ");

            Write(aFormat,aArgs);
        }

        public void WriteSpace (int aNumOf)
        {
            for (int i=0; i<aNumOf; i++) Write(" ");
        }

        public void WriteSkip ()
        {
            if (mLastWasNotEmpty)
            {
                WriteLine("");
                mLastWasNotEmpty = false;
            }
        }

        public void WriteLine (int aTab,string aString)
        {
            int tSpace = aTab*mSpacePerTab;
            for (int i=0; i<tSpace; i++) Write(" ");

            WriteLine(aString);
            mLastWasNotEmpty = true;
        }

        public void WriteLine (int aTab,string aFormat,params object[] aArgs)
        {
            int tSpace = aTab*mSpacePerTab;
            for (int i=0; i<tSpace; i++) Write(" ");

            WriteLine(aFormat,aArgs);
            mLastWasNotEmpty = true;
        }

        public void WriteEOL ()
        {
            WriteLine("");
            mLastWasNotEmpty = true;
        }

        public void WriteBar (int aTab,int aNumOf)
        {
            int tSpace = aTab*mSpacePerTab;

            for (int i = 0; i < aNumOf; i++)
            {
                for (int j = 0; j < tSpace; j++) Write(" ");
                Write("//");
                for (int j = tSpace + 2; j < 80; j++) Write("*");
                WriteLine("");
            }
            mLastWasNotEmpty = true;
        }

        public void WritePreCommentList (int aTab,List<String> aPreCommentList)
        {
            if (aPreCommentList==null) return;
            if (aPreCommentList.Count==0) return;

            int tNumOf = aPreCommentList.Count;
            int tSpace = aTab*mSpacePerTab;

            for (int i = 0; i < tNumOf; i++)
            {
                for (int j = 0; j < tSpace; j++) Write(" ");
                WriteLine("{0}",aPreCommentList[i]);
            }
            mLastWasNotEmpty = true;
        }
    }

}//namespace
