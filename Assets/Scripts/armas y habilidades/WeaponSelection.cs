using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    MovementMain movementMain; 
    static int weaponNumber = 4;
    int weaponNum;
    char weaponDirection;

    public GameObject player;
    GameObject[] weapons = new GameObject[weaponNumber];
    GameObject[] weaponIcon = new GameObject[weaponNumber];



    //crear un array

    // Start is called before the first frame update
    void Start()
    {
        movementMain = FindObjectOfType<MovementMain>();
    }

    // Update is called once per frame
    void Update()
    {

        weapons[0] = (GameObject)Resources.Load("Boots");
        weapons[1] = (GameObject)Resources.Load("HandEmpty");
        weapons[2] = (GameObject)Resources.Load("BoxItems");
        weapons[3] = (GameObject)Resources.Load("GunMultipurpose");
        weaponIcon[0] = GameObject.Find("ObjectA");
        weaponIcon[1] = GameObject.Find("ObjectB");
        weaponIcon[2] = GameObject.Find("ObjectX");
        weaponIcon[3] = GameObject.Find("ObjectY");



        //se hace el cambi del arma, se destruye la anterior y se crea la elegida
        if (Input.GetAxis("padVertical") > 0)
        {
          NormaliceBoneScale();
            Debug.Log("tecla A");
            DestroyWeapon(movementMain.weaponOption, weaponNum);
            movementMain.weaponOption = 0;
            weaponNum = 2;
            CreateWeapon(movementMain.weaponOption, 0, 2);

            
        }
        else if (Input.GetAxis("padHorizontal") > 0)
        {

          //s  Debug.Log("tecla B");
            DestroyWeapon(movementMain.weaponOption, weaponNum);
            movementMain.weaponOption = 1;
            weaponNum = 1;
            CreateWeapon(movementMain.weaponOption, 1, 1);
            NormaliceBoneScale();
        }
        else if (Input.GetAxis("padHorizontal") < 0)
        {

            Debug.Log("tecla X");
            DestroyWeapon(movementMain.weaponOption, weaponNum);
            movementMain.weaponOption = 2;
            weaponNum = 1;
            CreateWeapon(movementMain.weaponOption, 1, 1);
            NormaliceBoneScale();
        }
        else if (Input.GetAxis("padVertical") < 0)
        {
            NormaliceBoneScale();
           // Debug.Log("tecla Y");
            DestroyWeapon(movementMain.weaponOption, weaponNum);
            movementMain.weaponOption = 3;
            weaponNum = 1;
            CreateWeapon(movementMain.weaponOption, 1, 1);

        }

    }

    void CreateWeapon(int weaponOption, int option, int weaponNum)
    {

        //creacion de arma, el weaponNum significa el numero de armas que se pueden crear
        for (int i = 1; i <= weaponNum; i++)
        {
            int weaponIdentity = 0;
            //se crea el arma derecha
            if (i == 1)
            {
                weaponIdentity = 2;
                weaponDirection = 'D';
            }
            //se crea el arma izquierda
            else if (i == 2)
            {
                weaponIdentity = 1;
                weaponDirection = 'L';
            }

            switch (option)
            {
                case 0://arma de pies
                    //se encoje el pie elegido
                    player.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(weaponIdentity).GetChild(0).localScale = new Vector3(0.25f, 0.25f, 0.25f);
                    //se selecciona el icono
                    weaponIcon[weaponOption].GetComponent<CanvasRenderer>().SetAlpha(2f);
                    //se crea el arma izquierda
                    GameObject weaponCurrentFoot = Instantiate(weapons[weaponOption], player.transform.GetChild(1).GetChild(0).GetChild(0)
                                                   .GetChild(weaponIdentity).GetChild(0));
                    //aqui se va a parentar el arma con el
                    weaponCurrentFoot.transform.localScale = new Vector3(4, 4, 4);
                    weaponCurrentFoot.transform.localRotation = Quaternion.Euler(0, 0, 0) * Quaternion.Euler(90, 0, 0);
                    weaponCurrentFoot.transform.localPosition = Vector3.zero;
                    weaponCurrentFoot.name = weapons[weaponOption].name + "." + weaponDirection;
                    //se hacen condiciones para el arma derecha
                    if (i == 2)
                    {
                        //se desactiva el dash
                        weaponCurrentFoot.GetComponent<BootPower>().forceDashBoot = 0;
                        //se desactiva el uso de minas
                        weaponCurrentFoot.GetComponent<BootPower>().couldMine = false;
                    }

                    break;
                case 1://arma de manos

                    player.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(weaponIdentity - 1).GetChild(0).localScale = new Vector3(0.25f, 0.25f, 0.25f);
                    //se selecciona el icono
                    weaponIcon[weaponOption].GetComponent<CanvasRenderer>().SetAlpha(2f);
                    //se crea el arma izquierda
                    GameObject weaponCurrentHand = Instantiate(weapons[weaponOption], player.transform.GetChild(1).GetChild(0).GetChild(0)
                                                                                     .GetChild(0).GetChild(weaponIdentity - 1).GetChild(0));
                    //aqui se va a parentar el arma con el 
                    weaponCurrentHand.transform.localScale = new Vector3(4, 4, 4);
                    weaponCurrentHand.transform.localRotation = Quaternion.Euler(0, 0, -90);
                    weaponCurrentHand.transform.localPosition = Vector3.zero;
                    weaponCurrentHand.name = weapons[weaponOption].name + "." + weaponDirection;
                    //se crea el arma derecha
                    break;
            }
        }





    }

    void DestroyWeapon(int weaponOption, int WeaponNum)
    {

        //destruye el arma derecha (primera en crearse)
        Destroy(GameObject.Find(weapons[weaponOption].name + ".D"));
        if (WeaponNum == 2)
        {
            //destruye el arma izquierda
            Destroy(GameObject.Find(weapons[weaponOption].name + ".L"));

        }


        Debug.Log(weapons[weaponOption]);
        weaponIcon[weaponOption].GetComponent<CanvasRenderer>().SetAlpha(0.7f);//se deselecciona el icono
        CancelHabilities();

    }

    //aquí se cancelan todas las habilidades
    void CancelHabilities()
    {
        movementMain.twoJump = false;
        movementMain.focusCamera = false;
    }

    void NormaliceBoneScale()
    {
        player.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetChild(0).localScale = new Vector3(1, 1, 1);
        player.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).localScale = new Vector3(1, 1, 1);

        player.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetChild(0).localScale = new Vector3(1, 1, 1);
        player.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).localScale = new Vector3(1, 1, 1);
    }

}
