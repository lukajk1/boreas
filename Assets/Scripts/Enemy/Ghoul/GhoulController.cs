using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhoulController : UnitController
{
    [SerializeField] private EnemyUnit enemyUnit;
    [SerializeField] private GameObject criticalHitbox;
    [SerializeField] private Bobbing bobbing;

    private Material dissolveMaterial;
    private Rigidbody rb;
    private MeshCollider mesh;

    private float dissolveTime = 1.05f;
    private float cutoffHeightMax = 3.0f;
    private float cutoffHeightMin = -2.0f;

    private float maxXRotationForLookAt = 28f;
    
    private float movespeed;
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

        bobbing.StartBobbing();
    }

    private void Setup()
    {
        movespeed = enemyUnit.BaseMoveSpeed;
        if (canAttack) canAttack = false; // just to make unity shut up
    }
    private void OnDeath()
    {
        bobbing.Stop();

        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
        
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        rb.AddForce(direction * 4.0f, ForceMode.Impulse);
        rb.AddForce(Vector3.up * 2.2f, ForceMode.Impulse);
        rb.angularVelocity = Game.i.PlayerCamera.transform.forward * 25f;
        StartCoroutine(Dissolve());

    }

    void Update()
    {
        transform.LookAt(Game.i.PlayerTransform.position);

        Vector3 euler = transform.eulerAngles;
        euler.x = ClampAngle(euler.x, -maxXRotationForLookAt, maxXRotationForLookAt); // prevent x-axis rotation from getting too high
        transform.rotation = Quaternion.Euler(euler);

    }
    float ClampAngle(float angle, float min, float max)
    {
        angle = (angle > 180) ? angle - 360 : angle; // convert to [-180, 180]
        return Mathf.Clamp(angle, min, max);
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
