using System;
using System.Collections.Generic;
using App.Scripts.Libs.Factory;
using App.Scripts.Scenes.SceneWordSearch.Features.Level.Models.Level;

namespace App.Scripts.Scenes.SceneWordSearch.Features.Level.BuilderLevelModel
{
    public class FactoryLevelModel : IFactory<LevelModel, LevelInfo, int>
    {
        public LevelModel Create(LevelInfo value, int levelNumber)
        {
            var model = new LevelModel();

            model.LevelNumber = levelNumber;

            model.Words = value.words;
            model.InputChars = BuildListChars(value.words);

            return model;
        }

        private List<char> BuildListChars(List<string> words)
        {
            Dictionary<char,int> dict = new Dictionary<char, int>();
            List<char> letters = new List<char>();
            foreach (var word in words)
            {
                string bufferword=word;
                for (int i=0; i<bufferword.Length;i++)
                {
                    int countOfLetter = 0;
                    for (int j=i+1; j<bufferword.Length; j++)
                    {
                        if (bufferword[i] == bufferword[j])
                        {
                            countOfLetter++;
                            bufferword.Remove(j,1);
                            j--;
                        }
                    }
                    bool SucsessAdd=dict.TryAdd(bufferword[i], countOfLetter+1);
                    if (!SucsessAdd)
                    {
                        if (dict[bufferword[i]] < countOfLetter)
                        {
                            dict[bufferword[i]] = countOfLetter;
                        }
                    }
                    else
                    {
                        letters.Add(bufferword[i]);
                    }
                    bufferword = bufferword.Remove(i, 1);
                    i--;
                }
            }
            List<char> result = new List<char>();
            foreach (var letter in letters)
            {
                int count = dict[letter];
                for (int i=0;i<count;i++)
                {
                    result.Add(letter);
                }
            }
            return result;
            //напиши реализацию не меняя сигнатуру функции
            throw new NotImplementedException();
        }
    }
}