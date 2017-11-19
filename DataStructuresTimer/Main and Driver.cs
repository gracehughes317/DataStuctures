using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DataStructuresTimer
{
	class Program
	{
		static void Main(string[] args)
		{

			timerArray paths = timerArray.Master();
			medicationControl meds = medicationControl.Master();
			counter timer = counter.Master();
			meds.addMedication("saline", "30 mg", .12);
			meds.addMedication("Pulmozym", "10 mg", 10);
			meds.addMedication("Vest", "Minisota Protoical", 30);
			paths.addPath("Path one");
			paths.getPath("Path one").addMedication(meds.getMed("saline").getTreatmentLength(), meds.getMed("saline"));
			paths.getPath("Path one").addMedication(meds.getMed("Pulmozym").getTreatmentLength(), meds.getMed("Pulmozym"));
			timer.addTimer(paths.getPath("Path one").getNextMed());
			bool exit = false;
			do
			{
				Console.WriteLine("press 2 to exit");
				ConsoleKeyInfo KI = Console.ReadKey();
				if(KI.KeyChar == '2')
				{
					exit = true;
				}


			} while (exit != true);
			Environment.Exit(0);
		}
	}
}
