using System;
using System.Text;
using System.IO;
using Ris;

//******************************************************************************
//******************************************************************************
//******************************************************************************
// Comment for message set 1.
// Comment for message set 2.

namespace MassiveMsg
{

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************
    // Pre comment for ExampleMsg 1.
    // Pre comment for ExampleMsg 2.

    public partial class ExampleMsg : BaseMsg
    {
        //**********************************************************************
        // Members:

        // Pre comment for cSize1 1.
        // pre comment for cSize1 2.
        public const int cSize1     = 1001;    // Post comment 1
        public const int cSizeSize2 = 1002;    // Post comment 2

        // Pre comment for mCode1 1.
        // pre comment for mCode1 2.
        public int   mCode1;        // Post comment 1
        public int   mCode2;
        public int   mCode3;
        public int   mCode4;
        public int   mCodeCode5;    // Post comment 5
        // Pre comment for mWords
        public int   mWordsLoop;    // Post comment
        public int[] mWords;

    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public partial class TestMsg : BaseMsg
    {
        //**********************************************************************
        // Members:

        public int mCode1;
        public int mCode2;
        public int mCode3;
        public int mCode4;

    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public partial class FirstMessageMsg : BaseMsg
    {
        //**********************************************************************
        // Members:

        public int mCode1;

    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public partial class StatusRequestMsg : BaseMsg
    {
        //**********************************************************************
        // Members:

        public int mCode1;
        public int mCode2;
        public int mCode3;
        public int mCode4;

    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public partial class StatusResponseMsg : BaseMsg
    {
        //**********************************************************************
        // Members:

        public int   mCode1;
        public int   mCode2;
        public int   mCode3;
        public int   mCode4;
        public int   mWordsLoop;
        public int[] mWords;

    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public partial class DataRecord : ByteContent
    {
        //**********************************************************************
        // Members:

        public int mCode1;
        public int mCode2;
        public int mCode3;
        public int mCode4;

    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public partial class DataMag : BaseMsg
    {
        //**********************************************************************
        // Members:

        public byte               mUChar;
        public ushort             mUShort;
        public uint               mUInt;
        public ulong              mUInt64;
        public sbyte              mChar;
        public short              mShort;
        public int                mInt;
        public long               mInt64;
        public float              mFloat;
        public double             mDouble;
        public bool               mBool;
        public string             mString1;
        public string             mString2;
        public DataRecord         mDataRecord;

    };

}
