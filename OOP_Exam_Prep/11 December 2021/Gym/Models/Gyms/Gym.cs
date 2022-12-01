using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms.Contracts;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Models.Gyms
{
    public abstract class Gym : IGym
    {
        private string name;
        private List<IEquipment> equipment;
        private List<IAthlete> athletes;

        public Gym(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            this.equipment = new List<IEquipment>();
            this.athletes = new List<IAthlete>();
        }
        public string Name 
        {
            get => this.name;
            private set 
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(ExceptionMessages.InvalidGymName);

                this.name = value;
            }
        }

        public int Capacity { get; private set; }

        public double EquipmentWeight => this.equipment.Select(x => x.Weight).Sum();

        public ICollection<IEquipment> Equipment => this.equipment.AsReadOnly();

        public ICollection<IAthlete> Athletes => this.athletes.AsReadOnly();

        public void AddAthlete(IAthlete athlete)
        {
            if (this.athletes.Count == Capacity)
                throw new InvalidOperationException(ExceptionMessages.NotEnoughSize);
            
            this.athletes.Add(athlete);
        }

        public void AddEquipment(IEquipment equipment)
        {
            this.equipment.Add(equipment);
        }

        public void Exercise()
        {
            foreach (var athlete in athletes)
            {
                athlete.Exercise();
            }
        }

        public string GymInfo()
        {
            StringBuilder sb = new StringBuilder();
            string resulAthletes = athletes.Any() ? $"Athletes: {string.Join(", ", athletes.Select(x => x.FullName))}"
                : "Athletes: No athletes";

            sb.AppendLine($"{Name} is a {this.GetType().Name}:")
                .AppendLine(resulAthletes)
                .AppendLine($"Equipment total count: {equipment.Count}")
                .AppendLine($"Equipment total weight: {EquipmentWeight:f2} grams");

            return sb.ToString().TrimEnd();
        }

        public bool RemoveAthlete(IAthlete athlete)
        {
            return this.athletes.Remove(athlete);
        }
    }
}
