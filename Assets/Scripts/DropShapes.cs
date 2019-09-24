using Sirenix.Utilities;
using UnityEngine;

namespace DefaultNamespace
{
    public class DropShapes : MonoBehaviour
    {
        public Animator slurryAnimator;

        public void StartSlurry()
        {
            GetComponent<AudioSource>().Play();
            slurryAnimator.SetTrigger("Start");
        }

        public void Drop()
        {
            var gameObjects = GameObject.FindObjectsOfType<ShapeView>();
            
            
            gameObjects.ForEach(gameObject1 =>
            {
                if (gameObject1.GetComponent<ShapeView>()._isRelevant)
                    return;
                
                gameObject1.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            });
        }
    }
}