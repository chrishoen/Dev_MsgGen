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

        public const int cMemberT_Byte    =  1;
        public const int cMemberT_SByte   =  2;
        public const int cMemberT_Int16   =  3;
        public const int cMemberT_UInt16  =  4;
        public const int cMemberT_Int32   =  5;
        public const int cMemberT_UInt32  =  6;
        public const int cMemberT_Int64   =  7;
        public const int cMemberT_UInt64  =  8;
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
        public String       mTypeName;
        public String       mInitialValue;

        public String       mArraySize;
        public bool         mIsArrayIndex;
        public bool         mIsArray;

        public MemberData (int aVarType)
        {
            mMemberType = aVarType;

            switch (aVarType)
            {
                case Defs.cMemberT_Byte:
                    {
                        mTypeName = "byte";
                        mInitialValue = "0";
                    }
                    break;
                case Defs.cMemberT_SByte:
                    {
                        mTypeName = "sbyte";
                        mInitialValue = "0";
                    }
                    break;
                case Defs.cMemberT_Int16:
                    {
                        mTypeName = "short";
                        mInitialValue = "0";
                    }
                    break;
                case Defs.cMemberT_UInt16:
                    {
                        mTypeName = "ushort";
                        mInitialValue = "0";
                    }
                    break;
                case Defs.cMemberT_Int32:
                    {
                        mTypeName = "int";
                        mInitialValue = "0";
                    }
                    break;
                case Defs.cMemberT_UInt32:
                    {
                        mTypeName = "uint";
                        mInitialValue = "0";
                    }
                    break;
                case Defs.cMemberT_Int64:
                    {
                        mTypeName = "long";
                        mInitialValue = "0";
                    }
                    break;
                case Defs.cMemberT_UInt64:
                    {
                        mTypeName = "ulong";
                        mInitialValue = "0";
                    }
                    break;
                case Defs.cMemberT_Bool:
                    {
                        mTypeName = "bool";
                        mInitialValue = "false";
                    }
                    break;
                case Defs.cMemberT_Float:
                    {
                        mTypeName = "float";
                        mInitialValue = "0.0f";
                    }
                    break;
                case Defs.cMemberT_Double:
                    {
                        mTypeName = "double";
                        mInitialValue = "0.0";
                    }
                    break;
                case Defs.cMemberT_String:
                    {
                        mTypeName = "string";
                        mInitialValue = "String.Empty";
                    }
                    break;
                case Defs.cMemberT_Record:
                    {
                        mTypeName = String.Empty;
                        mInitialValue = String.Empty;
                    }
                    break;
            }
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

        //----------------------------------------------------------------------
        // Constructor

        public ConstData()
        {
            mName = "name";
            mInitialValue = "initial";
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
                if (aMember.mTypeName.Length > mTypeMaxSize)
                {
                    mTypeMaxSize = aMember.mTypeName.Length;
                }
            }
            else
            {
                if (aMember.mTypeName.Length + 2> mTypeMaxSize)
                {
                    mTypeMaxSize = aMember.mTypeName.Length + 2;
                }
            }

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
}
