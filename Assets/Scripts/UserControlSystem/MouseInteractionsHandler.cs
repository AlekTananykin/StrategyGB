
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInteractionsHandler : MonoBehaviour
{
    private enum MouseButton { left = 0, right = 1};

    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private Camera _camera;
    [SerializeField] private SelectableValue _selectedObject;

    [SerializeField] private Vector3Value _groundClickRMB;
    [SerializeField] private Transform _groundTRransform;

    private Plane _groundPlane;

    private void Start()
    {
        _groundPlane = new Plane(_groundTRransform.up, 0);
    }

    void Update()
    {
        if (!Input.GetMouseButtonUp((int)MouseButton.left) && 
            !Input.GetMouseButton((int)MouseButton.right))
            return;

        if (_eventSystem.IsPointerOverGameObject())
            return;

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonUp((int)MouseButton.left))
        {
            var hits = Physics.RaycastAll(ray);
            if (0 == hits.Length)
                return;

            var selectable = hits.Select(
                hits => hits.collider.GetComponentInParent<ISelectable>())
                .Where(c => null != c).FirstOrDefault();

            if (default == selectable)
                return;

            _selectedObject.SetValue(selectable);
        }
        else 
        {
            if (_groundPlane.Raycast(ray, out var enter))
            {
                _groundClickRMB.SetValue(ray.origin + ray.direction * enter);
            }
        }
    }
}
