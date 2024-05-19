using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLoadoutSlotUI : MonoBehaviour
{
    [SerializeField] private TMP_Text costText;
    [SerializeField] private Image icon;
    [SerializeField] private Image background;
    [SerializeField] private Button button;
    public delegate void OnPress();
    public OnPress onPress;
    public void Awake(){
        button.onClick.AddListener(()=>{
            if(onPress!=null) onPress();
        });
    }
    public void SetCost(int cost){
        costText.text = cost.ToString();
    }
    public void SetBackground(Color color){
        background.color = color;
    }
    public void SetIcon(Sprite sprite){
        icon.sprite = sprite;
    }
}
