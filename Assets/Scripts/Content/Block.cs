using System.Collections.Generic;
using UnityEngine;

namespace Content
{
    public abstract class Block : MonoBehaviour //TODO https://chat.openai.com/c/2b272a65-a220-45dd-9053-fab9e7e144a0
    {
        //todo idea implement CPU block that has a reccomended block management value that will control how responsive the ship is, 
        
        [SerializeField] public float health = 100f;

        [SerializeField] public float maxHealth = 100f;

        [SerializeField] public float mass = 100f; //update these to realistic values

        [SerializeField] public int cost = 100;

        [SerializeField] public float value = 1f; //value is for matchmaking and spawning rules, remove if not needed

        [SerializeField] public string blockName = "Placeholder Name";

        [TextArea] [SerializeField]
        public string blockDescription = "If you see this, this prefab is missing a description";

        [SerializeField] public string blockRole = "Purpose of the block";

        public List<Vector2> attachPoints;

        // Add any common properties or methods for all blocks here.
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