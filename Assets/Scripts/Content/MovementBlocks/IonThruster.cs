namespace Content.MovementBlocks
{
    using UnityEngine;

    namespace Content.MovementBlocks
    {
        [CreateAssetMenu(fileName = "NewIonThruster", menuName = "Content/IonThruster")] //this isn't working
        public class IonThrusterData : ScriptableObject
        {
            public Fuel fuelType = FuelManager.ElectricCharge;
            public float heatGeneration = 0.1f;
            public float thrustPower = 0.25f;
            public float rotationPower = 0f;
        }
    }

}