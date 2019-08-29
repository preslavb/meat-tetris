using System;
using UnityEngine;
using Random = System.Random;

namespace DefaultNamespace.GameManagerDefinitions
{
    [CreateAssetMenu(menuName = "Managers/Random Manager", fileName = "Random Manager", order = 1)]
    public class RandomManager: GenericManager
    {
        public override ManagerType ManagerType => ManagerType.RandomManager;
        
        
        public override void Setup()
        {
        }

        // The random instance
        [NonSerialized]
        public Random Random;

        private void OnEnable()
        {
            Random = new Random();
        }
    }
}