using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class LobbyUI : BaseUI
{
    [SerializeField] private MultiplayerMenuUI multiplayerMenuUI;
    [SerializeField] private Button backButton;
    [SerializeField] private Button startButton;
    [SerializeField] private TMP_Text lobbyCodeText;
    [SerializeField] private TMP_Text levelNameText;
    [SerializeField] private TMP_Text lobbyNameText;
    [SerializeField] private GameObject timeoutPanel;
    [SerializeField] private PlayerLobbyTemplateUI playerSlotTemplate;
    [SerializeField] private Transform playerSlotContainer;
    private float refreshTime;
    private List<GameObject> playerSlots = new List<GameObject>();
    void Start()
    {

        backButton.onClick.AddListener(() => {
            timeoutPanel.SetActive(true);
            NetworkManager.Singleton.Shutdown();
                LobbyManager.Singleton.LeaveLobby(()=>{
                    multiplayerMenuUI.UpdateLobbies(()=>{
                    timeoutPanel.SetActive(false);
                    Hide(()=>{
                        multiplayerMenuUI.Show(null);
                    });
                });
            });
        });

        startButton.onClick.AddListener(() => {
            timeoutPanel.SetActive(true);

            NetworkManager.Singleton.SceneManager.LoadScene("Beginnings", LoadSceneMode.Single); 

        });

        playerSlotTemplate.gameObject.SetActive(false);
    }
    void Update(){
        if(Time.timeSinceLevelLoad > refreshTime){
            refreshTime = Time.timeSinceLevelLoad + 1.5f;
        }
        RefreshLobby();
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

    public void RefreshLobby(){
        foreach(var player in playerSlots){
            Destroy(player);
        }
        playerSlots.Clear();

        foreach(var player in LobbyManager.Singleton.lobby.Players){
            var playerSlot = Instantiate(playerSlotTemplate, playerSlotContainer).GetComponent<PlayerLobbyTemplateUI>();
            playerSlots.Add(playerSlot.gameObject);

            playerSlot.SetPlayerName(player.Data["Nickname"].Value);

            playerSlot.gameObject.SetActive(true);
        }

        var data = LobbyManager.Singleton.lobby.Data;
        lobbyCodeText.text = LobbyManager.Singleton.lobby.LobbyCode;
        levelNameText.text = "Level: "+data["Level"].Value;
        lobbyNameText.text = LobbyManager.Singleton.lobby.Name;
    }

}
