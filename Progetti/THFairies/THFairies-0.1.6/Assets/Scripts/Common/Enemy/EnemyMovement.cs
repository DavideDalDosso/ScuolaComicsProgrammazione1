using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemyMovement : NetworkBehaviour
{
    private EnemyPath path;
    private int nodeIndex;
    private EnemyPathNode currentNode;
    private Vector3 offset;
    private EnemyBase enemy;
    private float movementX;
    private NetworkVariable<bool> mirrored;
    void Awake(){
        enemy = GetComponent<EnemyBase>();

        mirrored = new NetworkVariable<bool>(false);
    }
    void FixedUpdate()
    {
        if(!IsServer) return;

        UpdateNetworkVariables();

        UpdateCurrentNode();
        Move();

        var scale = transform.localScale;

        if(mirrored.Value){
            transform.localScale = new Vector3(-Mathf.Abs(scale.x),scale.y,1);
        } else
            transform.localScale = new Vector3(Mathf.Abs(scale.x),scale.y,1);

        if(currentNode != null){

            var nodeDistance = (currentNode.position - transform.position).magnitude;
            enemy.distance = currentNode.distance + nodeDistance;

        }
    }
    void OnDrawGizmosSelected(){
        if(path != null){
            Debug.DrawLine(transform.position, currentNode.transform.position, Color.red);
        }
    }

    void Move(){
        if(currentNode == null) return;

        var direction = (- transform.position + (currentNode.transform.position + offset) ).normalized;

        movementX = direction.x;

        transform.position += direction * enemy.speed * Time.fixedDeltaTime;
    }

    void UpdateCurrentNode(){
        if(path){

            if(currentNode != null ){
                var distance = (transform.position - (currentNode.transform.position + offset) ).sqrMagnitude;
                if(distance > 0.1f) return;
            }

            currentNode = path.GetNode(nodeIndex);
            nodeIndex++;

            if(currentNode == null){
                path = path.GetNextPath();
                nodeIndex=0;
                UpdateCurrentNode();
                return;
            }
        } else {
            enemy.ReachEnd();
        }
    }
    public void SetPath(EnemyPath path){
        this.path = path;
    }
    public void SetOffset(Vector2 offset){
        this.offset = offset;
    }

    void UpdateNetworkVariables(){

        if(movementX < 0)
            mirrored.Value = true;
        else if(movementX > 0)
            mirrored.Value = false;
    }
}
