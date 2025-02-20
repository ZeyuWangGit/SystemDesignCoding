namespace Coding.Karat;

public static class MeetingsCode
{
    /*
    类似meeting rooms，输入是一个int[][] meetings, int start, int end, 每个数都是时间，13：00 -> 1300， 9：30 -> 18930， 看新的meeting 能不能安排到meetings
    ex: {[1300, 1500], [930, 1200],[830, 845]}, 新的meeting[820, 830], return true; [1450, 1500] return false;
    */
    public static bool CanSchedule(List<int[]> meetings, int start, int end)
    {
        meetings.Add([start, end]);
        var sortedMeetings = meetings.OrderBy(meet => meet[0]).ToList();
        var lastMeet = sortedMeetings[0];
        for (var i = 1; i < sortedMeetings.Count; i++)
        {
            var currMeet = sortedMeetings[i];
            if (currMeet[0] < lastMeet[1])
            {
                return false;
            }

            lastMeet = currMeet;
        }

        return true;
    }

    /*
    类似merge interval，唯一的区别是输出，输出空闲的时间段，merge完后，再把两两个之间的空的输出就好，注意要加上0 - 第一个的start time
     */
    public static List<(int start, int end)> FindFreeTime(List<(int start, int end)> meetings)
    {
        var freeTimes = new List<(int start, int end)>();
        if (meetings.Count == 0)
        {
            freeTimes.Add((0, 2400));
            return freeTimes;
        }

        var sortedMeetings = meetings.OrderBy(meet => meet.start).ToList();
        if (sortedMeetings[0].start > 0)
        {
            freeTimes.Add((0, sortedMeetings[0].start));
        }

        for (int i = 1; i < sortedMeetings.Count; i++)
        {
            var prev = sortedMeetings[i - 1];
            var curr = sortedMeetings[i];
            if (curr.start > prev.end)
            {
                freeTimes.Add((prev.end, curr.start));
            }
        }

        if (sortedMeetings[^1].end < 2400)
        {
            freeTimes.Add((sortedMeetings[^1].end, 2400));
        }

        return freeTimes;
    }
}