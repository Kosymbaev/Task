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
        private List<int[]> _IndexOfWords;
        private int _SizeOfGrid;
        private int _MaxNumber;
        private bool _IsValid;
        private int[] AllNumbers;
        private int _CountOfIndexes;
        public Validation(List<int[]> IndexOfWords)
        {
            _IsValid = true;
            _IndexOfWords = IndexOfWords;
            _SizeOfGrid = 0;
            _MaxNumber = 0;
            findMaxIndex(_IndexOfWords);
            _CountOfIndexes = countIndexes(_IndexOfWords);
            var arrayOfNummbers = new int[_CountOfIndexes];
            _SizeOfGrid = (int) Math.Pow(_CountOfIndexes,0.5);
            SetAllNumbers();
            IsValidNumbers();
            IsValidSize();
        }
        private void IsValidNumbers()
        {
            for (int i = 0; i < AllNumbers.Length - 1; i++)
            {
                for (int j = i + 1; j < AllNumbers.Length; j++)
                {
                    if (AllNumbers[i] == AllNumbers[j])
                    {
                        _IsValid = false;
                    }
                }
            }
            if (_CountOfIndexes != _MaxNumber-1)
            {
                _IsValid = false;
            }
        }
        private void IsValidSize()
        {
            if (Math.Pow(_SizeOfGrid, 2) - _CountOfIndexes != 0)
            {
                _IsValid = false;
            }
        }
        private void SetAllNumbers()
        {
            int l = 0;
            foreach (var item in _IndexOfWords)
            {
                for (int i = 1; i < item.Length; i++)
                {
                    AllNumbers[l] = item[i];
                    l++;
                }
            }
        }
        private int countIndexes(List<int[]> IndexOfWords)
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

        private void findMaxIndex(List<int[]> IndexOfWords)
        {
            foreach (var item in IndexOfWords)
            {
                for (int i = 1; i < item.Length; i++)
                {
                    if (_MaxNumber < item[i])
                    {
                        _MaxNumber = item[i];
                    }
                }
            }
        }
        public bool IsValid()
        {
            return _IsValid;
        }
        public int getSizeOfGrid()
        {
            return _SizeOfGrid;
        }
    }
}
