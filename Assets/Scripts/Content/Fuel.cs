using System.Collections.Generic;
using UnityEngine;

namespace Content
{
    public class Fuel
    {
        public string FuelName { get; }
        public Color FuelColor { get; }
        public float BasePricePerUnit { get; }

        public Fuel(string fuelName, Color fuelColor, float basePricePerUnit)
        {
            FuelName = fuelName;
            FuelColor = fuelColor;
            BasePricePerUnit = basePricePerUnit;
        }

        public float CalculateSellPrice(float economyFactor)
        {
            // Calculate sell price based on economy factor and base price
            return BasePricePerUnit * economyFactor;
        }
        // Add any other methods or properties related to Fuel here.
    }

    public class ElectricCharge : Fuel
    {
        public ElectricCharge() : base("Electric Charge", new Color(1.0f, 0.8f, 0.4f), 0f)
        {
            // Additional initialization specific to ElectricCharge class if needed.
        }
    }
    public class HydrogenFuel : Fuel
    {
        public HydrogenFuel() : base("Hydrogen Fuel", new Color(0.0f, 0.5f, 1.0f), 1f)
        {
            // Additional initialization specific to HydrogenFuel class if needed.
        }
    }

    public class PetroleumFuel : Fuel
    {
        public PetroleumFuel() : base("Petroleum Fuel", new Color(1.0f, 0.1f, 0.1f), 2f)
        {
            // Additional initialization specific to PetroleumFuel class if needed.
        }
    }
    public static class FuelManager
    {
        private static List<Fuel> fuelTypes = new List<Fuel>();

        public static Fuel ElectricCharge { get; private set; }
        public static Fuel HydrogenFuel { get; private set; }
        public static Fuel PetroleumFuel { get; private set; }

        static FuelManager()
        {
            ElectricCharge = new ElectricCharge();
            HydrogenFuel = new HydrogenFuel();
            PetroleumFuel = new PetroleumFuel();

            fuelTypes.Add(ElectricCharge);
            fuelTypes.Add(HydrogenFuel);
            fuelTypes.Add(PetroleumFuel);
            // Add more fuel types if needed
        }

        public static List<Fuel> GetFuelTypes()
        {
            return fuelTypes;
        }
    }
}