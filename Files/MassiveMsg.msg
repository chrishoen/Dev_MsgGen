//******************************************************************************
//******************************************************************************
//******************************************************************************
//******************************************************************************
// Comment for message set 1.
// Comment for message set 2.
// 1001

namespace MassiveMsg

//******************************************************************************
// Pre comment for ExampleMsg 1.
// Pre comment for ExampleMsg 2.

message ExampleMsg
{
   // Pre comment for cSize1 1.
   // pre comment for cSize1 2.

   const        cSize1     = 1001      // Post comment 1
   const        cSizeSize2 = 1002      // Post comment 2

   // Pre comment for mCode1 1.
   // pre comment for mCode1 2.

   int          mCode1      // Post comment 1
   int          mCode2
   int          mCode3
   int          mCode4
   int          mCodeCode5  // Post comment 5

   // Pre comment for mWords
   int          mWords [4]  // Post comment        
}

//******************************************************************************

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

//******************************************************************************
//******************************************************************************
//******************************************************************************
// File header data

using      System
using      System.Text
using      System.IO
using      Ris

define_ch   _MASSIVEMSG_H_

include_ch  risByteContent.h
include_ch  risByteMsgMonkey.h
include_ch  massiveMsgBase.h

include_cp  <stdio.h>
include_cp  <string.h>
include_cp  massiveMsg.h

//******************************************************************************
//******************************************************************************
//******************************************************************************
// Output file paths

WriteFilePathCSMessage   C:\Prime\DevelopComm\Dev_MsgGen\MassiveCS\Source\MassiveMsgMessage.cs
WriteFilePathCSBody      C:\Prime\DevelopComm\Dev_MsgGen\MassiveCS\Source\MassiveMsgBody.cs
WriteFilePathCH          C:\Prime\DevelopComm\Dev_MsgGen\MassiveCP\Source\massiveMsg.h
WriteFilePathCP          C:\Prime\DevelopComm\Dev_MsgGen\MassiveCP\Source\massiveMsg.cpp
