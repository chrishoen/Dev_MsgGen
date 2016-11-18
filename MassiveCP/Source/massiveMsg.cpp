#include <stdio.h>
#include <string.h>
#include "massiveMsg.h"

namespace MassiveMsg
{

   //****************************************************************************
   //****************************************************************************
   //****************************************************************************
   // Message Creator

   BaseMsg* MessageCreator::createMessage(int aMessageType)
   {
      BaseMsg* tMsg = 0;

      switch (aMessageType)
      {
         case MsgIdT::cTestMsg :
            tMsg = new TestMsg();
            break;
         case MsgIdT::cStatusMsg :
            tMsg = new StatusMsg();
            break;
         case MsgIdT::cData1Msg :
            tMsg = new Data1Msg();
            break;
         case MsgIdT::cData2Msg :
            tMsg = new Data2Msg();
            break;
         case MsgIdT::cData3Msg :
            tMsg = new Data3Msg();
            break;
         case MsgIdT::cData4Msg :
            tMsg = new Data4Msg();
            break;
         default :
            break;
      }
      return tMsg;
   }

   //****************************************************************************
   //****************************************************************************
   //****************************************************************************
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

   //****************************************************************************
   //****************************************************************************
   //****************************************************************************
   // StatusMsg

   StatusMsg::StatusMsg()
   {
      mMessageType = MsgIdT::cStatusMsg;

      mCode1 = 0;
      mCode2 = 0;
      mCode3 = 0;
      mCode4 = 0;
   }

   void StatusMsg::copyToFrom (Ris::ByteBuffer* aBuffer)
   {
      mHeader.headerCopyToFrom(aBuffer,this);

      aBuffer->copy ( &mCode1 );
      aBuffer->copy ( &mCode2 );
      aBuffer->copy ( &mCode3 );
      aBuffer->copy ( &mCode4 );

      mHeader.headerReCopyToFrom(aBuffer,this);
   }

   //****************************************************************************
   //****************************************************************************
   //****************************************************************************
   // Data1Msg

   Data1Msg::Data1Msg()
   {
      mMessageType = MsgIdT::cData1Msg;

      mCode1 = 0;
      mCode2 = 0;
      mCode3 = 0;
      mCode4 = 0;
   }

   void Data1Msg::copyToFrom (Ris::ByteBuffer* aBuffer)
   {
      mHeader.headerCopyToFrom(aBuffer,this);

      aBuffer->copy ( &mCode1 );
      aBuffer->copy ( &mCode2 );
      aBuffer->copy ( &mCode3 );
      aBuffer->copy ( &mCode4 );

      mHeader.headerReCopyToFrom(aBuffer,this);
   }

   //****************************************************************************
   //****************************************************************************
   //****************************************************************************
   // Data2Msg

   Data2Msg::Data2Msg()
   {
      mMessageType = MsgIdT::cData2Msg;

      mCode1 = 0;
      mCode2 = 0;
      mCode3 = 0;
      mCode4 = 0;
   }

   void Data2Msg::copyToFrom (Ris::ByteBuffer* aBuffer)
   {
      mHeader.headerCopyToFrom(aBuffer,this);

      aBuffer->copy ( &mCode1 );
      aBuffer->copy ( &mCode2 );
      aBuffer->copy ( &mCode3 );
      aBuffer->copy ( &mCode4 );
      aBuffer->copy ( &mData1 );

      mHeader.headerReCopyToFrom(aBuffer,this);
   }

   //****************************************************************************
   //****************************************************************************
   //****************************************************************************
   // Data3Msg

   Data3Msg::Data3Msg()
   {
      mMessageType = MsgIdT::cData3Msg;

      mCode1     = 0;
      mCode2     = 0;
      mCode3     = 0;
      mCode4     = 0;
      mString1[0]=0;
      mCode5Loop = 4;
   }

   void Data3Msg::copyToFrom (Ris::ByteBuffer* aBuffer)
   {
      mHeader.headerCopyToFrom(aBuffer,this);

      aBuffer->copy ( &mCode1     );
      aBuffer->copy ( &mCode2     );
      aBuffer->copy ( &mCode3     );
      aBuffer->copy ( &mCode4     );
      aBuffer->copyS(  mString1   );

      aBuffer->copy ( &mCode5Loop );
      for (int i=0;  i<mCode5Loop; i++)
      {
      aBuffer->copy ( &mCode5[i]  );
      }

      mHeader.headerReCopyToFrom(aBuffer,this);
   }

   //****************************************************************************
   //****************************************************************************
   //****************************************************************************
   // Data4Msg

   Data4Msg::Data4Msg()
   {
      mMessageType = MsgIdT::cData4Msg;

      mCode1     = 0;
      mCode2     = 0;
      mCode3     = 0;
      mCode4     = 0;
      mData1Loop = 4;
   }

   void Data4Msg::copyToFrom (Ris::ByteBuffer* aBuffer)
   {
      mHeader.headerCopyToFrom(aBuffer,this);

      aBuffer->copy ( &mCode1     );
      aBuffer->copy ( &mCode2     );
      aBuffer->copy ( &mCode3     );
      aBuffer->copy ( &mCode4     );

      aBuffer->copy ( &mData1Loop );
      for (int i=0;  i<mData1Loop; i++)
      {
      aBuffer->copy ( &mData1[i]  );
      }

      mHeader.headerReCopyToFrom(aBuffer,this);
   }

}
