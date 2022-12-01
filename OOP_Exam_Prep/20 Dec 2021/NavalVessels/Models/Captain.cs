
namespace NavalVessels.Models
{
    using System;
    using System.Text;

    using Contracts;
    using System.Collections.Generic;
    using Utilities.Messages;

    public class Captain : ICaptain
    {
        private string fullName;
        private List<IVessel> vessels;
        private int combatExperience;

        public Captain(string fullName)
        {
            FullName = fullName;
            CombatExperience = 0;
            vessels = new List<IVessel>();
        }
        public string FullName 
        {
            get => this.fullName;
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(ExceptionMessages.InvalidCaptainName);

                this.fullName = value;
            }
        }

        public int CombatExperience
        {
            get { return combatExperience; }
            private set { combatExperience = value; }
        }

        public ICollection<IVessel> Vessels => this.vessels.AsReadOnly();

        public void AddVessel(IVessel vessel)
        {
            if (vessel == null)
                throw new NullReferenceException(ExceptionMessages.InvalidVesselForCaptain);
            
            this.vessels.Add(vessel);
        }

        public void IncreaseCombatExperience()
        {
            CombatExperience += 10;
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{FullName} has {CombatExperience} combat experience and commands {this.vessels.Count} vessels.");
            if (vessels.Count > 0)
            {
                foreach (var vessel in vessels)
                {
                    sb.AppendLine(vessel.ToString());
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
