using UnityEngine;

namespace Content.Blocks
{
    public abstract class MovementBlock : Block
    {
        [SerializeField] private FuelType fuel = FuelType.ElectricCharge;
        [SerializeField] private float fuelConsumption = 1f;
        [SerializeField] private float heatGeneration = 1f;

        public FuelType Fuel => fuel;
        public float FuelConsumption => fuelConsumption;
        public float HeatGeneration => heatGeneration;
    }
}