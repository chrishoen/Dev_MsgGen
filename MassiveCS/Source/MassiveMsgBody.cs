using System;
using System.Text;
using System.IO;
using Ris;

namespace MassiveMsg
{

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************
    // Message Types

    public class MsgIdT
    {
        public const int cUnspecified =   0;
        public const int cTestMsg     =   1;
        public const int cStatusMsg   =   2;
        public const int cData1Msg    =   3;
        public const int cData2Msg    =   4;
        public const int cData3Msg    =   5;
        public const int cData4Msg    =   6;
    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************
    // Message Creator

    public class MsgCreator : Ris.BaseMsgCreator
    {
        //**********************************************************************
        // Create a new message based on a message type.

        public override Ris.ByteContent createMsg(int aMessageType)
        {
            BaseMsg tMsg = null;

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
                default :
                    break;
            }
            return tMsg;
        }
    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public partial class TestMsg : BaseMsg
    {
        //**********************************************************************
        // Constructor

        public TestMsg()
        {
            mMessageType = MsgIdT.cTestMsg;
            mCode1 = 0;
            mCode2 = 0;
            mCode3 = 0;
            mCode4 = 0;
        }

        //**********************************************************************
        // Copy

        public override void copyToFrom (ByteBuffer aBuffer)
        {
            mHeader.headerCopyToFrom(aBuffer,this);

            aBuffer.copy ( ref mCode1 );
            aBuffer.copy ( ref mCode2 );
            aBuffer.copy ( ref mCode3 );
            aBuffer.copy ( ref mCode4 );

            mHeader.headerReCopyToFrom(aBuffer,this);
        }
    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public partial class StatusMsg : BaseMsg
    {
        //**********************************************************************
        // Constructor

        public StatusMsg()
        {
            mMessageType = MsgIdT.cStatusMsg;
            mCode1 = 0;
            mCode2 = 0;
            mCode3 = 0;
            mCode4 = 0;
        }

        //**********************************************************************
        // Copy

        public override void copyToFrom (ByteBuffer aBuffer)
        {
            mHeader.headerCopyToFrom(aBuffer,this);

            aBuffer.copy ( ref mCode1 );
            aBuffer.copy ( ref mCode2 );
            aBuffer.copy ( ref mCode3 );
            aBuffer.copy ( ref mCode4 );

            mHeader.headerReCopyToFrom(aBuffer,this);
        }
    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public partial class Data1Msg : BaseMsg
    {
        //**********************************************************************
        // Constructor

        public Data1Msg()
        {
            mMessageType = MsgIdT.cData1Msg;
            mCode1 = 0;
            mCode2 = 0;
            mCode3 = 0;
            mCode4 = 0;
        }

        //**********************************************************************
        // Copy

        public override void copyToFrom (ByteBuffer aBuffer)
        {
            mHeader.headerCopyToFrom(aBuffer,this);

            aBuffer.copy ( ref mCode1 );
            aBuffer.copy ( ref mCode2 );
            aBuffer.copy ( ref mCode3 );
            aBuffer.copy ( ref mCode4 );

            mHeader.headerReCopyToFrom(aBuffer,this);
        }
    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public partial class Data2Msg : BaseMsg
    {
        //**********************************************************************
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

        //**********************************************************************
        // Copy

        public override void copyToFrom (ByteBuffer aBuffer)
        {
            mHeader.headerCopyToFrom(aBuffer,this);

            aBuffer.copy ( ref mCode1 );
            aBuffer.copy ( ref mCode2 );
            aBuffer.copy ( ref mCode3 );
            aBuffer.copy ( ref mCode4 );
            aBuffer.copy (     mData1 );

            mHeader.headerReCopyToFrom(aBuffer,this);
        }
    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public partial class Data3Msg : BaseMsg
    {
        //**********************************************************************
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

        //**********************************************************************
        // Copy

        public override void copyToFrom (ByteBuffer aBuffer)
        {
            mHeader.headerCopyToFrom(aBuffer,this);

            aBuffer.copy ( ref mCode1 );
            aBuffer.copy ( ref mCode2 );
            aBuffer.copy ( ref mCode3 );
            aBuffer.copy ( ref mCode4 );
            aBuffer.copyS( ref mString1 );

            aBuffer.copy ( ref mCode5Loop );
            for (int i = 0;  i<mCode5Loop; i++ )
            {
            aBuffer.copy ( ref mCode5[i] );
            }

            mHeader.headerReCopyToFrom(aBuffer,this);
        }
    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public partial class Data4Msg : BaseMsg
    {
        //**********************************************************************
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

        //**********************************************************************
        // Copy

        public override void copyToFrom (ByteBuffer aBuffer)
        {
            mHeader.headerCopyToFrom(aBuffer,this);

            aBuffer.copy ( ref mCode1 );
            aBuffer.copy ( ref mCode2 );
            aBuffer.copy ( ref mCode3 );
            aBuffer.copy ( ref mCode4 );

            aBuffer.copy ( ref mData1Loop );
            for (int i = 0;  i<mData1Loop; i++ )
            {
            aBuffer.copy (     mData1[i] );
            }

            mHeader.headerReCopyToFrom(aBuffer,this);
        }
    };

}
