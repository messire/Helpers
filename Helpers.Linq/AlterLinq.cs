namespace Helpers.Linq;

public class AlterLinq
{
    public static int[] Distinct(int[] source)
    {
        ArgumentNullException.ThrowIfNull(source);
        return source.Length == 0 ? source : RemoveDuplicates(source);
    }

    private static int[] RemoveDuplicates(int[] source)
    {
        HashSet<int> set = new (source);
        int[] result = new int[set.Count];
        set.CopyTo(result);
        return result;
    }

    public static IEnumerable<T> FilterLast<T>(IEnumerable<T> source, Int32 count)
    {
        ArgumentNullException.ThrowIfNull(source);
        if (count <= 0) throw new NullReferenceException("Count must be more then 0");

        return FilterLastIterator(source, count);
    }

    private static IEnumerable<T> FilterLastIterator<T>(IEnumerable<T> source, Int32 count)
    {
        Queue<T> queue = new ();
            
        using IEnumerator<T> e = source.GetEnumerator();
        while (e.MoveNext())
        {
            if (queue.Count == count)
            {
                do
                {
                    yield return queue.Dequeue();
                    queue.Enqueue(e.Current);
                } while (e.MoveNext());

                break;
            }

            queue.Enqueue(e.Current);
        }
    }
        
        
}
