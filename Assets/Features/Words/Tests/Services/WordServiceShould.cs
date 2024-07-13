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

        [TestCase(WordAmountOfCharacters.Three)]
        [TestCase(WordAmountOfCharacters.Four)]
        [TestCase(WordAmountOfCharacters.Five)]
        [TestCase(WordAmountOfCharacters.Six)]
        [TestCase(WordAmountOfCharacters.Seven)]
        public void ReturnCorrectAmountOfCharactersForEveryWord(WordAmountOfCharacters amountOfCharacters)
        {
            GivenAListOfWords();
            var results = WhenReturningWords(amountOfCharacters);
            ThenAllWordsHaveExpectedAmountOfCharacters((int)amountOfCharacters, results);
        }

        private void GivenAListOfWords()
        {
            _wordsRepository.Get().Returns(WordMother.AListOfWords());
        }

        private Word WhenReturningWords(WordAmountOfCharacters amountOfCharacters)
        {
            return _wordService.GetWord(amountOfCharacters);
        }

        private void ThenAllWordsHaveExpectedAmountOfCharacters(int amountOfCharacters, Word result)
        {
            Assert.True(result.AmountOfCharacters <= amountOfCharacters);
        }
    }
}