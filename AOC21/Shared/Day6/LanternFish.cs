namespace AOC21.Shared.Day6
{
    public record LanternFish(int DaysLeft, long CountOfFishes)
    {
        public LanternFish Iterator()
        {
            if (DaysLeft == 0)
                return new LanternFish(6, CountOfFishes);

            return new LanternFish(DaysLeft - 1, CountOfFishes);
        }
    }
}
