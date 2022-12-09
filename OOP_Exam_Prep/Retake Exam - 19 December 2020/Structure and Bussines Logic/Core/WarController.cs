using System;
using System.Collections.Generic;
using System.Linq;
using WarCroft.Constants;
using WarCroft.Entities.Characters;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Items;

namespace WarCroft.Core
{
	public class WarController
	{
		private List<Character> party;
		private List<Item> itemsPool;
		public WarController()
		{
			party = new List<Character>();
			itemsPool = new List<Item>();
		}

		public string JoinParty(string[] args)
		{
			string characterType = args[0];
			string name = args[1];

			Character charToJoin;
			switch (characterType)
			{
				case "Warrior": charToJoin = new Warrior(name);
					break;
				case "Priest": charToJoin = new Priest(name);
					break;
                default: throw new ArgumentException(ExceptionMessages.InvalidCharacterType, characterType);
            }

			this.party.Add(charToJoin);
			return string.Format(SuccessMessages.JoinParty, name);
		}

		public string AddItemToPool(string[] args)
		{
			string itemName = args[0];

			Item itemToAdd;
			switch (itemName)
			{
				case "FirePotion": itemToAdd = new FirePotion();
					break;
				case "HealthPotion": itemToAdd = new HealthPotion();
					break;
                default: throw new ArgumentException(string.Format
					(ExceptionMessages.InvalidItem, itemName));
            }

			this.itemsPool.Add(itemToAdd);
            return string.Format(SuccessMessages.AddItemToPool, itemName);
        }

		public string PickUpItem(string[] args)
		{
			string characterName = args[0];

			if (!this.party.Any(x => x.Name == characterName))
				throw new ArgumentException(string.Format
					(ExceptionMessages.CharacterNotInParty, characterName));
			if(this.itemsPool.Count == 0)
				throw new InvalidOperationException(string.Format
                    (ExceptionMessages.ItemPoolEmpty));

			var itemToPick = itemsPool.Last();
			itemsPool.Remove(itemToPick);

			var character = this.party.Find(x => x.Name == characterName);
			character.Bag.AddItem(itemToPick);
			return string.Format(SuccessMessages.PickUpItem, characterName, itemToPick.GetType().Name);
        }

		public string UseItem(string[] args)
		{
			string characterName = args[0];
			string itemName = args[1];

            if (!this.party.Any(x => x.Name == characterName))
                throw new ArgumentException(string.Format
                    (ExceptionMessages.CharacterNotInParty, characterName));

			var charToUseItem = this.party.Find(x => x.Name == characterName);
			var itemToUse = charToUseItem.Bag.GetItem(itemName);

			itemToUse.AffectCharacter(charToUseItem);
			return $"{charToUseItem.Name} used {itemName}.";
        }

		public string GetStats()
		{
			var partySorted = this.party.OrderByDescending(x => x.IsAlive)
				.ThenByDescending(y => y.Health);

			return string.Join(Environment.NewLine, partySorted);
		}

		public string Attack(string[] args)
		{
			string attackerName = args[0];
			string receiverName = args[1];

			var attacker = this.party.Find(x => x.Name == attackerName);
			var receiver = this.party.Find(x => x.Name == receiverName);

			if(attacker == null)
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, attackerName));
			if(receiver == null)
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, receiverName));
			if(attacker.GetType().Name != "Warrior")
                throw new ArgumentException(string.Format(ExceptionMessages.AttackFail, attackerName));

			(attacker as Warrior).Attack(receiver);

            string output = String.Format(SuccessMessages.AttackCharacter,
				attackerName, receiverName, attacker.AbilityPoints, receiverName,
				receiver.Health, receiver.BaseHealth, receiver.Armor, receiver.BaseArmor);
            if (!receiver.IsAlive)
                output += Environment.NewLine + string.Format
					(SuccessMessages.AttackKillsCharacter, receiverName);
			return output;
        }

		public string Heal(string[] args)
		{
            string healerName = args[0];
            string healingReceiverName = args[1];

            var healer = this.party.Find(x => x.Name == healerName);
            var healingReceiver = this.party.Find(x => x.Name == healingReceiverName);

            if (healer == null)
                throw new ArgumentException(string.Format
					(ExceptionMessages.CharacterNotInParty, healerName));
            if (healingReceiver == null)
                throw new ArgumentException(string.Format
					(ExceptionMessages.CharacterNotInParty, healingReceiverName));
			if(healer.GetType().Name != "Priest")
                throw new ArgumentException(string.Format
					(ExceptionMessages.HealerCannotHeal, healerName));

			(healer as Priest).Heal(healingReceiver);
			return $"{healer.Name} heals {healingReceiver.Name} for {healer.AbilityPoints}!" +
				$" {healingReceiver.Name} has {healingReceiver.Health} health now!";
        }
	}
}
