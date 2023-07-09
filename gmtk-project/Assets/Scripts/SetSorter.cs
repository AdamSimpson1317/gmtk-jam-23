using System.Collections.Generic;
using System.Linq;


public class SetSorter
{
    public class SortFreeNodes : IComparer<KeyValuePair<int,int>>
    {
        public int Compare(KeyValuePair<int,int> x, KeyValuePair<int, int> y)
        {
            int temp = x.Key.CompareTo(y.Key);
            if (temp != 0)
            {
                return temp;
            }
            return y.Value.CompareTo(x.Value);
        }
    }

    public SortedSet<KeyValuePair<int, int>> set;

    public SetSorter()
    {
        set = new SortedSet<KeyValuePair<int, int>>(new SortFreeNodes());
    }

    public void Add(int x, int y)
    {
        set.Add(new KeyValuePair<int, int>(x, y));
    }
    public void Remove(int x, int y)
    {
        set.Remove(new KeyValuePair<int, int>(x, y));
    }

    public bool CheckEmpty()
    {
        bool IfEmpty = false;

        if (set.Count() == 0)
        {
            IfEmpty = true;
        }
        else if (set.Count() != 0)
        {
            IfEmpty = false;
        }

        return IfEmpty;
    }

    public List<int> GetNextNode()
    {
        List<int> xy = new List<int>();
        KeyValuePair<int,int> coordinates = set.First();
        int x = coordinates.Key;
        int y = coordinates.Value;
        xy.Add(x);
        xy.Add(y);
        return xy;
    }
}