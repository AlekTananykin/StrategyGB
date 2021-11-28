using UnityEngine;

public class OutlineSelectorPresenter : MonoBehaviour
{
    [SerializeField] SelectableValue _selectable;

    private ISelectable _currentSelectable;
    private Collider _selectedObjectCollider;

    void Start()
    {
        _selectable.OnNewValue += OnSelected;
        OnSelected(_selectable.CurrentValue);
    }

    private void OnSelected(ISelectable selectable)
    {
        if (_currentSelectable == selectable)
            return;

        _currentSelectable = selectable;

        Outline(selectable);
    }


    void Outline(ISelectable selectedObj)
    {
        var selectedNewCollider =
            (selectedObj as Component).gameObject.GetComponent<Collider>();
        var outlineComponent = selectedNewCollider?.gameObject.GetComponent<IOutlinable>();
        if (null == outlineComponent)
            return;

        if (selectedNewCollider == _selectedObjectCollider)
        {
            if (null != outlineComponent)
                outlineComponent.IsOutlined = false;

            _selectedObjectCollider = null;

            return;
        }
        _selectedObjectCollider = selectedNewCollider;
        if (null != outlineComponent)
            outlineComponent.IsOutlined = true;
    }

}
