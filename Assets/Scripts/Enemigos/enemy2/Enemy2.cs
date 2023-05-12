using UnityEngine;
using UnityEngine.AI;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class Enemy2 : MonoBehaviour
{
    //Variables
    public NavMeshAgent navEnemy;//Agente Navmesh para que el enemigo no se pegue contra las paredes

    public Transform player;
    
    public GameObject playerGO;

    public LayerMask floor, playerLayer;

    public float life;

    //Variables Comportamiento Normal
    public Vector3 destination;//Posicion destino
    bool markedDestination;//El destino ya esta marcado?
    public float rangeDestination;//Rango para escojer destino

    //Variables Comportamiento Ataque
    public float attackCooldown;//Recarga de ataque
    bool attacking;//Atacando
    public GameObject badBullet;

    //Variables Cambio de Comportamiento
    public float viewRange, attackRange;//Rango
    public bool playerInViewRange, playerInAttackRange;//Jugador en esta en el rango?
    bool stealth;//Sigilo

    private void Awake() //Busca al jugador y inicializa al enemigo como el agente para el NavMesh
    {
        player = GameObject.Find("Player").transform;
        navEnemy = GetComponent<NavMeshAgent>();
    }

    private void Update() 
    {
        //Sigilo Jugador (Esta agachado o no)
      //  stealth = playerGO.GetComponent<player>().noCrouch.enabled;

        //Verifica si ve al jugador o si el jugador esta en el rango de ataque
        playerInViewRange = Physics.CheckSphere(transform.position, viewRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        //Cambia Comportamientos
        if (!playerInViewRange && !playerInAttackRange) Normal();//Normal si no esta en rango
        if (playerInViewRange && !playerInAttackRange && !stealth) Normal();//Normal si esta en rango pero jugador con sigilo
        if (playerInViewRange && !playerInAttackRange && stealth) Follow();//Persigue en rango de vision
        if (playerInAttackRange && playerInViewRange) Attack();//Ataca en rango de ataque
    }

    private void Normal() //Comportamiento Normal (Caminar al azar)
    {
        //Si no hay destino, se busca uno nuevo
        if (!markedDestination) NewDestination();
        
        if (markedDestination)
            navEnemy.SetDestination(destination); //Agente Navmesh ubica su destino

        Vector3 distanceToWalkPoint = transform.position - destination; //Distancia entre el enemigo y su destino

        //Si ya llega a su destino, se queda sin destino ya que ya llego :v
        if (distanceToWalkPoint.magnitude < 1f)
            markedDestination = false;
    }
    private void NewDestination() //Buscar Nuevo Destino
    {
        //Numero al azar para el destino (Elije una ubicacion que no sea mas lejana a su rango de destino)
        float randomZ = Random.Range(-rangeDestination, rangeDestination);
        float randomX = Random.Range(-rangeDestination, rangeDestination);

        //Destino (posicion + rango de destino = destino)
        destination = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //Si ya hay ruta, entonces el destino queda como marcado
        if (Physics.Raycast(destination, -transform.up, 2f, floor))
            markedDestination = true;
    }

    private void Follow() //Acosar al jugador porque le debe plata (Perseguir)
    {
        navEnemy.SetDestination(player.position); //El destino es la posicion del jugador

    }

    private void Attack() //Acosar fisicamente al jugador :v (Atacar)
    {
        //El enemigo no se mueve al atacar
        navEnemy.SetDestination(transform.position);
        //Mirar al jugador (Aun hay que corregir el error de que rota tambien en Y)
        this.transform.LookAt(player);
        

        if (!attacking) //Verifica si aun no ha atacado
        {
            //Disparar bala mala
            GameObject BulletAux = Instantiate(badBullet, transform.position, Quaternion.identity);
            BulletAux.GetComponent<Rigidbody>().AddForce(transform.forward * 32f, ForceMode.Impulse);
            BulletAux.GetComponent<Rigidbody>().AddForce(transform.up * 8f, ForceMode.Impulse);
            Destroy(BulletAux, 2);

            //Esta atacando, asi que le toca esperar a la recarga para atacar de nuevo
            attacking = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }
    private void ResetAttack() //Despues de la recarga, se habilita el ataque otra vez
    {
        attacking = false;
    }

    public void RecibirDaño(int daño) //Recibir daño por el jugador (Aun no hay ataque de melee del jugador)
    {
        life -= daño;

        if (life <= 0) Invoke(nameof(DestroyEnemy), 0.5f); //Se muere si ya no hay vida
    }
    private void OnCollisionEnter(Collision collision) //Daño por bala
    {
        if (collision.collider.tag == "Bullet")
        {
            Destroy(collision.collider.gameObject);
            life--;
            if (life == 0)
            {
                DestroyEnemy();
            }
        }
    }

    private void DestroyEnemy() //Destruye al enemigo (No necesariamente por daño del jugador, por eso tengo esto aparte para futuros usos :v)
    {
        Destroy(gameObject);
    }


private void OnDrawGizmosSelected() //Gizmos para facilitarme la vida al modificar los rangos de ataque y vista :v
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRange);
    }
}
