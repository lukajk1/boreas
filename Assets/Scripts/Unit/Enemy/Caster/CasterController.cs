using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CasterController : UnitController
{
    [SerializeField] private EnemyUnit enemyUnit;
    [SerializeField] private GameObject casterBullet;
    [SerializeField] private Transform bulletOrigin;

    private float movespeed;
    private Game game;
    private NavMeshAgent agent;
    private bool allowedToMove = false;
    private float rangeFromPlayer = 15f;

    private float bulletMaxDuration = 5f; // seconds
    private float attackCD = 1.5f;
    private float bulletSpeed = 8.4f;
    private bool canAttack = true;

    void OnEnable()
    {
        if (enemyUnit != null)
        {
            enemyUnit.OnUnitReady += Setup;
        }
    }

    void OnDisable()
    {
        if (enemyUnit != null)
        {
            enemyUnit.OnUnitReady -= Setup;
        }
    }

    private void Setup()
    {
        movespeed = enemyUnit.BaseMoveSpeed;
        game = FindAnyObjectByType<Game>();
        allowedToMove = true;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = movespeed;
        agent.destination = Game.I.PlayerTransform.position;
    }

    void Start()
    {
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, Game.I.PlayerTransform.position) > rangeFromPlayer)
        {
            agent.isStopped = false;
            if (allowedToMove) agent.destination = Game.I.PlayerTransform.position;
        }
        else
        {
            agent.isStopped = true;
            TryAttack();
        }


    }

    private void TryAttack()
    {
        if (!canAttack ) return;

        //Debug.Log("attempting to attack");
        Ray ray = new Ray(transform.position, Game.I.PlayerTransform.position - transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Player"))
        {
            Vector3 dir = Game.I.PlayerTransform.position - transform.position;

            GameObject bullet = Instantiate(casterBullet, bulletOrigin.position, Quaternion.identity);
            bullet.GetComponent<CasterBullet>().Initialize(dir.normalized, bulletMaxDuration, enemyUnit.BaseDamage, bulletSpeed);

            canAttack = false;
            StartCoroutine(AttackCD());
        }
    }

    private IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(attackCD);
        canAttack = true;
    }


}
