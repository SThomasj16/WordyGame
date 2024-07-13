using System.Collections.Generic;
using System.Linq;
using Features.Words.Scripts.Domain;

namespace Features.Words.Tests.Domain
{
    public static class WordMother
    {
        public static List<Word> AListOfWords()
        {
            return new List<Word>
            {
                new("Leaf", WordTheme.Nature),
                new("Rain", WordTheme.Nature),
                new("Wind", WordTheme.Nature),
                new("Snow", WordTheme.Nature),
                new("Sun", WordTheme.Nature),
                new("Fog", WordTheme.Nature),
                new("Sky", WordTheme.Nature),
                new("Sea", WordTheme.Nature),
                new("Oak", WordTheme.Nature),
                new("Elm", WordTheme.Nature),
                new("Bay", WordTheme.Nature),
                new("Bee", WordTheme.Nature),
                new("Ant", WordTheme.Nature),
                
                new("Rice", WordTheme.Food),
                new("Cake", WordTheme.Food),
                new("Soup", WordTheme.Food),
                new("Bread", WordTheme.Food),
                new("Meat", WordTheme.Food),
                new("Sushi", WordTheme.Food),
                new("Banana", WordTheme.Food),
                new("Broccoli", WordTheme.Food),
                new( "Butterscotch", WordTheme.Food),
                
                new("Cat", WordTheme.Animals),
                new("Lion", WordTheme.Animals),
                new("Tiger", WordTheme.Animals),
                new("Dolphin", WordTheme.Animals),
                new("Elephant", WordTheme.Animals),
                new("Chameleon", WordTheme.Animals),
            };
        }

        public static Word AWordWithAmountOfCharacters(WordAmountOfCharacters amountOfCharacters) => 
            AListOfWords().Find(word => word.AmountOfCharacters <= (int)amountOfCharacters);
        
        public static List<Word> AListOfSpecificAmountOfWordsWithMaxAmountOfCharacters(int maxAmountOfCharacters, int amountOfWords) => 
            AListOfWords().FindAll(word => word.AmountOfCharacters <= maxAmountOfCharacters)
                .Take(amountOfWords).ToList();
    }
}