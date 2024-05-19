using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public float horizontal {get;set;}
    public float vertical {get;set;}
    private Player player;
    private Rigidbody2D rb;
    private Animator playerAnimator;
    private NetworkVariable<bool> moving;
    private NetworkVariable<bool> mirrored;
    void Awake(){
        player = GetComponent<Player>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        moving = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        mirrored = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    }
    void FixedUpdate(){

        UpdateNetworkVariables();

        playerAnimator.SetBool("Moving", moving.Value);

        if(mirrored.Value){
            transform.localScale = new Vector3(-1,1,1);
        } else
            transform.localScale = new Vector3(1,1,1);

        player.SetCanvasMirrored(mirrored.Value);

        rb.velocity = new Vector2(horizontal, vertical).normalized * player.speed;
    }

    void UpdateNetworkVariables(){

        if(!IsOwner) return;

        if(horizontal != 0 || vertical != 0)
            moving.Value = true;
        else 
            moving.Value = false;

        if(horizontal < 0)
            mirrored.Value = true;
        else if(horizontal > 0)
            mirrored.Value = false;
    }
}
