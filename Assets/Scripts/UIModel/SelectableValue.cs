
using UnityEngine;
[CreateAssetMenu(fileName = nameof(SelectableValue), menuName = "Game/" +
nameof(SelectableValue), order = 0)]
public class SelectableValue : StatefullActionValueBase<ISelectable>
{
}