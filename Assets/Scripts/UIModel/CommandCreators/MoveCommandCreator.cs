using UnityEngine;

public class MoveCommandCreator : CancellableCommandCreatorBase<IMoveCommand, Vector3>
{
    protected override IMoveCommand createCommand(Vector3 argument) =>
        new MoveCommand(argument);
}
