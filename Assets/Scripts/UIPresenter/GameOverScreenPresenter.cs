using TMPro;
using UnityEngine;
using Zenject;
using UniRx;
using System.Text;

public class GameOverScreenPresenter : MonoBehaviour
{
    [Inject] private IGameStatus _gameStatus;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _view;

    [Inject] private void Init()
    {
        _gameStatus.Status.ObserveOnMainThread().Subscribe(result =>
        {
            var message = new StringBuilder("Game Over!");
            if (0 == result)
            {
                message.AppendLine("Ничья");
            }
            else
            {
                message.AppendLine($"The winner is fraction number {FactionMember.GetWinner()}");
            }
            _view.SetActive(true);
            _text.text = message.ToString();
            Time.timeScale = 0;
        });
    }
}
