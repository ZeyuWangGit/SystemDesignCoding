package org.example.done;

import java.time.Clock;
import java.time.Duration;
import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.BitSet;
import java.util.Collections;
import java.util.Comparator;
import java.util.Deque;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.PriorityQueue;
import java.util.Queue;
import java.util.Set;
import java.util.Stack;
import java.util.TreeMap;
import java.util.concurrent.atomic.AtomicLong;
import java.util.stream.Collectors;

public class Karat {
    // [1]
    // SOLVED
    /*
    Example 1:
    Input:
    ["9001 discuss.leetcode.com"]
    Output:
    ["9001 discuss.leetcode.com", "9001 leetcode.com", "9001 com"]

    Example 2:
    Input:
    ["900 google.mail.com", "50 yahoo.com", "1 intel.mail.com", "5 wiki.org"]
    Output:
    ["901 mail.com","50 yahoo.com","900 google.mail.com","5 wiki.org","5 org","1 intel.mail.com","951 com"]
     */
    public static List<String> subdomainVisits(String[] cpdomains) {
        Map<String, Integer> map = new HashMap<>();
        for (String cpdomain : cpdomains) {
            String[] parts = cpdomain.split(" ");
            int count = Integer.parseInt(parts[0]);
            String domain = parts[1];
            map.merge(domain, count, Integer::sum);
            for (int i = 0; i < domain.length(); i++) {
                if (domain.charAt(i) == '.') {
                    String subdomain = domain.substring(i + 1);
                    map.merge(subdomain, count, Integer::sum);
                }
            }
        }
        List<String> result = new ArrayList<>();
        for (Map.Entry<String, Integer> entry : map.entrySet()) {
            result.add(entry.getValue() + " " + entry.getKey());
        }
        return result;
    }

    // [2]
    // SOLVED
    /*
    Longest Common Continuous Subarray
    [
      ["3234.html", "xys.html", "7hsaa.html"],
      ["3234.html", "sdhsfjdsh.html", "xys.html", "7hsaa.html"]
    ]
    => ["xys.html", "7hsaa.html"]
     */
    public static List<String> longestCommonContinuousSubarray(List<String> history1, List<String> history2) {
        if (history1 == null || history2 == null || history1.isEmpty() || history2.isEmpty()) {
            return new ArrayList<>();
        }

        int count = -1;
        List<String> result = new ArrayList<>();
        int[][] memo = new int[history1.size() + 1][history2.size() + 1];

        for (int i = 1; i <= history1.size(); i++) {
            for (int j = 1; j <= history2.size(); j++) {
                if (history1.get(i - 1).equals(history2.get(j - 1))) {
                    memo[i][j] = 1 + memo[i - 1][j - 1];
                    if (memo[i][j] > count) {
                        count = memo[i][j];
                        result = history1.subList(i - count, i);
                    }
                }
            }
        }

        return result;
    }

    // [3]
    // SOLVED
    // LC
    public int longestCommonSubsequence(String text1, String text2) {
        if (text1 == null || text2 == null) {
            return 0;
        }
        int l1 = text1.length(), l2 = text2.length();
        int[][] dp = new int[l1 + 1][l2 + 1];
        dp[0][0] = 0;
        // dp[0][1] = 0;
        // dp[1][0] = 0;
        for (int i = 1; i <= l1; i++) {
            for (int j = 1; j <= l2; j++) {
                dp[i][j] = Math.max(dp[i - 1][j], dp[i][j - 1]);
                if (text1.charAt(i - 1) == text2.charAt(j - 1)) {
                    dp[i][j] = Math.max(dp[i][j], dp[i - 1][j - 1] + 1);
                }
            }
        }
        return dp[l1][l2];
    }

    // [4]
    // SOLVED
    /*
    The people who buy ads on our network don't have enough data about how ads are working for
    their business. They've asked us to find out which ads produce the most purchases on their website.

    Our client provided us with a list of user IDs of customers who bought something on a landing page
    after clicking one of their ads:

     # Each user completed 1 purchase.
     completed_purchase_user_ids = ["3123122444","234111110", "8321125440", "99911063"]

    And our ops team provided us with some raw log data from our ad server showing every time a
    user clicked on one of our ads:
    #"IP_Address,Time,Ad_Text",
    ad_clicks = [
      "122.121.0.1,2016-11-03 11:41:19,Buy wool coats for your pets",
      "96.3.199.11,2016-10-15 20:18:31,2017 Pet Mittens",
      "122.121.0.250,2016-11-01 06:13:13,The Best Hollywood Coats",
      "82.1.106.8,2016-11-12 23:05:14,Buy wool coats for your pets",
      "92.130.6.144,2017-01-01 03:18:55,Buy wool coats for your pets",
      "92.130.6.145,2017-01-01 03:18:55,2017 Pet Mittens",
    ]

    The client also sent over the IP addresses of all their users.

    #"User_ID,IP_Address",
    all_user_ips = [
      "2339985511,122.121.0.155",
      "234111110,122.121.0.1",
      "3123122444,92.130.6.145",
      "39471289472,2001:0db8:ac10:fe01:0000:0000:0000:0000",
      "8321125440,82.1.106.8",
      "99911063,92.130.6.144"
    ]

    Write a function to parse this data, determine how many times each ad was clicked,
    then return the ad text, that ad's number of clicks, and how many of those ad clicks
    were from users who made a purchase.

     Expected output:
     Bought Clicked Ad Text
     1 of 2  2017 Pet Mittens
     0 of 1  The Best Hollywood Coats
     3 of 3  Buy wool coats for your pets
     */
    private static class Conversion {
        int bought;
        int total;

        public Conversion(int bought, int total) {
            this.bought = bought;
            this.total = total;
        }
    }

    public static List<String> adConversionRate(String[] completedPurchaseUserIds, String[] adClicks, String[] allUserIps) {
        Set<String> purchases = new HashSet<>(List.of(completedPurchaseUserIds));
        Map<String, Conversion> conversions = new HashMap<>();
        Map<String, String> ipToUserId = new HashMap<>();

        // Mapping IPs to user IDs
        for (String userIp : allUserIps) {
            String[] parts = userIp.split(",");
            String userId = parts[0];
            String ip = parts[1];
            ipToUserId.put(ip, userId);
        }

        // Processing ad clicks
        for (String click : adClicks) {
            String[] parts = click.split(",");
            String ip = parts[0];
            String adText = parts[2];

            int bought = purchases.contains(ipToUserId.get(ip)) ? 1 : 0;

            conversions.merge(adText, new Conversion(bought, 1), (oldV, newV) -> {
                oldV.bought += newV.bought;
                oldV.total += newV.total;
                return oldV;
            });
        }

        return conversions.entrySet().stream()
                .map(entry -> entry.getValue().bought + " of " + entry.getValue().total + " " + entry.getKey())
                .toList();
    }

    // [5]
    // SOLVED
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
    public static Map<List<String>, Set<String>> findPairs(List<String[]> studentCoursePairs) {
        if (studentCoursePairs == null || studentCoursePairs.isEmpty()) {
            return Collections.emptyMap();
        }

        // Map to store each student's list of courses
        Map<String, Set<String>> courses = new HashMap<>();

        // Populate the courses map
        for (String[] pair : studentCoursePairs) {
            String studentId = pair[0];
            String course = pair[1];
            courses.computeIfAbsent(studentId, k -> new HashSet<>()).add(course);
        }

        List<String> studentIds = new ArrayList<>(courses.keySet());

        Map<List<String>, Set<String>> result = new HashMap<>();
        // Find overlaps between every pair of students
        for (int i = 0; i < studentIds.size(); i++) {
            for (int j = i + 1; j < studentIds.size(); j++) {
                String student1 = studentIds.get(i);
                String student2 = studentIds.get(j);
                Set<String> overlap = new HashSet<>(courses.get(student1));
                Set<String> set2 = courses.get(student2);
                overlap.retainAll(set2);
                result.put(List.of(student1, student2), overlap);
            }
        }
        return result;
    }

    // [6]
    // SOLVED
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
    public static Set<String> findMidCourses(List<String[]> allCourses) {
        if (allCourses == null || allCourses.isEmpty()) {
            return Collections.emptySet();
        }

        Map<String, Set<String>> dag = new HashMap<>();
        Set<String> dests = new HashSet<>();
        Set<String> starts = new HashSet<>();
        for (String[] pair : allCourses) {
            String source = pair[0];
            String destination = pair[1];
            dag.computeIfAbsent(source, k -> new HashSet<>()).add(destination);
            starts.add(source);
            starts.add(destination);
            dests.add(destination);
        }
        starts.removeAll(dests);

        Set<String> result = new HashSet<>();
        for (String start : starts) {
            Set<String> visited = new HashSet<>();
            List<String> path = new ArrayList<>();
            visited.add(start);
            path.add(start);
            dfs(dag, start, visited, result, path);
        }

        return result;
    }

    private static void dfs(Map<String, Set<String>> dag, String node, Set<String> visited, Set<String> result, List<String> path) {
        Set<String> neighbors = dag.getOrDefault(node, Collections.emptySet());
        if (neighbors.isEmpty()) {
            int mid = path.size() / 2 - (path.size() % 2 == 0 ? 1 : 0);
            result.add(path.get(mid));
            return;
        }
        for (String neighbor : neighbors) {
            if (visited.add(neighbor)) {
                path.add(neighbor);
                dfs(dag, neighbor, visited, result, path);
                path.remove(path.size() - 1);
                visited.remove(neighbor);
            }
        }
    }

