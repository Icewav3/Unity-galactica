namespace Content.Blocks
{
    public class WeaponBlock : Block
    {
        public float HeatGenerationPerShot { get; }
        public float Recoil { get; }
        public AimType AimType { get; }

        public WeaponBlock(float heatGeneration, float recoil, AimType aimType)
        {
            HeatGenerationPerShot = HeatGenerationPerShot;
            Recoil = recoil;
            AimType = aimType;
        }
    }
}