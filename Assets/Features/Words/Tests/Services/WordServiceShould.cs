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
        private IUsedWordsRepository _usedWordsRepository;
        
        [SetUp]
        public void Setup()
        {
            _wordsRepository = Substitute.For<IWordsRepository>();
            _usedWordsRepository = Substitute.For<IUsedWordsRepository>();
            _wordService = new WordService(_wordsRepository, _usedWordsRepository);
        }

        [TestCase(WordAmountOfCharacters.Three)]
        [TestCase(WordAmountOfCharacters.Four)]
        [TestCase(WordAmountOfCharacters.Five)]
        [TestCase(WordAmountOfCharacters.Six)]
        [TestCase(WordAmountOfCharacters.Seven)]
        public void ReturnCorrectAmountOfCharactersForEveryWord(WordAmountOfCharacters amountOfCharacters)
        {
            GivenAListOfWords();
            GivenAEmptyUsedWordsRepository();
            var result = WhenReturningAWordWith(amountOfCharacters);
            ThenAllWordsHaveExpectedAmountOfCharacters((int)amountOfCharacters, result);
        }
        
        [Test]
        public void MarkReturningWordAsUsed()
        {
            GivenAListOfWords();
            GivenAEmptyUsedWordsRepository();
            var word = WhenReturningAWordWith(WordAmountOfCharacters.Three);
            ThenMarkWord(word);
        }
        
        [Test]
        public void ClearUsedWordRepositoryWhenWordRepositoryIsEmpty()
        {
            GivenAListOfWords();
            GivenAFullUsedWordsRepository();
            WhenReturningAWordWith(WordAmountOfCharacters.Three);
            ThenClearUsedWordsRepository();
        }
        
        [Test]
        public void GetNewWordWhenWordWhenUsedRepositoryIsFull()
        {
            GivenAListOfWords();
            GivenAFullUsedWordsRepository();
            WhenReturningAWordWith(WordAmountOfCharacters.Three);
            ThenWordWasFetchTwice();
        }

        private void GivenAFullUsedWordsRepository()
        {
            _usedWordsRepository.Get().Returns(WordMother.AListOfWords());
        }

        private void GivenAEmptyUsedWordsRepository()
        {
            _usedWordsRepository.Get().Returns(new List<Word>());
        }
        private void GivenAListOfWords()
        {
            _wordsRepository.Get().Returns(WordMother.AListOfWords());
        }

        private Word WhenReturningAWordWith(WordAmountOfCharacters amountOfCharacters) => 
            _wordService.GetWord(amountOfCharacters);

        private void ThenAllWordsHaveExpectedAmountOfCharacters(int amountOfCharacters, Word result)
        {
            Assert.True(result.AmountOfCharacters <= amountOfCharacters);
        }
        
        private void ThenMarkWord(Word word)
        {
            _usedWordsRepository.Received(1).Mark(word);
        }
        
        private void ThenClearUsedWordsRepository()
        {
            _usedWordsRepository.Received(1).Clear();
        }
        
        private void ThenWordWasFetchTwice()
        {
            _wordsRepository.Received(2).Get();
        }
    }
}