    // [7]
    /*
    there is an image filled with 0s and 1s. There is at most one rectangle in this image filled with 0s, find the rectangle.
    Output could be the coordinates of top-left and bottom-right elements of the rectangle, or top-left element, width and height.
     */
    public static List<int[]> findOneRectangle(int[][] board) {
        List<int[]> result = new ArrayList<>();
        if (board == null || board.length == 0 || board[0].length == 0) {
            return result;
        }

        int rows = board.length;
        int cols = board[0].length;

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                if (board[i][j] == 0) {
                    // Determine the width of the rectangle
                    int width = 1;
                    while (j + width < cols && board[i][j + width] == 0) {
                        width++;
                    }

                    // Determine the height of the rectangle
                    int height = 1;
                    while (i + height < rows && board[i + height][j] == 0) {
                        height++;
                    }

                    // Define the bottom-right corner
                    int bottomRightX = i + height - 1;
                    int bottomRightY = j + width - 1;

                    // Add the rectangle to the result list
                    result.add(new int[]{i, j});
                    result.add(new int[]{bottomRightX, bottomRightY});
                    return result;
                }
            }
        }

        return result;
    }

    // [8]
    /*
    for the same image, it is filled with 0s and 1s. It may have multiple rectangles filled with 0s. The rectangles are separated by 1s. Find all the rectangles.
     */
    public static List<int[][]> findMultipleRectangles(int[][] board) {
        List<int[][]> result = new ArrayList<>();
        if (board == null || board.length == 0 || board[0].length == 0) {
            return result;
        }

        int rows = board.length;
        int cols = board[0].length;

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                if (board[i][j] == 0) { // Found the top-left corner of a new rectangle
                    int topLeftX = i;
                    int topLeftY = j;

                    // Determine the width of the rectangle
                    int width = 0;
                    while (j + width < cols && board[i][j + width] == 0) {
                        width++;
                    }

                    // Determine the height of the rectangle
                    int height = 0;
                    while (i + height < rows && board[i + height][j] == 0) {
                        height++;
                    }

                    // Mark the rectangle as visited
                    for (int h = 0; h < height; h++) {
                        for (int w = 0; w < width; w++) {
                            board[i + h][j + w] = 1; // Marking cells as visited
                        }
                    }

                    // Define the bottom-right corner
                    int bottomRightX = topLeftX + height - 1;
                    int bottomRightY = topLeftY + width - 1;

                    // Add the rectangle to the result list
                    int[][] rectangle = new int[2][2];
                    rectangle[0][0] = topLeftX;
                    rectangle[0][1] = topLeftY;
                    rectangle[1][0] = bottomRightX;
                    rectangle[1][1] = bottomRightY;
                    result.add(rectangle);
                }
            }
        }

        return result;
    }

    // [9]
    /*
    the image has random shapes filled with 0s, separated by 1s. Find all the shapes. Each shape is represented by coordinates of all the elements inside.
     */
    public static List<List<int[]>> findMultipleShapes(int[][] board) {
        List<List<int[]>> result = new ArrayList<>();
        if (board == null || board.length == 0 || board[0].length == 0) {
            return result;
        }

        int rows = board.length;
        int cols = board[0].length;

        boolean[][] visited = new boolean[rows][cols];

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                if (board[i][j] == 0 && !visited[i][j]) {
                    List<int[]> shape = new ArrayList<>();
                    floodFillDFS(board, visited, i, j, shape);
                    result.add(shape);
                }
            }
        }

        return result;
    }

    private static void floodFillDFS(int[][] board, boolean[][] visited, int x, int y, List<int[]> shape) {
        if (x < 0 || x >= board.length || y < 0 || y >= board[0].length || board[x][y] == 1 || visited[x][y]) {
            return;
        }

        visited[x][y] = true;
        shape.add(new int[]{x, y});

        floodFillDFS(board, visited, x + 1, y, shape);
        floodFillDFS(board, visited, x - 1, y, shape);
        floodFillDFS(board, visited, x, y - 1, shape);
        floodFillDFS(board, visited, x, y + 1, shape);
    }

    // [10]
    /*
    给一个word list 和最大的长度，要求把这些word用 - 串联起来，但不能超过最大的长度。
     */
    public static List<String> wordWrap(String[] words, int maxLen) {
        List<String> result = new ArrayList<>();
        if (words == null || words.length == 0) {
            return result;
        }

        int i = 0;
        while (i < words.length) {
            int remain = maxLen;
            int count = 0;
            // Determine how many words fit in the current line
            while (i < words.length) {
                if (remain - words[i].length() < 0) {
                    break;
                }
                count++;
                remain -= words[i++].length() + 1;
            }
            // Create the line and add it to the result
            StringBuilder line = new StringBuilder();
            for (int j = i - count; j < i; j++) {
                if (j > i - count) {
                    line.append('-');
                }
                line.append(words[j]);
            }
            result.add(line.toString());
        }

        return result;
    }

    // [11]
    /*
    We are building a word processor, and we would like to implement a "reflow" functionality that also applies full justification to the text.
    Given an array containing lines of text and a new maximum width, re-flow the text to fit the new width. Each line should have the exact specified width. If any line is too short, insert '-' (as stand-ins for spaces) between words as equally as possible until it fits.
    Note: we are using '-' instead of spaces between words to make testing and visual verification of the results easier.

    lines = [ "The day began as still as the",
          "night abruptly lighted with",
          "brilliant flame" ]

    reflowAndJustify(lines, 24) ... "reflow lines and justify to length 24" =>

        [ "The--day--began-as-still",
          "as--the--night--abruptly",
          "lighted--with--brilliant",
          "flame" ] // <--- a single word on a line is not padded with spaces
     */
    public static List<String> reflowAndJustify(String[] lines, int maxWidth) {
        List<String> res = new ArrayList<>();

        List<String> temp = new ArrayList<>();
        for (String line : lines) {
            String[] parts = line.split(" ");
            temp.addAll(Arrays.asList(parts));
        }
        String[] words = temp.toArray(new String[0]);

        if (words.length == 0) {
            return res;
        }
        int start = 0;
        while (start < words.length) {
            int next = start + 1;
            int count = words[start].length();
            // fill until not fit
            while (next < words.length && count + 1 + words[next].length() <= maxWidth) {
                count += 1 + words[next++].length();
            }
            // distribute spaces
            int numOfGaps = next - start - 1;
            StringBuilder sb = new StringBuilder(words[start]);
            if (next == words.length || numOfGaps == 0) {
                for (int i = start + 1; i < next; i++) {
                    sb.append("-");
                    sb.append(words[start + 1]);
                }
            } else {
                int spacesLeft = maxWidth - count;
                int spacesInEach = spacesLeft / numOfGaps;
                int spacesRemaining = spacesLeft % numOfGaps;
                for (int i = start + 1; i < next; i++) {
                    sb.append("-".repeat(spacesInEach));
                    if (spacesRemaining-- > 0) {
                        sb.append("-");
                    }
                    sb.append("-");
                    sb.append(words[i]);
                }
            }
            start = next;
            res.add(sb.toString());
        }
        return res;
    }

    // [12]
    /*
    给输入为string，例如"2+3-999"，之包含+-操作，返回计算结果。
     */
    public static int basicCalculator(String expression) {
        if (expression == null || expression.isEmpty()) {
            return 0;
        }

        int result = 0;
        int sign = 1;
        int i = 0;

        while (i < expression.length()) {
            char c = expression.charAt(i);

            if (isNumeric(c)) {
                int num = 0;
                while (i < expression.length() && isNumeric(expression.charAt(i))) {
                    num = num * 10 + (expression.charAt(i) - '0');
                    i++;
                }
                result += num * sign;
                continue; // Skip to the next iteration to avoid incrementing `i` again
            } else if (c == '+') {
                sign = 1;
            } else if (c == '-') {
                sign = -1;
            }
            i++;
        }

        return result;
    }

    private static boolean isNumeric(char c) {
        return c >= '0' && c <= '9';
    }

    // [13]
    /*
    加上parenthesis， 例如"2+((8+2)+(3-999))"，返回计算结果。
     */
    public static int basicCalculator2(String expression) {
        if (expression == null || expression.isEmpty()) {
            return 0;
        }

        int result = 0;
        int sign = 1;
        Stack<Integer> stack = new Stack<>();
        int i = 0;

        while (i < expression.length()) {
            char c = expression.charAt(i);

            if (isNumeric(c)) {
                int num = 0;
                while (i < expression.length() && isNumeric(expression.charAt(i))) {
                    num = num * 10 + (expression.charAt(i) - '0');
                    i++;
                }
                result += num * sign;
                continue; // Skip incrementing i again
            } else if (c == '+') {
                sign = 1;
            } else if (c == '-') {
                sign = -1;
            } else if (c == '(') {
                // Push the result and sign to the stack
                stack.push(result);
                stack.push(sign);
                // Reset result and sign for the new sub-expression
                result = 0;
                sign = 1;
            } else if (c == ')') {
                // Pop the sign and previous result
                result = result * stack.pop() + stack.pop();
            }
            i++;
        }

        return result;
    }

    // [14]
    /*
    不光有数字和operator，还有一些变量，这些变量有些可以表示为一个数值，需要从给定的map里去get这个变量的value。然后有的变量不能转为数字，所以结果要包含这些不可变成数字的单词以及其他数字部分通过计算器得到的结果。
     */

    /*
    给一个N*N的矩阵，判定是否是有效的矩阵。有效矩阵的定义是每一行或者每一列的数字都必须正好是1到N的数。输出一个bool
     */
    public static boolean isValidMatrix(int[][] matrix) {
        for (int r = 0, n = matrix.length; r < n; ++r) {
            BitSet row = new BitSet(n + 1), col = new BitSet(n + 1);
            for (int c = 0; c < n; c++) {
                if (row.get(matrix[r][c]) || col.get(matrix[c][r])) {
                    return false;
                }
                row.set(matrix[r][c]);
                col.set(matrix[c][r]);
            }
        }
        return true;
    }


    // [15]
    /*
    A nonogram is a logic puzzle, similar to a crossword, in which the player is given a blank grid and has to color it according to some instructions.
    Specifically, each cell can be either black or white, which we will represent as 0 for black and 1 for white.

    +------------+
    | 1  1  1  1 |
    | 0  1  1  1 |
    | 0  1  0  0 |
    | 1  1  0  1 |
    | 0  0  1  1 |
    +------------+

    For each row and column, the instructions give the lengths of contiguous runs of black (0) cells.
    For example, the instructions for one row of [ 2, 1 ] indicate that there must be a run of two black cells, followed later by another run of one black cell, and the rest of the row filled with white cells.

    These are valid solutions: [ 1, 0, 0, 1, 0 ] and [ 0, 0, 1, 1, 0 ] and also [ 0, 0, 1, 0, 1 ]
    This is not valid: [ 1, 0, 1, 0, 0 ] since the runs are not in the correct order.
    This is not valid: [ 1, 0, 0, 0, 1 ] since the two runs of 0s are not separated by 1s.

    Your job is to write a function to validate a possible solution against a set of instructions. Given a 2D matrix representing a player's solution; and instructions for each row along with additional instructions for each column; return True or False according to whether both sets of instructions match.

    Example instructions #1

    matrix1 = [[1,1,1,1],
               [0,1,1,1],
               [0,1,0,0],
               [1,1,0,1],
               [0,0,1,1]]
    rows1_1    =  [], [1], [1,2], [1], [2]
    columns1_1 =  [2,1], [1], [2], [1]
    validateNonogram(matrix1, rows1_1, columns1_1) => True

    Example solution matrix:
    matrix1 ->
                                   row
                +------------+     instructions
                | 1  1  1  1 | <-- []
                | 0  1  1  1 | <-- [1]
                | 0  1  0  0 | <-- [1,2]
                | 1  1  0  1 | <-- [1]
                | 0  0  1  1 | <-- [2]
                +------------+
                  ^  ^  ^  ^
                  |  |  |  |
    column       [2,1] | [2] |
    instructions      [1]   [1]


    Example instructions #2

    (same matrix as above)
    rows1_2    =  [], [], [1], [1], [1,1]
    columns1_2 =  [2], [1], [2], [1]
    validateNonogram(matrix1, rows1_2, columns1_2) => False

    The second and third rows and the first column do not match their respective instructions.

    Example instructions #3

    matrix2 = [
    [ 1, 1 ],
    [ 0, 0 ],
    [ 0, 0 ],
    [ 1, 0 ]
    ]
    rows2_1    = [], [2], [2], [1]
    columns2_1 = [1, 1], [3]
    validateNonogram(matrix2, rows2_1, columns2_1) => False

    The black cells in the first column are not separated by white cells.

    n: number of rows in the matrix
    m: number of columns in the matrix
     */
    public static boolean isValidNonogram(int[][] matrix, List<List<Integer>> rows, List<List<Integer>> cols) {
        if (matrix == null || rows == null || cols == null) {
            return false;
        }

        int n = matrix.length;
        int m = matrix[0].length;

        if (n == 0 || n != rows.size() || m != cols.size()) {
            return false;
        }

        return isNonogramValid(matrix, rows, n, m, false) && isNonogramValid(matrix, cols, n, m, true);
    }

    private static boolean isNonogramValid(int[][] matrix, List<List<Integer>> instructions, int n, int m, boolean transpose) {
        if (transpose) {
            int temp = n;
            n = m;
            m = temp;
        }
        for (int i = 0; i < n; i++) {
            int idx = 0;
            for (int j = 0; j < m; j++) {
                if (transpose ? matrix[j][i] == 0 : matrix[i][j] == 0) {
                    if (instructions.get(i).isEmpty()) {
                        return false;
                    }
                    for (int k = 0; k < instructions.get(i).get(idx); k++) {
                        if (j + k >= m || (transpose ? matrix[j + k][i] != 0 : matrix[i][j + k] != 0)) {
                            return false;
                        }
                    }
                    j += instructions.get(i).get(idx++);
                }
            }
            if (idx != instructions.get(i).size()) {
                return false;
            }
        }
        return true;
    }

    private static boolean isNonogramRowsValid(int[][] matrix, List<List<Integer>> rows, int n, int m) {
        for (int i = 0; i < n; i++) {
            int rowIndex = 0;
            for (int j = 0; j < m; j++) {
                if (matrix[i][j] == 0) {
                    if (rows.get(i).isEmpty()) {
                        return false;
                    }
                    for (int k = 0; k < rows.get(i).get(rowIndex); k++) {
                        if (j + k >= m || matrix[i][j + k] != 0) {
                            return false;
                        }
                    }
                    j += rows.get(i).get(rowIndex++);
                }
            }
            if (rowIndex != rows.get(i).size()) {
                return false;
            }
        }
        return true;
    }

    private static boolean isNonogramColsValid(int[][] matrix, List<List<Integer>> cols, int n, int m) {
        for (int i = 0; i < m; i++) {
            int colIndex = 0;
            for (int j = 0; j < n; j++) {
                if (matrix[j][i] == 0) {
                    if (cols.get(i).isEmpty()) {
                        return false;
                    }
                    for (int k = 0; k < cols.get(i).get(colIndex); k++) {
                        if (j + k >= n || matrix[j + k][i] != 0) {
                            return false;
                        }
                    }
                    j += cols.get(i).get(colIndex++);
                }
            }
            if (colIndex != cols.get(i).size()) {
                return false;
            }
        }
        return true;
    }

    // [16]
    /*
    输入是int[][] input, input[0]是input[1] 的parent，比如 {{1,4}, {1,5}, {2,5}, {3,6}, {6,7}}会形成上面的图
    第一问是只有0个parents和只有1个parent的节点
     */
    public static List<Integer> findNodesWithZeroOrOneParent(int[][] edges) {
        if (edges == null || edges.length == 0) {
            return new ArrayList<>();
        }

        Map<Integer, Set<Integer>> parentMap = new HashMap<>();
        Set<Integer> allNodes = new HashSet<>();

        // Populate the parent map and all nodes set
        for (int[] edge : edges) {
            int parent = edge[0];
            int child = edge[1];

            allNodes.add(parent);
            allNodes.add(child);

            parentMap.computeIfAbsent(child, k -> new HashSet<>()).add(parent);
        }

        List<Integer> result = new ArrayList<>();

        // Find nodes with zero or one parent
        for (Integer node : allNodes) {
            int parentCount = parentMap.getOrDefault(node, Collections.emptySet()).size();
            if (parentCount <= 1) {
                result.add(node);
            }
        }

        return result;
    }

    // [17]
    /*
    两个节点是否有公共祖先
     */
    public static boolean hasCommonAncestor(int[][] edges, int x, int y) {
        if (edges == null || edges.length == 0) {
            return false;
        }

        // Map to store direct parents of each node
        Map<Integer, Set<Integer>> directParents = new HashMap<>();

        // Populate the direct parents map
        for (int[] edge : edges) {
            int parent = edge[0];
            int child = edge[1];

            directParents.putIfAbsent(child, new HashSet<>());
            directParents.get(child).add(parent);
        }

        // Find all ancestors of a given node
        Set<Integer> parentsOfX = findAllParents(x, directParents);
        Set<Integer> parentsOfY = findAllParents(y, directParents);

        // Check if there's any common ancestor
        for (Integer parentOfX : parentsOfX) {
            if (parentsOfY.contains(parentOfX)) {
                return true;
            }
        }

        return false;
    }

    // Helper method to find all parents of a given node using DFS
    private static Set<Integer> findAllParents(int node, Map<Integer, Set<Integer>> directParents) {
        Set<Integer> result = new HashSet<>();
        Stack<Integer> stack = new Stack<>();
        stack.push(node);

        while (!stack.isEmpty()) {
            int current = stack.pop();
            Set<Integer> parents = directParents.get(current);

            for (Integer parent : parents) {
                if (!result.contains(parent)) {
                    result.add(parent);
                    stack.push(parent);
                }
            }
        }

        return result;
    }

    // [18]
    /*
    最远祖先
     */
    public static Integer earliestAncestor(int[][] parentChildPairs, int x) {
        // Map to store direct parents of each node
        Map<Integer, Set<Integer>> directParents = new HashMap<>();

        // Populate the direct parents map
        for (int[] pair : parentChildPairs) {
            int parent = pair[0];
            int child = pair[1];
            directParents.computeIfAbsent(child, k -> new HashSet<>()).add(parent);
        }

        Queue<Integer> q = new LinkedList<>();
        Set<Integer> visited = new HashSet<>();

        // Start BFS with the given node `x`
        q.add(x);
        Integer curr = null;
        while (!q.isEmpty()) {
            int size = q.size();
            while (size-- > 0) {
                curr = q.poll();
                Set<Integer> parents = directParents.get(curr);

                if (parents == null) {
                    continue;
                }

                for (int parent : parents) {
                    if (visited.contains(parent)) {
                        continue;
                    }
                    q.add(parent);
                    visited.add(parent);
                }
            }
        }

        return curr;
    }

    // [19]
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
    public static List<List<String>> invalidBadgeRecords(String[][] records) {
        // Initialize the result lists
        List<List<String>> result = new ArrayList<>();
        List<String> invalidEnter = new ArrayList<>();
        List<String> invalidExit = new ArrayList<>();
        result.add(invalidEnter);
        result.add(invalidExit);

        // Initialize the state map to track each user's entry/exit state
        Set<String> room = new HashSet<>();

        // Process each record
        for (String[] record : records) {
            String name = record[0];
            String action = record[1];

            if (action.equals("enter")) {
                if (room.contains(name)) {
                    invalidExit.add(name); // Invalid entry
                } else {
                    room.add(name);
                }
            } else if (action.equals("exit")) {
                if (!room.contains(name)) {
                    invalidEnter.add(name); // Invalid exit
                } else {
                    room.remove(name);
                }
            }
        }

        invalidExit.addAll(room);

        return result;
    }

    // [20]
    /*
    给 list of [name, time], time is string format: '1300' //下午一点
    return: list of names and the times when their swipe badges within one hour. if there are multiple intervals that satisfy the condition, return any one of them.
    name1: time1, time2, time3...
    name2: time1, time2, time3, time4, time5...
    example:
    input: [['James', '1300'], ['Martha', '1600'], ['Martha', '1620'], ['Martha', '1530']]
    output: {
    'Martha': ['1600', '1620', '1530']
    }
     */
    public static Map<String, List<Integer>> frequentAccess(String[][] records) {
        // Initialize the result list
        Map<String, List<Integer>> result = new HashMap<>();

        // Map to store the name and corresponding list of timestamps
        Map<String, List<Integer>> times = new HashMap<>();

        // Populate the map with names and timestamps
        for (String[] record : records) {
            String name = record[0];
            int timestamp = Integer.parseInt(record[1]);
            times.computeIfAbsent(name, k -> new ArrayList<>()).add(timestamp);
        }

        // Process each person's timestamps to find frequent accesses
        for (Map.Entry<String, List<Integer>> entry : times.entrySet()) {
            String name = entry.getKey();
            List<Integer> timestamps = entry.getValue();
            timestamps.sort(Karat::timeDifference);

            Deque<Integer> timeWindow = new ArrayDeque<>();

            for (int timestamp : timestamps) {
                if (timeWindow.isEmpty() || timeDifference(timestamp, timeWindow.peekFirst()) <= 60) {
                    timeWindow.add(timestamp);

                    if (timeWindow.size() >= 3) {
                        result.computeIfAbsent(name, k -> new ArrayList<>()).addAll(timeWindow);
                        break;
                    }
                } else {
                    while (!timeWindow.isEmpty() && timeDifference(timestamp, timeWindow.peekFirst()) > 60) {
                        timeWindow.poll();
                    }
                }
            }
        }

        return result;
    }

    private static int timeDifference(int a, int b) {
        int aHour = a / 100;
        int bHour = b / 100;
        int aMinute = a % 100;
        int bMinute = b % 100;
        return (aHour * 60 + aMinute) - (bHour * 60 + bMinute);
    }

    // [21]
    /*
    We want to find employees who badged into our secured room together often.
    Given an unordered list of names and access times over a single day, find the largest group of people that were in the room together during two or more separate time periods, and the times when they were all present.
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
    enum Action {
        ENTER, EXIT
    }

    record Event(String name, int time, Action action) {
    }

    public static String together(String[][] records) {
        Map<Integer, Set<Event>> events = new TreeMap<>();

        for (String[] record : records) {
            String name = record[0];
            int time = Integer.parseInt(record[1]);
            Action action = record[2].equals("enter") ? Action.ENTER : Action.EXIT;
            events.computeIfAbsent(time, k -> new HashSet<>()).add(new Event(name, time, action));
        }

        Map<Set<String>, List<int[]>> count = new HashMap<>();

        Integer prev = null;
        LinkedHashSet<String> room = new LinkedHashSet<>();
        for (Map.Entry<Integer, Set<Event>> entry : events.entrySet()) {
            int time = entry.getKey();

            Set<Event> eventSet = entry.getValue();
            for (Event event : eventSet) {
                if (event.action == Action.ENTER) {
                    room.add(event.name);
                    Map<Set<String>, List<int[]>> copy = new HashMap<>(count);
                    if (count.isEmpty()) {
                        copy.computeIfAbsent(new HashSet<>(room), k -> new ArrayList<>()).add(new int[]{time, -1});
                    }
                    for (Map.Entry<Set<String>, List<int[]>> e : count.entrySet()) {
                        Set<String> group = e.getKey();
                        List<int[]> times = e.getValue();
                        Set<String> merge = new HashSet<>(group);
                        merge.add(event.name);
                        if (times.get(times.size() - 1)[1] == -1) {
                            if (merge.size() == group.size()) {
                                copy.computeIfAbsent(group, k -> new ArrayList<>(times));
                            } else {
                                copy.computeIfAbsent(merge, k -> new ArrayList<>()).add(new int[]{time, -1});
                            }
                        } else {
                            copy.computeIfAbsent(group, k -> new ArrayList<>(times));
                        }
                    }
                    Set<String> newGroup = new LinkedHashSet<>();
                    newGroup.add(event.name);
                    copy.computeIfAbsent(newGroup, k -> new ArrayList<>()).add(new int[]{time, -1});
                    count = copy;
                } else {
                    room.remove(event.name);
                    for (Map.Entry<Set<String>, List<int[]>> e : count.entrySet()) {
                        Set<String> group = e.getKey();
                        List<int[]> times = e.getValue();
                        if (group.contains(event.name)) {
                            if (times.get(times.size() - 1)[1] == -1) {
                                times.get(times.size() - 1)[1] = time;
                            }
                        }
                    }
                }
            }
            prev = time;
        }

        int maxGroupSize = 0;
        String resultGroup = "";
        for (Map.Entry<Set<String>, List<int[]>> entry : count.entrySet()) {
            Set<String> group = entry.getKey();
            List<int[]> times = entry.getValue();
            if (times.size() > 1 && group.size() > maxGroupSize) {
                maxGroupSize = group.size();
                resultGroup = String.join(", ", group) + ": " + times.stream().map(t -> t[0] + " to " + t[1]).collect(Collectors.joining(", "));
            }
        }

        return resultGroup;
    }

    // [22]
    /*
    类似meeting rooms，输入是一个int[][] meetings, int start, int end, 每个数都是时间，13：00 -> 1300， 9：30 -> 18930， 看新的meeting 能不能安排到meetings
    ex: {[1300, 1500], [930, 1200],[830, 845]}, 新的meeting[820, 830], return true; [1450, 1500] return false;
     */
    public static boolean canSchedule(List<int[]> meetings, int start, int end) {
        for (int[] meeting : meetings) {
            int meetingStart = meeting[0];
            int meetingEnd = meeting[1];

            if ((start >= meetingStart && start < meetingEnd) ||
                    (end > meetingStart && end <= meetingEnd) ||
                    (start < meetingStart && end > meetingEnd)) {
                return false;
            }
        }
        return true;
    }

    // [23]
    /*
    类似merge interval，唯一的区别是输出，输出空闲的时间段，merge完后，再把两两个之间的空的输出就好，注意要加上0 - 第一个的start time
     */
    public static List<int[]> spareTime(List<int[]> meetings) {
        if (meetings == null || meetings.isEmpty()) {
            return new ArrayList<>();
        }

        List<int[]> mergedMeetings = mergeMeetings(meetings);
        List<int[]> result = new ArrayList<>();

        int start = 0;
        for (int[] meeting : mergedMeetings) {
            int end = meeting[0];
            if (start < end) {
                result.add(new int[]{start, end});
            }
            start = meeting[1];
        }

        return result;
    }

    private static List<int[]> mergeMeetings(List<int[]> meetings) {
        List<int[]> result = new ArrayList<>();
        if (meetings.isEmpty()) {
            return result;
        }

        meetings.sort(Comparator.comparingInt(a -> a[0]));

        int start = meetings.get(0)[0];
        int end = meetings.get(0)[1];

        for (int i = 1; i < meetings.size(); i++) {
            int[] meeting = meetings.get(i);
            if (end >= meeting[0]) {
                end = Math.max(end, meeting[1]);
            } else {
                result.add(new int[]{start, end});
                start = meeting[0];
                end = meeting[1];
            }
        }

        result.add(new int[]{start, end});
        return result;
    }

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
    public static int[] bestDayToCross(int[] altitudes, int[][] forecasts) {
        int[] noSnow = new int[altitudes.length];
        int[] result = new int[]{-1, -1};
        int minClimbs = Integer.MAX_VALUE;

        for (int d = 0; d < forecasts.length; d++) {
            int[] forecast = forecasts[d];
            boolean canCross = true;
            int maxAltitude = Integer.MIN_VALUE;
            int minAltitude = Integer.MAX_VALUE;
            for (int i = 0; i < altitudes.length; i++) {
                if (forecast[i] != 0) {
                    noSnow[i] = 0;
                    altitudes[i] += forecast[i];
                } else {
                    noSnow[i]++;
                }
                if (noSnow[i] > 2) {
                    altitudes[i]--;
                }
                if (i == 0) {
                    continue;
                }
                if (Math.abs(altitudes[i] - altitudes[i - 1]) > 1) {
                    canCross = false;
                    break;
                } else {
                    maxAltitude = Math.max(maxAltitude, altitudes[i]);
                    minAltitude = Math.min(minAltitude, altitudes[i]);
                }
            }
            int steps = maxAltitude - minAltitude;
            if (canCross && steps < minClimbs) {
                minClimbs = steps;
                result[0] = d;
                result[1] = steps;
            }
        }
        return result;
    }

    // [25]
    /*
    grid1 = [
        ['b', 'b', 'b', 'a', 'l', 'l', 'o', 'o'],
        ['b', 'a', 'c', 'c', 'e', 's', 'c', 'n'],
        ['a', 'l', 't', 'e', 'w', 'c', 'e', 'w'],
        ['a', 'l', 'o', 's', 's', 'e', 'c', 'c'],
        ['w', 'o', 'o', 'w', 'a', 'c', 'a', 'w'],
        ['i', 'b', 'w', 'o', 'w', 'w', 'o', 'w']
    ]
    word1_1 = "access"      # [(1, 1), (1, 2), (1, 3), (2, 3), (3, 3), (3, 4)]
    word1_2 = "balloon"     # [(0, 2), (0, 3), (0, 4), (0, 5), (0, 6), (0, 7), (1, 7)]
    word1_3 = "wow"         # [(4, 3), (5, 3), (5, 4)] OR
                            # [(5, 2), (5, 3), (5, 4)] OR
                            # [(5, 5), (5, 6), (5, 7)]
    word1_4 = "sec"         # [(3, 4), (3, 5), (3, 6)] OR
                            # [(3, 4), (3, 5), (4, 5)]   
    word1_5 = "bbaal"       # [(0, 0), (1, 0), (2, 0), (3, 0), (3, 1)]

    grid2 = [
      ['a'],
    ]
    word2_1 = "a"

    grid3 = [
        ['c', 'a'],
        ['t', 't'],
        ['h', 'a'],
        ['a', 'c'],
        ['t', 'g']
    ]
    word3_1 = "cat"
    word3_2 = "hat"
    grid4 = [
        ['c', 'c', 'x', 't', 'i', 'b'],
        ['c', 'a', 't', 'n', 'i', 'i'],
        ['a', 'x', 'n', 'x', 'p', 't'],
        ['t', 'x', 'i', 'x', 't', 't']
    ]
    word4_1 = "catnip"      # [(1, 0), (1, 1), (1, 2), (1, 3), (1, 4), (2, 4)] OR
                            # [(0, 1), (1, 1), (1, 2), (1, 3), (1, 4), (2, 4)]
    All test cases:
    search(grid1, word1_1) => [(1, 1), (1, 2), (1, 3), (2, 3), (3, 3), (3, 4)]
    search(grid1, word1_2) => [(0, 2), (0, 3), (0, 4), (0, 5), (0, 6), (0, 7), (1, 7)]
    search(grid1, word1_3) => [(4, 3), (5, 3), (5, 4)] OR
                              [(5, 2), (5, 3), (5, 4)] OR
                              [(5, 5), (5, 6), (5, 7)]
    search(grid1, word1_4) => [(3, 4), (3, 5), (3, 6)] OR
                              [(3, 4), (3, 5), (4, 5)]                           
    search(grid1, word1_5) => [(0, 0), (1, 0), (2, 0), (3, 0), (3, 1)]
    search(grid2, word2_1) => [(0, 0)]
    search(grid3, word3_1) => [(0, 0), (0, 1), (1, 1)]
    search(grid3, word3_2) => [(2, 0), (3, 0), (4, 0)]
    search(grid4, word4_1) => [(1, 0), (1, 1), (1, 2), (1, 3), (1, 4), (2, 4)] OR
                              [(0, 1), (1, 1), (1, 2), (1, 3), (1, 4), (2, 4)]

    Complexity analysis variables:
    r = number of rows. Χ
    c = number of columns
    w = length of the word
     */
    public static List<int[]> search(char[][] grid, String word) {
        for (int i = 0; i < grid.length; i++) {
            for (int j = 0; j < grid[0].length; j++) {
                if (grid[i][j] == word.charAt(0)) {
                    List<int[]> result = new ArrayList<>();
                    dfs(grid, word, i, j, 0, new ArrayList<>(), result);
                    if (!result.isEmpty()) {
                        return result;
                    }
                }
            }
        }
        return Collections.emptyList();
    }

    private static void dfs(char[][] grid, String word, int i, int j, int cur, List<int[]> path, List<int[]> result) {
        if (!result.isEmpty()) {
            return;
        }
        if (cur == word.length()) {
            result.addAll(path);
            return;
        }
        if (i < 0 || i >= grid.length || j < 0 || j >= grid[0].length || grid[i][j] != word.charAt(cur)) {
            return;
        }
        char temp = grid[i][j];
        grid[i][j] = ' ';
        path.add(new int[]{i, j});
        dfs(grid, word, i + 1, j, cur + 1, path, result);
//        dfs(grid, word, i - 1, j, cur + 1, path, result);
        dfs(grid, word, i, j + 1, cur + 1, path, result);
//        dfs(grid, word, i, j - 1, cur + 1, path, result);
        path.remove(path.size() - 1);
        grid[i][j] = temp;
    }
    
    // [26]
    /*
    We are writing software to analyze logs for toll booths on a highway. This highway is a divided highway with limited access; the only way on to or off of the highway is through a toll booth.
    There are three types of toll booths:
    * ENTRY (E in the diagram) toll booths, where a car goes through a booth as it enters the highway.
    * EXIT (X in the diagram) toll booths, where a car goes through a booth as it exits the highway.
    * MAINROAD (M in the diagram), which have sensors that record a license plate as a car drives through at full speed.
            Entry Booth                      Exit Booth
                |                                |
                E                                X
               /                                  \
    ---<------------<---------M---------<-----------<---------<----
                                             (West-bound side)
    ===============================================================
                                             (East-bound side)
    ------>--------->---------M--------->--------->--------->------
               \                                    /
                X                                  E
                |                                  |
            Exit Booth                     Entry Booth
    We would like to catch people who are driving at unsafe speeds on the highway. To help us do that, we would like to identify journeys where a driver does either of the following:
    * Drive an average of 130 km/h or greater in any individual 10km segment of tollway.
    * Drive an average of 120 km/h or greater in any two 10km segments of tollway.
    For example, consider the following journey:

    1000.000 TST002 270W ENTRY
    1275.000 TST002 260W EXIT
    In this case, the driver of TST002 drove 10 km in 275 seconds. We can calculate
    that this driver drove an average speed of ~130.91km/hr over this segment:
    10 km * 3600 sec/hr
    ------------------- = 130.91 km/hr
          275 sec
    Note that:
    * A license plate may have multiple journeys in one file, and if they drive at unsafe speeds in both journeys, both should be counted.
    * We do not mark speeding if they are not on the highway (i.e. for any driving between an EXIT and ENTRY event).
    * Speeding is only marked once per journey. For example, if there are 4 segments 120km/h or greater, or multiple segments 130km/h or greater, the journey is only counted once.
    3-1) Write a function catch_speeders in LogFile that returns a collection of license plates that drove at unsafe speeds during a journey in the LogFile.
         If the same license plate drives at unsafe speeds during two different journeys, the license plate should appear twice (once for each journey they drove at unsafe speeds).
         
     class LogEntry:
    Represents an entry from a single log line.
    Log lines look like this in the file:
    34400.409 SXY288 210E ENTRY
    Where:
    * 34400.409 is the timestamp in seconds since the software was started.
    * SXY288 is the license plate of the vehicle passing through the toll booth.
    * 210E is the location and traffic direction of the toll booth. Here, the
        toll booth is at 210 kilometers from the start of the tollway, and the E
        indicates that the toll booth was on the eastbound traffic side.
        Tollbooths are placed every ten kilometers.
    * ENTRY indicates which type of toll booth the vehicle went through. This is
        one of "ENTRY", "EXIT", or "MAINROAD".
     */
