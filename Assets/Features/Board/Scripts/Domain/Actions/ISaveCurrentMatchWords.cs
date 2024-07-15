using System.Collections.Generic;
using Features.Words.Scripts.Domain;

namespace Features.Board.Scripts.Domain.Actions
{
    public interface ISaveCurrentMatchWords
    {
        void Execute(List<Word> words);
    }
}