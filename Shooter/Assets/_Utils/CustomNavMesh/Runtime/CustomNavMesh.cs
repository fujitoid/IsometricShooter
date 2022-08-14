using Sebastian.Geometry;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Shooter.Utils.CustomNavMesh.Runtime
{
    [RequireComponent(typeof(NavMeshSurface))]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class CustomNavMesh : MonoBehaviour
    {
        [SerializeField] private List<Vector3> _points;

        public List<Vector3> Points => _points;

        public void GenerateMesh()
        {
            if (_points == null || _points.Count < 1) return;

            Shape shape = new Shape();
            shape.points = new List<Vector3>() { new Vector3(transform.position.x, transform.position.y, transform.position.z) };
            shape.points.AddRange(_points.Select(point => point));

            var shapes = new List<Shape>() { shape };

            CompositeShape compositeShape = new CompositeShape(shapes);
            var meshFilter = GetComponent<MeshFilter>();
            meshFilter.mesh = compositeShape.GetMesh();

            var meshRenderer = GetComponent<MeshRenderer>();
            if (meshRenderer.sharedMaterial != null) return;

            var shader = Shader.Find("Universal Render Pipeline/Lit");
            var newMaterial = new Material(shader);

            newMaterial.color = new Color(Color.green.r, Color.green.g, Color.green.b, .8f);
            meshRenderer.sharedMaterial = newMaterial;
        }
    }
}
