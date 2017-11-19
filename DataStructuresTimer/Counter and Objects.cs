using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace DataStructuresTimer
{
	//You can rename any verable/class/namespace/method by putting the cursor over it and double tapping ctrl+R twice, it will automoaticaly rename all referances to it. Its called refreactoring
    class timerArray
    {
		#region instatiation methods
		private static timerArray master; //Static referance the the master timerArray
		private pathNode pathScanner; //a referance to a path scanner, paths will be stored in the timerArray, and medication nodes 'inside' the paths
		private pathNode firstPath; //referance the the first path, bacially the place where the path scanner will reset to

		#region constuctors
		//#region and endRegion are used to creat areas of code that can be folded for netness
		public timerArray() //defult constructor
		{
			pathScanner = null;
			firstPath = null;
		}
		#endregion
		#region master
		public static timerArray Master()//this is a static method, it is called to get the master copy of timerArray
        {
				if(master == null)
				{
					master = new timerArray();
				}

				return master;
        }
		#endregion
#endregion

		public void addPath(String pathName)
		{
			if (firstPath == null)
			{
				firstPath = new pathNode(null, null, pathName);
			}

			else
			{
				pathScanner = firstPath;
				while(pathScanner.getNextPath() != null)
				{
					pathScanner = pathScanner.getNextPath();
				}
				pathScanner.setNextPath(new pathNode(null, pathScanner, pathName));
			}
		}

		public pathNode getPath(String pathName)
		{
			pathNode returnValue = null;
			if(firstPath != null)
			{
				pathScanner = firstPath;
				while(pathScanner != null)
				{
					if(pathScanner.getPathName().Equals(pathName))
					{
						returnValue = pathScanner;
					}
					pathScanner = pathScanner.getNextPath();
				}
			}

			return returnValue;
		}
    }

    class medication
    {
		#region veriables
		private String name; //name of the medication
        private String dosage; //dosage of the medicaton
        private double treatmentTime; //in minutes;
		private medication nextMed; //the next medication. There is a linked list of medication so it can be easly searched for
		private medication prevMed; //the previous medication
#endregion
		#region Constructors
		public medication()
        {

        }

        public medication(String nameX, String dosageX,  double treatmentTimeX)
        {
            name = nameX;
            dosage = dosageX;

            if(treatmentTimeX >= 0)
            {
                treatmentTime = treatmentTimeX;
            }
        }
#endregion
		#region Getters/Setters
		public String getMedicationName()
		{
			return name;
		}
		public void setMedicationName(String nameX)
		{
			name = nameX;
		}

		public String getDosage()
		{
			return dosage;
		}
		public void setDosage(String dosageX)
		{
			dosage = dosageX;
		}

		public double getTreatmentLength()
		{
			return treatmentTime;
		}
		public void setTreatmentLength(double treatmentTimeX)
		{
			if (treatmentTimeX >= 0)
			{
				treatmentTime = treatmentTimeX;
			}
		}

		public medication getNextMed()
		{
			return nextMed;
		}
		public void setNextMed(medication nextMedX)
		{
			nextMed = nextMedX;
		}

		public medication getPrevMed()
		{
			return prevMed;
		}
		public void setprevMed(medication prevMedX)
		{
			prevMed = prevMedX;
		}
#endregion

		public void addMedication(String medicationName, String medicaionDoes, double treatmentTime)
		{
			this.nextMed = new medication(medicationName, medicaionDoes, treatmentTime);
			this.nextMed.prevMed = this;
		}
		public void addMedication(medication medToAdd)
		{
			this.nextMed = medToAdd;
			this.nextMed.prevMed = this;
		}

		public string toString()
		{
			//will crash if called on null
			return ("Medication Name: " + name + ", Medication Does: " + dosage + ", Medication Treatment Time: " + treatmentTime + " Minutes.");
		}
    }

	class medicationControl
	{
		#region verables
		private static medicationControl master;
		private medication firstMed;
		private medication medScanner;
#endregion
		#region constructors
		public medicationControl()
		{

			firstMed = null;
			medScanner = null;

		}
		#endregion
		#region master
		public static medicationControl Master()
		{
			if (master == null)
			{
				master = new medicationControl();
			}

			return master;
		}
#endregion



		public void addMedication(String medicationNameX, String medicationDoesX, double medicationTreatmentLengthX)
		{
			if (firstMed != null)
			{
				medScanner = firstMed;
				while (medScanner.getNextMed() != null)
				{
					medScanner = medScanner.getNextMed();
				}

				medScanner.addMedication(medicationNameX, medicationDoesX, medicationTreatmentLengthX);
			}

			else
			{
				firstMed = new medication(medicationNameX, medicationDoesX, medicationTreatmentLengthX);
			}
		}

		public medication getMed(String medName)
		{
			medication returnValue = null;
			if(firstMed != null)
			{
				medScanner = firstMed;
				while(medScanner != null)
				{
					if(medScanner.getMedicationName().Equals(medName))
					{
						returnValue = medScanner;
					}
					medScanner = medScanner.getNextMed();
				}
			}

			return returnValue;
		}
	}


	class MedicationNode
	{
		#region verables
		private double treatmentTime;
		private MedicationNode nextMed;
		private MedicationNode prevMed;
	    private medication medication;
		private medicationControl meds;
		private TimeSpan runTime;
		private DateTime start;
		private TimeSpan timerRemaining;
#endregion
		#region Constructors
		public MedicationNode()
		{
			treatmentTime = 0;
			nextMed = null;
			prevMed = null;
			medication = null;
			meds = new medicationControl();
		}
		public MedicationNode(double treatmentTimeX, medication medicationX)
		{
			if(treatmentTimeX >= 0)
			{
				treatmentTime = treatmentTimeX;
			}
		
			medication = medicationX;
			meds = new medicationControl();
		}
		public MedicationNode(double treatmentTimeX, MedicationNode nextMedX, MedicationNode prevMedX, medication medicationX)
		{
			if (treatmentTimeX >= 0)
			{
				treatmentTime = treatmentTimeX;
			}

			if(nextMedX != null)
			{
				nextMed = nextMedX;
			}

			if (prevMedX != null)
			{
				prevMed = prevMedX;
			}
			if(medicationX != null)
			{
				medication = medicationX;
			}
			meds = new medicationControl();
		}
		#endregion
		#region getters/setters
		public double getTreatmentTime()
			{
				return treatmentTime;
			}
			public void setTreatmentTime(double treatmentTimeX)
			{
				treatmentTime = treatmentTimeX;
			}

			public MedicationNode getNextMed()
			{
				return nextMed;
			}
			public void setNextMed(MedicationNode nextMedX)
			{
				nextMed = nextMedX;
			}

			public MedicationNode getPrevMed()
			{
				return prevMed;
			}
			public void setPrevMed(MedicationNode prevMedX)
			{
				prevMed = prevMedX;
			}

			public medication getMedication()
			{
				return medication;
			}
			public void setMedication(medication medicationX)
			{
				medication = medicationX;
			}
			
		public TimeSpan getRunTime()
		{
			return runTime;
		}

		public DateTime getStart()
		{
			return start;
		}
		public void setStart(DateTime startX)
		{
			start = startX;
		}
		public TimeSpan getTimeRemaining()
		{
			return timerRemaining;
		}
		public void setTimeRemaing(TimeSpan timeRemainingX)
		{
			timerRemaining = timeRemainingX;
		}
#endregion

		public void addMedicationNode()
		{
			this.nextMed = new MedicationNode();
			this.nextMed.setPrevMed(this);
		}
		public void addMedicationNode(double treatmentTime, medication medication)
		{
			this.nextMed = new MedicationNode(treatmentTime, medication);
			this.nextMed.setPrevMed(this);
		}	
		public void addMedicationNode(MedicationNode medToAdd)
		{
			this.nextMed = medToAdd;
			this.nextMed.prevMed = this;
		}

		public void calculateEndTime()
		{
			start = DateTime.Now;
		    runTime = TimeSpan.FromMinutes(treatmentTime);
			Console.WriteLine("RunTime: " + runTime);
			timerRemaining = TimeSpan.FromSeconds(0);
		}

		public void removeNode()
		{
			if(this.nextMed != null)
			{
				this.nextMed.prevMed = this.prevMed;
			}

			if(this.prevMed != null)
			{
				this.prevMed.nextMed = this.nextMed;
			}


		}

	}

	class pathNode
	{
		pathNode nextPath;
		pathNode prevPath;
		MedicationNode nextMed;
		MedicationNode medScanner;
		String pathName;
		#region constructors
		public pathNode()
		{
			nextPath = null;
			prevPath = null;
			pathName = null;
			medScanner = null;
		}

		public pathNode(pathNode nextPathX, pathNode prevPathX, String pathNameX)
		{
			nextPath = nextPathX;
			prevPath = prevPathX;
			pathName = pathNameX;
			medScanner = null;
			
		}
		#endregion
		#region getters/setters
		public pathNode getNextPath()
		{
			return nextPath;
		}
		public void setNextPath(pathNode nextPathX)
		{
			nextPath = nextPathX;
		}

		public pathNode getPrevPath()
		{
			return prevPath;
		}
		public void setPrevPath(pathNode prevPathX)
		{
			prevPath = prevPathX;
		}

		public MedicationNode getNextMed()
		{
			return nextMed;
		}
		public void setNextMed(MedicationNode nextMedX)
		{
			nextMed = nextMedX;
		}

		public string getPathName()
		{
			return pathName;
		}
		public void setPathName(string pathNameX)
		{
			pathName = pathNameX;
		}
#endregion

		public void addMedication(double treatmentTime, medication medToAdd)
		{
			if (nextMed != null)
			{
				medScanner = nextMed;
				while (nextMed.getNextMed() != null)
				{
					medScanner = medScanner.getNextMed();
				}

				medScanner.addMedicationNode(treatmentTime, medToAdd);
			}

			else
			{
				nextMed = new MedicationNode(treatmentTime, medToAdd);
			}
		}
	}

	class counter
	{

		private MedicationNode firstTimer;
		private MedicationNode timerScanner;
		private static counter master;
		public counter()
		{
			Thread burner = new Thread(() => TimerCallback()); //Lambda thread?? I don't know I asked the internet and it works, leave off. I'll read more about it later.
			burner.Start();
			firstTimer = null;
			timerScanner = null;
		}

		public static counter Master()
		{
			if (master == null)
			{
				master = new counter();
			}

			return master;
		}

		public void addTimer(MedicationNode timerToAdd)
		{
			if(firstTimer == null)
			{
				firstTimer = timerToAdd;
				timerToAdd.calculateEndTime();
				timerToAdd.removeNode();
				firstTimer.setNextMed(null);
				firstTimer.setPrevMed(null);
			}
			else
			{
				timerScanner = firstTimer;
				while (timerScanner.getNextMed() != null)
				{
					timerScanner = timerScanner.getNextMed();
				}
				timerScanner.setNextMed(timerToAdd);
				timerToAdd.setNextMed(timerScanner);
			}
		}

		public void TimerCallback()
		{
			Console.WriteLine("running");
			do
			{
				if (firstTimer != null)
				{
					if (firstTimer.getNextMed() == null)
					{

						timerScanner = firstTimer;
						while (timerScanner != null)
						{
							TimeSpan Delta = DateTime.Now - timerScanner.getStart();
							timerScanner.setTimeRemaing(timerScanner.getRunTime() - Delta);
							Console.WriteLine(timerScanner.getTimeRemaining());
							if (timerScanner.getTimeRemaining().TotalSeconds <= 0)
							{
								Console.WriteLine("Timer " + timerScanner.getMedication().getMedicationName() + " Has Finished");
								if (timerScanner.getNextMed() != null)
								{
									timerScanner.getNextMed().setPrevMed(timerScanner.getPrevMed());
								}

								if (timerScanner.getPrevMed() != null)
								{
									timerScanner.getPrevMed().setNextMed(timerScanner.getNextMed());
								}

								firstTimer = null;

							}
							timerScanner = timerScanner.getNextMed();
						}
					}

					else
					{
						timerScanner = firstTimer;
						while (timerScanner != null)
						{
							TimeSpan Delta = DateTime.Now - timerScanner.getStart();
							timerScanner.setTimeRemaing(timerScanner.getRunTime() - Delta);
							Console.WriteLine(timerScanner.getTimeRemaining());
							if (timerScanner.getTimeRemaining().TotalSeconds <= 0)
							{
								Console.WriteLine("Timer " + timerScanner.getMedication().getMedicationName() + " Has Finished");
								if (timerScanner.getNextMed() != null)
								{
									timerScanner.getNextMed().setPrevMed(timerScanner.getPrevMed());
								}

								if (timerScanner.getPrevMed() != null)
								{
									timerScanner.getPrevMed().setNextMed(timerScanner.getNextMed());
								}

							}
							timerScanner = timerScanner.getNextMed();
						}

					}
				}
			} while (true);

		}

	}


}