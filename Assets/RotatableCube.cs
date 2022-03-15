using UnityEngine;

public static class TransformExtensions
{
    public static void FromMatrix(this Transform transform, Matrix4x4 matrix)
    {
        transform.localScale = matrix.ExtractScale();
        transform.rotation = matrix.ExtractRotation();
        transform.position = matrix.ExtractPosition();
    }
}

public class RotatableCube : MonoBehaviour
{
    [Range(0, 360)] public float x;
    [Range(0, 360)] public float y;
    [Range(0, 360)] public float z;

    private void Update()
    {
        var xRad = x * Mathf.Deg2Rad;
        var yRad = y * Mathf.Deg2Rad;
        var zRad = z * Mathf.Deg2Rad;

        var m = Matrix4x4.identity;
        
        m *= new Matrix4x4(
            new Vector4(1, 0, 0, 0), 
            new Vector4(0, Mathf.Cos(xRad), Mathf.Sin(xRad), 0), 
            new Vector4(0, -Mathf.Sin(xRad), Mathf.Cos(xRad), 0), 
            new Vector4(0, 0, 0, 1)
        );

        m *= new Matrix4x4(
            new Vector4(Mathf.Cos(yRad), 0, -Mathf.Sin(yRad), 0), 
            new Vector4(0, 1, 0, 0), 
            new Vector4(Mathf.Sin(yRad), 0, Mathf.Cos(yRad), 0),
            new Vector4(0, 0, 0, 1)
        );
        
        m *= new Matrix4x4(
            new Vector4(Mathf.Cos(zRad), Mathf.Sin(zRad), 0, 0), 
            new Vector4(-Mathf.Sin(zRad), Mathf.Cos(zRad), 0, 0), 
            new Vector4(0, 0, 1, 0), 
            new Vector4(0, 0, 0, 1)
        );
        
        transform.FromMatrix(m);
    }
}
