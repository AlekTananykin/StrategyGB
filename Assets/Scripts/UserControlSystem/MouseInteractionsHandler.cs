
using System.Linq;
using UnityEngine;

public class MouseInteractionsHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;


    
    void Update()
    {
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
}