//    enum EntryType {
//        ENTRY, EXIT, MAINROAD
//    }
//    public record LogEntry(double timestamp, String licensePlate, String location, EntryType type) {}
//
//    public static List<String> catchSpeeders(List<LogEntry> logs) {
//        logs.sort(Comparator.comparingDouble(i -> i.timestamp));
//
//        Map<String, List<LogEntry>> drivers = new HashMap<>();
//        for (LogEntry log : logs) {
//            drivers.computeIfAbsent(log.licensePlate(), k -> new ArrayList<>()).add(log);
//        }
//
//        List<String> result = new ArrayList<>();
//        for (Map.Entry<String, List<LogEntry>> entry : drivers.entrySet()) {
//            List<LogEntry> driverLogs = entry.getValue();
//            int i = 0;
//            while (i < driverLogs.size()) {
//                LogEntry log = driverLogs.get(i);
//                if (log.type() == EntryType.ENTRY) {
//                    double maxSpeed = 0.0;
//                    while (i+1 < driverLogs.size()) {
//                        LogEntry nextLog = driverLogs.get(i+1);
//
//                        double dist = Double.parseDouble(nextLog.location().substring(0, nextLog.location().length() - 1)) - Double.parseDouble(log.location().substring(0, log.location().length() - 1));
//                        double time = nextLog.timestamp() - log.timestamp();
//                        double speed = dist / (time / 3600);
//
//                        if (speed >= 130) {
//                            result.add(log.licensePlate() + " over 130");
//                            break;
//                        }
//                        if (speed >= 120 && maxSpeed >= 120) {
//                            result.add(log.licensePlate() + " over 120");
//                            break;
//                        }
//
//                        maxSpeed = Math.max(maxSpeed, speed);
//
//                        if (nextLog.type() == EntryType.EXIT) {
//                            break;
//                        }
//                        i++;
//                    }
//                }
//                i++;
//            }
//        }
//        return result;
//    }

    static class LogEntry {
        double timestamp;
        String plate;
        int location;
        char direction;
        String event;

        public LogEntry(String logLine) {
            String[] parts = logLine.split(" ");
            this.timestamp = Double.parseDouble(parts[0]);
            this.plate = parts[1];
            this.location = Integer.parseInt(parts[2].substring(0, parts[2].length() - 1));
            this.direction = parts[2].charAt(parts[2].length() - 1);
            this.event = parts[3];
        }
    }

    static class LogFile {
        List<LogEntry> logEntries;

        public LogFile(List<String> logLines) {
            logEntries = new ArrayList<>();
            for (String logLine : logLines) {
                logEntries.add(new LogEntry(logLine));
            }
        }

        public List<String> catchSpeeders() {
            List<String> speeders = new ArrayList<>();
            Map<String, List<LogEntry>> entriesByPlate = new HashMap<>();

            // Group entries by license plate
            for (LogEntry entry : logEntries) {
                entriesByPlate.computeIfAbsent(entry.plate, k -> new ArrayList<>()).add(entry);
            }

            for (Map.Entry<String, List<LogEntry>> entry : entriesByPlate.entrySet()) {
                String plate = entry.getKey();
                List<LogEntry> entries = entry.getValue();
                entries.sort(Comparator.comparingDouble(e -> e.timestamp));
                List<LogEntry> journey = new ArrayList<>();
                boolean inJourney = false;

                for (LogEntry logEntry : entries) {
                    if (logEntry.event.equals("ENTRY")) {
                        journey.clear();
                        journey.add(logEntry);
                        inJourney = true;
                    } else if (logEntry.event.equals("EXIT") && inJourney) {
                        journey.add(logEntry);
                        if (isSpeeding(journey)) {
                            speeders.add(plate);
                        }
                        inJourney = false;
                        journey.clear();
                    } else if (inJourney) {
                        journey.add(logEntry);
                    }
                }
            }

            return speeders;
        }

        private boolean isSpeeding(List<LogEntry> journey) {
            List<Double> segments = new ArrayList<>();

            for (int i = 0; i < journey.size() - 1; i++) {
                LogEntry current = journey.get(i);
                LogEntry next = journey.get(i + 1);
                if ((current.event.equals("ENTRY") || current.event.equals("MAINROAD")) &&
                        (next.event.equals("MAINROAD") || next.event.equals("EXIT"))) {

                    int distance = Math.abs(next.location - current.location);
                    double timeElapsed = next.timestamp - current.timestamp;

                    if (distance == 10) {
                        double speed = (10 * 3600) / timeElapsed; // speed in km/hr
                        segments.add(speed);
                    }
                }
            }

            // Check for individual segments exceeding 130 km/h
            for (double speed : segments) {
                if (speed >= 130) {
                    return true;
                }
            }

            // Check for any two consecutive segments exceeding 120 km/h
            for (int i = 0; i < segments.size() - 1; i++) {
                double avgSpeed = (segments.get(i) + segments.get(i + 1)) / 2;
                if (avgSpeed >= 120) {
                    return true;
                }
            }

            return false;
        }
    }

    // [27]
    /*
    You are going on a camping trip, but before you leave you need to buy groceries. To optimize your time spent in the store, instead of buying the items from your shopping list in order, you plan to buy everything you need from one department before moving to the next.
    Given an unsorted list of products with their departments and a shopping list, return the time saved in terms of the number of department visits eliminated.
    Example:
    products = [
        ["Cheese",          "Dairy"],
        ["Carrots",         "Produce"],
        ["Potatoes",        "Produce"],
        ["Canned Tuna",     "Pantry"],
        ["Romaine Lettuce", "Produce"],
        ["Chocolate Milk",  "Dairy"],
        ["Flour",           "Pantry"],
        ["Iceberg Lettuce", "Produce"],
        ["Coffee",          "Pantry"],
        ["Pasta",           "Pantry"],
        ["Milk",            "Dairy"],
        ["Blueberries",     "Produce"],
        ["Pasta Sauce",     "Pantry"]
    ]
    list1 = ["Blueberries", "Milk", "Coffee", "Flour", "Cheese", "Carrots"]
    "Produce","Dairy", "Pantry"
    For example, buying the items from list1 in order would take 5 department visits, whereas your method would lead to only visiting 3 departments, a difference of 2 departments.

    Produce(Blueberries)->Dairy(Milk)->Pantry(Coffee/Flour)->Dairy(Cheese)->Produce(Carrots) = 5 department visits
    New: Produce(Blueberries/Carrots)->Pantry(Coffee/Flour)->Dairy(Milk/Cheese) = 3 department visits
    list2 = ["Blueberries", "Carrots", "Coffee", "Milk", "Flour", "Cheese"] => 2
    list3 = ["Blueberries", "Carrots", "Romaine Lettuce", "Iceberg Lettuce"] => 0
    list4 = ["Milk", "Flour", "Chocolate Milk", "Pasta Sauce"] => 2
    list5 = ["Cheese", "Potatoes", "Blueberries", "Canned Tuna"] => 0
    All Test Cases:
    shopping(products, list1) => 2
    shopping(products, list2) => 2
    shopping(products, list3) => 0
    shopping(products, list4) => 2
    shopping(products, list5) => 0
    Complexity Variable:
    n: number of products
     */
    public static int shopping(String[][] products, String[] list) {
        Map<String, String> productToDepartment = new HashMap<>();
        for (String[] product : products) {
            productToDepartment.put(product[0], product[1]);
        }

        int count = 0;
        String department = "";
        Set<String> visited = new HashSet<>();
        for (String item : list) {
            String nextDepartment = productToDepartment.get(item);
            if (!nextDepartment.equals(department)) {
                department = nextDepartment;
                count++;
            }
            visited.add(department);
        }

        return count - visited.size();
    }

    // [28]
    /*
    You and your friends are driving to a Campground to go camping. Only 2 of you have cars, so you will be carpooling.
    Routes to the campground are linear, so each location will only lead to 1 location and there will be no loops or detours. Both cars will leave from their starting locations at the same time. The first car to pass someone's location will pick them up. If both cars arrive at the same time, the person can go in either car.
    Roads are provided as a directed list of connected locations with the duration (in minutes) it takes to drive between the locations.
    [Origin, Destination, Duration it takes to drive]
    Given a list of roads, a list of starting locations and a list of people/where they live, return a collection of who will be in each car upon arrival to the Campground.
    ------------------------------------------------------
    Bridgewater--(30)-->Caledonia--(15)-->New Grafton--(5)-->Campground
                                           ^
    Liverpool---(10)---Milton-----(30)-----^
    roads1 = [
        ["Bridgewater", "Caledonia", "30"], <= The road from Bridgewater to Caledonia takes 30 minutes to drive.
        ["Caledonia", "New Grafton", "15"],
        ["New Grafton", "Campground", "5"],
        ["Liverpool", "Milton", "10"],
        ["Milton", "New Grafton", "30"]
    ]
    starts1 = ["Bridgewater", "Liverpool"]
    people1 = [
        ["Jessie", "Bridgewater"], ["Travis", "Caledonia"],
        ["Jeremy", "New Grafton"], ["Katie", "Liverpool"]
    ]
    Car1 path: (from Bridgewater): [Bridgewater(0, Jessie)->Caledonia(30, Travis)->New Grafton(45)->Campground(50)]
    Car2 path: (from Liverpool): [Liverpool(0, Katie)->Milton(10)->New Grafton(40, Jeremy)->Campground(45)]
    Output (In any order/format):
        [Jessie, Travis], [Katie, Jeremy]
    --------------------------------------
    Riverport->Chester->Campground
                 ^
    Halifax------^
    roads2 = [["Riverport", "Chester", "40"], ["Chester", "Campground", "60"], ["Halifax", "Chester", "40"]]
    starts2 = ["Riverport", "Halifax"]
    people2 = [["Colin", "Riverport"], ["Sam", "Chester"], ["Alyssa", "Halifax"]]
    Riverport->Bridgewater->Liverpool->Campground
    Output (In any order/format):
        [Colin, Sam], [Alyssa] OR [Colin], [Alyssa, Sam]
    ----------------------------------------
    Riverport->Bridgewater->Liverpool->Campground
    roads3 = [["Riverport", "Bridgewater", "1"], ["Bridgewater", "Liverpool", "1"], ["Liverpool", "Campground", "1"]]
    starts3_1 = ["Riverport", "Bridgewater"]
    starts3_2 = ["Bridgewater", "Riverport"]
    starts3_3 = ["Riverport", "Liverpool"]
    people3 = [["Colin", "Riverport"], ["Jessie", "Bridgewater"], ["Sam", "Liverpool"]]
    Output (starts3_1/starts3_2):  [Colin], [Jessie, Sam] - (Cars can be in any order)
    Output (starts3_3): [Jessie, Colin], [Sam]
    ----------------------------------------
    All Test Cases: (Cars can be in either order)
    carpool(roads1, starts1, people1) => [Jessie, Travis], [Katie, Jeremy]
    carpool(roads2, starts2, people2) => [Colin, Sam], [Alyssa] OR [Colin], [Alyssa, Sam]
    carpool(roads3, starts3_1, people3) => [Colin], [Jessie, Sam]
    carpool(roads3, starts3_2, people3) => [Jessie, Sam], [Colin]
    carpool(roads3, starts3_3, people3) => [Jessie, Colin], [Sam]
    ----------------------------------------
    Complexity Variable:
    n = number of roads
     */
    public record Car(String start, String loc, int time) {
    }

    public static List<List<String>> carpool(String[][] roads, String[] starts, String[][] people) {
        Map<String, Map<String, Integer>> graph = new HashMap<>();
        for (String[] road : roads) {
            graph.computeIfAbsent(road[0], k -> new HashMap<>()).put(road[1], Integer.parseInt(road[2]));
        }
        PriorityQueue<Car> pq = new PriorityQueue<>(Comparator.comparingInt(a -> a.time));
        for (String start : starts) {
            pq.offer(new Car(start, start, 0));
        }

        Map<String, List<String>> peopleAtLocation = new HashMap<>();
        for (String[] person : people) {
            peopleAtLocation.computeIfAbsent(person[1], k -> new ArrayList<>()).add(person[0]);
        }

        Map<String, List<String>> cars = new HashMap<>();
        while (!pq.isEmpty()) {
            Car car = pq.poll();
            cars.computeIfAbsent(car.start(), k -> new ArrayList<>()).addAll(peopleAtLocation.getOrDefault(car.loc(), Collections.emptyList()));
            peopleAtLocation.remove(car.loc());
            for (Map.Entry<String, Integer> entry : graph.getOrDefault(car.loc(), Collections.emptyMap()).entrySet()) {
                pq.offer(new Car(car.start(), entry.getKey(), car.time() + entry.getValue()));
            }
        }

        List<List<String>> result = new ArrayList<>();
        for (String start : starts) {
            result.add(cars.getOrDefault(start, Collections.emptyList()));
        }

        return result;
    }

    // [29]
    /*
    board3 中1代表钻石，给出起点和终点，问有没有一条不走回头路的路线，能从起点走到终点，并拿走所有的钻石，给出所有的最短路径。
    board3 = [
        [  1,  0,  0, 0, 0 ],
        [  0, -1, -1, 0, 0 ],
        [  0, -1,  0, 1, 0 ],
        [ -1,  0,  0, 0, 0 ],
        [  0,  1, -1, 0, 0 ],
        [  0,  0,  0, 0, 0 ],
    ]


    treasure(board3, (5, 0), (0, 4)) -> None

    treasure(board3, (5, 2), (2, 0)) ->
      [(5, 2), (5, 1), (4, 1), (3, 1), (3, 2), (2, 2), (2, 3), (1, 3), (0, 3), (0, 2), (0, 1), (0, 0), (1, 0), (2, 0)]
      Or
      [(5, 2), (5, 1), (4, 1), (3, 1), (3, 2), (3, 3), (2, 3), (1, 3), (0, 3), (0, 2), (0, 1), (0, 0), (1, 0), (2, 0)]

    treasure(board3, (0, 0), (4, 1)) ->
      [(0, 0), (0, 1), (0, 2), (0, 3), (1, 3), (2, 3), (2, 2), (3, 2), (3, 1), (4, 1)]
      Or
      [(0, 0), (0, 1), (0, 2), (0, 3), (1, 3), (2, 3), (3, 3), (3, 2), (3, 1), (4, 1)]
     */
    public static List<List<int[]>> findAllTreasures(int[][] board, int[] start, int[] end) {
        if (board == null) {
            return new ArrayList<>();
        }

        int numTreasures = 0;
        // Count total number of treasures
        for (int[] rows : board) {
            for (int j = 0; j < board[0].length; j++) {
                if (rows[j] == 1) {
                    numTreasures++;
                }
            }
        }

        List<List<int[]>> paths = new ArrayList<>();
        dfs(board, start[0], start[1], end, new ArrayList<>(), numTreasures, paths);

        if (paths.isEmpty()) {
            return Collections.emptyList();
        }

        int minPathsLength = Integer.MAX_VALUE;
        for (List<int[]> path : paths) {
            minPathsLength = Math.min(minPathsLength, path.size());
        }

        List<List<int[]>> result = new ArrayList<>();
        for (List<int[]> path : paths) {
            if (path.size() == minPathsLength) {
                result.add(path);
            }
        }

        return result;
    }

    private static void dfs(int[][] board, int x, int y, int[] end, List<int[]> path, int remainTreasure, List<List<int[]>> paths) {
        if (x < 0 || x >= board.length || y < 0 || y >= board[0].length || board[x][y] == -1 || board[x][y] == 2) {
            return;
        }

        path.add(new int[]{x, y});
        int temp = board[x][y];

        if (temp == 1) {
            remainTreasure--;
        }

        if (x == end[0] && y == end[1] && remainTreasure == 0) {
            paths.add(new ArrayList<>(path));
            path.remove(path.size() - 1);
            board[x][y] = temp;
            return;
        }

        board[x][y] = 2;

        // Explore all 4 directions
        dfs(board, x + 1, y, end, path, remainTreasure, paths);
        dfs(board, x - 1, y, end, path, remainTreasure, paths);
        dfs(board, x, y + 1, end, path, remainTreasure, paths);
        dfs(board, x, y - 1, end, path, remainTreasure, paths);

        board[x][y] = temp;
        path.remove(path.size() - 1);
    }

    // [30]
    public class TokenBucketRateLimiter {
        private final int capacity;
        private final Duration period;
        private final int tokensPerPeriod;
        private final Clock clock;
        private final Map<String, TokenBucket> userTokenBucket = new HashMap<>();

        public TokenBucketRateLimiter(int capacity, Duration period, int tokensPerPeriod, Clock clock) {
            this.capacity = capacity;
            this.period = period;
            this.tokensPerPeriod = tokensPerPeriod;
            this.clock = clock;
        }

        public boolean allowed(String userId) {
            // Initialize an empty bucket for new users or retrieve existing one.
            TokenBucket bucket = userTokenBucket.computeIfAbsent(userId,
                    k -> new TokenBucket(clock.millis(), tokensPerPeriod));

            // Refill the bucket with available tokens based on
            // elapsed time since last refill.
            bucket.refill();

            // Allow this request if a token was available and consumed,
            // Otherwise, reject the request.
            return bucket.consume();
        }

        private class TokenBucket {
            private volatile long refillTimestamp; // Timestamp of the last refill.
            private final AtomicLong tokens; // Current number of tokens in the bucket.

            TokenBucket(long refillTimestamp, long tokens) {
                this.refillTimestamp = refillTimestamp;
                this.tokens = new AtomicLong(tokens);
            }

            /**
             * Regenerates tokens at fixed intervals. Waits for the entire period
             * to elapse before regenerating the full amount of tokens
             * designated for that period.
             */
            private void refill() {
                if (tokens.get() == capacity) {
                    return;
                }
                long now = clock.millis();
                long elapsedTime = now - refillTimestamp;
                long elapsedPeriods = elapsedTime / period.toMillis();
                long availableTokens = elapsedPeriods * tokensPerPeriod;

                if (elapsedPeriods > 0) {
                    tokens.set(Math.min(capacity, tokens.get() + availableTokens));
                    refillTimestamp = now;
                }
            }

            boolean consume() {
                if (tokens.get() > 0) {
                    tokens.decrementAndGet();
                    return true;
                } else {
                    return false;
                }
            }
        }
    }

    // [31]
    public class LeakyBucketRateLimiter {
        // The maximum capacity of the bucket. Once water reaches this level, further requests are rejected.
        private final long threshold;
        // Time unit for measuring the leak rate, set to one second (1000 milliseconds).
        private final long windowUnit = 1000;
        // Current level of water in the bucket, managed atomically to ensure thread safety.
        private final AtomicLong water = new AtomicLong(0);
        // Timestamp of the last leak calculation, used to determine how much water has leaked over time.
        private volatile long lastLeakTimestamp;

        /**
         * Constructs a LeakyBucketRateLimiter with a specified threshold.
         *
         * @param threshold the maximum number of requests that can be handled in the time window.
         */
        public LeakyBucketRateLimiter(long threshold) {
            this.threshold = threshold;
            this.lastLeakTimestamp = System.currentTimeMillis();
        }

        /**
         * Tries to acquire a permit for a request based on the current state of the bucket.
         *
         * @return true if the request can be accommodated (water level is below threshold), false otherwise.
         */
        public boolean tryAcquire() {
            long currentTime = System.currentTimeMillis();
            // Calculate the amount of water that has leaked since the last check.
            long leakedAmount = ((currentTime - lastLeakTimestamp) / windowUnit) * threshold;

            // If any water has leaked, reduce the water level accordingly.
            if (leakedAmount > 0) {
                water.addAndGet(-leakedAmount);
                lastLeakTimestamp = currentTime;
            }

            // Ensure the water level does not go below zero.
            if (water.get() < 0) {
                water.set(0);
            }

            // If the bucket is not full, increment the water level and allow the request.
            if (water.get() < threshold) {
                water.getAndIncrement();
                return true;
            }

            // If the bucket is full, reject the request.
            return false;
        }
    }

    public static void main(String[] args) {
        String[] cpdomains1 = {"9001 discuss.leetcode.com"};
        System.out.println(subdomainVisits(cpdomains1)); // [9001 discuss.leetcode.com, 9001 leetcode.com, 9001 com]
        String[] cpdomains2 = {"900 google.mail.com", "50 yahoo.com", "1 intel.mail.com", "5 wiki.org"};
        System.out.println(subdomainVisits(cpdomains2)); // [951 com, 900 google.mail.com, 1 intel.mail.com, 5 org, 5 wiki.org, 901 mail.com, 50 yahoo.com]

        List<String> history1 = Arrays.asList("3234.html", "xys.html", "7hsaa.html");
        List<String> history2 = Arrays.asList("3234.html", "sdhsfjdsh.html", "xys.html", "7hsaa.html");
        System.out.println(longestCommonContinuousSubarray(history1, history2)); // [xys.html, 7hsaa.html]

        String[] adClicks = {
                "122.121.0.1,2016-11-03 11:41:19,Buy wool coats for your pets",
                "96.3.199.11,2016-10-15 20:18:31,2017 Pet Mittens",
                "122.121.0.250,2016-11-01 06:13:13,The Best Hollywood Coats",
                "82.1.106.8,2016-11-12 23:05:14,Buy wool coats for your pets",
                "92.130.6.144,2017-01-01 03:18:55,Buy wool coats for your pets",
                "92.130.6.145,2017-01-01 03:18:55,2017 Pet Mittens"
        };
        String[] allUserIps = {
                "2339985511,122.121.0.155",
                "234111110,122.121.0.1",
                "3123122444,92.130.6.145",
                "39471289472,2001:0db8:ac10:fe01:0000:0000:0000:0000",
                "8321125440,82.1.106.8",
                "99911063,92.130.6.144"
        };
        String[] completedPurchaseUserIds = {
                "3123122444", "234111110", "8321125440", "99911063"
        };
        System.out.println(adConversionRate(completedPurchaseUserIds, adClicks, allUserIps));

        List<String[]> studentCoursePairs1 = Arrays.asList(
                new String[]{"58", "Software Design"},
                new String[]{"58", "Linear Algebra"},
                new String[]{"94", "Art History"},
                new String[]{"94", "Operating Systems"},
                new String[]{"17", "Software Design"},
                new String[]{"58", "Mechanics"},
                new String[]{"58", "Economics"},
                new String[]{"17", "Linear Algebra"},
                new String[]{"17", "Political Science"},
                new String[]{"94", "Economics"},
                new String[]{"25", "Economics"}
        );
        System.out.println(findPairs(studentCoursePairs1));

        List<String[]> studentCoursePairs2 = Arrays.asList(
                new String[]{"42", "Software Design"},
                new String[]{"0", "Advanced Mechanics"},
                new String[]{"9", "Art History"}
        );
        System.out.println(findPairs(studentCoursePairs2));

        List<String[]> allCourses = Arrays.asList(
                new String[]{"Logic", "COBOL"},
                new String[]{"Data Structures", "Algorithms"},
                new String[]{"Creative Writing", "Data Structures"},
                new String[]{"Algorithms", "COBOL"},
                new String[]{"Intro to Computer Science", "Data Structures"},
                new String[]{"Logic", "Compilers"},
                new String[]{"Data Structures", "Logic"},
                new String[]{"Creative Writing", "System Administration"},
                new String[]{"Databases", "System Administration"},
                new String[]{"Creative Writing", "Databases"},
                new String[]{"Intro to Computer Science", "Graphics"}
        );
        System.out.println(findMidCourses(allCourses));

        String[] words = {"The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog"};
        int maxLen = 10;

        List<String> wrappedLines = wordWrap(words, maxLen);

        for (String line : wrappedLines) {
            System.out.println(line);
        }

        String[] lines = new String[]{
                "The day began as still as the",
                "night abruptly lighted with",
                "brilliant flame"
        };
        for (String line : reflowAndJustify(lines, 24)) {
            System.out.println(line);
        }

        int[][] matrix = new int[][]{
                {1, 1, 1, 1},
                {0, 1, 1, 1},
                {0, 1, 0, 0},
                {1, 1, 0, 1},
                {0, 0, 1, 1}
        };
        List<List<Integer>> rows1_1 = Arrays.asList(
                List.of(),
                List.of(1),
                List.of(1, 2),
                List.of(1),
                List.of(2)
        );
        List<List<Integer>> columns1_1 = Arrays.asList(
                List.of(2, 1),
                List.of(1),
                List.of(2),
                List.of(1)
        );
        System.out.println(isValidNonogram(matrix, rows1_1, columns1_1)); // true

        List<List<Integer>> rows1_2 = Arrays.asList(
                List.of(),
                List.of(),
                List.of(1),
                List.of(1),
                List.of(1, 1)
        );
        List<List<Integer>> columns1_2 = Arrays.asList(
                List.of(2),
                List.of(1),
                List.of(2),
                List.of(1)
        );
        System.out.println(isValidNonogram(matrix, rows1_2, columns1_2)); // false

        int[][] matrix2 = new int[][]{
                {1, 1},
                {0, 0},
                {0, 0},
                {1, 0}
        };
        List<List<Integer>> rows2_1 = Arrays.asList(
                List.of(),
                List.of(2),
                List.of(2),
                List.of(1)
        );
        List<List<Integer>> columns2_1 = Arrays.asList(
                List.of(1, 1),
                List.of(3)
        );
        System.out.println(isValidNonogram(matrix2, rows2_1, columns2_1)); // false

        int[][] edges = {{1, 4}, {1, 5}, {2, 5}, {3, 6}, {6, 7}};

        List<Integer> nodes = findNodesWithZeroOrOneParent(edges);
        System.out.println("Nodes with zero or one parent: " + nodes); // Output: [1, 2, 4, 7, 8, 9]

        int[][] parentChildPairs = {
                {1, 3},
                {2, 3},
                {3, 6},
                {5, 6},
                {5, 7},
                {4, 5},
                {4, 8},
                {8, 9}
        };

        /*
          1   2   4
           \ /   / \
            3   5   8
             \ / \   \
              6   7   9
         */

        int x = 6;
        int earliestAncestor = earliestAncestor(parentChildPairs, x);
        System.out.println("The earliest ancestor of node " + x + " is: " + earliestAncestor);  // Output: 1 or 2

        String[][] badgeRecords = {
                {"Martha", "exit"},
                {"Paul", "enter"},
                {"Martha", "enter"},
                {"Martha", "exit"},
                {"Jennifer", "enter"},
                {"Paul", "enter"},
                {"Curtis", "enter"},
                {"Paul", "exit"},
                {"Martha", "enter"},
                {"Martha", "exit"},
                {"Jennifer", "exit"}
        };
        List<List<String>> invalidBadgeRecords = invalidBadgeRecords(badgeRecords);
        System.out.println("People who entered without their badge: " + invalidBadgeRecords.get(0)); // Output: [Paul, Curtis]
        System.out.println("People who exited without their badge: " + invalidBadgeRecords.get(1)); // Output: [Martha]

        String[][] records = {
                {"James", "1300"},
                {"Martha", "1600"},
                {"Martha", "1620"},
                {"Martha", "1530"}
        };
        Map<String, List<Integer>> frequentAccess = frequentAccess(records);
        System.out.println(frequentAccess); // Output: {Martha=[1600, 1620, 1530]}

        List<int[]> meetings = new ArrayList<>(List.of(new int[]{1, 3}, new int[]{2, 6}, new int[]{8, 10}, new int[]{15, 18}));
        List<int[]> freeTimes = spareTime(meetings);
        for (int[] freeTime : freeTimes) {
            System.out.println("Free time: [" + freeTime[0] + ", " + freeTime[1] + "]");
        }

        records = new String[][]{
                {"Paul", "1", "enter"},
                {"Curtis", "2", "enter"},
                {"Jennifer", "30", "enter"},
                {"Nick", "30", "enter"},
                {"John", "455", "enter"},
                {"John", "512", "exit"},
                {"Ben", "457", "enter"},
                {"Ben", "458", "exit"},
                {"Robin", "459", "enter"},
                {"Robin", "500", "exit"},
                {"Nick", "630", "exit"},
                {"Jennifer", "715", "exit"},
                {"Steve", "815", "enter"},
                {"John", "930", "enter"},
                {"Steve", "1000", "exit"},
                {"Angela", "1115", "enter"},
                {"Curtis", "1510", "exit"},
                {"John", "1510", "exit"},
                {"Angela", "2045", "exit"},
                {"Paul", "2145", "exit"}
        };
        System.out.println(together(records));

        records = new String[][]{
                {"Paul", "10", "enter"},
                {"Curtis", "222", "enter"},
                {"John", "330", "enter"},
                {"Jennifer", "330", "enter"},
                {"Curtis", "330", "exit"},
                {"Paul", "330", "exit"},
                {"Curtis", "1410", "enter"},
                {"Paul", "1410", "enter"},
                {"Jennifer", "1410", "exit"},
                {"John", "1410", "exit"},
                {"Paul", "1545", "exit"},
                {"Curtis", "1630", "exit"}
        };
        System.out.println(together(records));

        int[] altitudes1 = {0, 1, 2, 1};
        int[][] forecasts1 = {
                {1, 0, 1, 0},
                {0, 0, 0, 0},
                {1, 1, 0, 2}
        };
        System.out.println(Arrays.toString(bestDayToCross(altitudes1, forecasts1))); // [2, 1]

        char[][] grid1 = {
                {'b', 'b', 'b', 'a', 'l', 'l', 'o', 'o'},
                {'b', 'a', 'c', 'c', 'e', 's', 'c', 'n'},
                {'a', 'l', 't', 'e', 'w', 'c', 'e', 'w'},
                {'a', 'l', 'o', 's', 's', 'e', 'c', 'c'},
                {'w', 'o', 'o', 'w', 'a', 'c', 'a', 'w'},
                {'i', 'b', 'w', 'o', 'w', 'w', 'o', 'w'}
        };
        String word1_1 = "access";
        System.out.println(search(grid1, word1_1).stream().map(Arrays::toString).collect(Collectors.joining(", "))); // [[1, 1], [1, 2], [1, 3], [2, 3], [3, 3], [3, 4]]
        String word1_2 = "balloon";
        System.out.println(search(grid1, word1_2).stream().map(Arrays::toString).collect(Collectors.joining(", "))); // [[0, 2], [0, 3], [0, 4], [0, 5], [0, 6], [0, 7], [1, 7]]
        String word1_3 = "wow";
        System.out.println(search(grid1, word1_3).stream().map(Arrays::toString).collect(Collectors.joining(", "))); // [[4, 3], [5, 3], [5, 4]]
        String word1_4 = "sec";
        System.out.println(search(grid1, word1_4).stream().map(Arrays::toString).collect(Collectors.joining(", "))); // [[3, 4], [3, 5], [3, 6]]
        String word1_5 = "bbaal";
        System.out.println(search(grid1, word1_5).stream().map(Arrays::toString).collect(Collectors.joining(", "))); // [[0, 0], [1, 0], [2, 0], [3, 0], [3, 1]]

        String[][] products = {
                {"Cheese", "Dairy"},
                {"Carrots", "Produce"},
                {"Potatoes", "Produce"},
                {"Canned Tuna", "Pantry"},
                {"Romaine Lettuce", "Produce"},
                {"Chocolate Milk", "Dairy"},
                {"Flour", "Pantry"},
                {"Iceberg Lettuce", "Produce"},
                {"Coffee", "Pantry"},
                {"Pasta", "Pantry"},
                {"Milk", "Dairy"},
                {"Blueberries", "Produce"},
                {"Pasta Sauce", "Pantry"}
        };
        String[] list1 = {"Blueberries", "Milk", "Coffee", "Flour", "Cheese", "Carrots"};
        System.out.println(shopping(products, list1)); // 2
        String[] list2 = {"Blueberries", "Carrots", "Coffee", "Milk", "Flour", "Cheese"};
        System.out.println(shopping(products, list2)); // 2
        String[] list3 = {"Blueberries", "Carrots", "Romaine Lettuce", "Iceberg Lettuce"};
        System.out.println(shopping(products, list3)); // 0
        String[] list4 = {"Milk", "Flour", "Chocolate Milk", "Pasta Sauce"};
        System.out.println(shopping(products, list4)); // 2
        String[] list5 = {"Cheese", "Potatoes", "Blueberries", "Canned Tuna"};
        System.out.println(shopping(products, list5)); // 0

        String[][] roads1 = {
                {"Bridgewater", "Caledonia", "30"},
                {"Caledonia", "New Grafton", "15"},
                {"New Grafton", "Campground", "5"},
                {"Liverpool", "Milton", "10"},
                {"Milton", "New Grafton", "30"}
        };
        String[] starts1 = {"Bridgewater", "Liverpool"};
        String[][] people1 = {
                {"Jessie", "Bridgewater"}, {"Travis", "Caledonia"},
                {"Jeremy", "New Grafton"}, {"Katie", "Liverpool"}
        };
        System.out.println(carpool(roads1, starts1, people1)); // [[Jessie, Travis], [Katie, Jeremy]]

        String[][] roads2 = {
                {"Riverport", "Chester", "40"}, {"Chester", "Campground", "60"}, {"Halifax", "Chester", "40"}
        };
        String[] starts2 = {"Riverport", "Halifax"};
        String[][] people2 = {
                {"Colin", "Riverport"}, {"Sam", "Chester"}, {"Alyssa", "Halifax"}
        };
        System.out.println(carpool(roads2, starts2, people2)); // [[Colin, Sam], [Alyssa]] OR [[Colin], [Alyssa, Sam]]

        String[][] roads3 = {
                {"Riverport", "Bridgewater", "1"}, {"Bridgewater", "Liverpool", "1"}, {"Liverpool", "Campground", "1"}
        };
        String[] starts3_1 = {"Riverport", "Bridgewater"};
        String[] starts3_2 = {"Bridgewater", "Riverport"};
        String[] starts3_3 = {"Riverport", "Liverpool"};
        String[][] people3 = {
                {"Colin", "Riverport"}, {"Jessie", "Bridgewater"}, {"Sam", "Liverpool"}
        };
        System.out.println(carpool(roads3, starts3_1, people3)); // [[Colin], [Jessie, Sam]]
        System.out.println(carpool(roads3, starts3_2, people3)); // [[Jessie, Sam], [Colin]]
        System.out.println(carpool(roads3, starts3_3, people3)); // [[Jessie, Colin], [Sam]]

        List<String> logLines = Arrays.asList(
                "1000.000 TST002 270W ENTRY",
                "1275.000 TST002 260W EXIT"
                // Add more log lines as needed
        );

        LogFile logFile = new LogFile(logLines);
        List<String> speeders = logFile.catchSpeeders();

        System.out.println("Speeders: " + speeders);

        int[][] board3 = {
                {1, 0, 0, 0, 0},
                {0, -1, -1, 0, 0},
                {0, -1, 0, 1, 0},
                {-1, 0, 0, 0, 0},
                {0, 1, -1, 0, 0},
                {0, 0, 0, 0, 0}
        };
        int[] start1 = {5, 0};
        int[] end1 = {0, 4};
        for (List<int[]> path : findAllTreasures(board3, start1, end1)) {
            System.out.println(path.stream().map(Arrays::toString).collect(Collectors.joining(", ")));
        } // None

        int[] start2 = {5, 2};
        int[] end2 = {2, 0};
        for (List<int[]> path : findAllTreasures(board3, start2, end2)) {
            System.out.println(path.stream().map(Arrays::toString).collect(Collectors.joining(", ")));
        } // [[5, 2], [5, 1], [4, 1], [3, 1], [3, 2], [2, 2], [2, 3], [1, 3], [0, 3], [0, 2], [0, 1], [0, 0], [1, 0], [2, 0]] OR [[5, 2], [5, 1], [4, 1], [3, 1], [3, 2], [3, 3], [2, 3], [1, 3], [0, 3], [0, 2], [0, 1], [0, 0], [1, 0], [2, 0]]

        int[] start3 = {0, 0};
        int[] end3 = {4, 1};
        for (List<int[]> path : findAllTreasures(board3, start3, end3)) {
            System.out.println(path.stream().map(Arrays::toString).collect(Collectors.joining(", ")));
        } // [[0, 0], [0, 1], [0, 2], [0, 3], [1, 3], [2, 3], [2, 2], [3, 2], [3, 1], [4, 1]] OR [[0, 0], [0, 1], [0, 2], [0, 3], [1, 3], [2, 3], [3, 3], [3, 2], [3, 1], [4, 1]]
    }
}
