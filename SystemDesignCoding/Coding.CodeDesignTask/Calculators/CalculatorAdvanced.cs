namespace Coding.CodeDesignTask.Calculators;

// + - * / ()
public class CalculatorAdvanced
{
    public int Calculate(string s)
    {
        var leftToRightIndex = new Dictionary<int, int>();
        var stack = new Stack<int>();
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
            {
                stack.Push(i);
            }

            if (s[i] == ')')
            {
                leftToRightIndex.Add(stack.Pop(), i);
            }
        }

        return CalculateSub(s, 0, s.Length - 1, leftToRightIndex);

    }

    private int CalculateSub(string s, int start, int end, Dictionary<int, int> leftToRightIndex)
    {
        var stack = new Stack<int>();
        var num = 0;
        var sign = '+';
        List<char> characters = ['+', '-', '*', '/'];
        for(var i = start; i <= end; i++)
        {
            var c = s[i];
            if (char.IsDigit(c))
            {
                num = num * 10 + (c - '0');
            }

            if (s[i] == '(')
            {
                num = CalculateSub(s, i + 1, leftToRightIndex[i] - 1, leftToRightIndex);
                i = leftToRightIndex[i];
            }

            if (characters.Contains(c) || i == end)
            {
                if (sign == '+')
                {
                    stack.Push(num);
                } else if (sign == '-')
                {
                    stack.Push(-num);
                } else if (sign == '*')
                {
                    stack.Push(stack.Pop() * num);
                } else if (sign == '/')
                {
                    stack.Push(stack.Pop() / num);
                }

                sign = c;
                num = 0;
            }
        }

        return stack.Sum();
    }
}