
namespace NavalVessels.Core
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models.Contracts;
    using Utilities.Messages;
    using Repositories;
    using Models;

    public class Controller : IController
    {
        private VesselRepository vessels;
        private List<ICaptain> captains;

        public Controller()
        {
            this.vessels = new VesselRepository();
            this.captains = new List<ICaptain>();
        }

        public string HireCaptain(string fullName)
        {
            if (captains.Any(x => x.FullName == fullName))
                return string.Format
                    (OutputMessages.CaptainIsAlreadyHired, fullName);

            this.captains.Add(new Captain(fullName));

            return $"Captain {fullName} is hired.";
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            if (vessels.FindByName(name) != null)
                return string.Format
                    (OutputMessages.VesselIsAlreadyManufactured, vesselType, name);

            IVessel vessel;
            switch (vesselType)
            {
                case
                    "Submarine":
                    vessel = new Submarine(name, mainWeaponCaliber, speed);
                    break;
                case
                    "Battleship":
                    vessel = new Battleship(name, mainWeaponCaliber, speed);
                    break;
                default: return OutputMessages.InvalidVesselType;
            }

            this.vessels.Add(vessel);
            return string.Format
                (OutputMessages.SuccessfullyCreateVessel, vesselType, name, mainWeaponCaliber, speed);
        }

        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            var vesselToAssing = vessels.FindByName(selectedVesselName);
            var captainToAsingVessel = captains.FirstOrDefault(x => x.FullName == selectedCaptainName);

            if (captainToAsingVessel == null)
                return string.Format
                    (OutputMessages.CaptainNotFound, selectedCaptainName);

            if (vesselToAssing == null)
                return string.Format
                    (OutputMessages.VesselNotFound, selectedVesselName);

            if (vesselToAssing.Captain != null)
                return string.Format
                    (OutputMessages.VesselOccupied, selectedVesselName);

            vesselToAssing.Captain = captainToAsingVessel;
            captainToAsingVessel.AddVessel(vesselToAssing);

            return string.Format
                (OutputMessages.SuccessfullyAssignCaptain, selectedCaptainName, selectedVesselName);

        }

        public string CaptainReport(string captainFullName)
        {
            var captainToReport = captains.FirstOrDefault(x => x.FullName == captainFullName);

            return captainToReport.Report();
        }
        public string VesselReport(string vesselName)
        {
            var vessleToReport = vessels.FindByName(vesselName);

            return vessleToReport.ToString();
        }

        public string ToggleSpecialMode(string vesselName)
        {
            var vesselToActivate = vessels.FindByName(vesselName);

            if (vesselToActivate == null)
                return string.Format(OutputMessages.VesselNotFound, vesselName);

            switch (vesselToActivate.GetType().Name)
            {
                case "Battleship":
                    (vesselToActivate as Battleship).ToggleSonarMode();
                    return string.Format(OutputMessages.ToggleBattleshipSonarMode, vesselName);

                case "Submarine":
                    (vesselToActivate as Submarine).ToggleSubmergeMode();
                    return string.Format(OutputMessages.ToggleSubmarineSubmergeMode, vesselName);
                default: return null;
            }
        }
        public string ServiceVessel(string vesselName)
        {
            var vesselToRepair = vessels.FindByName(vesselName);

            if (vesselToRepair == null)
               return string.Format(OutputMessages.VesselNotFound, vesselName);
            else 
            {
                vesselToRepair.RepairVessel();
                return string.Format(OutputMessages.SuccessfullyRepairVessel, vesselName);
            }
        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            var attackingVessel = vessels.FindByName(attackingVesselName);
            var defendingVessel = vessels.FindByName(defendingVesselName);

            if(attackingVessel == null)
                return string.Format(OutputMessages.VesselNotFound, attackingVesselName);
            else if(defendingVessel == null)
                return string.Format(OutputMessages.VesselNotFound, defendingVesselName);

            if(attackingVessel.ArmorThickness == 0)
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, attackingVesselName);
            else if(defendingVessel.ArmorThickness == 0)
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, defendingVesselName);

            attackingVessel.Attack(defendingVessel);
            attackingVessel.Captain.IncreaseCombatExperience();
            defendingVessel.Captain.IncreaseCombatExperience();

            return string.Format
                (OutputMessages.SuccessfullyAttackVessel, defendingVesselName, attackingVesselName, defendingVessel.ArmorThickness);
        }

    }
}
