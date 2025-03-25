using UnityEngine;

public class ZomboController : UnitController
{
    [SerializeField] private Unit unitTarget;
    private float movespeed;
    private Game game;
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

    private void Setup()
    {
        movespeed = unitTarget.BaseMoveSpeed;
        game = FindAnyObjectByType<Game>();
        allowedToMove = true;
    }

    void Update()
    {
        if (allowedToMove)
        {
            Vector3 targetPos = game.PlayerTransform.position;
            targetPos.y = transform.position.y;

            transform.position = Vector3.MoveTowards(transform.position, targetPos, movespeed * Time.deltaTime);
        }

    }

}
