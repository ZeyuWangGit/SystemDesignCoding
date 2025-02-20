namespace Coding.Karat;

public static class BestDayToCrossCode
{
    // [24]
    /*
    You are planning out a trek across a snowy mountain. On the mountain it snows in the morning, the snow melts with the sun in the afternoon, and in the evening you can attempt a crossing.
    * Snow piles up at each location, making that location higher.
    * If it has not snowed at a particular location for 2 days, the snow there starts melting on the afternoon of the second day, at a rate of one unit per day.
    * You can climb up and down one level while moving to the next position.
    * The player needs to cross the mountain with the least amount of climbing possible.
    * The crossing attempts are limited to the days in the forecast because the weather is unpredictable later.
    Write a function that, given the base altitude of locations on the mountain and a list of snow forecasts for each day, calculates and returns the best day to perform the crossing as well as the number of climbs needed on that day.
    For example, given the initial altitudes: [0,1,2,1]
       3
       2     -
       1   -   -  Side view of the mountain
       0 -
         0 1 2 3
         position
    And the snow forecast for each morning:
    [[1,0,1,0], # On day zero, one unit of snow will fall on positions 0 and 2.
    [0,0,0,0], # On day one, it will not snow.
    [1,1,0,2]] # On day two, two units of snow will fall on position 3, and one unit on positions 0 and 1.
    This is the resulting mountain profile each evening, the player is represented by the letter P:
        Day 0                 Day 1             Day 2

                                                starts melting
                                                      ↓
        3     *                                 3 P     *
        2 P   -               no new snow       2 * * - *
        1 * -   -                               1 * -   -
        0 -                   no melting        0 -
          0 1 2 3                                 0 1 2 3

        [0,1,2,1] -> [1,0,1,0] -> [1,1,3,1] ->X
        [1,1,3,1] -> [1,1,3,1] -> X
        [1,1,2,1] -> [2,2,2,3] -> [2,1] -> [-1,-1]
        [1,1,0,2]
    In the example above:
    At the end of day 0, the mountain cannot be crossed. The steps are too high to climb.
    At the end of day 1, there are no changes, still no crossing.
    At the end of day 2, the mountain can be crossed by climbing once. Notice that in position 2, one unit of snow melted.
    In case it's not possible to cross on any of the days, the function should return Null or [-1,-1].
    Expected results:
    best_day_to_cross(altitudes_1, snow_1) -> [2, 1] at the end of day two, only one climb is required
    best_day_to_cross(altitudes_2, snow_2) -> [0, 0] day zero is the best day to cross
    best_day_to_cross(altitudes_3, snow_3) -> [2, 0] zero climbs are required at the end of day two
    best_day_to_cross(altitudes_4, snow_4) -> [-1,-1] no viable days, the steps are always too high
    best_day_to_cross(altitudes_5, snow_5) -> [5, 1] melting can continue over a few days
    best_day_to_cross(altitudes_6, snow_6) -> [0, 4] it requires 4 climbs
    Complexity variables:
    A - number of altitude positions
    D - number of days in the forecast
     */
    
    public static int[] BestDayToCross(int[] altitudes, int[][] forecasts)
    {
        var n = altitudes.Length;
        var snowFreeDays = new int[n];
        var currentHeight = new List<int>(altitudes);
        var minClimb = int.MaxValue;
        var bestDay = -1;
        for (var day = 0; day < forecasts.Length; day++)
        {
            for (int i = 0; i < n; i++)
            {
                if (forecasts[day][i] > 0)
                {
                    currentHeight[i] += forecasts[day][i];
                    snowFreeDays[i] = 0;
                }
                else
                {
                    snowFreeDays[i]++;
                    if (snowFreeDays[i] > 1 && currentHeight[i] > altitudes[i])
                    {
                        currentHeight[i]--;
                    }
                }
            }

            var canCross = true;
            var climb = 0;
            for (var i = 1; i < n; i++)
            {
                var diff = Math.Abs(currentHeight[i] - currentHeight[i - 1]);
                if (diff > 1)
                {
                    canCross = false;
                    break;
                }

                climb += diff;
            }

            if (canCross && climb < minClimb)
            {
                minClimb = climb;
                bestDay = day;
            }
        }

        return bestDay == -1 ? [-1, -1] : [bestDay, minClimb];
    }

}