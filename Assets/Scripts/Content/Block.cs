using System;
using Unity.VisualScripting;
using UnityEngine;
using Utilities.Util;

namespace Content
{
    public abstract class Block : MonoBehaviour
    {
        [SerializeField] protected float health = 100f;

        [SerializeField] protected float mass = 100f;//update these to realistic values

        [SerializeField] protected int cost = 100;

        // Add any common properties or methods for all blocks here.
        /// <summary>
        /// Initialization Function
        /// </summary>
        public void Start()
        {
            
            //TODO how on earth do I setup attach points?
            //stuck between auto generating with max of 4, or trying to add manually to all prefabs. What if we want octagon blocks or Triangles???
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