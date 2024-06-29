using UnityEngine;

public class LineRendererUVRotator : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float rotationSpeed = 1.0f;

    private Material lineMaterial;

    void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineMaterial = lineRenderer.material;
    }

    void Update()
    {
        if (lineMaterial != null)
        {
            float rotation = Time.time * rotationSpeed % 1.0f; // Rotate UV over time
            lineMaterial.SetFloat("_Rotation", rotation);
        }
    }
}
