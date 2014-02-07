using System;

public class Tuple<T1>
{
    public Tuple(T1 p_item_1)
    {
        Item_1 = p_item_1;
    }

    public T1 Item_1 { get; set; }
}

public class Tuple<T1, T2> : Tuple<T1>
{
    public Tuple(T1 p_item_1, T2 p_item_2)
        : base(p_item_1)
    {
        Item_2 = p_item_2;
    }

    public T2 Item_2 { get; set; }
}

public class Tuple<T1, T2, T3> : Tuple<T1, T2>
{
    public Tuple(T1 p_item_1, T2 p_item_2, T3 p_item_3)
        : base(p_item_1, p_item_2)
    {
        Item_3 = p_item_3;
    }

    public T3 Item_3 { get; set; }
}

public static class Tuple
{
    public static Tuple<T1> Create<T1>(T1 p_item_1)
    {
        return new Tuple<T1>(p_item_1);
    }

    public static Tuple<T1, T2> Create<T1, T2>(T1 p_item_1, T2 p_item_2)
    {
        return new Tuple<T1, T2>(p_item_1, p_item_2);
    }

    public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 p_item_1, T2 p_item_2, T3 p_item_3)
    {
        return new Tuple<T1, T2, T3>(p_item_1, p_item_2, p_item_3);
    }
}