namespace Content.Blocks
{
    public class WeaponBlock : Block
    {
        public float heatGenerationPerShot = 1f;
        public float recoil = 1f;
        public AimType aimType = AimType.Fixed;

        /*public WeaponBlock(float heatGeneration, float recoil, AimType aimType)
        {
            HeatGenerationPerShot = HeatGenerationPerShot;
            Recoil = recoil;
            AimType = aimType;
        }*/
    }
}