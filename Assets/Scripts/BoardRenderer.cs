using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.GameManagerDefinitions;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEngine;

public class BoardRenderer : SerializedMonoBehaviour
{
    
    // The instance of the board manager
    [ShowInInspector]
    [OdinSerialize] 
    private BoardManager _boardManager;

    [OdinSerialize] 
    [ShowInInspector]
    private BlockSelector _blockSelector;

    [OdinSerialize] 
    [SceneObjectsOnly] 
    [ShowInInspector]
    private GameObject cutter;
    
    [OdinSerialize] 
    [SceneObjectsOnly] 
    [ShowInInspector]
    private GameObject pusher;
    
    // The list of cage assets
    [OdinSerialize]
    [PreviewField(alignment: ObjectFieldAlignment.Left)]
    [AssetsOnly]
    private List<GameObject> _cageAssets = new List<GameObject>(4);

    [OdinSerialize]
    [AssetsOnly] 
    private GameObject ShapePrefab;

    [OdinSerialize]
    [SceneObjectsOnly]
    private Transform SpawnPoint;

    [OdinSerialize]
    public (int, int) GridSize;
    
    // The currently controlled shape
    
    // On start, render the board on screen
    private void Start()
    {
        CreateNewShapeView();
        _boardManager.ShapeDoneDropping += CreateNewShapeView;
    }
    
    // Create a new shape view
    private void CreateNewShapeView()
    {
        // Create a shape for the next board shape
        var newShapeView = Instantiate(ShapePrefab);

        // Initialize shape prefab
        newShapeView.GetComponent<ShapeView>().Initialize(_boardManager.CurrentShape, SpawnPoint);
    }
    
    // Detach the bottom of the board
    public void DetachBottom()
    {
        // Enable the cutter
        cutter.SetActive(true);
        
        // The last block
        
        for (int xIndex = 0; xIndex < _boardManager.Board.Representation[0].Count; xIndex++)
        {
            // Get the bottom and enable all of its children
            var bottom = _blockSelector.GetBoxAtCoords(xIndex, 9);
            // var chickens = _blockSelector.GetChickensAtCoords(xIndex, 9);
            
            if (bottom == null)
                continue;

            bottom.transform.SetParent(pusher.transform);
        }
        
        // Disable the cutter
        cutter.SetActive(false);
    }
}
