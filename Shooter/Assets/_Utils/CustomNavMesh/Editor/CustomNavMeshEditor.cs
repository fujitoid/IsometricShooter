using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Shooter.Utils.CustomNavMesh.Editor
{
    [CustomEditor(typeof(Runtime.CustomNavMesh))]
    public class CustomNavMeshEditor : UnityEditor.Editor
    {
        private const int NEW_POINT_OFFSET = 50;

        private Runtime.CustomNavMesh _target;

        private Texture2D _labelBackround;
        private GUIStyle _textStyle;

        private int _lastPointsCount = 0;

        private void OnEnable()
        {
            _target = target as Runtime.CustomNavMesh;
            _labelBackround = new Texture2D(1, 1);
            _labelBackround.SetPixels(0, 0, 1, 1, new Color[] { Color.white });
            _labelBackround.Apply();

            _textStyle = new GUIStyle();
            _textStyle.fontSize = 20;
            _textStyle.normal.textColor = Color.black;
            _textStyle.normal.background = _labelBackround;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_target.Points == null) return;

            if (_target.Points.Count > _lastPointsCount)
            {
                var startPoint = _target.Points.Count == 1 ? _target.transform.position
                    : _target.Points[_target.Points.Count - 2];

                _target.Points[_target.Points.Count - 1] =
                    _target.Points.Count > 1 ? new Vector3(startPoint.x + NEW_POINT_OFFSET, startPoint.y, startPoint.z - NEW_POINT_OFFSET)
                    : new Vector3(startPoint.x + NEW_POINT_OFFSET / 2, startPoint.y, startPoint.z + NEW_POINT_OFFSET);

                _lastPointsCount = _target.Points.Count;
            }
            else
                _lastPointsCount = _target.Points.Count;


            var mesh = _target.GetComponent<MeshFilter>();

            GUI.enabled = mesh.sharedMesh != null;
            if(GUILayout.Button("Save mesh"))
            {
                var path = EditorUtility.SaveFilePanelInProject("Save mesh", "New mesh", "asset", string.Empty);
                AssetDatabase.CreateAsset(mesh.sharedMesh, path);
                AssetDatabase.SaveAssets();
            }
            GUI.enabled = true;
        }

        private void OnSceneGUI()
        {
            Handles.Label(_target.transform.position, "Center", _textStyle);

            if (_target.Points == null) return;

            var newPositions = new List<Vector3>();

            for (int i = 0; i < _target.Points.Count; i++)
            {
                var point = _target.Points[i];
                var newPosition = Handles.PositionHandle(point, Quaternion.identity);

                newPosition = new Vector3(newPosition.x, _target.transform.position.y, newPosition.z);

                Handles.Label(point, $"Point {i}", _textStyle);
                newPositions.Add(newPosition);

                _target.GenerateMesh();
            }

            _target.Points.Clear();
            _target.Points.AddRange(newPositions);
        }
    }
}
