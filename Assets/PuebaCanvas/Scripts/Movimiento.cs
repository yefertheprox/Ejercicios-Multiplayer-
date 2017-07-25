using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Movimiento : NetworkBehaviour {
    public Text Texto;
    [SyncVar]
    public int Numero;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 2)
        {
            Texto = GameObject.FindGameObjectWithTag("Texto").transform.GetChild(1).GetComponent<Text>();
        }
        else
        {
            Texto = GameObject.FindGameObjectWithTag("Texto").transform.GetChild(0).GetComponent<Text>();
        }
        print(GameObject.FindGameObjectsWithTag("Player").Length);
    }
    public override void OnStartLocalPlayer()
    {

    }
    void Update()
    {
        if (!isLocalPlayer)
        {
            Texto.text = Numero + "";
            return;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Numero++;
            SendInput();
        }
        Texto.text = Numero + "";
    }
    [Command]
    void CmdResive(int n)
    {
        Numero = n;
    }

    [ClientCallback]
    void SendInput()
    {
        if (isLocalPlayer)
        {
            CmdResive(Numero);
        }
    }
}
