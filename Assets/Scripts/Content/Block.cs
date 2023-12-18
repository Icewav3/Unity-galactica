using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Content
{
    public abstract class Block : MonoBehaviour
    {
        [SerializeField] public float health = 100f;

        [SerializeField] public float mass = 100f;//update these to realistic values

        [SerializeField] public int cost = 100;
        
        [SerializeField] public float value = 1f; //value is for matchmaking and spawning rules, remove if not needed

        public List<Vector2> attachPoints;

        // Add any common properties or methods for all blocks here.
        /// <summary>
        /// Initialization Function
        /// </summary>
        public void Start()
        {
            //TODO wip connection points
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