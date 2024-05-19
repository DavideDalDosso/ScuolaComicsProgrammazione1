using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathManager : MonoBehaviour
{
    void Start(){
        if(EnemyPath.enemyPaths == null) return;

        foreach(EnemyPath path in EnemyPath.enemyPaths){

        foreach(var nextPath in path.GetAllNextPath()){
            nextPath.AddPreviousPath(path);
        }

            if(path.GetAllNextPath().Length == 0){
                GenerateDistanceForPath(path, 0);
            }
        }
    }

    void GenerateDistanceForPath(EnemyPath path, float baseDistance){
        var nodes = path.GetAllNodes();

        nodes[nodes.Length - 1].distance = baseDistance;
        for(int i = nodes.Length-2; i >= 0; i--){
            var distance = (nodes[i].position - nodes[i+1].position).magnitude;
            nodes[i].distance = nodes[i+1].distance + distance;
        }

        foreach(EnemyPath prevPath in path.GetAllPreviousPath()){

            var prevNodes = prevPath.GetAllNodes();
            var prevLastNode = prevNodes[prevNodes.Length - 1];

            var prevDistance = (nodes[0].position - prevLastNode.position).magnitude;

            if( (nodes[0].distance + prevDistance) > prevLastNode.distance ){
                GenerateDistanceForPath(prevPath, nodes[0].distance + prevDistance);
            }
        }
    }
}
