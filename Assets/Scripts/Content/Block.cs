using UnityEngine;

namespace Content
{
    public abstract class Block : MonoBehaviour
    {
        protected float Health = 100;
        protected float Mass = 1; //update these to realistic values
        protected int Cost;

        // Add any common properties or methods for all blocks here.
    }

    public enum AimType
    {
        Fixed,
        Turret,
        Cursor
    }
}