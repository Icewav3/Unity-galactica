namespace Content
{
    public abstract class Block
    {
        protected float Health = 100;
        protected float Mass = 1; //update these to realistic values
        protected int cost;

        // Add any common properties or methods for all blocks here.
    }

    public class MovementBlock : Block
    {
        public Fuel FuelType { get; }
        public float HeatGeneration { get; }
        public float ThrustPower { get; }
        public float RotationPower { get; }

        public MovementBlock(Fuel fuelType, float heatGeneration, float thrustPower, float rotationPower)
        {
            FuelType = fuelType;
            HeatGeneration = heatGeneration;
            ThrustPower = thrustPower;
            RotationPower = rotationPower;
        }
    }

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

    public class FuelTankBlock : Block
    {
        public bool IsExplosive { get; }
        public float MaxFuel { get; }
        public float CurrentFuel { get; private set; }

        public FuelTankBlock(bool isExplosive, float maxFuel)
        {
            IsExplosive = isExplosive;
            MaxFuel = maxFuel;
            CurrentFuel = 0;
        }
    }

    public enum AimType
    {
        Fixed,
        Turret,
        Cursor
    }
}