using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Features.Board.Scripts.Domain
{
    public class BuildMatrix: IBuildMatrix
    {
        public List<char> Execute(string input, int amountOfRowsAndColumns)
        {
            var matrix = new char[amountOfRowsAndColumns, amountOfRowsAndColumns];

            for (var row = 0; row < amountOfRowsAndColumns; row++)
            {
                for (var col = 0; col < amountOfRowsAndColumns; col++)
                {
                    matrix.SetValue('_', row, col);
                }
            }

            var charIndex = 0;
            var randomAxis = Random.Range(0, 2);
            if (IsHorizontal(randomAxis))
            {
                var randomRow = Random.Range(0, amountOfRowsAndColumns -1);
            
                for (var col = 0; col < amountOfRowsAndColumns; col++)
                {
                    if (charIndex < input.Length)
                    {
                        matrix.SetValue(input[charIndex],randomRow, col) ;
                        charIndex++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                var randomColum = Random.Range(0, amountOfRowsAndColumns -1);
            
                for (var row = 0; row < amountOfRowsAndColumns; row++)
                {
                    if (charIndex < input.Length)
                    {
                        matrix.SetValue(input[charIndex],row, randomColum) ;
                        charIndex++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return matrix.Cast<char>().ToList();
        }
        private bool IsHorizontal(int randomAxis) => randomAxis == 0;

    }
}