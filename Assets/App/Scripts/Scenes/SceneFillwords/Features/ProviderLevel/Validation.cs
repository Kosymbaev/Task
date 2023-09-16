using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Assets.App.Scripts.Scenes.SceneFillwords.Features.FillwordModels
{
    public class Validation
    {
        private List<int[]> _indexOfWords;
        private int _sizeOfGrid;
        private int _maxNumber;
        private bool _isValid;
        private int[] allNumbers;
        private int _countOfIndexes;
        public Validation(List<int[]> indexOfWords)
        {
            _isValid = true;
            _indexOfWords = indexOfWords;
            _sizeOfGrid = 0;
            _maxNumber = 0;
            FindMaxIndex(_indexOfWords);
            _countOfIndexes = CountIndexes(_indexOfWords);
            var arrayOfNummbers = new int[_countOfIndexes];
            allNumbers = arrayOfNummbers;
            _sizeOfGrid = (int)Math.Pow(_countOfIndexes, 0.5);

            SetAllNumbers();
            IsValidNumbers();
            IsValidSize();
        }
        private void IsValidNumbers()
        {
            for (int i = 0; i < allNumbers.Length - 1; i++)
            {
                for (int j = i + 1; j < allNumbers.Length; j++)
                {
                    if (allNumbers[i] == allNumbers[j])
                    {
                        _isValid = false;
                    }
                }
            }
            if (_countOfIndexes != _maxNumber-1)
            {
                _isValid = false;
            }
        }
        private void IsValidSize()
        {
            if (Math.Pow(_sizeOfGrid, 2) - _countOfIndexes != 0)
            {
                _isValid = false;
            }
        }
        private void SetAllNumbers()
        {
            int l = 0;
            foreach (var item in _indexOfWords)
            {
                for (int i = 1; i < item.Length; i++)
                {
                    allNumbers[l] = item[i];
                    l++;
                }
            }
        }
        private int CountIndexes(List<int[]> IndexOfWords)
        {
            int NumbersOfIndexes = 0;
            foreach (var item in IndexOfWords)
            {
                for (int i = 1; i < item.Length; i++)
                {
                    NumbersOfIndexes++;
                }
            }
            return NumbersOfIndexes;
        }

        private void FindMaxIndex(List<int[]> IndexOfWords)
        {
            foreach (var item in IndexOfWords)
            {
                for (int i = 1; i < item.Length; i++)
                {
                    if (_maxNumber < item[i])
                    {
                        _maxNumber = item[i];
                    }
                }
            }
        }
        public bool IsValid()
        {
            return _isValid;
        }
        public int GetSizeOfGrid()
        {
            return _sizeOfGrid;
        }
    }
}
