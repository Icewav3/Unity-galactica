using UnityEngine;

namespace Content.Blocks
{
    public class FuelTankBlock : Block
    {
        [SerializeField] private bool explosive = true;
        [SerializeField] private float maxFuel = 100f;
        private float currentFuel;

        public bool Explosive => explosive;
        public float MaxFuel => maxFuel;
        public float CurrentFuel => currentFuel;

        public void Refuel(float amount)
        {
            currentFuel = Mathf.Clamp(currentFuel + amount, 0, maxFuel);
        }
    }
}