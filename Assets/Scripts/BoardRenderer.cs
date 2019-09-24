using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [ShowInInspector]
    private RandomManager _randomManager;

    [OdinSerialize] 
    [SceneObjectsOnly] 
    [ShowInInspector]
    private GameObject cutter;
    
    [OdinSerialize] 
    [SceneObjectsOnly] 
    [ShowInInspector]
    private GameObject pusher;

    [OdinSerialize]
    [AssetsOnly] 
    [ShowInInspector]
    private List<Sprite> goreSprites;

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
    
    // Smash the boxes
    public void Smash()
    {
        // Get all the shapes on the screen
        var shapes = GameObject.FindObjectsOfType<ShapeView>();

        shapes = shapes.Where(shapeView => !shapeView._isRelevant).ToArray();
        
        // Get all of the boxes in the shapes
        var boxes = shapes.Select(shape => shape.transform).SelectMany(shape => shape.Cast<Transform>().Select(child => child)).ToArray();
        var boxCount = boxes.Length;

        // Arrange the boxes when the event fires
        StartCoroutine(SmashCoroutine(boxes, boxCount));
    }

    private IEnumerator SmashCoroutine(Transform[] boxes, int count)
    {
        yield return new WaitForSeconds(0.334f);

        var size = 7.25f;

        for (int i = 0; i < count; i++)
        {
            var row = 9 - Mathf.Floor(i / 9);

            var position = SpawnPoint.position + new Vector3(size / 2, -size / 2) + new Vector3(0, -size * 2);
            
            position += new Vector3((i % 8) * size, - row * size);

            boxes[i].position = position;
            
            if (boxes[i].GetComponentInChildren<EyeSpriteController>() == null)
                continue;

            var gore = boxes[i].Find("Gore");

            gore.gameObject.SetActive(true);
            gore.GetComponent<SpriteRenderer>().sprite = goreSprites[_randomManager.Random.Next(0, goreSprites.Count)];
            gore.rotation = Quaternion.identity;
            Destroy(boxes[i].GetComponentInChildren<EyeSpriteController>()?.gameObject);
        }
    }
}
