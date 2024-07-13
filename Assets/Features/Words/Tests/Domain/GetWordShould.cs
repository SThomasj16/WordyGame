using Features.Words.Scripts.Domain;
using Features.Words.Scripts.Domain.Actions;
using Features.Words.Scripts.Services;
using NSubstitute;
using NUnit.Framework;

namespace Features.Words.Tests.Domain
{
    public class GetWordShould 
    {
        private GetWord _action;
        private IWordService _wordService;

        [SetUp]
        public void Setup()
        {
            _wordService = Substitute.For<IWordService>();
            _action = new GetWord(_wordService);
        }

        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void ReturnWordWithSpecificAmountOfCharacters(WordAmountOfCharacters amountOfCharacters)
        {
            GivenAListOfWords(amountOfCharacters);
            var selectedWord = WhenReturningAWord(amountOfCharacters);
            ThenReturningWordHasExpectedAmountOfCharacters((int)amountOfCharacters, selectedWord);
        }
        
        private void GivenAListOfWords(WordAmountOfCharacters amountOfCharacters)
        {
            _wordService.GetWord(amountOfCharacters)
                .Returns(WordMother.AWordWithAmountOfCharacters(amountOfCharacters));
        }
        private Word WhenReturningAWord(WordAmountOfCharacters amountOfCharacters) => _action.Execute(amountOfCharacters);
        private static void ThenReturningWordHasExpectedAmountOfCharacters(int maxAmountOfCharacters, Word selectedWord)
        {
            Assert.True(selectedWord.AmountOfCharacters <= maxAmountOfCharacters);
        }
    }
}
