using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MsgGen
{
    //**************************************************************************
    //**************************************************************************
    //**************************************************************************
    // Defs

    public class Defs
    {
        //----------------------------------------------------------------------
        // Members

        public const int cBlockT_Message =  1;
        public const int cBlockT_Record  =  2;
      
        public const int cMemberT_UChar   =  1;
        public const int cMemberT_UInt16  =  2;
        public const int cMemberT_UInt32  =  3;
        public const int cMemberT_UInt64  =  4;
        public const int cMemberT_Char    =  5;
        public const int cMemberT_Int16   =  6;
        public const int cMemberT_Int32   =  7;
        public const int cMemberT_Int64   =  8;
        public const int cMemberT_Bool    =  9;
        public const int cMemberT_Float   = 10;
        public const int cMemberT_Double  = 11;
        public const int cMemberT_String  = 12;
        public const int cMemberT_Record  = 13;
    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************
    // File data

    public class FileHeaderData
    {
        //----------------------------------------------------------------------
        // Members

        public String       mNameSpace;
        public List<String> mUsingList;
        public String       mDefineCH;
        public List<String> mIncludeCHList;
        public List<String> mIncludeCPList;
        public List<String> mPreCommentList;

        //----------------------------------------------------------------------
        // Constructor

        public FileHeaderData()
        {
            mNameSpace     = "namespace";
            mUsingList     = new List<string>();
            mDefineCH       = "definech";
            mIncludeCHList = new List<string>();
            mIncludeCPList = new List<string>();
        }

        //----------------------------------------------------------------------

        public void addPreCommentList(List<String> aPreCommentList)
        {
            if (aPreCommentList==null) return;
            if (aPreCommentList.Count==0) return;
            mPreCommentList = aPreCommentList;
        }
    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************
    // Member data

    public class MemberData
    {
        //----------------------------------------------------------------------
        // Members

        public int          mMemberType;
        public String       mName;
        public String       mTypeNameCP;
        public String       mTypeNameCS;
        public String       mInitialValue;
        public List<String> mPreCommentList;
        public String       mPostComment;

        public String       mArraySize;
        public bool         mIsArrayIndex;
        public bool         mIsArray;

        public MemberData (int aVarType)
        {
            mMemberType = aVarType;

            switch (aVarType)
            {
                case Defs.cMemberT_UChar:
                    {
                        mTypeNameCP = "unsigned char";
                        mTypeNameCS = "byte";
                        mInitialValue = "0";
                    }
                    break;
                case Defs.cMemberT_UInt16:
                    {
                        mTypeNameCP = "unsigned short";
                        mTypeNameCS = "ushort";
                        mInitialValue = "0";
                    }
                    break;
                case Defs.cMemberT_UInt32:
                    {
                        mTypeNameCP = "unsigned int";
                        mTypeNameCS = "uint";
                        mInitialValue = "0";
                    }
                    break;
                case Defs.cMemberT_UInt64:
                    {
                        mTypeNameCP = "unsigned long long";
                        mTypeNameCS = "ulong";
                        mInitialValue = "0";
                    }
                    break;
                case Defs.cMemberT_Char:
                    {
                        mTypeNameCP = "char";
                        mTypeNameCS = "sbyte";
                        mInitialValue = "0";
                    }
                    break;
                case Defs.cMemberT_Int16:
                    {
                        mTypeNameCP = "short";
                        mTypeNameCS = "short";
                        mInitialValue = "0";
                    }
                    break;
                case Defs.cMemberT_Int32:
                    {
                        mTypeNameCP = "int";
                        mTypeNameCS = "int";
                        mInitialValue = "0";
                    }
                    break;
                case Defs.cMemberT_Int64:
                    {
                        mTypeNameCP = "long long";
                        mTypeNameCS = "long";
                        mInitialValue = "0";
                    }
                    break;
                case Defs.cMemberT_Bool:
                    {
                        mTypeNameCP = "bool";
                        mTypeNameCS = "bool";
                        mInitialValue = "false";
                    }
                    break;
                case Defs.cMemberT_Float:
                    {
                        mTypeNameCP = "float";
                        mTypeNameCS = "float";
                        mInitialValue = "0.0f";
                    }
                    break;
                case Defs.cMemberT_Double:
                    {
                        mTypeNameCP = "double";
                        mTypeNameCS = "double";
                        mInitialValue = "0.0";
                    }
                    break;
                case Defs.cMemberT_String:
                    {
                        mTypeNameCP = "string";
                        mTypeNameCS = "string";
                        mInitialValue = "String.Empty";
                    }
                    break;
                case Defs.cMemberT_Record:
                    {
                        mTypeNameCP = String.Empty;
                        mInitialValue = String.Empty;
                    }
                    break;
            }
        }

        //----------------------------------------------------------------------

        public void addPreCommentList(List<String> aPreCommentList)
        {
            if (aPreCommentList==null) return;
            if (aPreCommentList.Count==0) return;
            mPreCommentList = aPreCommentList;
        }

        //----------------------------------------------------------------------

        public void addPostComment(String aPostComment)
        {
            if (String.IsNullOrEmpty(aPostComment)) return;
            mPostComment = aPostComment;
        }

    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************
    // Const data

    public class ConstData
    {
        //----------------------------------------------------------------------
        // Members

        public String       mName;
        public String       mInitialValue;
        public List<String> mPreCommentList;
        public String       mPostComment;

        //----------------------------------------------------------------------
        // Constructor

        public ConstData()
        {
            mName = "name";
            mInitialValue = "initial";
        }

        //----------------------------------------------------------------------

        public void addPreCommentList(List<String> aPreCommentList)
        {
            if (aPreCommentList==null) return;
            if (aPreCommentList.Count==0) return;
            mPreCommentList = aPreCommentList;
        }

        //----------------------------------------------------------------------

        public void addPostComment(String aPostComment)
        {
            if (String.IsNullOrEmpty(aPostComment)) return;
            mPostComment = aPostComment;
        }

    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************
    // Block data

    public class BlockData
    {
        //----------------------------------------------------------------------
        // Members

        public int              mBlockType;
        public String           mName;
        public List<ConstData>  mConstList;
        public List<MemberData> mMemberList;
        public List<String>     mPreCommentList;
        public int              mConstMaxSize;
        public int              mNameMaxSize;
        public int              mTypeMaxSize;

        //----------------------------------------------------------------------
        // Constructor

        public BlockData()
        {
            mBlockType    = 0;
            mName         = "name";

            mConstList    = new List<ConstData>();
            mMemberList   = new List<MemberData>();

            mConstMaxSize = 0;
            mNameMaxSize  = 0;
            mTypeMaxSize  = 0;
        }

        //----------------------------------------------------------------------

        public void addConst(ConstData aConst)
        {
            mConstList.Add(aConst);

            if (aConst.mName.Length > mConstMaxSize)
            {
                mConstMaxSize = aConst.mName.Length;
            }
        }

        //----------------------------------------------------------------------

        public void addMember(MemberData aMember)
        {
            mMemberList.Add(aMember);

            if (aMember.mName.Length > mNameMaxSize)
            {
                mNameMaxSize = aMember.mName.Length;
            }

            if (aMember.mIsArray == false)
            {
                if (aMember.mTypeNameCP.Length > mTypeMaxSize)
                {
                    mTypeMaxSize = aMember.mTypeNameCP.Length;
                }
            }
            else
            {
                if (aMember.mTypeNameCP.Length + 2> mTypeMaxSize)
                {
                    mTypeMaxSize = aMember.mTypeNameCP.Length + 2;
                }
            }
        }

        //----------------------------------------------------------------------

        public void addPreCommentList(List<String> aPreCommentList)
        {
            if (aPreCommentList==null) return;
            if (aPreCommentList.Count==0) return;
            mPreCommentList = aPreCommentList;
        }

    };

    //**************************************************************************
    //**************************************************************************
    //**************************************************************************
    // File data 

    public class FileData
    {
        //----------------------------------------------------------------------
        // Members

        public FileHeaderData   mFileHeaderData;
        public List<BlockData>  mBlockList;
        public int              mNameMaxSize;

        //----------------------------------------------------------------------
        // Constructor

        public FileData()
        {
            mFileHeaderData  = new FileHeaderData();
            mBlockList   = new List<BlockData>();
            mNameMaxSize = 11;
        }

        //----------------------------------------------------------------------

        public void addBlock(BlockData aBlock)
        {
            mBlockList.Add(aBlock);

            if (aBlock.mName.Length > mNameMaxSize)
            {
                mNameMaxSize = aBlock.mName.Length;
            }
        }
    };
    //**************************************************************************
    //**************************************************************************
    //**************************************************************************
    // Output data 

    public class OutputData
    {
        //----------------------------------------------------------------------
        // Members

        public String mWriteFilePathCSMessage;
        public String mWriteFilePathCSBody;
        public String mWriteFilePathCH;
        public String mWriteFilePathCP;
    };
}
