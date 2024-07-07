namespace Content.Blocks
{
    public class WeaponBlock : Block
    {
        /// <summary>
        /// The rotation speed of the weapon block. In Degrees/Second
        /// </summary>
        public float rotationSpeed = 180f;
        public float maxRange = 100;
        public float heatGenerationPerShot = 1f;
        public float recoil = 1f;
        public AimType aimType = AimType.Fixed;
    }
}