using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemy1State
{
    Idle,
    Follow,
    Wander,
    Die,
    Attack
};

public enum Enemy1Type
{
    Meele,
    Ranged,
    Explosive
};

public class Enemy1Controller : MonoBehaviour
{
    GameObject player;

    /*
     Para 2 players:
     GameObject[] player;

    Colocar na bala:
    void OnTriggerEnter2S(Collider2D col){
    if(col.tag == "Enemy1"){
    col.gameObject.GetComponent<EnemyController>().Death();
    Destroy(gameOject);
    }
    }


    */
    public Enemy1State currentState = Enemy1State.Wander;

    public Enemy1Type enemy1Type;

    public float range;

    public float speed;

    //private bool chooseDir = false;

    private bool died = false;

    private Vector3 randomDir;

    // Dano
    public float health;

    public float maxHealth;

    // Dano no player

    public float attackRange;

    public int damagePlayerMeele;



    public int coolDownEnemy;

    public int coolDownEnemyRanged;

    public bool notInRoom = true;



    private bool coolDownAttackEnemy;

    public GameObject projectilePrefab;
    [SerializeField] private RuntimeAnimatorController robot;
    [SerializeField] private RuntimeAnimatorController alien;
    [SerializeField] private RuntimeAnimatorController slime;
    private Animator animator;
    private float time = 0f;
    private Vector2 distancia;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //FindGameObjectsWithTag
        health = maxHealth;
        animator = GetComponent<Animator>();
        if (enemy1Type == Enemy1Type.Meele)
        {
            animator.runtimeAnimatorController = robot;
        }
        if (enemy1Type == Enemy1Type.Ranged)
        {
            animator.runtimeAnimatorController = alien;
        }
        if (enemy1Type == Enemy1Type.Explosive)
        {
            animator.runtimeAnimatorController = slime;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (died == true)
        {
            time += Time.deltaTime;
            if (time > 0.8f)
            {
                Destroy(gameObject);
            }
        }

        else
        {
            switch (currentState)
            {
                case (Enemy1State.Wander):
                    time = 0f;
                    Wander();
                    break;
                case (Enemy1State.Follow):
                    time = 0f;
                    Follow();
                    break;
                case (Enemy1State.Die):
                    time = 0f;
                    Die();
                    break;
                case (Enemy1State.Attack):
                    Attack();
                    break;
            }
            if (!notInRoom)
            {
                if (IsPlayerInRange(range) && currentState != Enemy1State.Die)
                {
                    currentState = Enemy1State.Follow;
                }
                else if (!IsPlayerInRange(range) && currentState != Enemy1State.Die)
                {
                    currentState = Enemy1State.Wander;
                }
                if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
                {
                    currentState = Enemy1State.Attack;
                }
            }
            else
            {
                currentState = Enemy1State.Idle;
            }
        }
    }
    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }
    /*
    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }*/
    void Wander()
    {
        /*if(!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }
        transform.position += -transform.right * speed * Time.deltaTime;*/
        animator.SetBool("Morreu", false);
        animator.SetBool("Parou", true);
        animator.SetBool("SeguindoCima", false);
        animator.SetBool("SeguindoAbaixo", false);
        animator.SetBool("SeguindoLados", false);
        if (enemy1Type == Enemy1Type.Ranged)
        {
            animator.SetBool("AtirandoAbaixo", false);
            animator.SetBool("AtirandoCima", false);
            animator.SetBool("AtirandoLados", false);
        }
        if (enemy1Type == Enemy1Type.Explosive)
        {
            animator.SetBool("ExplodindoAbaixo", false);
            animator.SetBool("ExplodindoCima", false);
            animator.SetBool("ExplodindoLados", false);
        }
        if (IsPlayerInRange(range))
        {
            currentState = Enemy1State.Follow;
        }
    }
    void Idle() { }
    void Follow()
    {
        FollowAnimation();
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    public void Die()
    {
       
        if (health <= 0)
        {
            animator.SetBool("Morreu", true);
            animator.SetBool("Parou", false);
            animator.SetBool("SeguindoCima", false);
            animator.SetBool("SeguindoAbaixo", false);
            animator.SetBool("SeguindoLados", false);

            if (enemy1Type == Enemy1Type.Ranged)
            {
                animator.SetBool("AtirandoAbaixo", false);
                animator.SetBool("AtirandoCima", false);
                animator.SetBool("AtirandoLados", false);
                
            }
            if (enemy1Type == Enemy1Type.Explosive)
            {
                animator.SetBool("ExplodindoAbaixo", false);
                animator.SetBool("ExplodindoCima", false);
                animator.SetBool("ExplodindoLados", false);
                
            }
           
            died = true;
            RoomController.instance.StartCoroutine(RoomController.instance.RoomCourotine());
            
        }
    }
    public void Explode()
    {
        ExplosionAnimation();
        time += Time.deltaTime;
        if (time > 0.5f)
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public void Attack()
    {
        if (!coolDownAttackEnemy)
        {
            switch (enemy1Type)
            {
                case (Enemy1Type.Meele):
                    player.GetComponent<PlayerLife>().PlayerDamage();
                    StartCoroutine(CoolDownAttack());
                    break;
                case (Enemy1Type.Ranged):
                    GameObject bullet = projectilePrefab;
                    ShootingAnimation();
                    bullet.GetComponent<ProjectileDamage>().isEnemyBullet = true;
                    bullet.GetComponent<ProjectileDamage>().GetPlayer(player.transform);
                    Instantiate(bullet, transform.position, Quaternion.identity);
                    //Debug.Log(bullet.GetComponent<ProjectileDamage>().playerPos);
                    //bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    StartCoroutine(CoolDownAttackRanged());
                    break;
                case (Enemy1Type.Explosive):
                    ExplosionAnimation();
                    Explode();
                    break;
            }
        }
    }

    private IEnumerator CoolDownAttack()
    {
        coolDownAttackEnemy = true;
        yield return new WaitForSeconds(coolDownEnemy);
        coolDownAttackEnemy = false;
    }
    private IEnumerator CoolDownAttackRanged()
    {
        coolDownAttackEnemy = true;
        yield return new WaitForSeconds(coolDownEnemyRanged);
        coolDownAttackEnemy = false;
    }

    public void DealDamage(float damageEnemy)
    {
        health -= damageEnemy;
        Die();
    }

    public void CheckOverheal()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    private void ShootingAnimation()
    {
        distancia = player.transform.position - transform.position;
        if (Mathf.Abs(distancia.x) < Mathf.Abs(distancia.y))
        {
            if (distancia.y > 0)
            {
                animator.SetBool("Morreu", false);
                animator.SetBool("Parou", false);
                animator.SetBool("SeguindoCima", false);
                animator.SetBool("SeguindoAbaixo", false);
                animator.SetBool("SeguindoLados", false);
                animator.SetBool("AtirandoAbaixo", false);
                animator.SetBool("AtirandoCima", true);
                animator.SetBool("AtirandoLados", false);
            }
            if (distancia.y < 0)
            {
                animator.SetBool("Morreu", false);
                animator.SetBool("Parou", false);
                animator.SetBool("SeguindoCima", false);
                animator.SetBool("SeguindoAbaixo", false);
                animator.SetBool("SeguindoLados", false);
                animator.SetBool("AtirandoAbaixo", true);
                animator.SetBool("AtirandoCima", false);
                animator.SetBool("AtirandoLados", false);
            }
        }
        else
        {
            animator.SetBool("Morreu", false);
            animator.SetBool("Parou", false);
            animator.SetBool("SeguindoCima", false);
            animator.SetBool("SeguindoAbaixo", false);
            animator.SetBool("SeguindoLados", false);
            animator.SetBool("AtirandoAbaixo", false);
            animator.SetBool("AtirandoCima", false);
            animator.SetBool("AtirandoLados", true);
            if (distancia.x > 0)
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            if (distancia.x < 0)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
        }

    }
    private void ExplosionAnimation()
    {
        distancia = player.transform.position - transform.position;
        if (Mathf.Abs(distancia.x) < Mathf.Abs(distancia.y))
        {
            if (distancia.y > 0)
            {
                animator.SetBool("Morreu", false);
                animator.SetBool("Parou", false);
                animator.SetBool("SeguindoCima", false);
                animator.SetBool("SeguindoAbaixo", false);
                animator.SetBool("SeguindoLados", false);
                animator.SetBool("ExplodindoAbaixo", false);
                animator.SetBool("ExplodindoCima", true);
                animator.SetBool("ExplodindoLados", false);
            }
            if (distancia.y < 0)
            {
                animator.SetBool("Morreu", false);
                animator.SetBool("Parou", false);
                animator.SetBool("SeguindoCima", false);
                animator.SetBool("SeguindoAbaixo", false);
                animator.SetBool("SeguindoLados", false);
                animator.SetBool("ExplodindoAbaixo", true);
                animator.SetBool("ExplodindoCima", false);
                animator.SetBool("ExplodindoLados", false);
            }
        }
        else
        {
            animator.SetBool("Morreu", false);
            animator.SetBool("Parou", false);
            animator.SetBool("SeguindoCima", false);
            animator.SetBool("SeguindoAbaixo", false);
            animator.SetBool("SeguindoLados", false);
            animator.SetBool("ExplodindoAbaixo", false);
            animator.SetBool("ExplodindoCima", false);
            animator.SetBool("ExplodindoLados", true);
            if (distancia.x > 0)
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            if (distancia.x < 0)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
        }
    }
    private void FollowAnimation()
    {
        distancia = player.transform.position - transform.position;
        if (Mathf.Abs(distancia.x) < Mathf.Abs(distancia.y))
        {
            if (distancia.y > 0)
            {
                animator.SetBool("Morreu", false);
                animator.SetBool("Parou", false);
                animator.SetBool("SeguindoCima", true);
                animator.SetBool("SeguindoAbaixo", false);
                animator.SetBool("SeguindoLados", false);
                if (enemy1Type == Enemy1Type.Ranged)
                {
                    animator.SetBool("AtirandoAbaixo", false);
                    animator.SetBool("AtirandoCima", false);
                    animator.SetBool("AtirandoLados", false);
                }
                if (enemy1Type == Enemy1Type.Explosive)
                {
                    animator.SetBool("ExplodindoAbaixo", false);
                    animator.SetBool("ExplodindoCima", false);
                    animator.SetBool("ExplodindoLados", false);
                }
            }
            if (distancia.y < 0)
            {
                animator.SetBool("Morreu", false);
                animator.SetBool("Parou", false);
                animator.SetBool("SeguindoCima", false);
                animator.SetBool("SeguindoAbaixo", true);
                animator.SetBool("SeguindoLados", false);
                if (enemy1Type == Enemy1Type.Ranged)
                {
                    animator.SetBool("AtirandoAbaixo", false);
                    animator.SetBool("AtirandoCima", false);
                    animator.SetBool("AtirandoLados", false);
                }
                if (enemy1Type == Enemy1Type.Explosive)
                {
                    animator.SetBool("ExplodindoAbaixo", false);
                    animator.SetBool("ExplodindoCima", false);
                    animator.SetBool("ExplodindoLados", false);
                }
            }
        }
        else
        {
            animator.SetBool("Morreu", false);
            animator.SetBool("Parou", false);
            animator.SetBool("SeguindoCima", false);
            animator.SetBool("SeguindoAbaixo", false);
            animator.SetBool("SeguindoLados", true);
            if (enemy1Type == Enemy1Type.Ranged)
            {
                animator.SetBool("AtirandoAbaixo", false);
                animator.SetBool("AtirandoCima", false);
                animator.SetBool("AtirandoLados", false);
            }
            if (enemy1Type == Enemy1Type.Explosive)
            {
                animator.SetBool("ExplodindoAbaixo", false);
                animator.SetBool("ExplodindoCima", false);
                animator.SetBool("ExplodindoLados", false);
            }
            if (distancia.x > 0)
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            if (distancia.x < 0)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
        }
    }
}
