namespace Coding.Karat;

public static class CoursePathFinder
{
    /*
    Students may decide to take different "tracks" or sequences of courses in the Computer Science curriculum.
    There may be more than one track that includes the same course, but each student follows a single linear track from a "root" node to a "leaf" node.
    In the graph below, their path always moves left to right.

    Write a function that takes a list of (source, destination) pairs, 
    and returns the name of all of the courses that the students could be taking when they are halfway through their track of courses.

    Sample input:
    all_courses = [
        ["Logic", "COBOL"], 
        ["Data Structures", "Algorithms"],
        ["Creative Writing", "Data Structures"],
        ["Algorithms", "COBOL"],
        ["Intro to Computer Science", "Data Structures"],
        ["Logic", "Compilers"],
        ["Data Structures", "Logic"],
        ["Creative Writing", "System Administration"],
        ["Databases", "System Administration"],
        ["Creative Writing", "Databases"],
        ["Intro to Computer Science", "Graphics"],
    ]

    Sample output (in any order):
              ["Data Structures", "Creative Writing", "Databases", "Intro to Computer Science"]

    All paths through the curriculum (midpoint *highlighted*):
    *Intro to C.S.* -> Graphics
    Intro to C.S. -> *Data Structures* -> Algorithms -> COBOL
    Intro to C.S. -> *Data Structures* -> Logic -> COBOL
    Intro to C.S. -> *Data Structures* -> Logic -> Compiler
    Creative Writing -> *Databases* -> System Administration
    *Creative Writing* -> System Administration
    Creative Writing -> *Data Structures* -> Algorithms -> COBOL
    Creative Writing -> *Data Structures* -> Logic -> COBOL
    Creative Writing -> *Data Structures* -> Logic -> Compilers

    Visual representation:

                        ____________
                        |          |
                        | Graphics |
                   ---->|__________|
                   |                          ______________
    ____________   |                          |            |
    |          |   |    ______________     -->| Algorithms |--\     _____________
    | Intro to |   |    |            |    /   |____________|   \    |           |
    | C.S.     |---+    | Data       |   /                      >-->| COBOL     |
    |__________|    \   | Structures |--+     ______________   /    |___________|
                     >->|____________|   \    |            |  /
    ____________    /                     \-->| Logic      |-+      _____________
    |          |   /    ______________        |____________|  \     |           |
    | Creative |  /     |            |                         \--->| Compilers |
    | Writing  |-+----->| Databases  |                              |___________|
    |__________|  \     |____________|-\     _________________________
                   \                    \    |                       |
                    \--------------------+-->| System Administration |
                                             |_______________________|
    */
    public static HashSet<String> FindMidCourses(List<String[]> allCourses)
    {
        var graph = new Dictionary<string, List<string>>();
        var allCoursesSet = new HashSet<String>();
        var destinationCoursesSet = new HashSet<String>();
        foreach (var coursePair in allCourses)
        {
            var from = coursePair[0];
            var to = coursePair[1];
            if (!graph.ContainsKey(from))
            {
                graph.Add(from, new List<string>());
            }
            graph[from].Add(to);
            allCoursesSet.Add(to);
            allCoursesSet.Add(from);
            destinationCoursesSet.Add(to);
        }
        var startCourses = new HashSet<string>(allCoursesSet);
        startCourses.ExceptWith(destinationCoursesSet);
        
        var midCourses = new HashSet<string>();

        foreach (var course in startCourses)
        {
            var path = new List<string>();
            var visited = new HashSet<string>();
            DFS(graph, course, visited, path, midCourses);
        }

        return midCourses;

    }

    private static void DFS(Dictionary<string, List<string>> graph, string course, HashSet<string> visited,
        List<string> path, HashSet<string> midCourses)
    {
        path.Add(course);
        visited.Add(course);
        if (!graph.ContainsKey(course) || graph[course].Count == 0)
        {
            var midIndex = path.Count / 2 - (path.Count % 2 == 0 ? 1 : 0);
            midCourses.Add(path[midIndex]);
        }
        else
        {
            foreach (var neighborCourse in graph[course])
            {
                if (!visited.Contains(neighborCourse))
                {
                    DFS(graph, neighborCourse, visited, path, midCourses);
                }
            }
        }
        path.RemoveAt(path.Count - 1);
        visited.Remove(course);
    } 
}