using UnityEngine;
using Unity.Netcode;

namespace MultProject
{
    public class MultProjectPlayer : NetworkBehaviour
    {
        public override void OnNetworkSpawn()
        {
            if (!IsOwner) return;

            Debug.Log("Player spawned with ownership");
        }

    }

}
