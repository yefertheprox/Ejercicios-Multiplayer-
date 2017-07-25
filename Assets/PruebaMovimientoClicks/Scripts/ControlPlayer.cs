using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ControlPlayer : NetworkBehaviour {
    public int counter;
    [ClientRpc]
    public void RpcDoMagic(int extra)
    {
        counter = extra;
    }
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            int ValorRandom = counter + 1;
            CmdEnvio(ValorRandom);
        }
    }
    [Command]
    void CmdEnvio(int Counter)
    {
        counter = Counter;
        RpcDoMagic(counter);
    }
}
