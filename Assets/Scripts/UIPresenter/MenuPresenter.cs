using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MenuPresenter : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private GameObject _lockPanel;


    void Start()
    {
        _backButton.OnClickAsObservable().Subscribe(_ => gameObject.SetActive(false));
        _backButton.OnClickAsObservable().Subscribe(_=> _lockPanel.SetActive(false));
        _backButton.OnClickAsObservable().Subscribe(_ => Time.timeScale = 1);

        _exitButton.OnClickAsObservable().Subscribe(_ => Application.Quit());
    }
}
