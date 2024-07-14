using Features.Board.Scripts.Domain;
using UnityEngine;
using Utils.Provider.Scripts;

namespace Features.Board.Scripts.Providers
{
    public static class BoardProvider
    {
        public static BoardConfiguration GetBoardConfig() =>
            Provider.GetOrInstanciate(ScriptableObject.CreateInstance<BoardConfiguration>,
                "BoardProvider.BoardConfiguration");
    }
}