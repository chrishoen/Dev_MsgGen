//******************************************************************************
using      System
using      System.Text
using      System.IO
using      Ris

define_ch   _BLUECOMMTMESSAGE_MESSAGE_H_
include_ch  risByteBuffer.h
include_ch  risByteTMessage.h
include_ch  risByteTMessageCopier.h
include_cp  <stdio.h>
include_cp  <string.h>
include_cp  bluecommTMessage.h

namespace BlueCommMsg

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
