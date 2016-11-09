using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MindboxTestProject
{
    public class MindboxTestProject
    {
        public struct Card
        {
            public string CityDeparture;
            public string CityArrival;

            public override string ToString()
            {
                return CityDeparture + "->" + CityArrival;
            }
        }

        public static IEnumerable<Card> CardsSort(IEnumerable<Card> cards)
        {
            if(cards == null || !cards.Any()) return new List<Card>();

            Dictionary<string, Card> cardsHashByDep = new Dictionary<string, Card>();
            Dictionary<string, Card> cardsHashByArr = new Dictionary<string, Card>();

            foreach (var card in cards)
            {
                cardsHashByDep.Add(card.CityDeparture, card);
                cardsHashByArr.Add(card.CityArrival, card);
            }

            LinkedList<Card> result = new LinkedList<Card>();
            result.AddFirst(cards.First());
            while (cardsHashByDep.ContainsKey(result.Last.Value.CityArrival))
                result.AddLast(cardsHashByDep[result.Last.Value.CityArrival]);
            while (cardsHashByArr.ContainsKey(result.First.Value.CityDeparture))
                result.AddFirst(cardsHashByArr[result.First.Value.CityDeparture]);

            return result;
        }

        static void Main(string[] args)
        {
            var cards = new List<MindboxTestProject.Card>()
            {
                new MindboxTestProject.Card() {CityDeparture = "Москва",CityArrival = "Париж" },
                new MindboxTestProject.Card() {CityDeparture = "Париж",CityArrival = "Токио" },
                new MindboxTestProject.Card() {CityDeparture = "Токио",CityArrival = "Реймс" },
                new MindboxTestProject.Card() {CityDeparture = "Реймс",CityArrival = "Берлин" },
                new MindboxTestProject.Card() {CityDeparture = "Берлин",CityArrival = "Лондон" },
                new MindboxTestProject.Card() {CityDeparture = "Лондон",CityArrival = "Вена" },
                new MindboxTestProject.Card() {CityDeparture = "Вена",CityArrival = "Прага" },
                new MindboxTestProject.Card() {CityDeparture = "Прага",CityArrival = "Рим" },
                new MindboxTestProject.Card() {CityDeparture = "Рим",CityArrival = "Петербург" },
            };

            var rnd = new Random((int)DateTime.Now.Ticks);
            var randomCards = cards.OrderBy(item => rnd.Next()); //shuffle...
            Console.WriteLine("Unordered list:");
            foreach (var c in randomCards)
                Console.WriteLine(c);

            Console.WriteLine("\nOrdered list:");
            foreach (var c in MindboxTestProject.CardsSort(randomCards))
                Console.WriteLine(c);
            Console.ReadKey();
        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_CardsSort()
        {
            var cards = new List<MindboxTestProject.Card>()
            {
                new MindboxTestProject.Card() {CityDeparture = "Москва",CityArrival = "Париж" },
                new MindboxTestProject.Card() {CityDeparture = "Париж",CityArrival = "Токио" },
                new MindboxTestProject.Card() {CityDeparture = "Токио",CityArrival = "Реймс" },
                new MindboxTestProject.Card() {CityDeparture = "Реймс",CityArrival = "Берлин" },
                new MindboxTestProject.Card() {CityDeparture = "Берлин",CityArrival = "Лондон" },
                new MindboxTestProject.Card() {CityDeparture = "Лондон",CityArrival = "Вена" },
                new MindboxTestProject.Card() {CityDeparture = "Вена",CityArrival = "Прага" },
                new MindboxTestProject.Card() {CityDeparture = "Прага",CityArrival = "Рим" },
                new MindboxTestProject.Card() {CityDeparture = "Рим",CityArrival = "Петербург" },
            };

            var rnd = new Random((int) DateTime.Now.Ticks);
            var randomCards = cards.OrderBy(item => rnd.Next()); //shuffle...

            var result = new List<MindboxTestProject.Card>(MindboxTestProject.CardsSort(randomCards));

            Assert.IsNotNull(result);
            Assert.AreEqual(cards.Count, result.Count);
            for (int i = 1; i < result.Count; i++)
            {
                Assert.AreEqual(result[i].CityDeparture, result[i - 1].CityArrival);
            }
        }
    }
}
