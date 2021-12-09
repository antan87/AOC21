namespace AOC21.Shared.Day7
{
    public record Crab(int Position, bool dubbelUp = false)
    {
        public int Distance(int FromPoint)
        {
            if (Position == FromPoint)
                return 0;

            if (!dubbelUp)
                return Position < FromPoint ? FromPoint - Position : Position - FromPoint;

            var distance = 0;
            var tempPosition = Position;
            Func<int> func = Position < FromPoint ? () => ++tempPosition : () => --tempPosition;
            int iteration = 0;
            Recursive(func, FromPoint, ref distance, ref iteration);

            return distance;

            static void Recursive(Func<int> positionFunc, int finalPosition, ref int distance, ref int iteration)
            {
                int position = positionFunc();
                distance = iteration + 1 + distance;
                if (position == finalPosition)
                    return;

                ++iteration;
                Recursive(positionFunc, finalPosition, ref distance, ref iteration);

            }
        }
    }
}
