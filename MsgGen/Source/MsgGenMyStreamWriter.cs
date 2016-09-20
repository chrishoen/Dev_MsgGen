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
#if false
        public MyStreamWriter(Stream stream)
            : base(stream)
        {

        }
        public MyStreamWriter(Stream stream, Encoding encoding)
            : base(stream, encoding)
        {

        }
        public MyStreamWriter(Stream stream, Encoding encoding, int bufferSize)
            : base(stream, encoding, bufferSize)
        {

        }
        public MyStreamWriter(string path, bool append)
            : base(path, append)
        {

        }
        public MyStreamWriter(string path, bool append, Encoding encoding)
            : base(path, append, encoding)
        {

        }
        public MyStreamWriter(string path, bool append, Encoding encoding, int bufferSize)
            : base(path, append, encoding, bufferSize)
        {

        }

        public override void Write(string value)
        {
            base.Write(value);
        }
#endif
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

        public void WriteBar (int aTab,int aNumOf)
        {
            int tSpace = aTab*mSpacePerTab;

            for (int i = 0; i < aNumOf; i++)
            {
                for (int j = 0; j < tSpace; j++) Write(" ");
                Write("//");
                for (int j = tSpace + 2; j < 81; j++) Write("*");
                WriteLine("");
            }
            mLastWasNotEmpty = true;
        }
    }

}//namespace
