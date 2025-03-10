using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableHashSet<T> : HashSet<T>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<T> items = new List<T>();

    public SerializableHashSet(IEnumerable<T> collection) : base(collection)
    {
    }

    public void OnBeforeSerialize()
    {
        items.Clear();
        foreach (T item in this)
        {
            items.Add(item);
        }
    }

    public void OnAfterDeserialize()
    {
        Clear();
        foreach (T item in items)
        {
            Add(item);
        }
    }
} 