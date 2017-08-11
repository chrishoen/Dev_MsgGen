using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ris;

namespace MainApp
{
    //**************************************************************************
    //**************************************************************************
    //**************************************************************************
    class MyCmdLineExec : BaseCmdLineExec
    {
        //**********************************************************************

        public MyCmdLineExec()
        {
            reset();
        }

        public override void reset()
        {
        }

        //**********************************************************************

        public override void execute(CmdLineCmd aCmd)
        {
            if (aCmd.isCmd("GO1")) OnGo1(aCmd);
            if (aCmd.isCmd("GO2")) OnGo2(aCmd);
            if (aCmd.isCmd("GO3")) OnGo3(aCmd);
            if (aCmd.isCmd("GO4")) OnGo4(aCmd);
            if (aCmd.isCmd("GO5")) OnGo5(aCmd);
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void OnGo1(CmdLineCmd aCmd)
        {
            aCmd.setArgDefault(1,1);
            int tN = aCmd.argInt(1);
            System.Random tRandom = new Random();

            for (int i = 0; i < tN; i++)
            {
                double tX = tRandom.NextDouble();
                Console.WriteLine("{0}  X = {1}", i,tX);
            }
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void OnGo2(CmdLineCmd aCmd)
        {
            aCmd.setArgDefault(1,1);
            aCmd.setArgDefault(1,10);

            int tN = aCmd.argInt(1);
            int tK = aCmd.argInt(2);
            System.Random tRandom = new Random();

            for (int i = 0; i < tN; i++)
            {
                int tX = tRandom.Next(tK);
                Console.WriteLine("{0}  X = {1}", i,tX);
            }
        }

        //**********************************************************************
        //**********************************************************************
        //**********************************************************************

        public void OnGo3(CmdLineCmd aCmd)
        {
            aCmd.setArgDefault(1, "aaaaaaa");

            Console.WriteLine("GO3{1,-10}GO3", aCmd.argString(1));
        }

        //**********************************************************************

        public void OnGo4(CmdLineCmd aCmd)
        {
            aCmd.setArgDefault(1, 101);

            UInt16 tN = aCmd.argUInt16(1);

            Console.WriteLine("{0}", tN);
        }

        //**********************************************************************

        public void OnGo5(CmdLineCmd aCmd)
        {
            aCmd.setArgDefault(1, 0x20);

            UInt64 tN = aCmd.argUInt64(1);

            Console.WriteLine("0x{0,16:X}", tN);
            
        }


    }
}
