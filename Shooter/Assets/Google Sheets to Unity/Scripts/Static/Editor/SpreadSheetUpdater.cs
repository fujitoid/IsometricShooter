using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpreadSheetUpdater : ScriptableObject
{
    [SerializeField] private List<SpreadsheetContainerBase> _spreadsheets;

    public void UpdateSpreadSheets()
    {
        foreach(var spreadSheet in _spreadsheets)
        {
            spreadSheet.UpdateData();
        }
    }
}

[CustomEditor(typeof(SpreadSheetUpdater))]
public class SpreadSheetUpdaterEditor : Editor
{
    private SpreadSheetUpdater _sheetUpdater;

    private void OnEnable()
    {
        _sheetUpdater = (SpreadSheetUpdater)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Update"))
        {
            _sheetUpdater.UpdateSpreadSheets();
        }
    }
}
