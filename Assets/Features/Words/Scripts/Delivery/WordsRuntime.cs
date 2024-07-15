using System;
using System.Linq;
using Features.Words.Scripts.Domain;
using Features.Words.Scripts.Providers;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;
using Utils.Runtime.Scripts;

namespace Features.Words.Scripts.Delivery
{
    public class WordsRuntime : RuntimeBehaviour
    {
        private const string FileName = "wordsJson";

        private void Awake()
        {
            Initialize()
                .Subscribe()
                .AddTo(this);
        }

        public override IObservable<Unit> Initialize()
        {
            return Observable.ReturnUnit()
                .Select(_ => LoadWordsJson())
                .Do(SaveToRepository)
                .AsUnitObservable();
        }

        private void SaveToRepository(WordsJsonDto dto)
        {
            var listOfWords = dto.Animals.Select(animal => new Word(animal)).ToList();
            listOfWords.AddRange(dto.Food.Select(food => new Word(food)));
            listOfWords.AddRange(dto.Nature.Select(nature => new Word(nature)));
            WordsProvider.GetWordsRepository().Set(listOfWords);
        }

        private WordsJsonDto LoadWordsJson()
        {
            var jsonTextFile = Resources.Load<TextAsset>(FileName).text;
            return JsonConvert.DeserializeObject<WordsJsonDto>(jsonTextFile);
        }
    }
}