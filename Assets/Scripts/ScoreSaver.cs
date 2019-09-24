using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Score Saver", order = 2)]
    public class ScoreSaver : ScriptableObject
    {
        public float score;

        public float KilosOfMeat => 1.5f * score;

        public float Co2Produced => 6.855f * score;
    }
}