#ifndef _MASSIVEMSG_H_
#define _MASSIVEMSG_H_

#include "risByteContent.h"
#include "risByteMsgMonkey.h"
#include "massiveMsgBase.h"

namespace MassiveMsg
{

//******************************************************************************
//******************************************************************************
//******************************************************************************
// Message Types

class MsgIdT
{
public:

    static const int cUnspecified =   0;
    static const int cExampleMsg        =   1;
    static const int cTestMsg           =   2;
    static const int cFirstMessageMsg   =   3;
    static const int cStatusRequestMsg  =   4;
    static const int cStatusResponseMsg =   5;
    static const int cDataMag           =   6;
};

//******************************************************************************
//******************************************************************************
//******************************************************************************
// Pre comment for ExampleMsg 1.
// Pre comment for ExampleMsg 2.

class ExampleMsg : public BaseMsg
{
public:

    //**************************************************************************
    // Members:

    // Pre comment for cSize1 1.
    // pre comment for cSize1 2.
    static const int cSize1     = 1001;    // Post comment 1
    static const int cSizeSize2 = 1002;    // Post comment 2

    // Pre comment for mCode1 1.
    // pre comment for mCode1 2.
    int mCode1;        // Post comment 1
    int mCode2;
    int mCode3;
    int mCode4;
    int mCodeCode5;    // Post Comment 5

    //**************************************************************************
    // Methods:

    ExampleMsg();
    void copyToFrom (Ris::ByteBuffer* aBuffer);
};

//******************************************************************************
//******************************************************************************
//******************************************************************************

class TestMsg : public BaseMsg
{
public:

    //**************************************************************************
    // Members:

    int mCode1;
    int mCode2;
    int mCode3;
    int mCode4;

    //**************************************************************************
    // Methods:

    TestMsg();
    void copyToFrom (Ris::ByteBuffer* aBuffer);
};

//******************************************************************************
//******************************************************************************
//******************************************************************************

class FirstMessageMsg : public BaseMsg
{
public:

    //**************************************************************************
    // Members:

    int mCode1;

    //**************************************************************************
    // Methods:

    FirstMessageMsg();
    void copyToFrom (Ris::ByteBuffer* aBuffer);
};

//******************************************************************************
//******************************************************************************
//******************************************************************************

class StatusRequestMsg : public BaseMsg
{
public:

    //**************************************************************************
    // Members:

    int mCode1;
    int mCode2;
    int mCode3;
    int mCode4;

    //**************************************************************************
    // Methods:

    StatusRequestMsg();
    void copyToFrom (Ris::ByteBuffer* aBuffer);
};

//******************************************************************************
//******************************************************************************
//******************************************************************************

class StatusResponseMsg : public BaseMsg
{
public:

    //**************************************************************************
    // Members:

    int   mCode1;
    int   mCode2;
    int   mCode3;
    int   mCode4;
    int   mWordsLoop;
    int   mWords [4];

    //**************************************************************************
    // Methods:

    StatusResponseMsg();
    void copyToFrom (Ris::ByteBuffer* aBuffer);
};

//******************************************************************************
//******************************************************************************
//******************************************************************************

class DataRecord : public Ris::ByteContent
{
public:

    //**************************************************************************
    // Members:

    int mCode1;
    int mCode2;
    int mCode3;
    int mCode4;

    //**************************************************************************
    // Methods:

    DataRecord();
    void copyToFrom (Ris::ByteBuffer* aBuffer);
};

//******************************************************************************
//******************************************************************************
//******************************************************************************

class DataMag : public BaseMsg
{
public:

    //**************************************************************************
    // Members:

    unsigned char      mUChar;
    unsigned short     mUShort;
    unsigned int       mUInt;
    unsigned long long mUInt64;
    char               mChar;
    short              mShort;
    int                mInt;
    long long          mInt64;
    float              mFloat;
    double             mDouble;
    bool               mBool;
    char               mString1 [100];
    char               mString2 [100];
    DataRecord         mDataRecord;

    //**************************************************************************
    // Methods:

    DataMag();
    void copyToFrom (Ris::ByteBuffer* aBuffer);
};

//******************************************************************************
//******************************************************************************
//******************************************************************************
// Message Creator:

class MsgCreator : public Ris::BaseMsgCreator
{
public:
    //**************************************************************************
    // Create a new message, based on a message type.

    Ris::ByteContent* createMsg (int aMessageType) override;
};

}
#endif
