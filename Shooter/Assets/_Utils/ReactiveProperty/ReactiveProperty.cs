using System;

public class ReactiveProperty<T>
{
    private T _value;
    private Action<T> _onValueChanged;

    public T Value
    {
        get => _value;
        internal set
        {
            _value = value;
            _onValueChanged.Invoke(_value);
        }
    }

    public Action<T> OnValueChanged => _onValueChanged;

    public ReactiveProperty(T value)
    {
        _value = value;
        _onValueChanged.Invoke(_value);
    }

    public void Fire() => _onValueChanged.Invoke(_value);
    public void SetValueSilently(T value)
    {
        _value = value;
    }
}
