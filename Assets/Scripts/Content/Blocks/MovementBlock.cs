using Content;
using static Content.Fuel;
namespace Content.Blocks
{
    public class MovementBlock : Block
    {
        public Fuel fuelType;
        public float heatGeneration = 0f;
        public float thrustPower = 0f;
        public float rotationPower = 0f;

        //TODO REMOVE THIS?
        /*public MovementBlock(Fuel fuelType, float heatGeneration, float thrustPower, float rotationPower)
        {
            FuelType = fuelType;
            HeatGeneration = heatGeneration;
            ThrustPower = thrustPower;
            RotationPower = rotationPower;
        }*/
    }
}