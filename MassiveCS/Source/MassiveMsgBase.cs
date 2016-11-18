using System;
using System.Text;
using System.IO;

/*==============================================================================
This file contains a set of classes that encapsulate the message set
that is used to communicate with Intranet. The messages are specified
in the IDD.

There is a class for each particular message in the set and there is a
base class that all of the messages inherit from.

These messages follow the ByteContent pattern, where they all inherit
from ByteContent so that they can be copied to/from ByteBuffers.

The base class is used to specify set membership, any inheriting class
is a member of the message set, and message objects can be referenced
anonymously via pointers to the the base class.

The base class provides a member function, makeFromByteBuffer, that
extracts particular messages from a byte buffer and returns a pointer
to the base class.

These messages all have the same common form, they all contain a
common message header. The base class has a Header member object that
encapsulates the header.

==============================================================================*/

namespace MassiveMsg
{
    //******************************************************************************
    //******************************************************************************
    //******************************************************************************
    // Message Definitions

    public class MsgDefT
    {
        //******************************************************************************
        // Use this for a buffer size for these messages

        public const int cMsgBufferSize = 20000;
    };

    //******************************************************************************
    //******************************************************************************
    //******************************************************************************
    // This encapsualtes the message header.

    public class Header : Ris.ByteContent
    {
        public Header()
        {
            mSyncWord1         = 0x11111111;
            mSyncWord2         = 0x22222222;
            mMessageIdentifier = 0;
            mMessageLength     = 0;
            mSourceId          = 0;
            mDestinationId     = 0;

            mInitialPosition   = 0;
            mInitialLength     = 0;
        }

        //------------------------------------------------
        // Header Content

        public int   mSyncWord1;
        public int   mSyncWord2;
        public int   mMessageIdentifier;
        public int   mMessageLength;
        public int   mSourceId;
        public int   mDestinationId;

        // Header Content
        //------------------------------------------------

        // Header length
        public const int cLength = 24;

        //--------------------------------------------------------------------------
        // If the byte buffer is configured for put operations then this puts the
        // contents of the object into the byte buffer (it does a copy to, it
        // copies the object to the byte buffer).
        // If the byte buffer is configured for get operations then this gets the
        // contents of the object from the byte buffer (it does a copy from, it
        // copies the object from the byte buffer).
        // Copy To and Copy From are symmetrical.
        //--------------------------------------------------------------------------

        public override void copyToFrom (Ris.ByteBuffer aBuffer)
        {
            aBuffer.copy( ref mSyncWord1         );
            aBuffer.copy( ref mSyncWord2         );
            aBuffer.copy( ref mMessageIdentifier );
            aBuffer.copy( ref mMessageLength     );
            aBuffer.copy( ref mSourceId          );
            aBuffer.copy( ref mDestinationId     );
        }

        //--------------------------------------------------------------------------
        // For variable content messages, the message length cannot be known until
        // the entire message has been written to a byte buffer. Therefore, the 
        // message header cannot be written to a byte buffer until the entire
        // message has been written and the length is known.
        //
        // The procedure to write a message to a byte buffer is to skip over the 
        // buffer segment where the header is located, write the message payload
        // to the buffer, set the header message length based on the now known
        // payload length, and write the header to the buffer.
        //
        // These are called explicitly by inheriting messages at the
        // beginning and end of their copyToFrom's to manage the headers.
        // For "get" operations, headerCopyToFrom "gets" the header and
        // headerReCopyToFrom does nothing. For "put" operations,
        // headerCopyToFrom stores the buffer pointer and advances past where the
        // header will be written and headerReCopyToFrom "puts" the header at the
        // stored position. Both functions are passed a byte buffer pointer to
        // where the copy is to take place. Both are also passed a MessageContent
        // pointer to where they can get and mMessageType
        // which they transfer into and out of the headers.
        //--------------------------------------------------------------------------

        public void headerCopyToFrom (Ris.ByteBuffer aBuffer,BaseMsg aParent)
        {
            //---------------------------------------------------------------------
            // Instances of this class are members of parent message classes.
            // A call to this function should be the first line of code in a
            // containing parent message class's copyToFrom. It performs pre-copyToFrom
            // operations. It's purpose is to copy headers to/from byte buffers. The
            // corresponding function headerReCopyToFrom should be called as the last
            // line of code in the containing message class' copyToFrom. Lines of code
            // in between should copy individual data elements into/out of the buffer.

            //---------------------------------------------------------------------
            // for a "copy to" put
            //
            // If this is a "copy to" put operation then the header copy will actually
            // be done by the headerReCopyToFrom, after the rest of the message has been
            // copied into the buffer. This is because some of the fields in the header
            // cannot be set until after the rest of the message has been put into the
            // buffer. (You don't know the length of the message until you put all of
            // the data into it, you also can't compute a checksum). This call stores
            // the original buffer position that is passed to it when it is called for
            // later use by the headerReCopyToFrom. The original buffer position points
            // to where the header should be copied. This call then forward advances
            // the buffer to point past the header. Forward advancing the buffer
            // position to point just after where the header should be is the same as
            // doing a pretend copy of the header. After this pretend copy of the
            // header, the buffer position points to where the data should be put into
            // the buffer.
            //
            // Store the original buffer position for later use by the
            // headerReCopyToFrom and advance the buffer position forward
            // to point past the header.

            if (aBuffer.isCopyTo())
            {
                // Store the buffer parameters for later use by the
                // headerReCopyToFrom
                mInitialPosition = aBuffer.getPosition();
                mInitialLength   = aBuffer.getLength();

                // Advance the buffer position to point past the header.
                aBuffer.forward(Header.cLength);
            }

            //---------------------------------------------------------------------
            // for a "copy from" get
            //
            // If this is a "copy from" get operation then copy the header from the
            // buffer into the header member. Also set the message content type from
            // the variable datum id

            else
            {
                // Copy the buffer content into the header object.
                copyToFrom(aBuffer);
                // Set the message content type.
                aParent.mMessageType = mMessageIdentifier;
            }
        }

