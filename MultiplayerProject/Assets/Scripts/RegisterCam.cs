using UnityEngine;
using Unity.Netcode;

public class RegisterCam : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        CameraFollow cam = FindFirstObjectByType<CameraFollow>();

        if (cam != null)
            cam.AddPlayer(transform);
    }
}
