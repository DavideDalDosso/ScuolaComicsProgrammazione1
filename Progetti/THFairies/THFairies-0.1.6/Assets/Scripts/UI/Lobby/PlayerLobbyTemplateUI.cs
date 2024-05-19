using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerLobbyTemplateUI : MonoBehaviour
{
    [SerializeField] private TMP_Text playerNickname;
    public void SetPlayerName(string playerName){
        this.playerNickname.text = playerName;
    }
}
