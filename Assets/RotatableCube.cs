using UnityEngine;
using UnityEngine.UI;

public class RotatableCube : MonoBehaviour
{
    [SerializeField] [Range(0, 360)] private float _x;
    [SerializeField] [Range(0, 360)] private float _y;
    [SerializeField] [Range(0, 360)] private float _z;

    [SerializeField] private Slider _xRotSlider;
    [SerializeField] private Slider _yRotSlider;
    [SerializeField] private Slider _zRotSlider;

    private void Update()
    {
        _x = _xRotSlider.value;
        _y = _yRotSlider.value;
        _z = _zRotSlider.value;
        
        MyHandleRotation();
    }

    private void MyHandleRotation()
    {
        var xRad = _x * Mathf.Deg2Rad;
        var yRad = _y * Mathf.Deg2Rad;
        var zRad = _z * Mathf.Deg2Rad;
        
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

        transform.FromMatrix(rotMatrix);
    }

    
}
