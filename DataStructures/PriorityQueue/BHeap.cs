﻿using System.Collections;

namespace PriorityQueue;
public abstract class BHeap<T> : IEnumerable<T>
    where T : IComparable
{
   public DataStructures.Array.Generic.Array<T> Array { get; private set; }
   protected int position;
   public int Count { get; private set; }
   public BHeap(int size = 128)
   {
       Count = 0; 
       Array = new DataStructures.Array.Generic.Array<T>(size);
       position = 0;
   }
    public BHeap(IEnumerable<T> collection):this(collection.ToArray().Length)
    {
        foreach (var item in collection)
            Add(item);
    }
    public int GetLeftChildIndex(int i) =>  2 * i + 1;
    public int GetRightChildIndex(int i) => 2 * i + 2;
    public int GetParentIndex(int i) => (i - 1) / 2;

    protected bool HasLeftChild(int i) =>
        GetLeftChildIndex(i) < position;
    
    protected bool HasRightChild(int i) => 
        GetRightChildIndex(i) < position;
    
    protected bool IsRoot(int i) => i == 0;

    protected T GetLeftChild(int i) => 
        Array.GetValue(GetLeftChildIndex(i));
    protected T GetRightChild(int i) => 
        Array.GetValue(GetRightChildIndex(i));
    
    protected T GetParent(int i) => 
        Array.GetValue(GetParentIndex(i));
    
    public bool IsEmpty() => position == 0;

    public T Peek()
    {
        if(IsEmpty())
            throw new Exception("Empty heap!");
        return Array.GetValue(0);
    }

    public void Swap(int first, int second)
    {
        var temp = Array.GetValue(first);
        Array.SetValue(Array.GetValue(second), first);
        Array.SetValue(temp, second);
    }

    public void Add(T value)
    {
        Array.SetValue(value, position);
        position++;
        Count++;
        HeapifUp();
    }

    public T DeleteMinMax()
    {
        if(position == 0)
            throw new IndexOutOfRangeException("Underflow!");
        
        var temp = Array.GetValue(0);
        Array.SetValue(Array.GetValue(position - 1), 0);
        position--;
        Count--;
        HeapifDown();
        return temp;
    }

    protected abstract void HeapifUp();
    protected abstract void HeapifDown();
    public IEnumerator<T> GetEnumerator()
    {
        return Array.Take(position).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
