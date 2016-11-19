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
        public const int cUnspecified       =   0;
        public const int cExampleMsg        =   1;
        public const int cTestMsg           =   2;
        public const int cFirstMessageMsg   =   3;
        public const int cStatusRequestMsg  =   4;
        public const int cStatusResponseMsg =   5;
        public const int cDataMag           =   6;
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
                case MsgIdT.cExampleMsg :
                    tMsg = new ExampleMsg();
                    break;
                case MsgIdT.cTestMsg :
                    tMsg = new TestMsg();
                    break;
                case MsgIdT.cFirstMessageMsg :
                    tMsg = new FirstMessageMsg();
                    break;
                case MsgIdT.cStatusRequestMsg :
                    tMsg = new StatusRequestMsg();
                    break;
                case MsgIdT.cStatusResponseMsg :
                    tMsg = new StatusResponseMsg();
                    break;
                case MsgIdT.cDataMag :
                    tMsg = new DataMag();
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

    public partial class ExampleMsg : BaseMsg
    {
        //**********************************************************************
        // Constructor

        public ExampleMsg()
        {
            mMessageType = MsgIdT.cExampleMsg;
            mCode1     = 0;
            mCode2     = 0;
            mCode3     = 0;
            mCode4     = 0;
            mCodeCode5 = 0;

            mWordsLoop = 4;
            mWords     = new int[4];

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
            aBuffer.copy ( ref mCodeCode5 );

            aBuffer.copy ( ref mWordsLoop );
            for (int i = 0;  i<mWordsLoop; i++ )
            {
            aBuffer.copy ( ref mWords[i] );
            }

            mHeader.headerReCopyToFrom(aBuffer,this);
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

    public partial class FirstMessageMsg : BaseMsg
    {
        //**********************************************************************
        // Constructor

        public FirstMessageMsg()
        {
            mMessageType = MsgIdT.cFirstMessageMsg;
            mCode1 = 0;
        }

        //**********************************************************************
        // Copy

        public override void copyToFrom (ByteBuffer aBuffer)
        {
            mHeader.headerCopyToFrom(aBuffer,this);

            aBuffer.copy ( ref mCode1 );

            mHeader.headerReCopyToFrom(aBuffer,this);
        }
    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public partial class StatusRequestMsg : BaseMsg
    {
        //**********************************************************************
        // Constructor

        public StatusRequestMsg()
        {
            mMessageType = MsgIdT.cStatusRequestMsg;
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

    public partial class StatusResponseMsg : BaseMsg
    {
        //**********************************************************************
        // Constructor

        public StatusResponseMsg()
        {
            mMessageType = MsgIdT.cStatusResponseMsg;
            mCode1     = 0;
            mCode2     = 0;
            mCode3     = 0;
            mCode4     = 0;

            mWordsLoop = 4;
            mWords     = new int[4];

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

            aBuffer.copy ( ref mWordsLoop );
            for (int i = 0;  i<mWordsLoop; i++ )
            {
            aBuffer.copy ( ref mWords[i] );
            }

            mHeader.headerReCopyToFrom(aBuffer,this);
        }
    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public partial class DataRecord : ByteContent
    {
        //**********************************************************************
        // Constructor

        public DataRecord()
        {
            mCode1 = 0;
            mCode2 = 0;
            mCode3 = 0;
            mCode4 = 0;
        }

        //**********************************************************************
        // Copy

        public override void copyToFrom (ByteBuffer aBuffer)
        {
            aBuffer.copy ( ref mCode1 );
            aBuffer.copy ( ref mCode2 );
            aBuffer.copy ( ref mCode3 );
            aBuffer.copy ( ref mCode4 );
        }
    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public partial class DataMag : BaseMsg
    {
        //**********************************************************************
        // Constructor

        public DataMag()
        {
            mMessageType = MsgIdT.cDataMag;
            mUChar      = 0;
            mUShort     = 0;
            mUInt       = 0;
            mUInt64     = 0;
            mChar       = 0;
            mShort      = 0;
            mInt        = 0;
            mInt64      = 0;
            mFloat      = 0.0f;
            mDouble     = 0.0;
            mBool       = false;
            mString1    = String.Empty;
            mString2    = String.Empty;
            mDataRecord = new DataRecord();
        }

        //**********************************************************************
        // Copy

        public override void copyToFrom (ByteBuffer aBuffer)
        {
            mHeader.headerCopyToFrom(aBuffer,this);

            aBuffer.copy ( ref mUChar );
            aBuffer.copy ( ref mUShort );
            aBuffer.copy ( ref mUInt );
            aBuffer.copy ( ref mUInt64 );
            aBuffer.copy ( ref mChar );
            aBuffer.copy ( ref mShort );
            aBuffer.copy ( ref mInt );
            aBuffer.copy ( ref mInt64 );
            aBuffer.copy ( ref mFloat );
            aBuffer.copy ( ref mDouble );
            aBuffer.copy ( ref mBool );
            aBuffer.copyS( ref mString1 );
            aBuffer.copyS( ref mString2 );
            aBuffer.copy (     mDataRecord );

            mHeader.headerReCopyToFrom(aBuffer,this);
        }
    };

}
