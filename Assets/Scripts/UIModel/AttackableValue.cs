
using UnityEngine;

[CreateAssetMenu(fileName = nameof(AttackableValue), menuName = "Game/" +
nameof(AttackableValue), order = 0)]

public class AttackableValue : StatelessActionValueBase<IAttackable>
{
}
