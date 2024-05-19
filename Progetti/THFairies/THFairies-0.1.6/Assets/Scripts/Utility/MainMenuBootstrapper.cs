using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBootstrapper : MonoBehaviour
{
    [SerializeField] private MainMenuUI  mainMenuUI;
    [SerializeField] private LobbyUI  lobbyUI;
    void Start()
    {
        mainMenuUI.gameObject.SetActive(true);
    }
}
