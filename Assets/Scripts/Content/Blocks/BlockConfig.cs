using System.Collections.Generic;
using UnityEngine;

namespace Content
{
    [CreateAssetMenu(fileName = "BlockConfig", menuName = "Block Configuration")]
    public class BlockConfig : ScriptableObject
    {
        public float maxHealth = 100f;
        public float mass = 100f;
        public int cost = 100;
        public float value = 1f;
        public string blockName = "Placeholder Name";
        [TextArea] public string blockDescription = "If you see this, this prefab is missing a description";
        public string blockRole = "Purpose of the block";
        public List<Vector2> attachPoints;

        public AimType aimType;
        public ThrusterType thrusterType;
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