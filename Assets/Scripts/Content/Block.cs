using System.Collections.Generic;
using SceneManagement;
using UnityEngine;

namespace Content
{
    public abstract class
        Block : MonoBehaviour
    {
        //todo component ize this stuff
        
        //todo idea implement mainframe block should add a score (CPU) which is used to determine the effectiveness of functional blocks. Each functional block has a processing score which indicates how much CPU it needs to work at max efficiency. The percentage over 100%  will be the scaling modifier of the block's effectiveness

        // [SerializeField] public float health = 100f;

        [SerializeField] public float maxHealth = 100f;

        [SerializeField]
        public float mass = 100f; //update these to realistic values

        [SerializeField] public int cost = 100;

        [SerializeField] public float
            value = 1f; //value is for matchmaking and spawning rules, remove if not needed

        [SerializeField] public string blockName = "Placeholder Name";

        [TextArea] [SerializeField] public string blockDescription =
            "If you see this, this prefab is missing a description";

        [SerializeField] public string blockRole = "Purpose of the block";

        public List<Vector2> attachPoints;
        
        /// <summary>
        /// Destroys the block object. If debug mode is enabled, it will log the block name to the console.
        /// </summary>
        public void DestroyBlock()
        {
            if (StateMachine.DebugMode)
            {
                Debug.Log("Destroyed block: " + blockName);
            }
            Destroy(gameObject);
        }
        
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
    }

    public enum AimType //TODO create aimtype class
    {
        Fixed,
        Turret,
        Cursor
    }

    public enum ThrusterType //todo create Thrustertype class
    {
        Fixed,
        Gimballed,
        OmniDirectional
    }
}