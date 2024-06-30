namespace Content.Blocks
{
    public class WeaponBlock : Block
    {
        public float rotationSpeed = 1f;
        public float heatGenerationPerShot = 1f;
        public float recoil = 1f;
        public AimType aimType = AimType.Fixed;
    }
}