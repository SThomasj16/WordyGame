using System.Collections.Generic;
using System.Linq;
using Features.Words.Scripts.Domain;

namespace Utils
{
    public static class WordMother
    {
        public static List<Word> AListOfWords()
        {
            return new List<Word>
            {
                new("Leaf"),
                new("Rain"),
                new("Wind"),
                new("Snow"),
                new("Sun"),
                new("Fog"),
                new("Sky"),
                new("Sea"),
                new("Oak"),
                new("Elm"),
                new("Bay"),
                new("Bee"),
                new("Ant"),
                
                new("Rice"),
                new("Cake"),
                new("Soup"),
                new("Bread"),
                new("Meat"),
                new("Sushi"),
                new("Banana"),
                new("Broccoli"),
                new( "Butterscotch"),
                
                new("Cat"),
                new("Lion"),
                new("Tiger"),
                new("Dolphin"),
                new("Elephant"),
                new("Chameleon"),
            };
        }

        public static Word AWordWithAmountOfCharacters(WordAmountOfCharacters amountOfCharacters) => 
            AListOfWords().Find(word => word.AmountOfCharacters <= (int)amountOfCharacters);
        
        public static List<Word> AListOfSpecificAmountOfWordsWithMaxAmountOfCharacters(int maxAmountOfCharacters, int amountOfWords) => 
            AListOfWords().FindAll(word => word.AmountOfCharacters <= maxAmountOfCharacters)
                .Take(amountOfWords).ToList();
    }
}