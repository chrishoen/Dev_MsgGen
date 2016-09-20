using System;
using System.Text;
using System.IO;
using Ris;

namespace Example
{
    //******************************************************************************
    //******************************************************************************
    //******************************************************************************
    // Message Types

    public class MsgIdT
    {
        //--------------------------------------------------------------------------
        // Message type indentifier

        public const int cUnspecified    =  0;
        public const int cTest           =  1;
        public const int cFirstMessage   =  2;
        public const int cStatusRequest  =  3;
        public const int cStatusResponse =  4;
        public const int cData           =  5;
    };

    //******************************************************************************
    //******************************************************************************
    //******************************************************************************
    // Message Creator

    public class MessageCreator
    {
        //--------------------------------------------------------------------------
        // Create a new message based on a message type

        public static BaseMsg createMessage(int aMessageType)
        {
            BaseMsg tMsg = null;

            switch (aMessageType)
            {
                case MsgIdT.cTest :
                    tMsg = new TestMsg();
                    break;
                case MsgIdT.cFirstMessage :
                    tMsg = new FirstMessageMsg();
                    break;
                case MsgIdT.cStatusRequest :
                    tMsg = new StatusRequestMsg();
                    break;
                case MsgIdT.cStatusResponse:
                    tMsg = new StatusResponseMsg();
                    break;
                case MsgIdT.cData:
                    tMsg = new DataMsg();
                    break;
                default :
                    break;
            }
            return tMsg;
        }
    }

    //******************************************************************************
    //******************************************************************************
    //******************************************************************************

    public class TestMsg : BaseMsg
    {

        public int   mCode1;
        public int   mCode2;
        public int   mCode3;
        public int   mCode4;

        public int   mLoopArray1;
        public int[] mArray1;

        public TestMsg ()
        {
            mMessageType = MsgIdT.cTest;
            mCode1 = 901;
            mCode2 = 902;
            mCode3 = 903;
            mCode4 = 904;

            mLoopArray1 = 0;
            mArray1 = new int[10]; 
        } 

        public override void copyToFrom (ByteBuffer aBuffer)
        {
            mHeader.headerCopyToFrom(aBuffer,this);

            aBuffer.copy ( ref mCode1 );
            aBuffer.copy ( ref mCode2 );
            aBuffer.copy ( ref mCode3 );
            aBuffer.copy ( ref mCode4 );

            aBuffer.copy ( ref mLoopArray1 );
            for (int i = 0; i < mLoopArray1; i++)
            {
                aBuffer.copy (ref mArray1[i]);
            }

            mHeader.headerReCopyToFrom(aBuffer,this);
        }

        public void initialize()
        {
            mCode1 = 701;
            mCode2 = 702;
            mCode3 = 703;
            mCode4 = 704;

            mLoopArray1 = 4;
            for (int i = 0; i < mLoopArray1; i++)
            {
                mArray1[i] = 800 + i;
            }
        }

        public void show()
        {
            Console.WriteLine("{0}", mCode1);
            Console.WriteLine("{0}", mCode2);
            Console.WriteLine("{0}", mCode3);
            Console.WriteLine("{0}", mCode4);
            for (int i = 0; i < mLoopArray1; i++)
            {
                Console.WriteLine("{0} {1}", i,mArray1[i]);
            }
            Console.WriteLine("");
        }
    };

    //******************************************************************************
    //******************************************************************************
    //******************************************************************************

    public class FirstMessageMsg : BaseMsg
    {
        //------------------------------------------------
        // Content

        int mCode1;

        // Content
        //------------------------------------------------

        public FirstMessageMsg()
        {
            mMessageType = MsgIdT.cFirstMessage;
            mCode1 = 0;
        }

        public override void copyToFrom (ByteBuffer aBuffer)
        {
            mHeader.headerCopyToFrom(aBuffer,this);

            aBuffer.copy( ref mCode1 );

            mHeader.headerReCopyToFrom(aBuffer,this);
        }
    };

    //******************************************************************************
    //******************************************************************************
    //******************************************************************************

    public class StatusRequestMsg : BaseMsg
    {
        //------------------------------------------------
        // Content

        public int mCode1;
        public int mCode2;
        public int mCode3;
        public int mCode4;

        public const int cMaxWords = 10;
        public int       mNumOfWords;
        public int[]     mWords;

        // Content
        //------------------------------------------------

        public StatusRequestMsg ()
        {
            mMessageType = MsgIdT.cStatusRequest;

            mCode1 = 101;
            mCode2 = 102;
            mCode3 = 103;
            mCode4 = 104;

            mNumOfWords = cMaxWords;
            mWords = new int[cMaxWords];

            for (int i=0; i<cMaxWords; i++)
            {
                mWords[i] = 101 + i;
            }
        } 

