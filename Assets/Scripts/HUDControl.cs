using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDControl : MonoBehaviour
{
    //este es el panel dnde aparece la vida
    public GameObject life;
    public GameObject money;
    public GameObject energy;
    public GameObject zalockWin;
    public GameObject UIUp;
    public GameObject habilidadA;
    public GameObject habilidadB;
    MovementMain player;
    public GunMultipurpose gunMultipurpose;
    public BootPower bootPower;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<MovementMain>();
        
        //todas las harmas pueden ser clases hijas d una clase llamada armas
    }

    // Update is called once per frame
    void Update()
    {

        gunMultipurpose = FindObjectOfType<GunMultipurpose>();
        bootPower = FindObjectOfType<BootPower>();
        
        
       
        life.GetComponent<Text>().text = player.lifePlayer + " puntos";
        money.GetComponent<Text>().text = player.moneyPlayer + " Llave";
        energy.GetComponent<Text>().text = player.energyPlayer + " Energia";
        
        if(player.moneyPlayer == 1)
        {
            zalockWin.SetActive(true);  
            UIUp.SetActive(false);
        }
        if (player.energyPlayer == 1)
        {
            //se borra la mision 1
            Destroy(GameObject.Find("textMision2"));
        }

        //verifica si existe el arma GunMultipurpose
        if (player.weaponOption == 3)
        {
            if (gunMultipurpose.activeSkill == 1)
            {
                // se activa la habilidad B
                habilidadB.SetActive(true);
                habilidadA.SetActive(false);
            }
            else
            {
                // se activa la habilidad A
                habilidadB.SetActive(false);
                habilidadA.SetActive(true);
            }
        }

        //verifica si existe el arma Boots
        if (player.weaponOption == 0)
        {
            if (bootPower.activeSkill == 1)
            {
                // se activa la habilidad B
                habilidadB.SetActive(true);
                habilidadA.SetActive(false);
            }
            else
            {
                // se activa la habilidad A
                habilidadB.SetActive(false);
                habilidadA.SetActive(true);
            }
        }

            
        

    }
}
