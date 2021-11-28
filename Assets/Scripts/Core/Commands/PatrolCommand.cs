using UnityEngine;

public class PatrolCommand : IPatrolCommand
{
    public PatrolCommand(Vector3 from, Vector3 to)
    {
        From = From;
        To = to;
    }
    public Vector3 From {get;}

    public Vector3 To { get; }
}
