using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Cards
{
    public class Card
    {
        public Card(string face, string suit)
        {
            Face = face;
            Suit = suit;
        }

        public string Face { get; set; }
        public string Suit { get; set; }
        public override string ToString()
        {
            return $"[{Face}{Suit}]";
        }
    }
    class Program
    {
        static void Main()
        {
            List<Card> cards = new List<Card>();

            string[] input = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < input.Length; i++)
            {
                try
                {
                    string[] info = input[i]
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    string face = info[0];
                    string suit = info[1];
                    cards.Add(CreateCard(face, suit));
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }

            }

            Console.WriteLine(string.Join(" ", cards));
        }
        public static Card CreateCard(string face, string suit)
        {
            string[] faces = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            string[] suits = new string[] { "S", "H", "D", "C" };

            string utfSuit = string.Empty;

            if (!faces.Contains(face) || !suits.Contains(suit))
            {
                throw new ArgumentException("Invalid card!");
            }
            else
            {
                switch (suit)
                {
                    case "S":
                        utfSuit = "\u2660";
                        break;
                    case "H":
                        utfSuit = "\u2665";
                        break;
                    case "D":
                        utfSuit = "\u2666";
                        break;
                    case "C":
                        utfSuit = "\u2663";
                        break;
                    default:
                        break;
                }
            }
            return new Card(face, utfSuit);
        }
    }
}
