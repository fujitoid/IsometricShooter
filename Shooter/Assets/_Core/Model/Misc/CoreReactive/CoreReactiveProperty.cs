using System;
using Newtonsoft.Json;

namespace Shooter.Core.Model.Misc.CoreReactive
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
                _value = value;
                OnValueChanged?.Invoke(value);
            }
        }
        
        [JsonConstructor]
        public CoreReactiveProperty()
        {
            
        }

        internal void SetSilently(T value)
        {
            _value = value;
        }
    }
}
