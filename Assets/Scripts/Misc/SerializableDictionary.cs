using System;
using System.Collections.Generic;
using UnityEngine;

namespace NecatiAkpinar.Misc
{
    [Serializable]
    public class SerializableDictionaryEntry<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;
    }

    [Serializable]
    public class SerializableDictionary<TKey, TValue>
    {
        [SerializeField] private List<SerializableDictionaryEntry<TKey, TValue>> _list = new List<SerializableDictionaryEntry<TKey, TValue>>();

        public void Add(TKey key, TValue value)
        {
            _list.Add(new SerializableDictionaryEntry<TKey, TValue> { Key = key, Value = value });
        }

        public bool ContainsKey(TKey key)
        {
            foreach (var entry in _list)
            {
                if (EqualityComparer<TKey>.Default.Equals(entry.Key, key))
                {
                    return true;
                }
            }

            return false;
        }

        public TValue GetValue(TKey key)
        {
            foreach (var entry in _list)
            {
                if (EqualityComparer<TKey>.Default.Equals(entry.Key, key))
                {
                    return entry.Value;
                }
            }

            throw new KeyNotFoundException($"Key {key} not found");
        }

        public void SetValue(TKey key, TValue newValue)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (EqualityComparer<TKey>.Default.Equals(_list[i].Key, key))
                {
                    _list[i].Value = newValue;
                    return;
                }
            }

            Add(key, newValue);
        }
        
        public IEnumerable<SerializableDictionaryEntry<TKey, TValue>> GetEntries()
        {
            return _list;
        }
    }
}