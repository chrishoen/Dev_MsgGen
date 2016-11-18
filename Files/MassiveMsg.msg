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
   int          mCode1
   int          mCode2
   int          mCode3
   int          mCode4
   DataRecord   mDataRecord
}
