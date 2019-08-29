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
    [OdinSerialize] 
    private BoardManager _boardManager;
    
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
    
    // Render the board
}
