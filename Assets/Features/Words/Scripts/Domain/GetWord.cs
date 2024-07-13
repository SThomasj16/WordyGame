using System.Linq;
using Features.Words.Scripts.Services;
using Features.Words.Tests.Domain;

namespace Features.Words.Scripts.Domain
{
    public class GetWord: IGetWord
    {
        private readonly IGetWordService _wordService;

        public GetWord(IGetWordService wordService)
        {
            _wordService = wordService;
        }

        public Word Execute(int maxAmountOfCharacters) => 
            _wordService.GetWords(maxAmountOfCharacters, 1).First();
    }
}