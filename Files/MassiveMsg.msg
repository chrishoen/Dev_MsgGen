//******************************************************************************
using      System
using      System.Text
using      System.IO
using      Ris

define_ch   _MASSIVEMSG_H_
include_ch risByteContent.h
include_ch risByteMsgMonkey.h
include_ch massiveMsgBase.h
include_cp  <stdio.h>
include_cp  <string.h>
include_cp  massiveMsg.h

namespace MassiveMsg

//******************************************************************************
// Comment for TestMsg 1.
// Comment for TestMsg 2.

message TestMsg
{
   int          mCode1
   int          mCode2
   int          mCode3
   int          mCode4
}

//******************************************************************************

message FirstMessageMsg
{
   int          mCode1
}

//******************************************************************************

message StatusRequestMsg
{
   int          mCode1
   int          mCode2
   int          mCode3
   int          mCode4
}

//******************************************************************************

message StatusResponseMsg
{
   int          mCode1
   int          mCode2
   int          mCode3
   int          mCode4
   int          mWords [4]
}

//******************************************************************************

record  DataRecord
{
   int          mCode1
   int          mCode2
   int          mCode3
   int          mCode4
}

//******************************************************************************

message DataMag
{
   uchar        mUChar
   ushort       mUShort
   uint         mUInt
   uint64       mUInt64
   char         mChar
   short        mShort
   int          mInt
   int64        mInt64
   float        mFloat
   double       mDouble
   bool         mBool
   string       mString1 [100]
   string       mString2 [100]
   DataRecord   mDataRecord
}
