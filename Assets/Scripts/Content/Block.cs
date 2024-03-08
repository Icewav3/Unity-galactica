using SceneManagement;
using UnityEngine;

namespace Content
{
    public abstract class Block : MonoBehaviour
    {
        [SerializeField] private BlockConfig blockConfig;

        /// <summary>
        /// Initialization Function
        /// </summary>
        public void Start()
        {
            GameObject me = gameObject;
            //Collider2D hitboxObject = new Collider2D();
            //hitboxObject = Instantiate(attachHitbox);
            me.transform.SetParent(me.transform);
            Helper.Util.DisableChildrenWithName(me.transform, "AttachPoint");
        }

        /// <summary>
        /// Destroys the block object. If debug mode is enabled, it will log the block name to the console.
        /// </summary>
        public void DestroyBlock()
        {
            if (StateMachine.DebugMode)
            {
                Debug.Log("Destroyed block: " + blockConfig.blockName);
            }

            Destroy(gameObject);
        }
    }
}