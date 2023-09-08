using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemy1State
{
    Follow,
    Wander,
    Die
};
public class Enemy1Controller : MonoBehaviour
{
    GameObject player;
    /*
     Para 2 players:
     GameObeject[] player;

    Colocar na bala:
    void OnTriggerEnter2S(Collider2D col){
    if(col.tag == "Enemy1"){
    col.gameObject.GetComponent<EnemyController>().Death();
    Destroy(gameOject);
    }
    }


    */
    public Enemy1State currentState = Enemy1State.Wander;

    public float range;

    public float speed;

    private bool chooseDir = false;

    private bool died = false;

    private Vector3 randomDir;

    // Dano
    public float health;
    public float maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //FindGameObjectsWithTag
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case(Enemy1State.Wander):
                Wander();
            break;
            case(Enemy1State.Follow):
                Follow();
            break;
            case(Enemy1State.Die):
                Die();
            break;
        }
        if(IsPlayerInRange(range) && currentState != Enemy1State.Die)
        {
            currentState = Enemy1State.Follow;
        }else if(!IsPlayerInRange(range) && currentState!= Enemy1State.Die)
        {
            currentState = Enemy1State.Wander;
        }
    }
    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }
    void Wander()
    {
        if(!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * speed * Time.deltaTime;
        if (IsPlayerInRange(range))
        {
            currentState = Enemy1State.Follow;
        }
    }

    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    public void Die()
    {
       // Destroy(gameObject);
       if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DealDamage(float damage)
    {
        health -= damage;
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
