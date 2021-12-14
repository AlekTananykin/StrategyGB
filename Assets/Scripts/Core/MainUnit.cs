using UnityEngine;

public class MainUnit : MonoBehaviour, ISelectable, IAttackable, IUnit, IDamageDealer
{
    [SerializeField]private float _health = 100;
    [SerializeField]private float _maxHealth = 100;
    [SerializeField]private Sprite _icon;

    [SerializeField] private Animator _animator;
    [SerializeField] private StopCommandExecutor _stopCommand;
    [SerializeField] private int _damage = -25;


    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public Sprite Icon => _icon;

    public Transform PivotPoint => transform;

    public int Damage => _damage;

    public void RecieveDamage(int amount)
    {
        if(_health <= 0)
        {
            return;
        }
        
        _health -= amount;
        if (_health <= 0)
        {
            //_animator.SetTrigger("PlayDead");
            Invoke(nameof(destroy), 1f);
        }

    }

    private async void destroy()
    {
        await _stopCommand.ExecuteSpecificCommand(new StopCommand());
        Destroy(gameObject);
    }

}
