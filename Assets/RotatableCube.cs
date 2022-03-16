using UnityEngine;
using UnityEngine.UI;

public class RotatableCube : MonoBehaviour
{
    [SerializeField] [Range(0, 360)] private float _xRot;
    [SerializeField] [Range(0, 360)] private float _yRot;
    [SerializeField] [Range(0, 360)] private float _zRot;
    
    [SerializeField] [Range(0, 360)] private float _xTrans;
    [SerializeField] [Range(0, 360)] private float _yTrans;
    [SerializeField] [Range(0, 360)] private float _zTrans;

    [SerializeField] private Slider _xRotSlider;
    [SerializeField] private Slider _yRotSlider;
    [SerializeField] private Slider _zRotSlider;

    private void Update()
    {
        // _x = _xRotSlider.value;
        // _y = _yRotSlider.value;
        // _z = _zRotSlider.value;
        
        HandleRotation();
    }

    private void HandleRotation()
    {
        var xRad = _xRot * Mathf.Deg2Rad;
        var yRad = _yRot * Mathf.Deg2Rad;
        var zRad = _zRot * Mathf.Deg2Rad;

        // TODO: Put translations value in last column of this matrix
        var transMatrix = new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(0, 0, 0, 1)
        );
        
        var xRot = new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, Mathf.Cos(xRad), Mathf.Sin(xRad), 0),
            new Vector4(0, -Mathf.Sin(xRad), Mathf.Cos(xRad), 0),
            new Vector4(0, 0, 0, 1)
        );

        var yRot = new Matrix4x4(
            new Vector4(Mathf.Cos(yRad), 0, -Mathf.Sin(yRad), 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(Mathf.Sin(yRad), 0, Mathf.Cos(yRad), 0),
            new Vector4(0, 0, 0, 1)
        );

        var zRot = new Matrix4x4(
            new Vector4(Mathf.Cos(zRad), Mathf.Sin(zRad), 0, 0),
            new Vector4(-Mathf.Sin(zRad), Mathf.Cos(zRad), 0, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(0, 0, 0, 1)
        );

        var rotMatrix = MatrixExtensions.MultiplyMatrices(MatrixExtensions.MultiplyMatrices(xRot, yRot), zRot);
        
        // TODO: multiply trans matrix with rot matrix and apply the result to the transform

        transform.FromMatrix(rotMatrix);
    }
}
