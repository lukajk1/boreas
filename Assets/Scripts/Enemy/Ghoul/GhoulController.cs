using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhoulController : UnitController
{
    [SerializeField] private EnemyUnit enemyUnit;
    [SerializeField] private GameObject casterBullet;
    [SerializeField] private Transform bulletOrigin;
    [SerializeField] private GameObject criticalHitbox;

    private Material dissolveMaterial;
    private Rigidbody rb;
    private MeshCollider mesh;

    private float dissolveTime = 2.2f;
    private float cutoffHeightMax = 3.0f;
    private float cutoffHeightMin = -2.0f;
    
    private float movespeed;

    //private NavMeshAgent agent;
    //private bool allowedToMove = false;

    private float bulletMaxDuration = 5f; // seconds
    private float bulletSpeed = 7.4f;
    private bool canAttack = true;

    void OnEnable()
    {
        if (enemyUnit != null)
        {
            enemyUnit.OnUnitReady += Setup;
            enemyUnit.OnUnitDeath += OnDeath;
        }
    }

    void OnDisable()
    {
        if (enemyUnit != null)
        {
            enemyUnit.OnUnitReady -= Setup;
            enemyUnit.OnUnitDeath -= OnDeath;
        }
    }


    protected override void Start()
    {
        base.Start();

        dissolveMaterial = GetComponent<MeshRenderer>().material;
        dissolveMaterial.SetFloat("_CutoffHeight", cutoffHeightMax);

        rb = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshCollider>();
        Physics.IgnoreCollision(mesh, Game.i.PlayerBodyCollider, true);
        Physics.IgnoreCollision(mesh, Game.i.PlayerHeadCollider, true);
    }
    private void OnDeath()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
        
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        rb.AddForce(direction * 4.0f, ForceMode.Impulse);
        StartCoroutine(Dissolve());

    }

    private void Setup()
    {
        movespeed = enemyUnit.BaseMoveSpeed;
        //allowedToMove = true;

        //agent = GetComponent<NavMeshAgent>();
        //agent.speed = movespeed;
        //agent.destination = Game.i.PlayerTransform.position;
    }

    void Update()
    {
        //if (!enemyUnit.IsDead) transform.LookAt(Game.i.PlayerTransform.position);


        //if (Vector3.Distance(transform.position, Game.i.PlayerTransform.position) > enemyUnit.AttackRange)
        //{
        //    agent.isStopped = false;
        //    if (allowedToMove) agent.destination = Game.i.PlayerTransform.position;
        //}
        //else
        //{
        //    agent.isStopped = true;
        //    TryAttack();
        //}

    }

    private void TryAttack()
    {
        if (!canAttack) return;

        //Debug.Log("attempting to attack");
        Ray ray = new Ray(transform.position, Game.i.PlayerTransform.position - transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Player"))
        {
            Vector3 dir = Game.i.PlayerTransform.position - transform.position;

            GameObject bullet = Instantiate(casterBullet, bulletOrigin.position, Quaternion.identity);
            bullet.GetComponent<CasterBullet>().Initialize(dir.normalized, bulletMaxDuration, enemyUnit.BaseDamage, bulletSpeed);

            SFXManager.i.PlaySFXClip(SFXManager.SoundType._3D, EnemySFXList.i.casterAttack, transform.position);

            canAttack = false;
            StartCoroutine(AttackCD());
        }
    }

    private IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(enemyUnit.AttackCDLength);
        canAttack = true;
    }

    private IEnumerator Dissolve()
    {
        float elapsed = 0f;
        while (elapsed < dissolveTime)
        { 
            elapsed += Time.deltaTime;
            dissolveMaterial.SetFloat("_CutoffHeight", Mathf.Lerp(cutoffHeightMax, cutoffHeightMin, elapsed / dissolveTime));
            if ((elapsed / dissolveTime) >= 0.5f) criticalHitbox.SetActive(false);
            yield return null;
        }
        Destroy(enemyUnit.gameObject);
    }
}
