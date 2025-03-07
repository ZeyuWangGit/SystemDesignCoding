namespace Coding.Practice;

/*
 * 中位数是有序整数列表中的中间值。如果列表的大小是偶数，则没有中间值，中位数是两个中间值的平均值。
   
   例如 arr = [2,3,4] 的中位数是 3 。
   例如 arr = [2,3] 的中位数是 (2 + 3) / 2 = 2.5 。
   实现 MedianFinder 类:
   
   MedianFinder() 初始化 MedianFinder 对象。
   
   void addNum(int num) 将数据流中的整数 num 添加到数据结构中。
   
   double findMedian() 返回到目前为止所有元素的中位数。与实际答案相差 10-5 以内的答案将被接受。
 */

public class MedianFinder
{
    private PriorityQueue<int, int> _large = new PriorityQueue<int, int>();
    private PriorityQueue<int, int> _small = new PriorityQueue<int, int>(Comparer<int>.Create((x, y) => y - x));

    public MedianFinder() {}
    
    public void AddNum(int num) {
        if (_large.Count >= _small.Count)
        {
            _large.Enqueue(num, num);
            var temp = _large.Dequeue();
            _small.Enqueue(temp, temp);
        }
        else
        {
            _small.Enqueue(num, num);
            var temp = _small.Dequeue();
            _large.Enqueue(temp, temp);
        }
    }
    
    public double FindMedian()
    {
        if (_large.Count > _small.Count)
        {
            return _large.Peek();
        }

        if (_large.Count < _small.Count)
        {
            return _small.Peek();
        }

        return (double)(_large.Peek() + _small.Peek()) / 2;
    }
}
