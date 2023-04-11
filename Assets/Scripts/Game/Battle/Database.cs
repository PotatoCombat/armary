using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Database<T> : ScriptableObject
    where T : UnityEngine.Object
{
    [SerializeField]
    private List<T> items;
    private Dictionary<string, T> _database;

    public T this[string id] => _database.TryGetValue(id, out var item) ? item : null;

    public void Load()
    {
        _database = new Dictionary<string, T>();
        foreach (var item in items)
        {
            _database[item.name] = item;
        }
    }

    private void Reset()
    {
        // Get all assets in current folder
    }
}
