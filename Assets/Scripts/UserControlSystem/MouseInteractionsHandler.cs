
using System.Linq;
using UnityEngine;

public class MouseInteractionsHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private SelectableValue _selectedObject;

    private Collider _selectedObjectCollider;
    
    void Update()
    {
        if (!Input.GetMouseButtonUp(0))
            return;

        var hits = Physics.RaycastAll(
            _camera.ScreenPointToRay(Input.mousePosition));
        if (0 == hits.Length)
            return;

        var selectable = hits.Select(
            hits => hits.collider.GetComponentInParent<ISelectable>())
            .Where(c=>null != c).FirstOrDefault();

        if (default == selectable)
            return;

        _selectedObject.SetValue(selectable);
    }

    void Outline(ISelectable mainBuilding)
    {
        var selectedNewCollider = 
            (mainBuilding as Component).gameObject.GetComponent<Collider>();
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
