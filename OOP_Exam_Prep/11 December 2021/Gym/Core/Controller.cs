using System;
using System.Collections.Generic;

namespace Gym.Core
{
    using Contracts;
    using Gym.Models.Athletes;
    using Gym.Models.Athletes.Contracts;
    using Gym.Models.Equipment;
    using Gym.Models.Equipment.Contracts;
    using Gym.Models.Gyms;
    using Models.Gyms.Contracts;
    using Repositories;
    using System.Linq;
    using Utilities.Messages;

    public class Controller : IController
    {
        private EquipmentRepository equipment;
        private List<IGym> gyms;

        public Controller()
        {
            this.equipment = new EquipmentRepository();
            this.gyms = new List<IGym>();
        }
        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            var gymToAddAthlete = gyms.Find(x => x.Name == gymName);

            IAthlete athleteToAdd;
            switch (athleteType)
            {
                case "Boxer": athleteToAdd = new Boxer(athleteName, motivation, numberOfMedals);
                    if (gymToAddAthlete.GetType().Name != "BoxingGym")
                        return OutputMessages.InappropriateGym;
                    break;
                case "Weightlifter": athleteToAdd = new Weightlifter(athleteName, motivation, numberOfMedals);
                    if (gymToAddAthlete.GetType().Name != "WeightliftingGym")
                        return OutputMessages.InappropriateGym;
                    break;
                default: throw new InvalidOperationException(ExceptionMessages.InvalidAthleteType);
            }

            gymToAddAthlete.AddAthlete(athleteToAdd);
            return string.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
        }

        public string AddEquipment(string equipmentType)
        {
            IEquipment eqToAdd;
            switch (equipmentType)
            {
                case "BoxingGloves": eqToAdd = new BoxingGloves();
                    break;
                case "Kettlebell": eqToAdd = new Kettlebell();
                    break;
                default: throw new InvalidOperationException(ExceptionMessages.InvalidEquipmentType);
            }

            this.equipment.Add(eqToAdd);
            return string.Format(OutputMessages.SuccessfullyAdded, equipmentType);
        }

        public string AddGym(string gymType, string gymName)
        {
            IGym gymToAdd;
            switch (gymType)
            {
                case "BoxingGym": gymToAdd = new BoxingGym(gymName);
                    break;
                case "WeightliftingGym": gymToAdd = new WeightliftingGym(gymName);
                    break;
                default: throw new InvalidOperationException(ExceptionMessages.InvalidGymType);
            }

            gyms.Add(gymToAdd);
            return string.Format(OutputMessages.SuccessfullyAdded, gymType);
        }

        public string EquipmentWeight(string gymName)
        {
            var targetedGym = gyms.Find(x => x.Name == gymName);
            var equipmentWeightTotal = targetedGym.EquipmentWeight;

            return string.Format(OutputMessages.EquipmentTotalWeight, gymName, equipmentWeightTotal);
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            if (equipment.FindByType(equipmentType) == null)
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.InexistentEquipment, equipmentType));

            var equipToAdd = equipment.FindByType(equipmentType);
            var gymToAddEqipTo = gyms.Find(x => x.Name == gymName);

            gymToAddEqipTo.AddEquipment(equipToAdd);
            equipment.Remove(equipToAdd);

            return string.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
        }

        public string Report()
            => string.Join(Environment.NewLine, gyms.Select(x => x.GymInfo()));

        public string TrainAthletes(string gymName)
        {
            var targetedGym = gyms.Find(x => x.Name == gymName);

            targetedGym.Exercise();

            return string.Format(OutputMessages.AthleteExercise, targetedGym.Athletes.Count);
        }
    }
}
