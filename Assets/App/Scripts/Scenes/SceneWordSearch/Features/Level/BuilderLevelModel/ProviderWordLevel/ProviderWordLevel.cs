using System;
using App.Scripts.Scenes.SceneWordSearch.Features.Level.Models.Level;
using System.IO;
using UnityEngine;

namespace App.Scripts.Scenes.SceneWordSearch.Features.Level.BuilderLevelModel.ProviderWordLevel
{
    public class ProviderWordLevel : IProviderWordLevel
    {
        public LevelInfo LoadLevelData(int levelIndex)
        {
            string pathToLevel = Directory.GetCurrentDirectory() + "\\Assets\\App\\Resources\\WordSearch\\Levels\\"+levelIndex+".json";
            string ContainJson = File.ReadAllText(pathToLevel);
            LevelInfo levelInfo = new LevelInfo();
            levelInfo = JsonUtility.FromJson<LevelInfo>(ContainJson);
            return levelInfo;
            //напиши реализацию не меняя сигнатуру функции
            throw new NotImplementedException();
        }
    }
}