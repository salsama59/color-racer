
using UnityEngine;

public class PointInTime
{
    private Vector2 translationVector;
    private Vector3 rotationVector;

    public PointInTime(Vector2 translationVector, Vector3 rotationVector)
    {
        this.TranslationVector = translationVector;
        this.RotationVector = rotationVector;
    }

    public Vector2 TranslationVector { get => translationVector; set => translationVector = value; }
    public Vector3 RotationVector { get => rotationVector; set => rotationVector = value; }
}
