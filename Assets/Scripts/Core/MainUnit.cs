using UnityEngine;

public class MainUnit : MonoBehaviour, ISelectable
{
    [SerializeField]private float _health = 100;
    [SerializeField]private float _maxHealth = 100;
    [SerializeField]private Sprite _icon;

    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public Sprite Icon => _icon;


}
