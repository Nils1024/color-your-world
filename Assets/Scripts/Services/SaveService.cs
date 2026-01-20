
using System;

namespace Services
{
    public static class SaveService
    {
        public const string SAVEDATA_FILENAME = "/DATA.json";
    }
    
    [Serializable]
    public class LevelData
    {
        public string levelName;
    }
}

