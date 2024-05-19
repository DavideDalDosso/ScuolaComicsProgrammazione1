using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] TMP_Text waveText;
    [SerializeField] TMP_Text waveTimeText;
    [SerializeField] TMP_Text cashText;
    [SerializeField] TMP_Text livesText;
    [SerializeField] GameLoadoutSlotUI turretSlotTemplate;
    [SerializeField] Transform turretSlotContainer;
    private List<GameObject> turretList = new List<GameObject>();
    private float nextWaveTime;
    private Match match;
    private Player player;
    public void Awake(){
        turretSlotTemplate.gameObject.SetActive(false);
        LoadTowers();
    }
    public void Setup(Match match, Player player){
        this.match = match;
        this.player = player;

        player.cash.OnValueChanged += (previousValue, newValue)=>{
            cashText.text = newValue.ToString();
        };
        cashText.text = player.cash.Value.ToString();

        match.lives.OnValueChanged += (previousValue, newValue)=>{
            livesText.text = newValue.ToString();
        };
        livesText.text = match.lives.Value.ToString();

        match.currentWave.OnValueChanged += (previousValue, newValue)=>{
            waveText.text = "Wave "+newValue;
        };
        waveText.text = "Wave "+match.currentWave.Value;

        match.nextWaveTime.OnValueChanged += (previousValue, newValue)=>{
            nextWaveTime = newValue;
        };
        nextWaveTime = match.nextWaveTime.Value;
    }

    void Update() {
        if(Time.timeSinceLevelLoad > nextWaveTime) {
            if(nextWaveTime == -1f)
                if(match.lives.Value > 0)
                    waveTimeText.text = "Finished waves! WIN!";
                else
                    waveTimeText.text = "Game Over :kaguyastare:";
            else
                waveTimeText.text = "Time left: 0";
        } else {
            var timeLeft = Math.Round(nextWaveTime - Time.timeSinceLevelLoad);
             waveTimeText.text = "Time left: "+timeLeft;
        }
    }

    void LoadTowers(){
        foreach (TurretSO turretSO in TurretManager.scriptableObjects){
            var turretSlot = Instantiate(turretSlotTemplate, turretSlotContainer).GetComponent<GameLoadoutSlotUI>();
            turretSlot.SetIcon(turretSO.icon);
            turretSlot.SetCost(turretSO.baseCost);
            turretSlot.SetBackground(Color.white);
            turretSlot.onPress += ()=>{
                TowerPlacementSystem.Singleton.StartPlacing();
            };
            turretSlot.gameObject.SetActive(true);
        }
    }
}
