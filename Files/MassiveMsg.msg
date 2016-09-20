//******************************************************************************
using      System
using      System.Text
using      System.IO
using      Ris

define_ch   _MASSIVEMSG_H_
include_ch  risByteBuffer.h
include_ch  risByteMsgB.h
include_ch  risByteMsgBCopier.h
include_cp  <stdio.h>
include_cp  <string.h>
include_cp  massiveMsg.h

namespace MassiveMsg

//******************************************************************************

message TestMsg
{
   int          mCode1
   int          mCode2
   int          mCode3
   int          mCode4
}

//******************************************************************************

message StatusMsg
{
   int          mCode1
   int          mCode2
   int          mCode3
   int          mCode4
}

//******************************************************************************

message Data1Msg
{
   int     mCode1
   int     mCode2
   int     mCode3
   int     mCode4
}

//******************************************************************************

message Data2Msg
{
   int        mCode1
   int        mCode2
   int        mCode3
   int        mCode4
   Data1Msg   mData1
}

//******************************************************************************

message Data3Msg
{
   int     mCode1
   int     mCode2
   int     mCode3
   int     mCode4
   String  mString1 [40]
   int     mCode5 [4]
}

//******************************************************************************

message Data4Msg
{
   int        mCode1
   int        mCode2
   int        mCode3
   int        mCode4
   Data1Msg   mData1 [4]
}

