using UnityEngine;

namespace Util
{
    public static class Tools
    {
        public static string timeFloatToString(float time)
        {
            int totalSeconds = Mathf.FloorToInt(time);
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;

            return $"{minutes:00}:{seconds:00}";
        }
    }
}