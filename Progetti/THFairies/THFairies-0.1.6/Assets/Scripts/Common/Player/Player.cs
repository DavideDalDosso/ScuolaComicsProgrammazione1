using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private NetworkVariable<FixedString32Bytes> nickname;
    [SerializeField] private TMP_Text nicknameText;
    [SerializeField] private Canvas canvas;
    public NetworkVariable<int> cash;
    public float speed {get { return _speed; }}
    void Awake(){
        nickname = new NetworkVariable<FixedString32Bytes>("");

        nickname.OnValueChanged += (prevValue, newValue)=>{
            nicknameText.text = newValue.ToString();
        };
        nicknameText.text = "";
    }
    void Start() {
        
        if(IsOwner && IsClient){
            virtualCamera.Priority += 1;
            PlayerManager.Singleton.SetLocalPlayer(this);
        }

        PlayerManager.Singleton.AddPlayer(this);
    }
    public void SetNickname(string name){
        nickname.Value = name;
    }
    public void SetCanvasMirrored(bool mirrored){
        var scale = canvas.transform.localScale.z;
        if(mirrored){
            canvas.transform.localScale = new Vector3(-scale,scale,scale);
        } else
            canvas.transform.localScale = new Vector3(scale, scale, scale);
    }
}
