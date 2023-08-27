using UnityEngine;

namespace Content
{
    public abstract class Block : MonoBehaviour
    {
        [SerializeField]
        protected float health = 100;
        [SerializeField]
        protected float mass = 1;//update these to realistic values
        [SerializeField]
        protected int cost;

        // Add any common properties or methods for all blocks here.
    }

    public enum AimType
    {
        Fixed,
        Turret,
        Cursor
    }
}