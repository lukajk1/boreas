using UnityEngine;
using UnityEngine.AI;

public class ZomboController : UnitController
{
    [SerializeField] private EnemyUnit enemyUnit;
    private float movespeed;
    private Game game;
    private NavMeshAgent agent;
    private Timer timer;

    private bool allowedToMove = false;
    private float attackCD = 2f;
    private bool attackUp = true;
    private float attackRange = 1.5f;
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

        timer = gameObject.AddComponent<Timer>();
    }

    void Start()
    {
    }

    void Update()
    {
        if (allowedToMove)
        {
            agent.destination = Game.I.PlayerTransform.position;
        }

        if (attackUp && Vector3.Distance(Game.I.PlayerTransform.position, transform.position) < attackRange)
        {
            attackUp = false;
            Game.I.PlayerUnitInstance.TakeDamage(false, enemyUnit.BaseDamage);
            StartCoroutine(timer.TimerCR(attackCD, () => attackUp = true));
        }

    }

}
