
using System.Linq;
using UnityEngine;

public class MouseInteractionsHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private Collider _selectedObjectCollider;
    
    void Update()
    {
        ToOutLine();

        if (!Input.GetMouseButtonUp(0))
            return;

        var hits = Physics.RaycastAll(
            _camera.ScreenPointToRay(Input.mousePosition));
        if (0 == hits.Length)
            return;

        var mainBuilding = hits.Select(
            hits => hits.collider.GetComponentInParent<IUnitProducer>())
            .Where(c=>null != c).FirstOrDefault();

        if (default == mainBuilding)
            return;

        mainBuilding.ProduceUnit();
    }

    void ToOutLine()
    {
        if (!Physics.Raycast(
            _camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            return;

        if (null != _selectedObjectCollider)
        {
            var prevObjectOutline = _selectedObjectCollider.GetComponent<Outline>();
            if (null != prevObjectOutline)
            {
                prevObjectOutline.enabled = false;
            }
        }

        var outlineComponent = hit.collider.GetComponent<Outline>();
        if (null == outlineComponent)
            return;

        _selectedObjectCollider = hit.collider;
        outlineComponent.enabled = true;
    }
}
