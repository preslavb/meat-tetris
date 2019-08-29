using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace.GameManagerDefinitions
{
    public abstract class GenericManager : SerializedScriptableObject
    {
        
        // The manager type of this manager
        public abstract ManagerType ManagerType { get; }
        
        // Set up the manager
        public abstract void Setup();
        
        // Update logic
        public virtual void Update() {}
    }
}