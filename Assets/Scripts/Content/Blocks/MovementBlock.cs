using System;
using Content;
using static Content.Fuel;
namespace Content.Blocks
{
    public abstract class MovementBlock : Block
    {
        public FuelType fuel = FuelType.ElectricCharge;
        public float heatGeneration = 1f;
        
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