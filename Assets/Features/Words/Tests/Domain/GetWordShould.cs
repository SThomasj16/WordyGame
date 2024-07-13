using NUnit.Framework;
using UnityEngine;

namespace Features.Words.Tests.Domain
{
    public class GetWordShould : MonoBehaviour
    {
        private GetWord _action;
        
        [Test]
        public void ReturnWordWithSpecificAmountOfCharacters()
        {
            
        }
    }

    public class GetWord: IGetWord
    {
    }
}
