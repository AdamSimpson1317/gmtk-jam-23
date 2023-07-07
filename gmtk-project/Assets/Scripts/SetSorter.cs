using System.Collections.Generic;
using System.Linq;


public class SetSorter
{
    /*static void Main(string[] args)
    {
        //SortedSet<Node> freenodeset = new SortedSet<FreeNodes>();
        //foreach (FreeNodes freenodes in ) { }
        //freenodeset.Add();

    }*/

    /*public bool CheckNull()
    {
        bool IfNull;

        if ()
        {
            IfNull = true;
        }
        else if ()
        {
            IfNull = false;
        }

        return IfNull;
    }*/

    /*public class FreeNodes
    {
        public int x { get; set; }
        public int y { get; set; }
    }*/

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
        /*foreach (KeyValuePair<int, int> frame in set)
        {
            //Debug.Log(frame.Key + "," + frame.Value);
        }*/
        List<int> xy = new List<int>();
        //Debug.Log(set.Count() == 0);
        KeyValuePair<int,int> coordinates = set.First();
        int x = coordinates.Key;
        int y = coordinates.Value;
        xy.Add(x);
        xy.Add(y);
        return xy;
    }

    /*public static void Adds(int x, int y)
    {
        new SetSorter().Add(x, y);
    }

    public static void Removes(int x, int y)
    {
        new SetSorter().Remove(x, y);
    }

    public static bool NullCheck ()
    {
        bool IfNull = new SetSorter().CheckNull();
        return IfNull;
    }

    public static List<int> NextFreeNode()
    {
        int x;
        int y;
        List<int> xy = new List<int>();  
        (x,y)= new SetSorter().GetNextNode();
        xy.Add(x);
        xy.Add(y);
        return xy;
    }*/
}