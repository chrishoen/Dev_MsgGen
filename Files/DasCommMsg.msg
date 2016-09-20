//******************************************************************************
using      System
using      System.Text
using      System.IO
using      Ris

define_ch   _DASCOMMMSG_MESSAGE_H_
include_ch  risByteBuffer.h
include_ch  risByteTMessage.h
include_ch  risByteTMessageCopier.h
include_cp  <stdio.h>
include_cp  <string.h>
include_cp  dascommMsg.h

namespace DasCommMsg


//******************************************************************************

message SensorRecord
{
   int     mIndex
   double  mToa
   double  mTiltAngleX
   double  mTiltAngleY
}

//******************************************************************************

message SensorDataMsg
{
   int            mCount

   SensorRecord   mForeSensor
   SensorRecord   mAftSensor
   SensorRecord   mRefSensor

   double         mAlignmentAngle
   double         mAlignmentFigureOfMerit
}

//******************************************************************************

message TestMsg
{
   int          mCode1 = 1001
   int          mCode2
   int          mCode3
   int          mCode4
}

//******************************************************************************

message EchoRequestMsg
{
   int   mCode1
}

//******************************************************************************

message EchoResponseMsg
{
   int   mCode1
}

//******************************************************************************

message CaptureRequestMsg
{
   int       mCode1
}

//******************************************************************************

message CaptureCompletionMsg
{
   int            mCode1
   bool           mValidFlag

   SensorRecord   mForeSensor
   SensorRecord   mAftSensor
   SensorRecord   mRefSensor

   double         mAlignmentAngle
   double         mAlignmentFigureOfMerit
}

//******************************************************************************

message CalibrationRequestMsg
{
   int       mCode1
}

//******************************************************************************

message CalibrationCompletionMsg
{
   bool      mCompletionStatus
   double    mUncertainty
}

