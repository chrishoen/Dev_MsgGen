using System;
using System.Text;
using System.IO;
using Ris;

namespace MassiveMsg
{

    //***************************************************************************
    //***************************************************************************
    //***************************************************************************
    // Message Types

    public class MsgIdT
    {
        public const int cUnspecified = 0;
        public const int cTestMsg     =   1;
        public const int cStatusMsg   =   2;
        public const int cData1Msg    =   3;
        public const int cData2Msg    =   4;
        public const int cData3Msg    =   5;
        public const int cData4Msg    =   6;
    };

    //***************************************************************************
    //***************************************************************************
    //***************************************************************************

    public class TestMsg : ByteMsgB
    {
        //***********************************************************************
        // Members

        public int mCode1;
        public int mCode2;
        public int mCode3;
        public int mCode4;

        //***********************************************************************
        // Constructor

        public TestMsg()
        {
            mMessageType = MsgIdT.cTestMsg;

            mCode1 = 0;
            mCode2 = 0;
            mCode3 = 0;
            mCode4 = 0;
        }
    };

    //***************************************************************************
    //***************************************************************************
    //***************************************************************************

    public class StatusMsg : ByteMsgB
    {
        //***********************************************************************
        // Members

        public int mCode1;
        public int mCode2;
        public int mCode3;
        public int mCode4;

        //***********************************************************************
        // Constructor

        public StatusMsg()
        {
            mMessageType = MsgIdT.cStatusMsg;

            mCode1 = 0;
            mCode2 = 0;
            mCode3 = 0;
            mCode4 = 0;
        }
    };

    //***************************************************************************
    //***************************************************************************
    //***************************************************************************

    public class Data1Msg : ByteMsgB
    {
        //***********************************************************************
        // Members

        public int mCode1;
        public int mCode2;
        public int mCode3;
        public int mCode4;

        //***********************************************************************
        // Constructor

        public Data1Msg()
        {
            mMessageType = MsgIdT.cData1Msg;

            mCode1 = 0;
            mCode2 = 0;
            mCode3 = 0;
            mCode4 = 0;
        }
    };

    //***************************************************************************
    //***************************************************************************
    //***************************************************************************

    public class Data2Msg : ByteMsgB
    {
        //***********************************************************************
        // Members

        public int      mCode1;
        public int      mCode2;
        public int      mCode3;
        public int      mCode4;
        public Data1Msg mData1;

        //***********************************************************************
        // Constructor

        public Data2Msg()
        {
            mMessageType = MsgIdT.cData2Msg;

            mCode1 = 0;
            mCode2 = 0;
            mCode3 = 0;
            mCode4 = 0;
            mData1 = new Data1Msg();
        }
    };

    //***************************************************************************
    //***************************************************************************
    //***************************************************************************

    public class Data3Msg : ByteMsgB
    {
        //***********************************************************************
        // Members

        public int    mCode1;
        public int    mCode2;
        public int    mCode3;
        public int    mCode4;
        public string mString1;
        public int    mCode5Loop;
        public int[]  mCode5;

        //***********************************************************************
        // Constructor

        public Data3Msg()
        {
            mMessageType = MsgIdT.cData3Msg;

            mCode1     = 0;
            mCode2     = 0;
            mCode3     = 0;
            mCode4     = 0;
            mString1   = String.Empty;

            mCode5Loop = 4;
            mCode5     = new int[4];

        }
    };

    //***************************************************************************
    //***************************************************************************
    //***************************************************************************

    public class Data4Msg : ByteMsgB
    {
        //***********************************************************************
        // Members

        public int        mCode1;
        public int        mCode2;
        public int        mCode3;
        public int        mCode4;
        public int        mData1Loop;
        public Data1Msg[] mData1;

        //***********************************************************************
        // Constructor

        public Data4Msg()
        {
            mMessageType = MsgIdT.cData4Msg;

            mCode1     = 0;
            mCode2     = 0;
            mCode3     = 0;
            mCode4     = 0;

            mData1Loop = 4;
            mData1     = new Data1Msg[4];
            for (int i=0; i<4; i++)
            {
            mData1[i]  = new Data1Msg();
            }
        }
    };

    //***************************************************************************
    //***************************************************************************
    //***************************************************************************
    // Copier

    public class MsgBCopier : BaseMsgBCopier
    {

        //***********************************************************************
        //***********************************************************************
        //***********************************************************************
        // Create

