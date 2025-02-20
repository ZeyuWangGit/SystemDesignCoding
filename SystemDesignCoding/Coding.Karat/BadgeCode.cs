namespace Coding.Karat;

public static class BadgeCode
{
    /*
    Given a list of people who enter and exit, find the people who entered without
    their badge and who exited without their badge.
     badge_records = [
       ["Martha",   "exit"],
       ["Paul",     "enter"],
       ["Martha",   "enter"],
       ["Martha",   "exit"],
       ["Jennifer", "enter"],
       ["Paul",     "enter"],
       ["Curtis",   "enter"],
       ["Paul",     "exit"],
       ["Martha",   "enter"],
       ["Martha",   "exit"],
       ["Jennifer", "exit"],
     ]

     Expected output: ["Paul", "Curtis"], ["Martha"]
     */
    public static List<List<string>> InvalidBadgeRecords(string[][] records)
    {
        var dict = new Dictionary<string, int[]>();
        foreach (var record in records)
        {
            var name = record[0];
            var type = record[1];
            if (!dict.ContainsKey(name))
            {
                dict.Add(name, [0, 0]);
            }

            if (type == "enter")
            {
                dict[name][0]++;
            }
            else
            {
                dict[name][1]++;
            }
        }

        var enterList = new List<string>();
        var exitList = new List<string>();
        foreach (var pair in dict)
        {
            var name = pair.Key;
            var enter = pair.Value[0];
            var exit = pair.Value[1];
            if (enter > exit)
            {
                enterList.Add(name);
            }

            if (enter < exit)
            {
                exitList.Add(name);
            }
        }

        var res = new List<List<string>>
        {
            enterList,
            exitList
        };
        return res;
    }

    /*
    We want to find employees who badged into our secured room together often.
    Given an unordered list of names and access times over a single day, 
    find the largest group of people that were in the room together during two or more separate time periods, and the times when they were all present.
    John: {(455,enter), (512,exit), (1510, exit)}
    ->
    John: {{455,512}, {1000, 1510}}
    Steve: {{300, 500}}
    [John, Steve] : {{455, 500}}
    records = [
        ["Curtis", "2", "enter"],
        ["John", "1510", "exit"],
        ["John", "455", "enter"],
        ["John", "512", "exit"],
        ["Jennifer", "715", "exit"],
        ["Steve", "815", "enter"],
        ["John", "930", "enter"],
        ["Steve", "1000", "exit"],
        ["Paul", "1", "enter"],
        ["Angela", "1115", "enter"],
        ["Curtis", "1510", "exit"],
        ["Angela", "2045", "exit"],
        ["Nick", "630", "exit"],
        ["Jennifer", "30", "enter"],
        ["Nick", "30", "enter"],
        ["Paul", "2145", "exit"],
        ["Ben", "457", "enter"],
        ["Ben", "458", "exit"],
        ["Robin", "459", "enter"],
        ["Robin", "500", "exit"]
    ]
    Expected output:
      Paul, Curtis, and John: 455 to 512, 930 to 1510
    For this input data:
      From 455 til 512, the room contains Paul, Curtis and John. Jennifer and Nick are also in the room at this time, and Ben and Robin enter and leave during this time period.
      From 930 til 1510, Paul, Curtis, and John are in the room while Steve and Angela enter and leave, until Curtis leaves at 1510.
    The group "Paul, Curtis and John" exists at both of these times, and is the largest group that exists multiple times.
    You should note that the group in the expected output is a subset of the people in the room in both cases.
    records2 = [
        ["Paul", "1545", "exit"],
        ["Curtis", "1410", "enter"],
        ["Curtis", "222", "enter"],
        ["Curtis", "1630", "exit"],
        ["Paul", "10", "enter"],
        ["Paul", "1410", "enter"],
        ["John", "330", "enter"],
        ["Jennifer", "330", "enter"],
        ["Jennifer", "1410", "exit"],
        ["John", "1410", "exit"],
        ["Curtis", "330", "exit"],
        ["Paul", "330", "exit"],
    ]
    Expected output:
    Curtis, Paul: 222 to 330, 1410 to 1545
    All Test Cases:
    together(records) => Paul, Curtis, and John: 455 to 512, 930 to 1510
    together(records2) => Curtis, Paul: 222 to 330, 1410 to 1545
     */
    public static string FindLargestGroup(List<Record> records)
    {
        var sortedRecords = records.OrderBy(r => r.Time).ToList();
        var currentOccupants = new HashSet<string>();
        var timeIntervals = new List<(int? startTime, int endTime, HashSet<string> occupants)>();
        int? intervalStart = null;
        foreach (var sortedRecord in sortedRecords)
        {
            if (intervalStart.HasValue && sortedRecord.Time != intervalStart)
            {
                timeIntervals.Add((intervalStart.Value, sortedRecord.Time, new HashSet<string>(currentOccupants)));
            }

            if (sortedRecord.ActionType == Action.Enter)
            {
                currentOccupants.Add(sortedRecord.Name);
            }
            else if (sortedRecord.ActionType == Action.Exit)
            {
                currentOccupants.Remove(sortedRecord.Name);
            }

            intervalStart = sortedRecord.Time;
        }

        var groupOccupants =
            new Dictionary<HashSet<string>, List<(int? start, int end)>>(HashSet<string>.CreateSetComparer());
        foreach (var timeInterval in timeIntervals)
        {
            if (!groupOccupants.ContainsKey(timeInterval.occupants))
            {
                groupOccupants.Add([..timeInterval.occupants], [(timeInterval.startTime, timeInterval.endTime)]);
            }
            else
            {
                groupOccupants[timeInterval.occupants].Add((timeInterval.startTime, timeInterval.endTime));
            }
        }

        var largestGroup = groupOccupants.Where(item => item.Value.Count > 1).OrderByDescending(item => item.Key.Count)
            .FirstOrDefault();

        var groupNames = String.Join(", ", largestGroup.Key);
        var timePeriod = String.Join(", ", largestGroup.Value.Select(item => $"{item.start} to {item.end}"));
        return $"{groupNames}: {timePeriod}";
    }
}

public enum Action
{
    Enter,
    Exit
}

public class Record
{
    public string Name { get; set; }
    public int Time { get; set; }
    public Action ActionType { get; set; }
}