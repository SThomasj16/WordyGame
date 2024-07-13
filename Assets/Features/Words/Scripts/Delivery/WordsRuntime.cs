using System;
using Features.Runtime.Scripts;
using Features.Words.Scripts.Domain;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;

namespace Features.Words.Scripts.Delivery
{
    public class WordsRuntime : RuntimeBehaviour
    {
        private const string FileName = "wordsJson";

        private void Start()
        {
            Initialize()
                .Subscribe()
                .AddTo(this);
        }

        public override IObservable<Unit> Initialize()
        {
            return Observable.ReturnUnit()
                .Select(_ => LoadWordsJson())
                .Do(dto => Debug.Log(dto.Animals.threeLetterWords[0]))
                .AsUnitObservable();
        }

        private WordsJsonDto LoadWordsJson()
        {
            var jsonTextFile = Resources.Load<TextAsset>(FileName).text;
            return JsonConvert.DeserializeObject<WordsJsonDto>(jsonTextFile);
        }
    }
}