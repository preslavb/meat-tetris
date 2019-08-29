using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.GameManagerDefinitions;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class BoardRenderer : SerializedMonoBehaviour
{
    
    // The instance of the board manager
    [ShowInInspector]
    private BoardManager _boardManager;
    
    // The list of cage assets
    [OdinSerialize]
    [PreviewField]
    private List<GameObject> _cageAssets = new List<GameObject>(4);
    
    // On start, render the board on screen
    private void Start()
    {
        // DEBUG: Test the rendering 
        _boardManager.Board.InsertBlockAt(7, 9);
    }
    
    // Render the board
}
