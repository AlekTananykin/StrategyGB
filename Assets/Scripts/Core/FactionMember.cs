using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FactionMember : MonoBehaviour, IFactionMember
{

    private static Dictionary<int, List<int>> _memberCount =
        new Dictionary<int, List<int>>();

    public static int FactionCount
    {
        get 
        {
            lock (_memberCount)
            {
                return _memberCount.Count;
            }
        }
    }

    public static int GetWinner()
    {
        lock (_memberCount)
        {
            return _memberCount.Keys.First();
        }
    }

    public int FactionId => _factionId;
    [SerializeField] private int _factionId;

    private void Awake()
    {
        if (0 != _factionId)
        {
            register();
        }
    }

    public void SetFaction(int factionId)
    {
        _factionId = factionId;
        register();
    }

    private void OnDestroy()
    {
        unregister();
    }

    private void register()
    {
        lock (_memberCount)
        {
            if (!_memberCount.ContainsKey(_factionId))
            {
                _memberCount.Add(_factionId, new List<int>());
            }
            if (!_memberCount[_factionId].Contains(GetInstanceID()))
            {
                _memberCount[_factionId].Add(GetInstanceID());
            }
        }
    }

    private void unregister()
    {
        lock (_memberCount)
        {
            if (_memberCount[_factionId].Contains(GetInstanceID()))
            {
                _memberCount[_factionId].Remove(GetInstanceID());
            }
            if (0 == _memberCount[_factionId].Count)
            {
                _memberCount.Remove(_factionId);
            }
        }
    }

}
