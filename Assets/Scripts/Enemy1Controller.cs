using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemy1State
{
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

    

    private bool coolDownAttackEnemy;

    public GameObject projectilePrefab;
    [SerializeField] private RuntimeAnimatorController robot;
    private Animator animator;
    private float time = 0f;
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
                    Wander();
                    break;
                case (Enemy1State.Follow):
                    Follow();
                    break;
                case (Enemy1State.Die):
                    Die();
                    break;
                case (Enemy1State.Attack):
                    Attack();
                    break;
            }
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
        if (IsPlayerInRange(range))
        {
            currentState = Enemy1State.Follow;
        }
    }
    void Follow()
    {
        Vector2 distancia;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        distancia = player.transform.position - transform.position;
        if(Mathf.Abs(distancia.x) < Mathf.Abs(distancia.y))
        {
            if (distancia.y > 0)
            {
                animator.SetBool("Morreu", false);
                animator.SetBool("Parou", false);
                animator.SetBool("SeguindoCima", true);
                animator.SetBool("SeguindoAbaixo", false);
                animator.SetBool("SeguindoLados", false);
            }
            if(distancia.y < 0)
            {
                animator.SetBool("Morreu", false);
                animator.SetBool("Parou", false);
                animator.SetBool("SeguindoCima", false);
                animator.SetBool("SeguindoAbaixo", true);
                animator.SetBool("SeguindoLados", false);
            }
        }
        else
        {
            if (distancia.x > 0)
            {
                animator.SetBool("Morreu", false);
                animator.SetBool("Parou", false);
                animator.SetBool("SeguindoCima", false);
                animator.SetBool("SeguindoAbaixo", false);
                animator.SetBool("SeguindoLados", true);
                transform.eulerAngles = new Vector3(0, 180f, 0);
            }
            if (distancia.x < 0)
            {
                animator.SetBool("Morreu", false);
                animator.SetBool("Parou", false);
                animator.SetBool("SeguindoCima", false);
                animator.SetBool("SeguindoAbaixo", false);
                animator.SetBool("SeguindoLados", true);
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
        }
    }
    public void Die()
    {
       // Destroy(gameObject);
       if(health <= 0)
        {
            animator.SetBool("Morreu", true);
            animator.SetBool("Parou", false);
            animator.SetBool("SeguindoCima", false);
            animator.SetBool("SeguindoAbaixo", false);
            animator.SetBool("SeguindoLados", false);
            died = true;
        }
    }
    public void Explode()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    public void Destroy()
    {
        Destroy(gameObject);
        
    }
    public void Attack()
    {
        if (!coolDownAttackEnemy)
        {
            switch (enemy1Type)
            {
                case(Enemy1Type.Meele):
                    player.GetComponent<PlayerLife>().PlayerDamage();
                    StartCoroutine(CoolDownAttack());
                break;
                case(Enemy1Type.Ranged):
                    GameObject bullet = projectilePrefab;
                    bullet.GetComponent<ProjectileDamage>().isEnemyBullet = true;
                    bullet.GetComponent<ProjectileDamage>().GetPlayer(player.transform);
                    Instantiate(bullet, transform.position, Quaternion.identity);
                    //Debug.Log(bullet.GetComponent<ProjectileDamage>().playerPos);
                    //bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    StartCoroutine(CoolDownAttackRanged());
                break;
                case (Enemy1Type.Explosive):
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
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
