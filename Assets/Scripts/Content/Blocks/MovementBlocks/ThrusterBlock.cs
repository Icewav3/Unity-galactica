using UnityEngine;

namespace Content.Blocks.MovementBlocks
{
    public class ThrusterBlock : MovementBlock
    {
        [SerializeField] private ThrusterType type = ThrusterType.Fixed;
        [SerializeField] private float thrustPower = 1f;

        public ThrusterType Type => type;
        public float ThrustPower => thrustPower;
    }
}