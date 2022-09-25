
using UnityEngine;

public class PointInTime
{
    private Vector2 forceVector;
    private float rotationAngle;
    private float dragValue;
    private Vector2 velocityVector;
    private bool isForceApplied;

    public PointInTime() { }

    public PointInTime(Vector2 forceVector, float rotationAngle, Vector2 velocityVector, float dragValue, bool isForceApplied)
    {
        this.ForceVector = forceVector;
        this.RotationAngle = rotationAngle;
        this.VelocityVector = velocityVector;
        this.DragValue = dragValue;
        this.IsForceApplied = isForceApplied;
    }

    public Vector2 ForceVector { get => forceVector; set => forceVector = value; }
    public float RotationAngle { get => rotationAngle; set => rotationAngle = value; }
    public float DragValue { get => dragValue; set => dragValue = value; }
    public Vector2 VelocityVector { get => velocityVector; set => velocityVector = value; }
    public bool IsForceApplied { get => isForceApplied; set => isForceApplied = value; }
}
