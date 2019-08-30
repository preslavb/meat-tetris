using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Block Selector", fileName = "Block Selector", order = 1)]
    public class BlockSelector: ScriptableObject
    {

        private float size = 7.25f;
        
        // The selector prefab
        [AssetsOnly]
        public GameObject selectorPrefab;
        
        // The spawn point
        [SceneObjectsOnly]
        public GameObject spawnPoint;

        // Select the block at the specified location
        public GameObject GetBoxAtCoords(int x, int y)
        {
            var position = spawnPoint.transform.position + new Vector3(size / 2, -size / 2);

            position.x += x * size;
            position.y -= (y + 2) * size;
            
            
            // Instantiate a selector there
            var tempSelector = Instantiate(selectorPrefab, position, Quaternion.identity);
            
            // Container for collisions
            List<Collider2D> collider2Ds = new List<Collider2D>();
            
            // The contact filter
            var contactFilter = new ContactFilter2D
            {
                useTriggers = true,
                useLayerMask = true,
                layerMask = 1 << 8
            };

            tempSelector.GetComponent<BoxCollider2D>().OverlapCollider(contactFilter, collider2Ds);

            var boxFound = collider2Ds.Count > 0 ? collider2Ds[0].transform.parent : null;
            
            Destroy(tempSelector);
            
            // Get the first contact and return the object
            return boxFound == null ? null : boxFound.gameObject;
        }
        
        // Select the chickens at the specified location
        public GameObject[] GetChickensAtCoords(int x, int y)
        {
            var position = spawnPoint.transform.position + new Vector3(size / 2, -size / 2);

            position.x += x * size;
            position.y -= (y + 2) * size;
            
            
            // Instantiate a selector there
            var tempSelector = Instantiate(selectorPrefab, position, Quaternion.identity);
            
            // Container for collisions
            List<Collider2D> collider2Ds = new List<Collider2D>();
            
            // The contact filter
            var contactFilter = new ContactFilter2D
            {
                useTriggers = true,
                useLayerMask = true,
                layerMask = 1 << 10
            };

            tempSelector.GetComponent<BoxCollider2D>().OverlapCollider(contactFilter, collider2Ds);
            
            // Get the first contact and return the object
            return collider2Ds.Select(collider2d => collider2d.gameObject).ToArray();
        }
    }
}