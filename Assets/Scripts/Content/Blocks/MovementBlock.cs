namespace Content.Blocks
{
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
}