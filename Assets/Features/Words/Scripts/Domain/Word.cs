namespace Features.Words.Scripts.Domain
{
    public struct Word
    {
        public string Value;
        public WordTheme Theme;
        public int AmountOfCharacters => Value.Length;

        public Word(string value)
        {
            Value = value;
            Theme = WordTheme.Animals;
        }
    }
}