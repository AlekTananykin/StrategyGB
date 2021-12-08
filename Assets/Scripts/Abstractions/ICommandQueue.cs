
public interface ICommandQueue
{
    void EnqueueCommand(object command);
    void Clear();
}
