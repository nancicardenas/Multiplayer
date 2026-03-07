using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPoints;

    private bool wasdJoined = false;
    private bool arrowsJoined = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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

            wasdJoined = true;
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

            arrowsJoined = true;
        }

        //foreach (var gamePad in Gamepad.all)
        //{
        //    if(gamePad.buttonSouth.wasPressedThisFrame)
        //    {
        //        PlayerInput.Instantiate(playerPrefab,
        //            controlScheme: "Gamepad",
        //            pairWithDevice: gamePad);
        //    }
        //}



    }
}
