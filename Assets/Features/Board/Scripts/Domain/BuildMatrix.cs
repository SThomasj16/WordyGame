using System.Collections.Generic;
using System.Linq;
using Features.Words.Scripts.Domain;
using UnityEngine;

namespace Features.Board.Scripts.Domain
{
    public class BuildMatrix : IBuildMatrix
    {
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public List<char> Execute(List<Word> words, int amountOfRowsAndColumns)
        {
            var firstWord = words.First();
            var remainingWords = words.Skip(1).ToList();
            var matrix = new char[amountOfRowsAndColumns, amountOfRowsAndColumns];

            FillMatrixWithPlaceholders(amountOfRowsAndColumns, matrix);

            var charIndex = 0;
            var randomAxis = Random.Range(0, 2);
            
            if (IsHorizontal(randomAxis))
                AddStarterHorizontalWord(firstWord.Value, amountOfRowsAndColumns, charIndex, matrix);
            else
                AddStarterVerticalWord(firstWord.Value, amountOfRowsAndColumns, charIndex, matrix);

            PlaceRemainingWords(remainingWords, amountOfRowsAndColumns, matrix);
            ReplaceRemainingPlaceHolderWithRandomLetters(matrix,amountOfRowsAndColumns);
            
            return matrix.Cast<char>().ToList();
        }

        private void ReplaceRemainingPlaceHolderWithRandomLetters(char[,] matrix, int amountOfRowsAndColumns)
        {
            for (var row = 0; row < amountOfRowsAndColumns; row++)
            {
                for (var col = 0; col < amountOfRowsAndColumns; col++)
                {
                    if (matrix[row, col] == '_')
                    {
                        var num = Random.Range(0, Alphabet.Length);
                        matrix.SetValue(Alphabet[num],row,col);
                    }
                }
            }
        }

        private void PlaceRemainingWords(List<Word> remainingWords, int amountOfRowsAndColumns, char[,] matrix)
        {
            foreach (var word in remainingWords)
            {
                FindPositions(word.Value, amountOfRowsAndColumns, matrix);
            }
        }

        private void FindPositions(string word, int amountOfRowsAndColumns, char[,] matrix)
        {
                for (var row = 0; row < amountOfRowsAndColumns; row++)
                {
                    for (var col = 0; col < amountOfRowsAndColumns; col++)
                    {
                        if (matrix[row, col] == '_') 
                        {
                            if (HasRoomToTheRight(matrix,row, col, word,amountOfRowsAndColumns))
                            {
                                for (var i = 0; i < word.Length; i++)
                                {
                                    matrix[row, col + i] = word[i];
                                }
                                return;
                            }
                        
                            if(HasRoomToTheLeft(matrix,row, col, word))
                            {
                                for (var i = 0; i < word.Length; i++)
                                {
                                    matrix[row, col - i] = word[i];
                                }
                                return;                            
                            }

                            if (HasRoomDownwardsLeft(matrix, row, col, word))
                            {
                                for (var i = 0; i < word.Length; i++)
                                {
                                    matrix[row - i, col] = word[i];
                                }
                                return;
                            }

                            if (HasRoomUpwardsLeft(matrix, row, col, word, amountOfRowsAndColumns))
                            {
                                for (var i = 0; i < word.Length; i++)
                                {
                                    matrix[row + i, col] = word[i];
                                }
                                return;
                            }
                        }
                    }
                }
        }


        private static bool HasRoomUpwardsLeft(char[,] matrix, int row, int col, string wordValue,
            int amountOfRowsAndColumns)
        {
            var amountOfSpaces = 0;
            for (var i = 0; i < wordValue.Length; i++)
            {
                if (row + i >= amountOfRowsAndColumns) break;
                if (matrix[row+i, col] == '_')
                    amountOfSpaces++;
            }

            return amountOfSpaces >= wordValue.Length;
        }

        private static bool HasRoomDownwardsLeft(char[,] matrix, int row, int col, string wordValue)
        {
            var amountOfSpaces = 0;
            for (var i = wordValue.Length; i > 0; i--)
            {
                if (row - i < 0) break;
                if (matrix[row -i, col] == '_')
                    amountOfSpaces++;
            }

            return amountOfSpaces >= wordValue.Length;
        }

        private static bool HasRoomToTheLeft(char[,] matrix, int row, int col, string wordValue)
        {
            var amountOfSpaces = 0;
            for (var i = wordValue.Length; i > 0; i--)
            {
                if (col - i < 0) break;
                if (matrix[row, col - i] == '_')
                    amountOfSpaces++;
            }

            return amountOfSpaces >= wordValue.Length;
        }

        private static bool HasRoomToTheRight(char[,] matrix, int row, int col, string wordValue,
            int amountOfRowsAndColumns)
        {
            var amountOfSpaces = 0;
            for (var i = 0; i < wordValue.Length; i++)
            {
                if (col + i >= amountOfRowsAndColumns) break;
                if (matrix[row, col + i] == '_')
                    amountOfSpaces++;
            }

            return amountOfSpaces >= wordValue.Length;
        }


        private static void FillMatrixWithPlaceholders(int amountOfRowsAndColumns, char[,] matrix)
        {
            for (var row = 0; row < amountOfRowsAndColumns; row++)
            {
                for (var col = 0; col < amountOfRowsAndColumns; col++)
                {
                    matrix.SetValue('_', row, col);
                }
            }
        }

        private static void AddStarterVerticalWord(string input, int amountOfRowsAndColumns, int charIndex, char[,] matrix)
        {
            var randomColum = Random.Range(0, amountOfRowsAndColumns - 1);

            for (var row = 0; row < amountOfRowsAndColumns; row++)
            {
                if (charIndex < input.Length)
                {
                    matrix.SetValue(input[charIndex], row, randomColum);
                    charIndex++;
                }
                else
                {
                    break;
                }
            }
        }

        private static void AddStarterHorizontalWord(string input, int amountOfRowsAndColumns, int charIndex, char[,] matrix)
        {
            var randomRow = Random.Range(0, amountOfRowsAndColumns - 1);

            for (var col = 0; col < amountOfRowsAndColumns; col++)
            {
                if (charIndex < input.Length)
                {
                    matrix.SetValue(input[charIndex], randomRow, col);
                    charIndex++;
                }
                else
                {
                    break;
                }
            }
        }

        private bool IsHorizontal(int randomAxis) => randomAxis == 0;

    }
}