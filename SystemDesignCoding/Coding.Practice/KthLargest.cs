namespace Coding.Practice;

public class KthLargest
{
    /*
     * 设计一个找到数据流中第 k 大元素的类（class）。注意是排序后的第 k 大元素，不是第 k 个不同的元素。
       
       请实现 KthLargest 类：
       
       KthLargest(int k, int[] nums) 使用整数 k 和整数流 nums 初始化对象。
       int add(int val) 将 val 插入数据流 nums 后，返回当前数据流中第 k 大的元素。
     */
    private PriorityQueue<int, int> _topK = new PriorityQueue<int, int>();
    private int _k;

    public KthLargest(int k, int[] nums)
    {
        _k = k;
        foreach (var num in nums)
        {
            if (_topK.Count < k)
            {
                _topK.Enqueue(num, num);
            } else if (num > _topK.Peek())
            {
                _topK.EnqueueDequeue(num, num);
            }
        }
    }
    
    public int Add(int val)
    {
        if (_topK.Count < _k)
        {
            _topK.Enqueue(val, val);
        } else if (val > _topK.Peek())
        {
            _topK.EnqueueDequeue(val, val);
        }

        return _topK.Peek();
    }
}