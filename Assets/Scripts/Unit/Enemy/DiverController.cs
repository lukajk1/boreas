using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class DiverController : UnitController
{
    [SerializeField] private EnemyUnit enemyUnit;
    private NavMeshAgent agent;
    private bool isAttacking = false;
    private PlayerPhysicsBus playerPhysicsBus;
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
        playerPhysicsBus = FindFirstObjectByType<PlayerPhysicsBus>();
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
            if (enemyUnit.AttackReady && playerPhysicsBus.IsGrounded) TryAttack();
        }

        //Debug.Log("attackready" + enemyUnit.AttackReady);
        //Debug.Log("isgrounded" +playerPhysicsBus.IsGrounded);
    }

    private void TryAttack()
    {
        Ray ray = new Ray(transform.position, Game.I.PlayerTransform.position - transform.position); // only attack if LOS on player
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Player"))
        {
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

        float dashDuration = 7.5f;
        float diveAttackDamageRadius = 3.2f;
        bool hasDamaged = false;

        Vector3 dashTargetPos = Game.I.PlayerTransform.position;
        elapsedTime = 0f;
        while (Vector3.Distance(transform.position, dashTargetPos) >= 0.6f)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, dashTargetPos, elapsedTime / dashDuration);

            if (!hasDamaged)
            {
                if (Vector3.Distance(transform.position, Game.I.PlayerTransform.position) <= diveAttackDamageRadius)
                {
                    Game.I.PlayerUnitInstance.TakeDamage(false, enemyUnit.BaseDamage);
                    hasDamaged = true;

                    Vector3 xz = transform.position - Game.I.PlayerTransform.position;
                    xz = xz.normalized * 27f;
                    Vector3 y = Vector3.up * 3f;

                    playerPhysicsBus.AddForceToPlayer(xz + y, ForceMode.Impulse);
                }
            }

            yield return null;
        }

        isAttacking = false;
        agent.enabled = true;
    }

}
