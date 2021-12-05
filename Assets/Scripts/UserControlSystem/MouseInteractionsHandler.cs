using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

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

    [Inject]
    private void Init()
    {
        _groundPlane = new Plane(_groundTransform.up, 0);
        var nonBlockedByUiFramesStream = Observable.EveryUpdate().Where(
            _ =>!_eventSystem.IsPointerOverGameObject()
            );

        var leftClickSystem = nonBlockedByUiFramesStream
            .Where(_ => Input.GetMouseButtonDown((int)MouseButton.left));

        var rightClickStream = nonBlockedByUiFramesStream
            .Where(_ => Input.GetMouseButtonDown((int)MouseButton.right));

        var lmbRays = leftClickSystem.Select(
            _=> _camera.ScreenPointToRay(Input.mousePosition));
        var rmbRays = rightClickStream.Select(
            _ => _camera.ScreenPointToRay(Input.mousePosition));

        var lmbHitsStream = lmbRays.Select(ray => Physics.RaycastAll(ray));
        var rmbHitsStream = rmbRays.Select(
            ray => (ray, Physics.RaycastAll(ray)));

        lmbHitsStream.Subscribe(hits =>
        {
            if (weHit<ISelectable>(hits, out var selectable))
            {
                _selectedObject.SetValue(selectable);
            }

        });

        rmbHitsStream.Subscribe((ray, hits) => 
        {
            if (weHit<IAttackable>(hits, out var attackable))
            {
                _attackablesRBM.SetValue(attackable);
            }
            else if (_groundPlane.Raycast(ray, out var enter))
            {
                _groundClickRMB.SetValue(ray.origin + ray.direction * enter);
            }
        });
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
