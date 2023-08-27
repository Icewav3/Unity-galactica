namespace Content.Blocks
{

    public class FuelTankBlock : Block
    {
        public bool explosive = true;
        public float MaxFuel = 100f;
        public float CurrentFuel { get; private set; }

        /*public FuelTankBlock(bool isExplosive, float maxFuel)
        {
            IsExplosive = isExplosive;
            MaxFuel = maxFuel;
            CurrentFuel = 0;
        }*/
    }
}