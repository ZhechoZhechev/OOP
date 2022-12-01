
namespace NavalVessels.Models.Contracts
{
    using System;
    public class Submarine : Vessel, ISubmarine
    {
        private const double SUBMARINE_ARMOR_THICKNESS = 200;
        public Submarine(string name, double mainWeaponCaliber, double speed)
            : base(name, mainWeaponCaliber, speed, SUBMARINE_ARMOR_THICKNESS)
        {
            SubmergeMode = false;
        }

        public bool SubmergeMode { get; private set; }

        public override void RepairVessel()
        {
            if (ArmorThickness < SUBMARINE_ARMOR_THICKNESS)
                ArmorThickness = SUBMARINE_ARMOR_THICKNESS;
        }

        public void ToggleSubmergeMode()
        {
            SubmergeMode = !SubmergeMode;

            if (SubmergeMode)
            {
                MainWeaponCaliber += 40;
                Speed -= 4;
            }
            else
            {
                MainWeaponCaliber -= 40;
                Speed += 4;
            }
        }
        public override string ToString()
        {
            string subMode = SubmergeMode == true ? "ON" : "OFF";

            return base.ToString()
                + Environment.NewLine + $" *Submerge mode: {subMode}";
        }
    }
}
