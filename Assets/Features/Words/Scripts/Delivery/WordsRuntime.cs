using System;
using System.Collections.Generic;
using System.Linq;
using Features.Runtime.Scripts;
using Features.Words.Scripts.Domain;
using Features.Words.Scripts.Providers;
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
                .Do(SaveToRepository)
                .AsUnitObservable();
        }

        private void SaveToRepository(WordsJsonDto dto)
        {
            var listOfWords = dto.Animals.Select(animal => new Word(animal, WordTheme.Animal)).ToList();
            listOfWords.AddRange(dto.Food.Select(food => new Word(food, WordTheme.Food)));
            listOfWords.AddRange(dto.Nature.Select(nature => new Word(nature, WordTheme.Nature)));
            WordsProvider.GetWordsRepository().Set(listOfWords);
        }

        private WordsJsonDto LoadWordsJson()
        {
            var jsonTextFile = Resources.Load<TextAsset>(FileName).text;
            return JsonConvert.DeserializeObject<WordsJsonDto>(jsonTextFile);
        }
    }
}