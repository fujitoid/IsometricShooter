using GoogleSheetsToUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public abstract class SpreadsheetContainerBase : ScriptableObject
{
    [SerializeField] private string _sheetID;

    public virtual void UpdateData()
    {
        Type type = this.GetType();

        foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
        {
            var attrField = field.GetCustomAttribute<SpreadSheetAttribute>();

            field.SetValue(this, default);
            SpreadsheetManager.Read(new GSTU_Search(_sheetID, attrField.SpreadName), DownloadData);
        }
    }

    private void DownloadData(GstuSpreadSheet gstu)
    {
        Type type = this.GetType();

        foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
        {
            var countOfObjects = gstu.columns["A"].Count;
            Type elementType = field.FieldType.GetElementType();

            var array = Array.CreateInstance(elementType, countOfObjects - 1);

            for (int i = 0; i < array.Length; i++)
            {
                array.SetValue(Activator.CreateInstance(elementType), i);
            }

            field.SetValue(this, array);

            for (int i = 0; i < array.Length; i++)
            {
                var element = array.GetValue(i);

                foreach (FieldInfo elementField in element.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
                {
                    var elementAttr = elementField.GetCustomAttribute<SpreadFieldAttribute>();

                    if (elementField.FieldType.TryParse(gstu.columns[elementAttr.RawName].ElementAt(i + 1).value, out var result))
                    {
                        elementField.SetValue(element, result);
                    }
                }
            }
        }
    }
}
