#ifndef _MASSIVEMSG_H_
#define _MASSIVEMSG_H_

#include "risByteContent.h"
#include "risByteMsgMonkey.h"
#include "massiveMsgBase.h"

namespace MassiveMsg
{

    //***************************************************************************
    //***************************************************************************
    //***************************************************************************
    // Message Types

    class MsgIdT
    {
    public:

        static const int cUnspecified =   0;
        static const int cTestMsg     =   1;
        static const int cStatusMsg   =   2;
        static const int cData1Msg    =   3;
        static const int cData2Msg    =   4;
        static const int cData3Msg    =   5;
        static const int cData4Msg    =   6;
    };

    //***************************************************************************
    //***************************************************************************
    //***************************************************************************

    class TestMsg : public BaseMsg
    {
    public:

        //***********************************************************************
        // Members:

        int mCode1;
        int mCode2;
        int mCode3;
        int mCode4;

        //***********************************************************************
        // Methods:

        TestMsg();
        void copyToFrom (Ris::ByteBuffer* aBuffer);
    };

    //***************************************************************************
    //***************************************************************************
    //***************************************************************************

    class StatusMsg : public BaseMsg
    {
    public:

        //***********************************************************************
        // Members:

        int mCode1;
        int mCode2;
        int mCode3;
        int mCode4;

        //***********************************************************************
        // Methods:

        StatusMsg();
        void copyToFrom (Ris::ByteBuffer* aBuffer);
    };

    //***************************************************************************
    //***************************************************************************
    //***************************************************************************

    class Data1Msg : public BaseMsg
    {
    public:

        //***********************************************************************
        // Members:

        int mCode1;
        int mCode2;
        int mCode3;
        int mCode4;

        //***********************************************************************
        // Methods:

        Data1Msg();
        void copyToFrom (Ris::ByteBuffer* aBuffer);
    };

    //***************************************************************************
    //***************************************************************************
    //***************************************************************************

    class Data2Msg : public BaseMsg
    {
    public:

        //***********************************************************************
        // Members:

        int      mCode1;
        int      mCode2;
        int      mCode3;
        int      mCode4;
        Data1Msg mData1;

        //***********************************************************************
        // Methods:

        Data2Msg();
        void copyToFrom (Ris::ByteBuffer* aBuffer);
    };

    //***************************************************************************
    //***************************************************************************
    //***************************************************************************

    class Data3Msg : public BaseMsg
    {
    public:

        //***********************************************************************
        // Members:

        int    mCode1;
        int    mCode2;
        int    mCode3;
        int    mCode4;
        char   mString1 [40];
        int    mCode5Loop;
        int    mCode5 [4];

        //***********************************************************************
        // Methods:

        Data3Msg();
        void copyToFrom (Ris::ByteBuffer* aBuffer);
    };

    //***************************************************************************
    //***************************************************************************
    //***************************************************************************

    class Data4Msg : public BaseMsg
    {
    public:

        //***********************************************************************
        // Members:

        int        mCode1;
        int        mCode2;
        int        mCode3;
        int        mCode4;
        int        mData1Loop;
        Data1Msg   mData1 [4];

        //***********************************************************************
        // Methods:

        Data4Msg();
        void copyToFrom (Ris::ByteBuffer* aBuffer);
    };

    //***************************************************************************
    //***************************************************************************
    //***************************************************************************
    // Message Creator:

    class MsgCreator : public Ris::BaseMsgCreator
    {
    public:
        //***********************************************************************
        // Create a new message, based on a message type.

        Ris::ByteContent* createMsg (int aMessageType) override;
    };

}
#endif