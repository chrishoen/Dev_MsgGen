#include <stdio.h>
#include <string.h>
#include "massiveMsg.h"

namespace MassiveMsg
{

//******************************************************************************
//******************************************************************************
//******************************************************************************
// ExampleMsg

ExampleMsg::ExampleMsg()
{
   mMessageType = MsgIdT::cExampleMsg;

   mCode1     = 0;
   mCode2     = 0;
   mCode3     = 0;
   mCode4     = 0;
   mCodeCode5 = 0;
   mWordsLoop = 4;
}

void ExampleMsg::copyToFrom (Ris::ByteBuffer* aBuffer)
{
   mHeader.headerCopyToFrom(aBuffer,this);

   aBuffer->copy ( &mCode1     );
   aBuffer->copy ( &mCode2     );
   aBuffer->copy ( &mCode3     );
   aBuffer->copy ( &mCode4     );
   aBuffer->copy ( &mCodeCode5 );

   aBuffer->copy ( &mWordsLoop );
   for (int i=0;  i<mWordsLoop; i++)
   {
   aBuffer->copy ( &mWords[i]  );
   }

   mHeader.headerReCopyToFrom(aBuffer,this);
}

//******************************************************************************
//******************************************************************************
//******************************************************************************
// TestMsg

TestMsg::TestMsg()
{
   mMessageType = MsgIdT::cTestMsg;

   mCode1 = 0;
   mCode2 = 0;
   mCode3 = 0;
   mCode4 = 0;
}

void TestMsg::copyToFrom (Ris::ByteBuffer* aBuffer)
{
   mHeader.headerCopyToFrom(aBuffer,this);

   aBuffer->copy ( &mCode1 );
   aBuffer->copy ( &mCode2 );
   aBuffer->copy ( &mCode3 );
   aBuffer->copy ( &mCode4 );

   mHeader.headerReCopyToFrom(aBuffer,this);
}

//******************************************************************************
//******************************************************************************
//******************************************************************************
// FirstMessageMsg

FirstMessageMsg::FirstMessageMsg()
{
   mMessageType = MsgIdT::cFirstMessageMsg;

   mCode1 = 0;
}

void FirstMessageMsg::copyToFrom (Ris::ByteBuffer* aBuffer)
{
   mHeader.headerCopyToFrom(aBuffer,this);

   aBuffer->copy ( &mCode1 );

   mHeader.headerReCopyToFrom(aBuffer,this);
}

//******************************************************************************
//******************************************************************************
//******************************************************************************
// StatusRequestMsg

StatusRequestMsg::StatusRequestMsg()
{
   mMessageType = MsgIdT::cStatusRequestMsg;

   mCode1 = 0;
   mCode2 = 0;
   mCode3 = 0;
   mCode4 = 0;
}

void StatusRequestMsg::copyToFrom (Ris::ByteBuffer* aBuffer)
{
   mHeader.headerCopyToFrom(aBuffer,this);

   aBuffer->copy ( &mCode1 );
   aBuffer->copy ( &mCode2 );
   aBuffer->copy ( &mCode3 );
   aBuffer->copy ( &mCode4 );

   mHeader.headerReCopyToFrom(aBuffer,this);
}

//******************************************************************************
//******************************************************************************
//******************************************************************************
// StatusResponseMsg

StatusResponseMsg::StatusResponseMsg()
{
   mMessageType = MsgIdT::cStatusResponseMsg;

   mCode1     = 0;
   mCode2     = 0;
   mCode3     = 0;
   mCode4     = 0;
   mWordsLoop = 4;
}

void StatusResponseMsg::copyToFrom (Ris::ByteBuffer* aBuffer)
{
   mHeader.headerCopyToFrom(aBuffer,this);

   aBuffer->copy ( &mCode1     );
   aBuffer->copy ( &mCode2     );
   aBuffer->copy ( &mCode3     );
   aBuffer->copy ( &mCode4     );

   aBuffer->copy ( &mWordsLoop );
   for (int i=0;  i<mWordsLoop; i++)
   {
   aBuffer->copy ( &mWords[i]  );
   }

   mHeader.headerReCopyToFrom(aBuffer,this);
}

//******************************************************************************
//******************************************************************************
//******************************************************************************
// DataRecord

DataRecord::DataRecord()
{
   mCode1 = 0;
   mCode2 = 0;
   mCode3 = 0;
   mCode4 = 0;
}

void DataRecord::copyToFrom (Ris::ByteBuffer* aBuffer)
{
   aBuffer->copy ( &mCode1 );
   aBuffer->copy ( &mCode2 );
   aBuffer->copy ( &mCode3 );
   aBuffer->copy ( &mCode4 );
}

//******************************************************************************
//******************************************************************************
//******************************************************************************
// DataMag

DataMag::DataMag()
{
   mMessageType = MsgIdT::cDataMag;

   mUChar      = 0;
   mUShort     = 0;
   mUInt       = 0;
   mUInt64     = 0;
   mChar       = 0;
   mShort      = 0;
   mInt        = 0;
   mInt64      = 0;
   mFloat      = 0.0f;
   mDouble     = 0.0;
   mBool       = false;
   mString1[0]=0;
   mString2[0]=0;
}

void DataMag::copyToFrom (Ris::ByteBuffer* aBuffer)
{
   mHeader.headerCopyToFrom(aBuffer,this);

   aBuffer->copy ( &mUChar      );
   aBuffer->copy ( &mUShort     );
   aBuffer->copy ( &mUInt       );
   aBuffer->copy ( &mUInt64     );
   aBuffer->copy ( &mChar       );
   aBuffer->copy ( &mShort      );
   aBuffer->copy ( &mInt        );
   aBuffer->copy ( &mInt64      );
   aBuffer->copy ( &mFloat      );
   aBuffer->copy ( &mDouble     );
   aBuffer->copy ( &mBool       );
   aBuffer->copyS(  mString1    );
   aBuffer->copyS(  mString2    );
   aBuffer->copy ( &mDataRecord );

   mHeader.headerReCopyToFrom(aBuffer,this);
}

//******************************************************************************
//******************************************************************************
//******************************************************************************
// Create a new message, based on a message type.

Ris::ByteContent* MsgCreator::createMsg (int aMessageType)
{
   BaseMsg* tMsg = 0;

   switch (aMessageType)
   {
      case MsgIdT::cExampleMsg :
         tMsg = new ExampleMsg();
         break;
      case MsgIdT::cTestMsg :
         tMsg = new TestMsg();
         break;
      case MsgIdT::cFirstMessageMsg :
         tMsg = new FirstMessageMsg();
         break;
      case MsgIdT::cStatusRequestMsg :
         tMsg = new StatusRequestMsg();
         break;
      case MsgIdT::cStatusResponseMsg :
         tMsg = new StatusResponseMsg();
         break;
      case MsgIdT::cDataMag :
         tMsg = new DataMag();
         break;
      default :
         break;
   }
   return tMsg;
}

}
