using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Unity.Netcode.Transports.UTP;

public class MainMenuUI : BaseUI
{
    [SerializeField] private LevelSelectionUI levelSelectionUI;
    [SerializeField] private MultiplayerMenuUI multiplayerMenuUI;
    [SerializeField] private GameObject timeoutPanel;
    [SerializeField] private Button singleplayerButton;
    [SerializeField] private Button multiplayerButton;
    [SerializeField] private Button exitButton;
    void Start()
    {
        singleplayerButton.onClick.AddListener(() => {
            Hide(()=>{
                timeoutPanel.SetActive(true);//should change someday
                NetworkManager.Singleton.StartHost();
                NetworkManager.Singleton.SceneManager.LoadScene("Beginnings", LoadSceneMode.Single);
            });
        });

        multiplayerButton.onClick.AddListener(() => {
            
            timeoutPanel.SetActive(true);
            multiplayerMenuUI.UpdateLobbies(()=>{
                timeoutPanel.SetActive(false);
                Hide(()=>{
                    multiplayerMenuUI.Show(null);
                });
            });
        });

        exitButton.onClick.AddListener(() => {
            timeoutPanel.SetActive(true);
            Application.Quit();
        });
    }
    public override void Hide(Action callback)
    {
        gameObject.SetActive(false);
        callback?.Invoke();
    }

    public override void Show(Action callback)
    {
        gameObject.SetActive(true);
        callback?.Invoke();
    }
    
}
