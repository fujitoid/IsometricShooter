using System;
using Newtonsoft.Json;

namespace Shooter.Core.Model.ReactiveProperty
{
    [Serializable]
    public class CoreReactiveProperty<T>
    {
        public event Action<T> OnValueChanged;
        
        [JsonProperty] private T _value;

        [JsonIgnore]
        public T Value
        {
            get
            {
                return _value;
            }
            internal set
            {
                OnValueChanged?.Invoke(value);
                _value = value;
            }
        }
        
        [JsonConstructor]
        public CoreReactiveProperty(T value)
        {
            
        }

        internal void SetSilently(T value)
        {
            _value = value;
        }
    }
}
