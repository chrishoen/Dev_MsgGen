#include <stdio.h>
#include <string.h>
#include "massiveMsg.h"

namespace MassiveMsg
{

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

   mCode1      = 0;
   mCode2      = 0;
   mCode3      = 0;
   mCode4      = 0;
}

void DataMag::copyToFrom (Ris::ByteBuffer* aBuffer)
{
   mHeader.headerCopyToFrom(aBuffer,this);

   aBuffer->copy ( &mCode1      );
   aBuffer->copy ( &mCode2      );
   aBuffer->copy ( &mCode3      );
   aBuffer->copy ( &mCode4      );
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
