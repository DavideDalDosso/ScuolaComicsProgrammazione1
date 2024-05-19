using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerInput : NetworkBehaviour
{
    private PlayerInputActions playerInputActions;
    private PlayerMovement playerMovement;
    public override void OnNetworkSpawn() {
        if(!IsOwner) return;

        playerInputActions = new PlayerInputActions();

        playerMovement = GetComponent<PlayerMovement>();

        playerInputActions.Player.Horizontal.performed += (context)=>{
            playerMovement.horizontal = context.ReadValue<float>();
        };
        playerInputActions.Player.Horizontal.canceled += (context)=>{
            playerMovement.horizontal = 0;
        };

        playerInputActions.Player.Vertical.performed += (context)=>{
            playerMovement.vertical = context.ReadValue<float>();
        };
        playerInputActions.Player.Vertical.canceled += (context)=>{
            playerMovement.vertical = 0;
        };
        playerInputActions.Management.Tower1.performed += (context)=>{
            TowerPlacementSystem.Singleton.StartPlacing();
        };
        playerInputActions.Management.Cancel.performed += (context)=>{
            TowerPlacementSystem.Singleton.StopPlacing();
        };
        playerInputActions.Management.Confirm.performed += (context)=>{
            if(TowerPlacementSystem.Singleton.IsPlacing())
                TowerPlacementSystem.Singleton.SpawnTurret();
        };
        playerInputActions.Management.Select.performed += (context)=>{
            
        };

        playerInputActions.Enable();
    }

    public override void OnNetworkDespawn()
    {
        playerInputActions.Dispose();
    }
}
