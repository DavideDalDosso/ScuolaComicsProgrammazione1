using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class NetworkManagerBootstrapper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(NetworkManager.Singleton == null)
            SceneManager.LoadScene("NetworkManagerScene", LoadSceneMode.Additive);
    }
}
