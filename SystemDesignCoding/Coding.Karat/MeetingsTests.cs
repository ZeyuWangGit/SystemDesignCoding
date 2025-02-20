namespace Coding.Karat;

using System.Collections.Generic;
using Xunit;

public class MeetingsCodeTests
{
    [Fact]
    public void Test_CanSchedule_NoOverlap()
    {
        var meetings = new List<int[]>
        {
            new int[] { 1300, 1500 },
            new int[] { 930, 1200 },
            new int[] { 830, 845 }
        };

        Assert.True(MeetingsCode.CanSchedule(meetings, 820, 830));
    }

    [Fact]
    public void Test_CanSchedule_Overlap()
    {
        var meetings = new List<int[]>
        {
            new int[] { 1300, 1500 },
            new int[] { 930, 1200 },
            new int[] { 830, 845 }
        };

        Assert.False(MeetingsCode.CanSchedule(meetings, 1450, 1500));
    }

    [Fact]
    public void Test_CanSchedule_BoundaryCase()
    {
        var meetings = new List<int[]>
        {
            new int[] { 900, 1000 },
            new int[] { 1100, 1200 }
        };

        Assert.True(MeetingsCode.CanSchedule(meetings, 1000, 1100));
    }

    [Fact]
    public void Test_CanSchedule_FullyContained()
    {
        var meetings = new List<int[]>
        {
            new int[] { 900, 1200 }
        };

        Assert.False(MeetingsCode.CanSchedule(meetings, 930, 1130));
    }

    [Fact]
    public void Test_CanSchedule_EmptyMeetings()
    {
        var meetings = new List<int[]>();

        Assert.True(MeetingsCode.CanSchedule(meetings, 1000, 1100));
    }

    [Fact]
    public void Test_FindFreeTime_NormalCase()
    {
        var meetings = new List<(int start, int end)>
        {
            (1300, 1500),
            (930, 1200),
            (830, 845)
        };

        var expected = new List<(int start, int end)>
        {
            (0, 830),
            (845, 930),
            (1200, 1300),
            (1500, 2400)
        };

        var result = MeetingsCode.FindFreeTime(meetings);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_FindFreeTime_FullDayFree()
    {
        var meetings = new List<(int start, int end)>();

        var expected = new List<(int start, int end)>
        {
            (0, 2400)
        };

        var result = MeetingsCode.FindFreeTime(meetings);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_FindFreeTime_NoFreeTime()
    {
        var meetings = new List<(int start, int end)>
        {
            (0, 2400)
        };

        var expected = new List<(int start, int end)>();

        var result = MeetingsCode.FindFreeTime(meetings);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_FindFreeTime_GapsBetweenMeetings()
    {
        var meetings = new List<(int start, int end)>
        {
            (800, 900),
            (1000, 1100),
            (1200, 1300)
        };

        var expected = new List<(int start, int end)>
        {
            (0, 800),
            (900, 1000),
            (1100, 1200),
            (1300, 2400)
        };

        var result = MeetingsCode.FindFreeTime(meetings);

        Assert.Equal(expected, result);
    }
}