
public interface IAttackCommand : ICommand
{
    IAttackable Target { get; }
}
