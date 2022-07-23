
using UnityEngine;

public class PointInTime
{
    private Vector2 position;
    private Quaternion rotation;

    public PointInTime(Vector2 position, Quaternion rotation)
    {
        this.Position = position;
        this.Rotation = rotation;
    }

    public Vector2 Position { get => position; set => position = value; }
    public Quaternion Rotation { get => rotation; set => rotation = value; }
}
