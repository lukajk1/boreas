using UnityEngine;
using UnityEngine.AI;

public class CasterController : UnitController
{
    [SerializeField] private Unit unitTarget;
    [SerializeField] private GameObject projectile;

    private float movespeed;
    private Game game;
    private NavMeshAgent agent;
    private bool allowedToMove = false;
    private float rangeFromPlayer = 15f;

    void OnEnable()
    {
        if (unitTarget != null)
        {
            unitTarget.OnUnitReady += Setup;
        }
    }

    void OnDisable()
    {
        if (unitTarget != null)
        {
            unitTarget.OnUnitReady -= Setup;
        }
    }

    private void Setup()
    {
        movespeed = unitTarget.BaseMoveSpeed;
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
        //else
        //{
        //    Ray ray = new Ray(transform.position, Game.I.PlayerTransform.position - transform.position);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name == "Player")
        //    {
        //        Debug.Log("Ray hit the Player!");
        //    }
        //}
        else
        {
            agent.isStopped = true;
            Attack();
        }

    }

    private void Attack()
    {
        //
    }

}
