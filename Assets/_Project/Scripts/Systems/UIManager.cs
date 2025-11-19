using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    [SerializeField] private GameObject window;
    [SerializeField] private TextMeshProUGUI titleText;
    
    [SerializeField] private Button buttonRetry;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void ShowComplete()
    {
        titleText.text = "Уровень пройден!";
        window.SetActive(true);
    }
    
    public void ShowFailed()
    {
        titleText.text = "Уровень провален!";
        window.SetActive(true);
    }

    private void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnEnable()
    {
        buttonRetry.onClick.AddListener(Retry);
    }
    
    private void OnDisable()
    {
        buttonRetry.onClick.RemoveListener(Retry);
    }
}
