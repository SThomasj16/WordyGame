using System.Linq;
using Features.Words.Scripts.Services;

namespace Features.Words.Scripts.Domain.Actions
{
    public class GetWord: IGetWord
    {
        private readonly IWordService _wordService;

        public GetWord(IWordService wordService)
        {
            _wordService = wordService;
        }

        public Word Execute(int maxAmountOfCharacters) => 
            _wordService.GetWords(maxAmountOfCharacters).First();
    }
}