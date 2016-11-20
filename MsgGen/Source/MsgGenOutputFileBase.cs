using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MsgGen
{
    //**************************************************************************
    //**************************************************************************
    //**************************************************************************

    public abstract class OutputFileBase
    {

        public abstract bool open (String aFilePath);
        public abstract void close ();
        public abstract void write(InputData aInputData);
    };

}//namespace
