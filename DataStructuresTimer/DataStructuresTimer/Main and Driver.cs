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
			bool exit = false;
			char option = '1';

			/*
			 * This code is not bullet proff
			 * If you imput bad data it will error out
			 * This is also not the end all be all of the program there is functionality that I was not able to add.
			 * read comments scattered around the main method and in the methods for more info
			 */
			do
			{
				Console.WriteLine("Please select an option from the list bellow");
				Console.WriteLine("1: Add a Path");
				Console.WriteLine("2: Add a Medication");
				Console.WriteLine("3: Add a Medication to and existing path");
				Console.WriteLine("4: Start a Timer");
				Console.WriteLine("5: Exit");
				option = Console.ReadKey().KeyChar;
				switch (option)
				{
					case '1':
						{
							Console.WriteLine("what would you like the name of this path to be?");
							String pathName = Console.ReadLine();
							paths.addPath(pathName);
							break;
						}

					case '2':
						{
							Console.WriteLine("Please enter the name of the medication you wish to add");
							String medName = Console.ReadLine();
							Console.WriteLine("Please enter the doasage of the medication, including the unit");
							String medDoes = Console.ReadLine();
							Console.WriteLine("Please enter how long the medication takes to administer in minutes");
							int medTreatent = Int32.Parse(Console.ReadLine());//this will error the code out FYI
							meds.addMedication(medName, medDoes, medTreatent);
							break;
						}

					case '3':
						{
							Console.WriteLine("What is the name of the path you wish the add the medication to?");
							String pathName = Console.ReadLine();
							Console.WriteLine("What is the name of the medication you wish to add");
							String medName = Console.ReadLine();
							paths.getPath(pathName).addMedication(meds.getMed(medName).getTreatmentLength(), meds.getMed(medName));

							break;
						}

					case '4':
						{
							Console.WriteLine("What is the name of the path you would liek to start the first timer from");
							String pathName = Console.ReadLine();
							timer.addTimer(paths.getPath(pathName).getNextMedRemove());//this will also break the code

							break;
						}

					case '5':
						{
							Console.WriteLine("Are you sure you would like to exit. Nothing will be save");
							Console.WriteLine("Y/N");
							char exitChoice = Console.ReadKey().KeyChar;
							if(exitChoice == 'y' || exitChoice == 'Y')
							{
								Console.WriteLine("Exiting program");
								exit = true;
							}

							else
							{
								Console.WriteLine("Resuming program");
							}
							break;
						}

					default:
						{
							Console.WriteLine("That is not a valid imput");
							break;
						}


				}
				
			} while (exit == false);






			meds.addMedication("saline", "30 mg", .12);
			meds.addMedication("Pulmozym", "10 mg", .2);
			meds.addMedication("Vest", "Minisota Protoical", 30);
			paths.addPath("Path one");
			paths.getPath("Path one").addMedication(meds.getMed("saline").getTreatmentLength(), meds.getMed("saline"));
			paths.getPath("Path one").addMedication(meds.getMed("Pulmozym").getTreatmentLength(), meds.getMed("Pulmozym"));
			timer.addTimer(paths.getPath("Path one").getNextMedRemove());
			timer.addTimer(paths.getPath("Path one").getNextMedRemove());
		}
	}
}
