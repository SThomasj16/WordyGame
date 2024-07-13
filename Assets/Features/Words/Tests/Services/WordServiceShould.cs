using System.Collections.Generic;
using Features.Words.Scripts.Domain;
using Features.Words.Scripts.Infrastructure;
using Features.Words.Scripts.Services;
using Features.Words.Tests.Domain;
using NSubstitute;
using NUnit.Framework;

namespace Features.Words.Tests.Services
{
    public class WordServiceShould
    {
        private WordService _wordService;
        private IWordsRepository _wordsRepository;
        private const int MaxAmountOfCharacters = 3;

        [SetUp]
        public void Setup()
        {
            _wordsRepository = Substitute.For<IWordsRepository>();
            _wordService = new WordService(_wordsRepository);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void ReturnCorrectAmountOfWords(int amountOfWords)
        {
            GivenAListOfWords(amountOfWords);
            var amount = WhenReturningWords();
            ThenExpectedAmountEqualsResult(amountOfWords, amount.Count);
        }

        private static void ThenExpectedAmountEqualsResult(int amountOfWords, int amount)
        {
            Assert.AreEqual(amountOfWords, amount);
        }

        private void GivenAListOfWords(int amountOfWords)
        {
            _wordsRepository.Get().Returns(WordMother.AListOfSpecificAmountOfWordsWithMaxAmountOfCharacters(MaxAmountOfCharacters,amountOfWords));
        }
        
        private List<Word> WhenReturningWords()
        {
            return _wordService.GetWords(MaxAmountOfCharacters);
        }
    }
}