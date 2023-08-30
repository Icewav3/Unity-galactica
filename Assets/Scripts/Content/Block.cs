using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Content
{
    public abstract class Block : MonoBehaviour
    {
        [SerializeField] protected float health = 100f;

        [SerializeField] protected float mass = 100f;//update these to realistic values

        [SerializeField] protected int cost = 100;

        public Collider2D attachHitbox;

        // Add any common properties or methods for all blocks here.
        /// <summary>
        /// Initialization Function
        /// </summary>
        public void Start()
        {
            //TODO wip
            GameObject me = gameObject;
            //Collider2D hitboxObject = new Collider2D();
            //hitboxObject = Instantiate(attachHitbox);
            me.transform.SetParent(me.transform);
            Helper.Util.DisableChildrenWithName(me.transform, "AttachPoint");
        }
    }

    public enum AimType
    {
        Fixed,
        Turret,
        Cursor
    }

    public enum ThrusterType
    {
        Fixed,
        Gimballed,
        OmniDirectional
    }
}