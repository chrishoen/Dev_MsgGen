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
        public const int cTestMsg           =   1;
        public const int cFirstMessageMsg   =   2;
        public const int cStatusRequestMsg  =   3;
        public const int cStatusResponseMsg =   4;
        public const int cDataMag           =   5;
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
            mCode1      = 0;
            mCode2      = 0;
            mCode3      = 0;
            mCode4      = 0;
            mDataRecord = new DataRecord();
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
            aBuffer.copy (     mDataRecord );

            mHeader.headerReCopyToFrom(aBuffer,this);
        }
    };

}
