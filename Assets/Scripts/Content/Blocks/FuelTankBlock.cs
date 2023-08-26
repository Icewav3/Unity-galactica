namespace Content.Blocks
{

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
}