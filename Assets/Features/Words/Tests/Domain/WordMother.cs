using System.Collections.Generic;
using Features.Words.Scripts.Domain;

namespace Features.Words.Tests.Domain
{
    public static class WordMother
    {
        public static List<Word> AListOfWords()
        {
            return new List<Word>
            {
                new("Leaf",WordTheme.Nature),
                new("Rain",WordTheme.Nature),
                new("Wind",WordTheme.Nature),
                new("Snow",WordTheme.Nature),
                new("Sun",WordTheme.Nature),
                
                new("Rice", WordTheme.Food),
                new("Cake", WordTheme.Food),
                new("Soup", WordTheme.Food),
                new("Bread", WordTheme.Food),
                new("Meat", WordTheme.Food),
                
                new("Blue", WordTheme.Color),
                new("Red", WordTheme.Color),
                new("Pink", WordTheme.Color),
                new("Green", WordTheme.Color),
                new("Black", WordTheme.Color)
            };
        }

        public static List<Word> AListOfWordsWith(int maxAmountOfCharacters) => 
            AListOfWords().FindAll(word => word.AmountOfCharacters <= maxAmountOfCharacters);
    }
}