        public void headerReCopyToFrom  (Ris.ByteBuffer aBuffer,BaseMsg aParent)
        {
            // If this is a put operation then this actually copies the header into
            // the buffer.
            // This sets some header length parameters and copies the header into the
            // buffer position that was stored when headerCopyToFrom was called.

            if (aBuffer.isCopyTo())
            {
                // Store the buffer parameters for later use by the
                // headerReCopyToFrom
                int tFinalPosition = aBuffer.getPosition();
                int tFinalLength   = aBuffer.getLength();

                // Get message parameters from parent
                mMessageIdentifier = aParent.mMessageType;
                mMessageLength     = aBuffer.getLength();

                // Restore buffer parameters
                // to the initial position
                aBuffer.setPosition (mInitialPosition);
                aBuffer.setLength   (mInitialPosition);

                // Copy the adjusted header into the buffer'
                // at the original position
                copyToFrom( aBuffer );

                // Restore buffer parameters
                // to the final position
                aBuffer.setPosition (tFinalPosition);
                aBuffer.setLength   (tFinalPosition);
            }
            else
            {
            }
        }

        //---------------------------------------------------------------------------
        // These are set by headerCopyToFrom and used by headerReCopyToFrom,
        // for "put" operations.Theyt contain the buffer position and length of
        // where the headerReCopyToFrom will take place, which should be
        // where headerCopyToFrom was told to do the copy.

        public int mInitialPosition;
        public int mInitialLength;

        public void reset()
        {
            mSyncWord1         = 0;
            mSyncWord2         = 0;
            mMessageIdentifier = 0;
            mMessageLength     = 0;
            mSourceId          = 0;
            mDestinationId     = 0;

            mInitialPosition   = 0;
            mInitialLength     = 0;
        }

    };

    //******************************************************************************
    //******************************************************************************
    //******************************************************************************
    // Base message class. All particular messages for this message set inherit
    // from this class.

    public class BaseMsg : Ris.ByteContent
    {
        // Message type. 
        public int mMessageType;

        // Message Header 
        public Header mHeader;

        // Constructor
        public BaseMsg()
        {
            mMessageType = 0;
            mHeader=new Header();
        }
    };

    //******************************************************************************
    //******************************************************************************
    //******************************************************************************
    // This is the message monkey. It is used by code that receives messages into
    // byte buffers such that the message classes don't have to be visible to the
    // receiving code. Inheriting classes provide all of the details that are
    // needed by receiving code to receive and extract messages, as opposed to
    // having the message classes being visible to the receiving code. Likewise for
    // the transmitting code.

    public class MsgMonkey : Ris.BaseMsgMonkey
    {
        //--------------------------------------------------------------------------

        // Constructor
        public MsgMonkey()
        :  base(new MsgCreator())
        {
            mSourceId = 0;
        }

        // Configure members
        public void configure(int aSourceId)
        {
            mSourceId=aSourceId;
        }

        // Return a contant header length
        public override int getHeaderLength()
        {
            return Header.cLength;
        }

        // Return a contant max buffer size
        public override int getMaxBufferSize()
        {
            return MsgDefT.cMsgBufferSize;
        }

        //--------------------------------------------------------------------------
        // Extract message header parameters from a buffer and validate them

        public override bool extractMessageHeaderParms(Ris.ByteBuffer aBuffer)
        {
            // Extract header from buffer
            Header tHeader = new Header();
            tHeader.reset();
            aBuffer.getFromBuffer(tHeader);

            // Set header parameters
            mHeaderLength = Header.cLength; ;
            mMessageLength = tHeader.mMessageLength;
            mMessageType = tHeader.mMessageIdentifier;
            mPayloadLength = tHeader.mMessageLength - Header.cLength;

            // Test for error
            bool tError =
                tHeader.mSyncWord1 != 0x11111111 ||
                tHeader.mSyncWord2 != 0x22222222 ||
                tHeader.mMessageLength < Header.cLength  ||
                tHeader.mMessageLength > MsgDefT.cMsgBufferSize;

            // If no error then valid
            mHeaderValidFlag = !tError;

            // Return valid flag
            return mHeaderValidFlag;
        }

        //--------------------------------------------------------------------------
        // Preprocess a message before it is sent

        public override void processBeforeSend(Ris.ByteContent aMsg)
        {
            BaseMsg tMsg = (BaseMsg)aMsg;

            if (tMsg.mHeader.mSourceId==0)
            {
                tMsg.mHeader.mSourceId = mSourceId;
            }
        }

        //-------------------------------------------------------
        // Members

        public int mSourceId;
    };

    //******************************************************************************
    // This is a message parser creator. It defines a method that creates a new
    // message parser. It is used by transmitters and receivers to create new
    // instances of message parsers.

    public class MsgMonkeyCreator : Ris.BaseMsgMonkeyCreator
    {
        // Members
        int  mSourceId;

        // Constructor
        public MsgMonkeyCreator()
        {
           mSourceId = 0;
        }

        // Configure members
        public void configure(int aSourceId)
        {
           mSourceId = aSourceId;
        }

        // Create new message parser and configure it
        public override Ris.BaseMsgMonkey createNew()
        {
           // New message parser
           MsgMonkey tMonkey = new MsgMonkey();
           // Configure 
           tMonkey.configure(mSourceId);
           // Return base message parser pointer
           return (Ris.BaseMsgMonkey)tMonkey;
        }
    };
}
