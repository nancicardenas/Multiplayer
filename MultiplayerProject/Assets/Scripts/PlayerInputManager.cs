using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using Unity.Netcode;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPoints;

    //private CinemachineTargetGroup targetGroup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if(NetworkManager.Singleton == null)
        {
            Debug.LogError("NetworkManager not found");

        }

        NetworkManager.Singleton.OnClientConnectedCallback += SpawnPlayerForClient;
    }

    private void OnDestroy()
    {
        if(NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= SpawnPlayerForClient;
        }
    }

    //spawn player 
    private void SpawnPlayerForClient(ulong clientId)
    {
        Debug.Log("Player spawned for client: " + clientId);

        if (!NetworkManager.Singleton.IsServer) return;

        int spawnIndex = (int)(clientId % (ulong)spawnPoints.Length);
        Vector3 spawnPos = spawnPoints[spawnIndex].position;

        GameObject playerInstance = Instantiate(playerPrefab, spawnPos, Quaternion.identity);

        NetworkObject netObj = playerInstance.GetComponent<NetworkObject>();
        netObj.SpawnAsPlayerObject(clientId);
    }
}
