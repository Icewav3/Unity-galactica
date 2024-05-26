using UnityEngine;

namespace Content.Blocks.MovementBlocks
{
    public class ThrusterBlock : MovementBlock
    {
        public ThrusterType type = ThrusterType.Fixed;
        public float thrustPower = 1f;
        public Vector2 direction = Vector2.up;
    }
}