using UnityEngine;
using UnityEngine.UI;

public class RotatableCube : MonoBehaviour
{
    [SerializeField] [Range(0, 360)] private float _xRot;
    [SerializeField] [Range(0, 360)] private float _yRot;
    [SerializeField] [Range(0, 360)] private float _zRot;
    
    [SerializeField] [Range(-5, 5)] private float _xTrans = 0;
    [SerializeField] [Range(-5, 5)] private float _yTrans = 0;
    [SerializeField] [Range(-5, 5)] private float _zTrans = 0;

    [SerializeField] private Slider _xRotSlider;
    [SerializeField] private Slider _yRotSlider;
    [SerializeField] private Slider _zRotSlider;
    
    [SerializeField] private Slider _xTransSlider;
    [SerializeField] private Slider _yTransSlider;
    [SerializeField] private Slider _zTransSlider;

    public void ResetTranslationAxis(int axis)
    {
        switch (axis)
        {
            case 0:
                _xTransSlider.value = 0;
                break;
            case 1:
                _yTransSlider.value = 0;
                break;
            case 2:
                _zTransSlider.value = 0;
                break;
        }
        
        print("Saaf");
    }

    private void Update()
    {
        _xRot = _xRotSlider.value;
        _yRot = _yRotSlider.value;
        _zRot = _zRotSlider.value;

        _xTrans = _xTransSlider.value;
        _yTrans = _yTransSlider.value;
        _zTrans = _zTransSlider.value;
        
        HandleRotation();
    }

    private void HandleRotation()
    {
        var xRad = _xRot * Mathf.Deg2Rad;
        var yRad = _yRot * Mathf.Deg2Rad;
        var zRad = _zRot * Mathf.Deg2Rad;

        var transMatrix = new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(_xTrans, _yTrans, _zTrans, 1)
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

        var t = MatrixExtensions.MultiplyMatrices(transMatrix, rotMatrix);
        
        transform.FromMatrix(t);
    }
}
