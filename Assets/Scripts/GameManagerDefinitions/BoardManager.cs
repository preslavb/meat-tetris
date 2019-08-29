using Code;
using Sirenix.Serialization;
using UnityEngine;

namespace DefaultNamespace.GameManagerDefinitions
{
    [CreateAssetMenu(menuName = "Managers/Board Manager", fileName = "Board Manager", order = 0)]
    public class BoardManager: GenericManager
    {
        public override ManagerType ManagerType => ManagerType.BoardManager;
        
        // The board dimensions setup
        [OdinSerialize] private int width; 
        [OdinSerialize] private int height; 
        
        // The board
        public Board Board;
        
        public override void Setup()
        {
            Board = new Board(width, height);
        }
    }
}