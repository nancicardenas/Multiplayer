using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPoints;

    private bool wasdJoined = false;
    private bool arrowsJoined = false;

    private CinemachineTargetGroup targetGroup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetGroup = GetComponent<CinemachineTargetGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current == null) return;

        //instantiate first player if they have pressed the space key 
        if(!wasdJoined && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            var player = PlayerInput.Instantiate (playerPrefab,
                controlScheme: "WASD",
                pairWithDevice: Keyboard.current);

            //spawns at the first position
            if(spawnPoints.Length > 0)
            {
                player.transform.position = spawnPoints[0].position;
            }

            //add player to target for cinemachine 
            targetGroup.AddMember(player.transform, 1f, 2f);

            wasdJoined = true;
            return;
        }

        if(!arrowsJoined && Keyboard.current.digit9Key.wasPressedThisFrame)
        {
            var player = PlayerInput.Instantiate(playerPrefab,
                controlScheme: "Arrows",
                pairWithDevice: Keyboard.current);

            if(spawnPoints.Length > 1)
            {
                player.transform.position = spawnPoints[1].position;
            }

            //add player to target for cinemachine 
            targetGroup.AddMember(player.transform, 1f, 2f);


            arrowsJoined = true;
        }
    }
}
