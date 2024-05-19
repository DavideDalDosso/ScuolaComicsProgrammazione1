using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyTemplateUI : MonoBehaviour
{
    [SerializeField] private TMP_Text lobbyName;
    [SerializeField] private TMP_Text lobbyLevel;
    [SerializeField] private TMP_Text playerCount;
    [SerializeField] Button button;
    public delegate void OnPress();
    public OnPress onPress;
    void Start(){
        button.onClick.AddListener(()=>{
            if(onPress != null)
                onPress();
        });
    }
    public void SetLobbyName(string lobbyName){
        this.lobbyName.text = lobbyName;
    }
    public void SetLobbyLevel(string levelName){
        this.lobbyLevel.text = levelName;
    }
    public void SetPlayerCount(int playerCount){
        this.playerCount.text = playerCount+"/6";
    }
}
