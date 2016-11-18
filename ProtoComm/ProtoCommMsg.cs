using System;
using System.Text;
using System.IO;
using Ris;

/*==============================================================================
This file contains a set of classes that encapsulate a message set.

There is a class for each particular message in the set and there is a
base class that all of the messages inherit from.

These messages follow the ByteContent pattern, where they all inherit
from ByteContent so that they can be copied to/from ByteBuffers.

The base class is used to specify set membership, any inheriting class
is a member of the message set, and message objects can be referenced
anonymously via pointers to the the base class.

The base class provides a member function, makeFromByteBuffer, that
extracts particular messages from a byte buffer and returns a pointer
to the base class.

These messages all have the same common form, they all contain a
common message header. The base class has a Header member object that
encapsulates the header.
==============================================================================*/

namespace ProtoComm
{
    //******************************************************************************
    //******************************************************************************
    //******************************************************************************
    // Particular message classes.
    // There is one class for each message in the message set.

    public partial class TestMsg : BaseMsg
    {
        //**************************************************************************
        // Members:

        public int mCode1;
        public int mCode2;
        public int mCode3;
        public int mCode4;
    };

    //******************************************************************************
    //******************************************************************************
    //******************************************************************************

    public partial class FirstMessageMsg : BaseMsg
    {
        //**************************************************************************
        // Members:

        public int mCode1;
    };

    //******************************************************************************
    //******************************************************************************
    //******************************************************************************

    public partial class StatusRequestMsg : BaseMsg
    {
        //**************************************************************************
        // Members:

        public int mCode1;
        public int mCode2;
        public int mCode3;
        public int mCode4;

        public const int cMaxWords = 10;
        public int       mNumOfWords;
        public int[]     mWords;

    };
    //******************************************************************************
    //******************************************************************************
    //******************************************************************************

    public partial class StatusResponseMsg : BaseMsg
    {
        //**************************************************************************
        // Members:

        public int mCode1;
        public int mCode2;
        public int mCode3;
        public int mCode4;

        public const int cMaxWords = 10;
        public int       mNumOfWords;
        public int[]     mWords;

    };
    
    public partial class DataRecord : ByteContent
    {
        //**************************************************************************
        // Members:

        public int mCode1;
        public int mCode2;
        public int mCode3;
        public int mCode4;

    }

    //******************************************************************************
    //******************************************************************************
    //******************************************************************************

    public partial class DataMsg : BaseMsg
    {
        //**************************************************************************
        // Members:

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

    };
}
