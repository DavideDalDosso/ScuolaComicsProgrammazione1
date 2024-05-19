using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport;
using Unity.Services.Lobbies;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerMenuUI : BaseUI
{
    [SerializeField] private LobbyUI lobbyUI;
    [SerializeField] private MainMenuUI mainMenuUI;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject timeoutPanel;
    [SerializeField] private TMP_InputField nicknameField;
    [SerializeField] LobbyTemplateUI lobbySlotTemplate;
    [SerializeField] Transform lobbySlotContainer;
    [SerializeField] GameObject joinPanel;
    [SerializeField] private Button joinPanelJoinButton;
    [SerializeField] private Button joinPanelBackButton;
    [SerializeField] private TMP_InputField lobbyCodeField;
    private List<GameObject> lobbySlots = new List<GameObject>();
    void Start()
    {
        lobbySlotTemplate.gameObject.SetActive(false);

        hostButton.onClick.AddListener(() => {

            timeoutPanel.SetActive(true);
            RelayManager.Singleton.CreateRelay((joinCode)=>{
                LobbyManager.Singleton.CreateLobby(joinCode, (lobbyCode)=>{
                    timeoutPanel.SetActive(false);
                    joinPanel.SetActive(false);
                    Hide(()=>{
                        lobbyUI.Show(null);
                    });
                });
            });
        });

        joinButton.onClick.AddListener(() => {
            joinPanel.SetActive(true);
        });

        backButton.onClick.AddListener(() => {
            joinPanel.SetActive(false);
            Hide(()=>{
                mainMenuUI.Show(null);
            });
        });

        joinPanelBackButton.onClick.AddListener(() => {
            joinPanel.SetActive(false);
        });

        joinPanelJoinButton.onClick.AddListener(() => {

            var lobbyCode = lobbyCodeField.text;

            JoinLobby(lobbyCode);
        });

        nicknameField.onEndEdit.AddListener((text) => {
            LocalPlayerData.nickname = text;
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
    public void UpdateLobbies(Action callback){
        LobbyManager.Singleton.ListLobbies((lobbyQuery)=>{

            foreach(var lobby in lobbySlots){
                Destroy(lobby);
            }
            lobbySlots.Clear();

            foreach(var lobby in lobbyQuery.Results){
                var lobbySlot = Instantiate(lobbySlotTemplate, lobbySlotContainer).GetComponent<LobbyTemplateUI>();
                lobbySlots.Add(lobbySlot.gameObject);

                lobbySlot.SetLobbyName(lobby.Name);
                lobbySlot.SetLobbyLevel(lobby.Data["Level"].Value);
                lobbySlot.SetPlayerCount(lobby.Players.Count);

                lobbySlot.onPress += ()=>{
                    JoinLobby(lobby.LobbyCode);
                };

                lobbySlot.gameObject.SetActive(true);
            }

            callback?.Invoke();

        });
    }
    private void JoinLobby(string lobbyCode){
        timeoutPanel.SetActive(true);
        LobbyManager.Singleton.JoinLobby(lobbyCode, (succeded)=>{

            if(!succeded){
                timeoutPanel.SetActive(false);
                Debug.Log("Could not find Lobby.");
                return;
            }

            RelayManager.Singleton.JoinRelay(LobbyManager.Singleton.lobby.Data["JoinCode"].Value, ()=>{
                timeoutPanel.SetActive(false);
                joinPanel.SetActive(false);
                Hide(()=>{
                    lobbyUI.Show(null);
                });
            });
        });
    }
}
