#include <stdio.h>
#include <string.h>
#include "massiveMsg.h"

namespace MassiveMsg
{

   //****************************************************************************
   //****************************************************************************
   //****************************************************************************
   // Message Creator

   Ris::ByteMsgB* MsgBCopier::createMessage(int aMessageType)
   {
      Ris::ByteMsgB* tMsg = 0;

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
   // Message Copy

   void MsgBCopier::copyToFrom( Ris::ByteBuffer* aBuffer, Ris::ByteMsgB* aMsg)
   {
      switch (aMsg->mMessageType)
      {
         case MsgIdT::cTestMsg :
               copyToFrom(aBuffer, (TestMsg*)aMsg);
            break;
         case MsgIdT::cStatusMsg :
               copyToFrom(aBuffer, (StatusMsg*)aMsg);
            break;
         case MsgIdT::cData1Msg :
               copyToFrom(aBuffer, (Data1Msg*)aMsg);
            break;
         case MsgIdT::cData2Msg :
               copyToFrom(aBuffer, (Data2Msg*)aMsg);
            break;
         case MsgIdT::cData3Msg :
               copyToFrom(aBuffer, (Data3Msg*)aMsg);
            break;
         case MsgIdT::cData4Msg :
               copyToFrom(aBuffer, (Data4Msg*)aMsg);
            break;
         default :
            break;
      }
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

   void MsgBCopier::copyToFrom (Ris::ByteBuffer* aBuffer, TestMsg* aMsg)
   {
      aBuffer->copy ( &aMsg->mCode1 );
      aBuffer->copy ( &aMsg->mCode2 );
      aBuffer->copy ( &aMsg->mCode3 );
      aBuffer->copy ( &aMsg->mCode4 );
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

   void MsgBCopier::copyToFrom (Ris::ByteBuffer* aBuffer, StatusMsg* aMsg)
   {
      aBuffer->copy ( &aMsg->mCode1 );
      aBuffer->copy ( &aMsg->mCode2 );
      aBuffer->copy ( &aMsg->mCode3 );
      aBuffer->copy ( &aMsg->mCode4 );
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

   void MsgBCopier::copyToFrom (Ris::ByteBuffer* aBuffer, Data1Msg* aMsg)
   {
      aBuffer->copy ( &aMsg->mCode1 );
      aBuffer->copy ( &aMsg->mCode2 );
      aBuffer->copy ( &aMsg->mCode3 );
      aBuffer->copy ( &aMsg->mCode4 );
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

   void MsgBCopier::copyToFrom (Ris::ByteBuffer* aBuffer, Data2Msg* aMsg)
   {
      aBuffer->copy ( &aMsg->mCode1 );
      aBuffer->copy ( &aMsg->mCode2 );
      aBuffer->copy ( &aMsg->mCode3 );
      aBuffer->copy ( &aMsg->mCode4 );
      MsgBCopier::copyToFrom (aBuffer, &aMsg->mData1 );
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

   void MsgBCopier::copyToFrom (Ris::ByteBuffer* aBuffer, Data3Msg* aMsg)
   {
      aBuffer->copy ( &aMsg->mCode1     );
      aBuffer->copy ( &aMsg->mCode2     );
      aBuffer->copy ( &aMsg->mCode3     );
      aBuffer->copy ( &aMsg->mCode4     );
      aBuffer->copyS(  aMsg->mString1   );

      aBuffer->copy ( &aMsg->mCode5Loop );
      for (int i=0;  i<aMsg->mCode5Loop; i++)
      {
      aBuffer->copy ( &aMsg->mCode5[i]  );
      }

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

   void MsgBCopier::copyToFrom (Ris::ByteBuffer* aBuffer, Data4Msg* aMsg)
   {
      aBuffer->copy ( &aMsg->mCode1     );
      aBuffer->copy ( &aMsg->mCode2     );
      aBuffer->copy ( &aMsg->mCode3     );
      aBuffer->copy ( &aMsg->mCode4     );

      aBuffer->copy ( &aMsg->mData1Loop );
      for (int i=0;  i<aMsg->mData1Loop; i++)
      {
      MsgBCopier::copyToFrom (aBuffer, &aMsg->mData1[i]);
      }

   }

}
