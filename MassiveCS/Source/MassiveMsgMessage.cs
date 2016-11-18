using System;
using System.Text;
using System.IO;
using Ris;

namespace MassiveMsg
{

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
