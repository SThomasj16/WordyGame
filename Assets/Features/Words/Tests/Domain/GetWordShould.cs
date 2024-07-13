using Features.Words.Scripts.Domain;
using Features.Words.Scripts.Services;
using NSubstitute;
using NUnit.Framework;

namespace Features.Words.Tests.Domain
{
    public class GetWordShould 
    {
        private GetWord _action;
        private IGetWordService _getWordService;

        [SetUp]
        public void Setup()
        {
            _getWordService = Substitute.For<IGetWordService>();
            _action = new GetWord(_getWordService);
        }

        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void ReturnWordWithSpecificAmountOfCharacters(int maxAmountOfCharacters)
        {
            GivenAListOfWords(maxAmountOfCharacters);
            var selectedWord = WhenReturningAWord(maxAmountOfCharacters);
            ThenReturningWordHasExpectedAmountOfCharacters(maxAmountOfCharacters, selectedWord);
        }
        
        private void GivenAListOfWords(int maxAmountOfCharacters)
        {
            _getWordService.GetWords(maxAmountOfCharacters, 1)
                .Returns(WordMother.AListOfWordsWith(maxAmountOfCharacters));
        }
        private Word WhenReturningAWord(int maxAmountOfCharacters) => _action.Execute(maxAmountOfCharacters);
        private static void ThenReturningWordHasExpectedAmountOfCharacters(int maxAmountOfCharacters, Word selectedWord)
        {
            Assert.True(selectedWord.AmountOfCharacters <= maxAmountOfCharacters);
        }
    }
}
