namespace Coding.CodeDesignTask.Calculators;

// only + - * /
public class CalculatorTwo
{
    public int Calculate(string s)
    {
        var stack = new Stack<int>();
        var num = 0;
        var sign = '+';
        List<char> characters = ['+', '-', '*', '/'];
        for(var i = 0; i < s.Length; i++)
        {
            var c = s[i];
            if (char.IsDigit(c))
            {
                num = num * 10 + (c - '0');
            }

            if (characters.Contains(c) || i == s.Length - 1)
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