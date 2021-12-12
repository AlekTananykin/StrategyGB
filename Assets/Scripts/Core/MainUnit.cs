using System;
using UnityEngine;

public class MainUnit : MonoBehaviour, ISelectable, IAttackable, IUnit, IDamageDealer
{
    [SerializeField] private float _health = 100;
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Animator _animator;
    [SerializeField] private StopCommandExecutor _stopCommand;

    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public Sprite Icon => _icon;

    public Transform PivotPoint => transform;

    [SerializeField] private int _damage = 25;
    public int Damage => _damage;

    public void ReceiveDamage(int amount)
    {
        if (_health <= 0)
            return;
        _health -= amount;
        if (_health <= 0)
        {
            //_animator.setTrigger("playDead");
            Invoke(nameof(destroy), 0);
        }
    }

    private async void destroy()
    {
        await _stopCommand.ExecuteSpecificCommand(new StopCommand());
        Destroy(gameObject);
    }
}
