using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlacementSystem : NetworkBehaviour
{
    private static TowerPlacementSystem _singleton;
    public static TowerPlacementSystem Singleton {get {return _singleton;}}
    [SerializeField] GameObject selectionDisplay;
    private Animator selectionDisplayAnimator;
    [SerializeField] List<Tilemap> restrictedPlacementTilemaps;
    [SerializeField] SpriteRenderer turretDisplay;
    [SerializeField] Transform rangeDisplay;
    private bool placeableStatus;
    public bool placing;
    private int selectedTurret;
    private Vector3Int selectedPosition;
    
    void Awake()
    {
        _singleton = this;

        selectionDisplayAnimator = selectionDisplay.GetComponent<Animator>();
        placing = false;
    }
    void FixedUpdate() {
        selectionDisplay.SetActive(placing);
        turretDisplay.gameObject.SetActive(placing);

        if(placing){
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            selectedPosition = restrictedPlacementTilemaps[0].WorldToCell(mousePosition);
            selectedPosition = new Vector3Int(selectedPosition.x, selectedPosition.y, 0);

            var placeable = CheckIfPlaceable(selectedPosition);

            if( placeableStatus != placeable ){
                placeableStatus = placeable;

                if(placeable) {
                    selectionDisplayAnimator.CrossFade("Active",0);
                    turretDisplay.color = Color.white;
                } else {
                    selectionDisplayAnimator.CrossFade("Inactive",0);
                    turretDisplay.color = Color.gray;
                }
            }

            var turretSO = Resources.FindObjectsOfTypeAll(typeof(TurretSO))[selectedTurret] as TurretSO;

            selectionDisplay.transform.position = selectedPosition + new Vector3(0.5f, 0.5f, 0);
            turretDisplay.sprite = turretSO.icon;
            turretDisplay.transform.position = selectionDisplay.transform.position;

            rangeDisplay.position = selectionDisplay.transform.position;
            var rangeSize = turretSO.range;
            rangeDisplay.localScale = new Vector3(rangeSize, rangeDisplay.localScale.y, rangeSize);
        }
    }
    
    void OnApplicationQuit() {
        _singleton = null;
    }
    public bool CheckIfPlaceable(Vector3Int cellPosition){

        var targetPosition = cellPosition + new Vector3(0.5f, 0.5f, 0);

        foreach(var tilemap in restrictedPlacementTilemaps){
                
            if(tilemap.HasTile(cellPosition)) return false;

        }

        foreach(var turret in TurretManager.turrets){
            if(turret.transform.position == targetPosition) return false;
        }

        return true;
    }
    public void StartPlacing(){
        placing = true;
    }
    public void StopPlacing(){
        placing = false;
    }
    public bool IsPlacing(){
        return placing;
    }
    public void SpawnTurret(){
        SpawnTurretServerRpc(selectedTurret, selectedPosition, RpcTarget.Server);
    }
    [Rpc(SendTo.SpecifiedInParams)]
    private void SpawnTurretServerRpc(int turretIndex, Vector3Int position, RpcParams rpcParams){
        
        var clientId = rpcParams.Receive.SenderClientId;
        var player = PlayerManager.players[PlayerManager.playersIndexes[clientId]];

        var turretSO = TurretManager.scriptableObjects[turretIndex];//ensure out of bounds later

        if(!CheckIfPlaceable(position)) return;

        if(player.cash.Value >= turretSO.baseCost){
            player.cash.Value -= turretSO.baseCost;
        } else return;

        var spawnPosition = position + new Vector3(0.5f, 0.5f, 0);

        TurretManager.Singleton.SpawnTurret(turretSO, spawnPosition);

        TurretSpawnedClientRpc(RpcTarget.Single(rpcParams.Receive.SenderClientId, RpcTargetUse.Temp));
    }
    [Rpc(SendTo.SpecifiedInParams)]
    private void TurretSpawnedClientRpc(RpcParams rpcParams){
        StopPlacing();
    }
}