        public override void copyToFrom (ByteBuffer aBuffer)
        {
            mHeader.headerCopyToFrom(aBuffer,this);

            aBuffer.copy ( ref mCode1 );
            aBuffer.copy ( ref mCode2 );
            aBuffer.copy ( ref mCode3 );
            aBuffer.copy ( ref mCode4 );

            aBuffer.copy ( ref mNumOfWords  );
            for (int i=0;i<mNumOfWords;i++)
            {
                aBuffer.copy (ref mWords[i] );
            }

            mHeader.headerReCopyToFrom(aBuffer,this);
        }
    };
    //******************************************************************************
    //******************************************************************************
    //******************************************************************************

    public class StatusResponseMsg : BaseMsg
    {
        //------------------------------------------------
        // Content

        public int mCode1;
        public int mCode2;
        public int mCode3;
        public int mCode4;

        public const int cMaxWords = 10;
        public int       mNumOfWords;
        public int[]     mWords;

        // Content
        //------------------------------------------------

        public StatusResponseMsg ()
        {
            mMessageType = MsgIdT.cStatusResponse;

            mCode1 = 201;
            mCode2 = 202;
            mCode3 = 203;
            mCode4 = 204;

            mNumOfWords = cMaxWords;
            mWords = new int[cMaxWords];

            for (int i=0; i<cMaxWords; i++)
            {
                mWords[i] = 201 + i;
            }
        } 

        public override void copyToFrom (ByteBuffer aBuffer)
        {
            mHeader.headerCopyToFrom(aBuffer,this);

            aBuffer.copy ( ref mCode1 );
            aBuffer.copy ( ref mCode2 );
            aBuffer.copy ( ref mCode3 );
            aBuffer.copy ( ref mCode4 );

            aBuffer.copy( ref mNumOfWords  );
            for (int i=0;i<mNumOfWords;i++)
            {
                aBuffer.copy (ref mWords[i] );
            }

            mHeader.headerReCopyToFrom(aBuffer,this);
        }
    };
    
    public class DataRecord : ByteContent
    {
        //------------------------------------------------
        // Content

        public int mX1;
        public int mX2;
        public int mX3;
        public int mX4;

        // Content
        //------------------------------------------------

        public DataRecord()
        {
            mX1 = 0;
            mX2 = 0;
            mX3 = 0;
            mX4 = 0;
        }

        public override void copyToFrom (ByteBuffer aBuffer)
        {
            aBuffer.copy(ref mX1 );
            aBuffer.copy(ref mX2 );
            aBuffer.copy(ref mX3 );
            aBuffer.copy(ref mX4 );
        }

    };

    //******************************************************************************
    //******************************************************************************
    //******************************************************************************

    public class DataMsg : BaseMsg
    {
        //------------------------------------------------
        // Content

        public const int cMaxStringSize = 10;

        public byte          mUChar;
        public ushort        mUShort;
        public uint          mUInt;
        public ulong         mUInt64;
        public sbyte         mChar;
        public short         mShort;
        public int           mInt;
        public long          mInt64;
        public float         mFloat;
        public double        mDouble;
        public bool          mBool;
        public String        mString1;
        public String        mString2;
        public DataRecord    mDataRecord;

        // Content
        //------------------------------------------------

        public DataMsg()
        {
            mMessageType = MsgIdT.cData;

            mUChar  = 0;
            mUShort = 0;
            mUInt   = 0;
            mUInt64 = 0;
            mChar   = 0;
            mShort  = 0;
            mInt    = 0;
            mInt64  = 0;
            mFloat  = 0.0f;
            mDouble = 0.0;
            mBool   = false;
            mString1 = String.Empty;
            mString2 = String.Empty;
            mDataRecord = new DataRecord();
        }

        public override void copyToFrom (ByteBuffer aBuffer)
        {
            mHeader.headerCopyToFrom(aBuffer, this);

            aBuffer.copy ( ref mUChar  );
            aBuffer.copy ( ref mUShort );
            aBuffer.copy ( ref mUInt   );
            aBuffer.copy ( ref mUInt64 );
            aBuffer.copy ( ref mChar   );
            aBuffer.copy ( ref mShort  );
            aBuffer.copy ( ref mInt    );
            aBuffer.copy ( ref mInt64  );
            aBuffer.copy ( ref mFloat  );
            aBuffer.copy ( ref mDouble );
            aBuffer.copy ( ref mBool   );
            aBuffer.copy ( mDataRecord );
            aBuffer.copyS( ref mString1 );
            aBuffer.copyS( ref mString2 );

            mHeader.headerReCopyToFrom(aBuffer, this);
        }
    };
}
