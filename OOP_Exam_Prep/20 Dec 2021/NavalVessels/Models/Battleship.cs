
namespace NavalVessels.Models
{
    using System;

    using Contracts;

    public class Battleship : Vessel, IBattleship
    {
        private const double BATTLESHIP_ARMOR_THICKNESS = 300;
        public Battleship(string name, double mainWeaponCaliber, double speed)
            : base(name, mainWeaponCaliber, speed, BATTLESHIP_ARMOR_THICKNESS)
        {
            SonarMode = false;
        }

        public bool SonarMode { get; private set; }

        public override void RepairVessel()
        {
            if (ArmorThickness < BATTLESHIP_ARMOR_THICKNESS)
                ArmorThickness = BATTLESHIP_ARMOR_THICKNESS;
        }

        public void ToggleSonarMode()
        {
            SonarMode = !SonarMode;
            if (SonarMode)
            {
                MainWeaponCaliber += 40;
                Speed -= 5;
            }
            else
            {
                MainWeaponCaliber -= 40;
                Speed += 5;
            }
        }
        public override string ToString()
        {
            string sonMode = SonarMode == true ? "ON" : "OFF";

            return base.ToString() 
                + Environment.NewLine + $" *Sonar mode: {sonMode}";
        }
    }
}
