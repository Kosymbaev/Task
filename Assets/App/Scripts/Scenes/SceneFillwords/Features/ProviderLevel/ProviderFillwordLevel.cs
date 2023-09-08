using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using App.Scripts.Scenes.SceneFillwords.Features.FillwordModels;
using Assets.App.Scripts.Scenes.SceneFillwords.Features.FillwordModels;

namespace App.Scripts.Scenes.SceneFillwords.Features.ProviderLevel
{
    public class ProviderFillwordLevel : IProviderFillwordLevel
    {
        private string ReadingLine(string path, int index)
        {
            string result;
            using (var reader = new StreamReader(path))
            {
                for (int i = 0; i < index - 1; i++)
                {
                    reader.ReadLine();
                }
                result = reader.ReadLine();
            }
            return result;
        }
        private List<int[]> ParseOfLevels(string indexes)
        {
            List<int[]> ints = new List<int[]>();
            int[] integ = new int[indexes.Length / 2 + 1];
            int j = 0, space = 0, cur = 0;
            for (int i = 0; i < indexes.Length; i++)
            {
                string ParseInt;
                if (indexes[i] == ' ' && space == 0)
                {

                    ParseInt = indexes.Substring(cur, i - cur);
                    space++;
                    cur = i + 1;
                    integ[j] = Int32.Parse(ParseInt);
                    j++;
                    continue;
                }
                if (indexes[i] == ';')
                {
                    ParseInt = indexes.Substring(cur, i - cur);
                    cur = i + 1;
                    integ[j] = Int32.Parse(ParseInt);
                    j++;
                    continue;
                }
                if (indexes[i] == ' ' && space != 0)
                {
                    ParseInt = indexes.Substring(cur, i - cur);
                    space++;
                    cur = i + 1;
                    integ[j] = Int32.Parse(ParseInt);
                    j++;
                    if (space % 2 == 0)
                    {
                        int[] postintfor = new int[j];
                        Array.Copy(integ, postintfor, j);
                        ints.Add(postintfor);
                        integ = new int[indexes.Length / 2];
                        j = 0;
                    }
                    continue;

                }
                if (i == indexes.Length - 1)
                {
                    ParseInt = indexes.Substring(cur, i - cur + 1);
                    integ[j] = Int32.Parse(ParseInt);
                    j++;
                }
            }
            int[] postint = new int[j];
            Array.Copy(integ, postint, j);
            ints.Add(postint);
            return ints;
        }

        public GridFillWords LoadModel(int index)
        {
            string curentPath = Directory.GetCurrentDirectory();
            string indexes, pathToPack_0 = "D:\\Task\\Assets\\App\\Resources\\Fillwords\\pack_0.txt";
            string pathToWordsList = "D:\\Task\\Assets\\App\\Resources\\Fillwords\\words_list.txt";

            try
            {
                indexes = ReadingLine(pathToPack_0, index);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            List<int[]> IndexOfWords = ParseOfLevels(indexes);
            var valid = new Validation(IndexOfWords);
            if (valid.IsValid())
            {
                return null;
            }
            int SizeOfGrid = valid.getSizeOfGrid();
            var Size = new Vector2Int(SizeOfGrid, SizeOfGrid);
            var grid = new GridFillWords(Size);
            foreach (var item in IndexOfWords)
            {

                string Word = ReadingLine(pathToWordsList, item[0]);
                for (int k = 1; k < item.Length; k++)
                {
                    int i = 0, j = 0;
                    for (int l = 0; l < item[k]; l++)
                    {

                        j++;
                        if (j == SizeOfGrid)
                        {
                            i++;
                            j = 0;
                        }
                    }
                    var Char = new CharGridModel(Word[k - 1]);
                    grid.Set(i, j, Char);
                }
            }
            return grid;
            //напиши реализацию не меняя сигнатуру функции
        }
    }
}