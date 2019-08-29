using System.Collections.Generic;
using DefaultNamespace.GameManagerDefinitions;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    
    public class GameManager : SerializedMonoBehaviour
    {
        
        // The array of submanagers (assigned in Unity)
        public Dictionary<ManagerType, GenericManager> managers;
        
        // Initialize the managers
        private void Awake()
        {
            managers.ForEach(manager => manager.Value.Setup());
        }
    }
}