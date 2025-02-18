namespace Coding.Karat;

public static class SharedCoursesFinderCode
{
    /*
    You are a developer for a university. Your current project is to develop a system for students to find courses they share with friends. 
    The university has a system for querying courses students are enrolled in, returned as a list of (ID, course) pairs.
    Write a function that takes in a list of (student ID number, course name) pairs and returns, for every pair of students, a list of all courses they share.
    Sample Input:

    student_course_pairs_1 = [
      ["58", "Software Design"],
      ["58", "Linear Algebra"],
      ["94", "Art History"],
      ["94", "Operating Systems"],
      ["17", "Software Design"],
      ["58", "Mechanics"],
      ["58", "Economics"],
      ["17", "Linear Algebra"],
      ["17", "Political Science"],
      ["94", "Economics"],
      ["25", "Economics"],
    ]

    Sample Output (pseudocode, in any order):

    find_pairs(student_course_pairs_1) =>
    {
      [58, 17]: ["Software Design", "Linear Algebra"]
      [58, 94]: ["Economics"]
      [58, 25]: ["Economics"]
      [94, 25]: ["Economics"]
      [17, 94]: []
      [17, 25]: []
    }

    Additional test cases:

    Sample Input:

    student_course_pairs_2 = [
      ["42", "Software Design"],
      ["0", "Advanced Mechanics"],
      ["9", "Art History"],
    ]

    Sample output:

    find_pairs(student_course_pairs_2) =>
    {
      [0, 42]: []
      [0, 9]: []
      [9, 42]: []
    }
     */
    public static Dictionary<Tuple<string, string>, HashSet<string>> FindPairs(List<string[]> studentCoursePairs)
    {
        var studentCourseDict = new Dictionary<string, HashSet<string>>();
        foreach (var studentCoursePair in studentCoursePairs)
        {
          var student = studentCoursePair[0];
          var studentCourse = studentCoursePair[1];
          if (!studentCourseDict.ContainsKey(student))
          {
            studentCourseDict.Add(student, new HashSet<string>());
          }
          studentCourseDict[student].Add(studentCourse);
        }
        var students = studentCourseDict.Keys.ToList();
        var res = new Dictionary<Tuple<string, string>, HashSet<string>>();
        for (int i = 0; i < students.Count; i++)
        {
          for (int j = i + 1; j < students.Count; j++)
          {
            var student1 = students[i];
            var student2 = students[j];
            var sharedCourses = new HashSet<string>(studentCourseDict[student1]);
            sharedCourses.IntersectWith(studentCourseDict[student2]);
            res[new Tuple<string, string>(student1, student2)] = sharedCourses;
          }
        }

        return res;
    }
}