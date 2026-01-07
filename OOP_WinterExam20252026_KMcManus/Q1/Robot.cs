using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//https://github.com/JackWilliamDonaghy-Dev/OOPExam2026

namespace Q1
{

    public enum HouseholdSkill
    {
        Cooking,
        Cleaning,
        Laundry,
        Gardening,
        ChildCare
    }

    public enum DeliveryMode
    {
        Walking,
        Driving,
        Flying
    }


    abstract class Robot
    {
        public string RobotName { get; set; }
        public double PowerCapacityKWH { get; set; }
        public double CurrentPowerKWH { get; set; }

        public double GetBatteryPercentage()
        {
            if (PowerCapacityKWH == 0) return 0;
            return (CurrentPowerKWH / PowerCapacityKWH) * 100;
        }

        public string DisplayBatteryInformation()
        {
            return $"Battery Information\nCapacity: {PowerCapacityKWH}kWh\nCurrent Power:{CurrentPowerKWH}kWh\nBattery Level: {GetBatteryPercentage():F2}%";
        }

        public abstract string DescribeRobot();

        public string RobotInfo()
        {
            return $"{DescribeRobot()}";
        }


        public Robot(string robotName, double powerCapacityKWH, double currentPowerKWH)
        {
            RobotName = robotName;
            PowerCapacityKWH = powerCapacityKWH;
            CurrentPowerKWH = currentPowerKWH;
        }

        public Robot(string robotName)
        {
            RobotName = robotName;
            PowerCapacityKWH = 100.0;
        }

        public Robot()
        {
            RobotName = "unknown";
        }
    }

    class HouseholdRobot : Robot
    {
        private List<HouseholdSkill> Skills { get; set; } = new List<HouseholdSkill>();
        public override string DescribeRobot()
        {
            string skillsDescription = Skills.Count > 0 ? string.Join(", ", Skills) : "No skills";
            return $"I am a household robot.\nI can help with chores around the house.\n\nHousehold Robot Skills:\n{skillsDescription}\n\n{DisplayBatteryInformation()}";
        }

        public void DownloadSkill(HouseholdSkill skill)
        {
            if (!Skills.Contains(skill))
            {
                Skills.Add(skill);
            }
        }
        public override string ToString()
        {
            return $"{RobotName} - [HouseholdRobot]";
        }

        public HouseholdRobot(string robotName) : base(robotName)
        {
            RobotName = robotName;
            DownloadSkill(HouseholdSkill.Cleaning);
        }

    }

    class DeliveryRobot : Robot
    {
        public DeliveryMode ModeOfDelivery { get; set; }
        public double MaxLoadKG { get; set; }

        public override string DescribeRobot()
        {
            return $"I am a delivery robot.\nI specialise in delivery by {ModeOfDelivery}\n\nThe maximum load I can carry is {MaxLoadKG:f2} kg\n\n{DisplayBatteryInformation()}";
        }

        public override string ToString()
        {
            return $"{RobotName} - [DeliveryRobot]";
        }

        public DeliveryRobot(string robotName, DeliveryMode delivery, double maxLoad) : base(robotName)
        {
            RobotName = robotName;
            ModeOfDelivery = delivery;
            MaxLoadKG = maxLoad;
        }

        public DeliveryRobot(string robotName) : base(robotName)
        {
            RobotName = robotName;
            ModeOfDelivery = DeliveryMode.Walking;
            MaxLoadKG = 5.0;
        }

    }
}
