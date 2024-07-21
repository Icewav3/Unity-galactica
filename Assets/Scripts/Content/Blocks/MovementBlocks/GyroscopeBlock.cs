using UnityEngine;

namespace Content.Blocks.MovementBlocks
{
    public class GyroscopeBlock : MovementBlock
    {
        [SerializeField] private float rotationPower = 0f;

        public float RotationPower => rotationPower;
    }
}