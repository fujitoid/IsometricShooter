using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shooter.Core.Model.Misc.CoreReactive
{
    [Serializable]
    public class CoreReactiveList<T>
    {
        public event Action OnCollectionChanged; 

        [JsonProperty] private List<T> _list;

        [JsonIgnore] public IReadOnlyList<T> List => _list;

        [JsonConstructor]
        public CoreReactiveList()
        {
            _list = new List<T>();
        }

        internal void Add(T type)
        {
            _list.Add(type);
            OnCollectionChanged?.Invoke();
        }

        internal void Remove(T type)
        {
            _list.Remove(type);
            OnCollectionChanged?.Invoke();
        }

        internal void Clear()
        {
            _list.Clear();
            OnCollectionChanged?.Invoke();
        }
    }
}
