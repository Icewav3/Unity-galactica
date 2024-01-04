using System;
using Content;
using static Content.Fuel;
namespace Content.Blocks
{
    public abstract class MovementBlock : Block
    {
        public FuelType fuel = FuelType.ElectricCharge;
        public float fuelConsumption = 1f;
        public float heatGeneration = 1f;
    }
}