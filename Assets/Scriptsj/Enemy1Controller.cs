using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    Explosive,
    Boss
};

public class Enemy1Controller : MonoBehaviour
{
    public GameObject player;

    protected GameObject[] comparePlayers;
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
    [SerializeField] private Enemy1State currentState = Enemy1State.Wander;

    [SerializeField] protected Enemy1Type enemy1Type;

    [SerializeField] private float range;

    [SerializeField] private float speed;

    //private bool chooseDir = false;

    protected bool died = false;

    private Vector3 randomDir;

    // Dano
    public float health;

    public float maxHealth;

    // Dano no player

    [SerializeField] private float attackRange;

    [SerializeField] private int damagePlayerMeele;



    [SerializeField] private int coolDownEnemy;

    [SerializeField] private int coolDownEnemyRanged;

    public bool notInRoom = true;



    private bool coolDownAttackEnemy;

    public GameObject projectilePrefab;
    [SerializeField] private RuntimeAnimatorController robot;
    [SerializeField] private RuntimeAnimatorController alien;
    [SerializeField] private RuntimeAnimatorController slime;
    private Animator animator;
    protected float time = 0f;
    private Vector2 distancia;
    // Start is called before the first frame update
    protected void Start()
    {
        if (GameControl.multiplayer == true)
        {
            comparePlayers = GameObject.FindGameObjectsWithTag("Player");
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
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
        if (!notInRoom)
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
                if (GameControl.multiplayer == true)
                {
                    if (IsPlayerAlive())
                    {
                        GetNearestPlayer();
                    }
                }
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
                if (IsPlayerInRange(range) && currentState != Enemy1State.Die)
                {
                    if (currentState == Enemy1State.Wander || currentState == Enemy1State.Idle)
                    {
                        AudioManager.instance.PlaySound("RDeteccao");
                    }
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
        else
        {
            currentState = Enemy1State.Idle;
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
        animator.SetBool("Seguindo", false);
        if (enemy1Type == Enemy1Type.Ranged)
        {
            animator.SetBool("Atirando", false);
        }
        if (enemy1Type == Enemy1Type.Explosive)
        {
            animator.SetBool("Explodindo", false);
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
            if (enemy1Type == Enemy1Type.Boss)
            {
                animator.SetBool("Morte", true);
                animator.SetBool("Parar", false);
                animator.SetBool("Arranhando", false);
                animator.SetBool("BolaLa", false);
                animator.SetBool("BolaPelo", false);
                FindObjectOfType<AudioManager>().PlaySound("RMorte");
            }
            if (enemy1Type == Enemy1Type.Meele)
            {
                animator.SetBool("Morreu", true);
                animator.SetBool("Parou", false);
                animator.SetBool("Seguindo", false);
                FindObjectOfType<AudioManager>().PlaySound("RMorte");
                UIScore.instance.AddPointRobot();

            }
            if (enemy1Type == Enemy1Type.Ranged)
            {
                animator.SetBool("Morreu", true);
                animator.SetBool("Parou", false);
                animator.SetBool("Seguindo", false);
                animator.SetBool("Atirando", false);
                FindObjectOfType<AudioManager>().PlaySound("AMorte");
                UIScore.instance.AddPointAlien();

            }
            if (enemy1Type == Enemy1Type.Explosive)
            {
                animator.SetBool("Morreu", true);
                animator.SetBool("Parou", false);
                animator.SetBool("Seguindo", true);
                animator.SetBool("Explodindo", false);
                FindObjectOfType<AudioManager>().PlaySound("SMorte");
                UIScore.instance.AddPointSlime();
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
            RoomController.instance.StartCoroutine(RoomController.instance.RoomCourotine());
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
                    player.GetComponentInParent<PlayerLife>().PlayerDamage();
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
        if (enemy1Type == Enemy1Type.Ranged)
        {
            FindObjectOfType<AudioManager>().PlaySound("RAtingido");
        }
        if (enemy1Type == Enemy1Type.Explosive)
        {
            FindObjectOfType<AudioManager>().PlaySound("SAtingido");
        }
        if (enemy1Type == Enemy1Type.Meele)
        {
            FindObjectOfType<AudioManager>().PlaySound("RAtingido");
        }
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
        animator.SetBool("Morreu", false);
        animator.SetBool("Parou", false);
        animator.SetBool("Seguindo", false);
        animator.SetBool("Atirando", true);
        AxisAnimation();
    }
    private void ExplosionAnimation()
    {
        animator.SetBool("Morreu", false);
        animator.SetBool("Parou", false);
        animator.SetBool("Seguindo", false);
        animator.SetBool("Explodindo", true);
        AxisAnimation();
    }
    private void FollowAnimation()
    {
        if (enemy1Type == Enemy1Type.Ranged)
        {
            animator.SetBool("Atirando", false);
        }
        if (enemy1Type == Enemy1Type.Explosive)
        {
            animator.SetBool("Explodindo", false);
        }
        animator.SetBool("Morreu", false);
        animator.SetBool("Parou", false);
        animator.SetBool("Seguindo", true);
        AxisAnimation();
    }
    public void GetNearestPlayer()
    {
        if (Vector3.Distance(transform.position, comparePlayers[0].transform.position) <= Vector3.Distance(transform.position, comparePlayers[1].transform.position))
        {
            player = comparePlayers[0];
        }
        else
        {
            player = comparePlayers[1];
        }
    }
    public bool IsPlayerAlive()
    {
        if (comparePlayers[0] == null)
        {
            player = comparePlayers[1];
            return false;
        }
        if (comparePlayers[1] == null)
        {
            player = comparePlayers[0];
            return false;
        }
        return true;
    }
    public void AxisAnimation()
    {
        distancia = player.transform.position - transform.position;
        if (Mathf.Abs(distancia.x) < Mathf.Abs(distancia.y))
        {
            if (distancia.y > 0)
            {
                animator.SetFloat("AnimMoveX", 0f);
                animator.SetFloat("AnimMoveY", 1f);
            }
            if (distancia.y < 0)
            {
                animator.SetFloat("AnimMoveX", 0f);
                animator.SetFloat("AnimMoveY", -1f);
            }
        }
        else
        {
            if (distancia.x > 0)
            {
                animator.SetFloat("AnimMoveX", 1f);
                animator.SetFloat("AnimMoveY", 0f);
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            if (distancia.x < 0)
            {
                animator.SetFloat("AnimMoveX", -1f);
                animator.SetFloat("AnimMoveY", 0f);
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
        }
    }
}