        public override ByteMsgB createMessage (int aMessageType)
        {
            ByteMsgB tMsg = null;

            switch (aMessageType)
            {
                case MsgIdT.cTestMsg :
                    tMsg = new TestMsg();
                    break;
                case MsgIdT.cStatusMsg :
                    tMsg = new StatusMsg();
                    break;
                case MsgIdT.cData1Msg :
                    tMsg = new Data1Msg();
                    break;
                case MsgIdT.cData2Msg :
                    tMsg = new Data2Msg();
                    break;
                case MsgIdT.cData3Msg :
                    tMsg = new Data3Msg();
                    break;
                case MsgIdT.cData4Msg :
                    tMsg = new Data4Msg();
                    break;
            }

            return tMsg;
        }

        //***********************************************************************
        //***********************************************************************
        //***********************************************************************
        // Copy

        public override void copyToFrom( ByteBuffer aBuffer, ByteMsgB aMsg)
        {
            switch (aMsg.mMessageType)
            {
                case MsgIdT.cTestMsg :
                    copyToFrom(aBuffer, (TestMsg)aMsg);
                    break;
                case MsgIdT.cStatusMsg :
                    copyToFrom(aBuffer, (StatusMsg)aMsg);
                    break;
                case MsgIdT.cData1Msg :
                    copyToFrom(aBuffer, (Data1Msg)aMsg);
                    break;
                case MsgIdT.cData2Msg :
                    copyToFrom(aBuffer, (Data2Msg)aMsg);
                    break;
                case MsgIdT.cData3Msg :
                    copyToFrom(aBuffer, (Data3Msg)aMsg);
                    break;
                case MsgIdT.cData4Msg :
                    copyToFrom(aBuffer, (Data4Msg)aMsg);
                    break;
            }
        }

        //***********************************************************************
        //***********************************************************************
        //***********************************************************************
        // Copy TestMsg

        public void copyToFrom( ByteBuffer aBuffer, TestMsg aMsg)
        {
            aBuffer.copy ( ref aMsg.mCode1 );
            aBuffer.copy ( ref aMsg.mCode2 );
            aBuffer.copy ( ref aMsg.mCode3 );
            aBuffer.copy ( ref aMsg.mCode4 );
        }

        //***********************************************************************
        // Copy StatusMsg

        public void copyToFrom( ByteBuffer aBuffer, StatusMsg aMsg)
        {
            aBuffer.copy ( ref aMsg.mCode1 );
            aBuffer.copy ( ref aMsg.mCode2 );
            aBuffer.copy ( ref aMsg.mCode3 );
            aBuffer.copy ( ref aMsg.mCode4 );
        }

        //***********************************************************************
        // Copy Data1Msg

        public void copyToFrom( ByteBuffer aBuffer, Data1Msg aMsg)
        {
            aBuffer.copy ( ref aMsg.mCode1 );
            aBuffer.copy ( ref aMsg.mCode2 );
            aBuffer.copy ( ref aMsg.mCode3 );
            aBuffer.copy ( ref aMsg.mCode4 );
        }

        //***********************************************************************
        // Copy Data2Msg

        public void copyToFrom( ByteBuffer aBuffer, Data2Msg aMsg)
        {
            aBuffer.copy ( ref aMsg.mCode1 );
            aBuffer.copy ( ref aMsg.mCode2 );
            aBuffer.copy ( ref aMsg.mCode3 );
            aBuffer.copy ( ref aMsg.mCode4 );
            copyToFrom(aBuffer,aMsg.mData1 );
        }

        //***********************************************************************
        // Copy Data3Msg

        public void copyToFrom( ByteBuffer aBuffer, Data3Msg aMsg)
        {
            aBuffer.copy ( ref aMsg.mCode1 );
            aBuffer.copy ( ref aMsg.mCode2 );
            aBuffer.copy ( ref aMsg.mCode3 );
            aBuffer.copy ( ref aMsg.mCode4 );
            aBuffer.copyS( ref aMsg.mString1 );
            aBuffer.copy ( ref aMsg.mCode5Loop );
            for (int i = 0;  i<aMsg.mCode5Loop; i++ )
            {
            aBuffer.copy ( ref aMsg.mCode5[i] );
            }
        }

        //***********************************************************************
        // Copy Data4Msg

        public void copyToFrom( ByteBuffer aBuffer, Data4Msg aMsg)
        {
            aBuffer.copy ( ref aMsg.mCode1 );
            aBuffer.copy ( ref aMsg.mCode2 );
            aBuffer.copy ( ref aMsg.mCode3 );
            aBuffer.copy ( ref aMsg.mCode4 );
            aBuffer.copy ( ref aMsg.mData1Loop );
            for (int i = 0;  i<aMsg.mData1Loop; i++ )
            {
            copyToFrom(aBuffer,aMsg.mData1[i]);
            }
        }

    }
}
