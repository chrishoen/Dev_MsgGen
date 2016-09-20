#include <math.h>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>

#include "prnPrint.h"
#include "CmdLineExec.h"

using namespace std;

// change201
//******************************************************************************
CmdLineExec::CmdLineExec()
{
}
//******************************************************************************
void CmdLineExec::reset()
{
}
//******************************************************************************
void CmdLineExec::execute(Ris::CmdLineCmd* aCmd)
{
   if(aCmd->isCmd("RESET"  ))  reset();
   if(aCmd->isCmd("GO1"    ))  executeGo1(aCmd);
   if(aCmd->isCmd("GO2"    ))  executeGo2(aCmd);
   if(aCmd->isCmd("GO3"    ))  executeGo3(aCmd);
   if(aCmd->isCmd("GO4"    ))  executeGo4(aCmd);
   if(aCmd->isCmd("GO5"    ))  executeGo5(aCmd);
}

//******************************************************************************
//******************************************************************************
//******************************************************************************

//******************************************************************************
//******************************************************************************
//******************************************************************************

void CmdLineExec::executeGo1(Ris::CmdLineCmd* aCmd)
{
   aCmd->setArgDefault(1,0.00);

   double tX = aCmd->argDouble(1);

   Prn::print(0, "012345678901234567");
   Prn::print(0, "%+08.3f",tX);
}

//******************************************************************************
//******************************************************************************
//******************************************************************************

void CmdLineExec::executeGo2(Ris::CmdLineCmd* aCmd)
{
   aCmd->setArgDefault(1,101.123);
   aCmd->setArgDefault(2,-88.99);

   double tX1 = aCmd->argDouble(1);
   double tY1 = aCmd->argDouble(2);

   char tString[100];
   strcpy(tString, "123.456");

   Prn::print(0, "XY1_%s",tString);

   double tX2=0.0;
   double tY2=0.0;

   sscanf(tString, "%lf",&tX2);

   Prn::print(0, "XY2 %8.4f %8.4f",tX2,tY2);
}

//******************************************************************************

void CmdLineExec::executeGo3(Ris::CmdLineCmd* aCmd)
{
   aCmd->setArgDefault(1,101.123);
   aCmd->setArgDefault(2,-88.99);

   double tX1 = aCmd->argDouble(1);
   double tY1 = aCmd->argDouble(2);

   char tString[100];
   sprintf(tString, "%+08.3lf,%+08.3lf\r",tX1,tY1);

   Prn::print(0, "XY1_%s",tString);

   double tX2=0.0;
   double tY2=0.0;

   sscanf(tString, "%lf,%lf",&tX2,&tY2);

   Prn::print(0, "XY2 %8.4f %8.4f",tX2,tY2);
}

//******************************************************************************

void CmdLineExec::executeGo4(Ris::CmdLineCmd* aCmd)
{
   aCmd->setArgDefault(1,true);

   bool tX = aCmd->argBool(1);

   Prn::print(0, "X %d",tX);

   tX ^= true;

   Prn::print(0, "X %d",tX);
}

//******************************************************************************

void CmdLineExec::executeGo5(Ris::CmdLineCmd* aCmd)
{
}

