using UnityEngine;

namespace Content
{
    public abstract class Block : MonoBehaviour
    {
        [SerializeField] protected float health = 100f;

        [SerializeField] protected float mass = 100f;//update these to realistic values

        [SerializeField] protected int cost = 100;

        // Add any common properties or methods for all blocks here.
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