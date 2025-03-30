using UnityEngine;
using UnityEngine.AI;

public class ZomboController : UnitController
{
    [SerializeField] private Unit unitTarget;
    private float movespeed;
    private Game game;
    private NavMeshAgent agent;
    private bool allowedToMove = false;
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
    public Transform goal;

    private void Setup()
    {
        movespeed = unitTarget.BaseMoveSpeed;
        game = FindAnyObjectByType<Game>();
        allowedToMove = true;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = movespeed;
        agent.destination = goal.position;
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

    }

}
