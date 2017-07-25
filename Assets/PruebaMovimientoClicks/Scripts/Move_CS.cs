using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Move_CS : NetworkBehaviour {
    [SyncVar]
    public Vector2 IrPos;
    [SyncVar]
    public Vector2 SyncPosicion;
    private bool activarPrediccion;
    private Toggle boolPredicion;
	// Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            boolPredicion = GameObject.FindGameObjectWithTag("Texto").GetComponent<Toggle>();
        }
        transform.rotation = Quaternion.Euler(0, 0, 45);
	}
	
	// Update is called once per frame
	void Update () {
        Mover();
        EnviarPosicion();
        MoverLerp();
        if (!isLocalPlayer)
        {
            return;
        }
        activarPrediccion = boolPredicion.isOn;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 OldPosIr = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (activarPrediccion)
            {
                IrPos = OldPosIr;
                Mover();
            }
            EnviarPuntoIr(OldPosIr);
        }
	}
    [Server]
    void EnviarPosicion()
    {
        CmdRecivirPosicion((Vector2)transform.position);
    }
    [Command]
    void CmdRecivirPosicion(Vector2 p)
    {
        SyncPosicion = p;
    }
    void Mover()
    {
        if (Vector2.Distance(IrPos, transform.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, IrPos, Time.deltaTime * 3);
        }
    }
    void MoverLerp()
    {
        transform.position = Vector2.Lerp(transform.position, SyncPosicion, Time.deltaTime * 3);
    }
    [ClientCallback]
    void EnviarPuntoIr(Vector2 p)
    {
        CmdEnviarPuntoIrServer(p);
    }
    [Command]
    void CmdEnviarPuntoIrServer(Vector2 p)
    {
        IrPos = p;
    }
}
