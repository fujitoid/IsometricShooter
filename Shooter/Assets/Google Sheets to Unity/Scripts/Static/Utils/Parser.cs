using System;

public static class Parser
{
    public static bool TryParse<T>(this T field, string data, out object result)
    {
        object obj = (object)field;
        result = default;
        var type = field as Type;

        if(type == typeof(string))
        {
            obj = (object)data;
            result = (string)obj;
            return true;
        }

        if (type == typeof(int))
        {
            int.TryParse(data, out var currentResult);
            obj = (object)currentResult;
            result = (int)obj;
            return int.TryParse(data, out currentResult);
        }

        if (type == typeof(float))
        {
            float.TryParse(data, out var currentResult);
            obj = (object)currentResult;
            result = (float)obj;
            return float.TryParse(data, out currentResult);
        }

        if (type == typeof(double))
        {
            Double.TryParse(data, out var currentResult);
            obj = (object)currentResult;
            result = (double)obj;
            return Double.TryParse(data, out currentResult);
        }

        return false;
    }
}
