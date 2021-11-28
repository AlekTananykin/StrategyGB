
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
    [SerializeField] private AttackableValue _attackablesRBM;
    [SerializeField] private Transform _groundTransform;

    private Plane _groundPlane;

    private void Start()
    {
        _groundPlane = new Plane(_groundTransform.up, 0);
    }

    void Update()
    {
        if (!Input.GetMouseButtonUp((int)MouseButton.left) && 
            !Input.GetMouseButton((int)MouseButton.right))
            return;

        if (_eventSystem.IsPointerOverGameObject())
            return;

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray);

        if (Input.GetMouseButtonUp((int)MouseButton.left))
        {
            if (weHit<ISelectable>(hits, out var selectable))
            {
                _selectedObject.SetValue(selectable);
            }
        }
        else 
        {
            if (weHit<IAttackable>(hits, out var attackable))
            {
                _attackablesRBM.SetValue(attackable);
            }
            else if (_groundPlane.Raycast(ray, out var enter))
            {
                _groundClickRMB.SetValue(ray.origin + ray.direction * enter);
            }

        }
    }

    private bool weHit<T>(RaycastHit[] hits, out T result) where T : class
    {
        result = default;
        if (hits.Length == 0)
        {
            return false;
        }
        result = hits
            .Select(hit => hit.collider.GetComponentInParent<T>())
            .Where(c => c != null)
            .FirstOrDefault();
        return result != default;
    }


}
