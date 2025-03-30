using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class DiverController : UnitController
{
    [SerializeField] private EnemyUnit enemyUnit;
    private NavMeshAgent agent;
    private bool isAttacking = false;
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
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyUnit.CurrentMoveSpeed;
        agent.destination = Game.I.PlayerTransform.position;
        enemyUnit.AttackReady = true;
    }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, Game.I.PlayerTransform.position) > enemyUnit.AttackRange && !isAttacking)
        {
            agent.isStopped = false;
            agent.destination = Game.I.PlayerTransform.position;
        }
        else
        {
            if (enemyUnit.AttackReady) TryAttack();
        }


    }

    private void TryAttack()
    {
        Ray ray = new Ray(transform.position, Game.I.PlayerTransform.position - transform.position); // only attack if LOS on player
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("attacking");
            StartCoroutine(Jump(10f, 1.4f));
            enemyUnit.AttackReady = false;
            StartCoroutine(timer.TimerCR(enemyUnit.AttackCDLength, () => enemyUnit.AttackReady = true));
        }


    }

    private IEnumerator Jump(float targetHeight, float duration)
    {
        isAttacking = true;
        agent.enabled = false;

        float startHeight = transform.position.y;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newY = Mathf.Lerp(startHeight, startHeight + targetHeight, elapsedTime / duration);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            yield return null;
        }

        transform.position = new Vector3(transform.position.x, startHeight + targetHeight, transform.position.z);

        //Ray ray = new Ray(transform.position, Game.I.PlayerTransform.position - transform.position); // only attack if LOS on player
        //RaycastHit hit;


        //if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Player"))
        //{
            float dashDuration = 7.5f;

            Vector3 dashTargetPos = Game.I.PlayerTransform.position;
            elapsedTime = 0f;
            while (Vector3.Distance(transform.position, dashTargetPos) > 0.6f)
            {
                elapsedTime += Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, dashTargetPos, elapsedTime / dashDuration);

                yield return null;
            }
        //}

        isAttacking = false;
        agent.enabled = true;
    }

}
