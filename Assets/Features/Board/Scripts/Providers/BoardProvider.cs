using Features.Board.Scripts.Domain;
using Features.Board.Scripts.Domain.Actions;
using Features.Board.Scripts.Infrastructure;
using UniRx;
using UnityEngine;
using Utils.Provider.Scripts;

namespace Features.Board.Scripts.Providers
{
    public static class BoardProvider
    {
        public static BoardConfiguration GetBoardConfig() =>
            Provider.GetOrInstanciate(ScriptableObject.CreateInstance<BoardConfiguration>,
                "BoardProvider.BoardConfiguration");

        public static IBuildMatrix GetMatrixBuilder() =>
            Provider.GetOrInstanciate(() => new BuildMatrix(),
                "BoardProvider.BuildMatrix");

        public static ISaveCurrentMatchWords GetSaveCurrentMatchWordsAction() =>
            Provider.GetOrInstanciate(() => new SaveCurrentMatchWords(GetCurrentMatchWordsRepository()),
                "BoardProvider.SaveCurrentMatchWords");

        private static ICurrentMatchWordsRepository GetCurrentMatchWordsRepository() =>
            Provider.GetOrInstanciate(() => new CurrentMatchWordsRepository(),
                "BoardProvider.CurrentMatchWordsRepository");

        public static IIsWordInBoard GetIsWordInBoardAction() =>
            Provider.GetOrInstanciate(() => new IsWordInBoard(GetCurrentMatchWordsRepository()),
                "BoardProvider.IsWordInBoardAction");

        public static ISaveSelectedMatchWords GetSaveSelectedMatchWords() =>
            Provider.GetOrInstanciate(() => new SaveSelectedMatchWords(GetCurrentMatchSelectedWordsRepository()),
                "BoardProvider.GetSaveSelectedMatchWords");

        private static ICurrentMatchSelectedWordsRepository GetCurrentMatchSelectedWordsRepository() =>
            Provider.GetOrInstanciate(() => new CurrentMatchSelectedWordsRepository(),
                "BoardProvider.GetCurrentMatchSelectedWordsRepository");

        public static ICheckVictoryStatus GetCheckVictoryStatusAction() =>
            Provider.GetOrInstanciate(
                () => new CheckVictoryStatus(GetCurrentMatchWordsRepository(),
                    GetCurrentMatchSelectedWordsRepository()),
                "BoardProvider.GetCheckVictoryStatusAction");

        public static ISubject<Unit> GetOnVictoryEvent() =>
            Provider.GetOrInstanciate(() => new Subject<Unit>(),
                "BoardProvider.OnVictoryEvent");

        public static ISubject<Unit> GetOnResetBoardEvent() =>
            Provider.GetOrInstanciate(() => new Subject<Unit>(),
                "BoardProvider.OnResetBoardEvent");

        public static IResetMatchRepositories GetResetMatchRepositoriesAction() =>
            Provider.GetOrInstanciate(() => new ResetMatchRepositories(GetCurrentMatchWordsRepository(),
                    GetCurrentMatchSelectedWordsRepository()),
                "BoardProvider.ResetMatchRepositoriesAction");
    }
}