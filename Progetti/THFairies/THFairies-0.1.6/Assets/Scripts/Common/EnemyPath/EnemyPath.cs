using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    private static List<EnemyPath> _enemyPaths;
    public static List<EnemyPath> enemyPaths {get {return _enemyPaths;}}
    private EnemyPathNode[] nodes;
    private int nextPathIndex;
    [SerializeField] EnemyPath[] nextPath;
    private List<EnemyPath> prevPath;
    void Awake(){
        if(_enemyPaths == null){
            _enemyPaths = new List<EnemyPath>();
        }
        _enemyPaths.Add(this);

        nodes = new EnemyPathNode[transform.childCount];
        prevPath = new List<EnemyPath>();

        int i=0;

        foreach(Transform t in transform){
            var pathNode = t.AddComponent<EnemyPathNode>();
            pathNode.distance = 0;
            nodes[i] = pathNode;
            i++;
        }
    }
    void OnApplicationQuit() {
        _enemyPaths = null;
    }
    void Start(){
        foreach(var path in nextPath){
            path.prevPath.Add(this);
        }
    }

    public void OnDrawGizmosSelected() {
        if(nodes != null){
            for(int i=0;i<nodes.Length-1;i++){
                Debug.DrawLine(nodes[i].transform.position, nodes[i+1].transform.position, Color.white);
            }
        }

        if(nextPath != null){
            for (int i=0;i<nextPath.Length;i++){
                var nextNode = nextPath[i].GetNode(0);
                if(nextNode == null) continue;
                Debug.DrawLine(nodes[nodes.Length-1].transform.position, nextNode.transform.position, Color.yellow);
                nextPath[i].OnDrawGizmosSelected();
            }
        }
    }

    public EnemyPathNode GetNode(int index){
        if(index < 0 || index >= nodes.Length){
            return null;
        }
        return nodes[index];
    }

    public EnemyPath GetNextPath(){
        if(nextPath.Length == 0) return null;
        var nextPathResult = nextPath[nextPathIndex];
        nextPathIndex++;
        if(nextPathIndex == nextPath.Length){
            nextPathIndex = 0;
        }
        return nextPathResult;
    }

    public void AddPreviousPath(EnemyPath path){
        prevPath.Add(path);
    }

    public EnemyPath[] GetAllNextPath(){
        return nextPath.ToArray();
    }
    public EnemyPath[] GetAllPreviousPath(){
        return prevPath.ToArray();
    }

    public EnemyPathNode[] GetAllNodes(){
        return nodes.ToArray();
    }

}
