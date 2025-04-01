using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.InputSystem.HID;

public class BouncyController : UnitController
{
    [SerializeField] private EnemyUnit enemyUnit;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletOrigin;

    private PlayerPhysicsBus playerPhysicsBus;
    private Vector3 targetBouncePos;
    private bool isGrounded;

    private float extraGravityForce = 8f;
    private float jumpForce = 8f;

    //private float bulletMaxDuration = 5f; // seconds
    //private float bulletSpeed = 7.4f;
    private bool attackUp = true;

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
        enemyUnit.AttackReady = true;
        playerPhysicsBus = FindFirstObjectByType<PlayerPhysicsBus>();
    }

    protected override void Start()
    {
        base.Start();
    }
    private void FixedUpdate()
    {
        RaycastHit hit; 

        rb.AddForce(Vector3.down * extraGravityForce, ForceMode.Force);

        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 0.9f);
        if (isGrounded)
        {
            Bounce();
        }

        Vector3 direction = Game.I.PlayerTransform.position - transform.position;
        direction.y = 0;
        direction = Quaternion.Euler(0, 20f, 0) * direction; // adjust by 30 deg
        direction = Vector3.MoveTowards(Vector3.zero, direction, 1f).normalized;

        rb.MovePosition(rb.position + direction * enemyUnit.CurrentMoveSpeed * Time.fixedDeltaTime);
        //Debug.Log(enemyUnit.CurrentMoveSpeed);
        //Debug.Log(direction);
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, Game.I.PlayerTransform.position) <= enemyUnit.AttackRange)
        {
            if (attackUp) Attack();
        }
        //Debug.Log("attackrange" + enemyUnit.AttackRange);
        //Debug.Log("distancetoplayer" + Vector3.Distance(transform.position, Game.I.PlayerTransform.position));
        //Vector3 targetPos = Game.I.PlayerTransform.position;
        //targetPos.y = transform.position.y;
        //transform.LookAt(targetPos);

    }
    void Attack()
    {
        //Ray ray = new Ray(bulletOrigin.position, Game.I.PlayerTransform.position - bulletOrigin.position);
        //RaycastHit hit;

        //if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Player"))
        //{
        //    Vector3 dir = Game.I.PlayerTransform.position - transform.position;

        //    GameObject bullet = Instantiate(bulletPrefab, bulletOrigin.position, Quaternion.identity);
        //    bullet.GetComponent<CasterBullet>().Initialize(dir.normalized, bulletMaxDuration, enemyUnit.BaseDamage, bulletSpeed);

        //    attackUp = false;
        //    StartCoroutine(timer.TimerCR(enemyUnit.AttackCDLength, () => attackUp = true));
        //}

        Game.I.PlayerUnitInstance.TakeDamage(false, enemyUnit.BaseDamage);
        KnockbackPlayer();
        attackUp = false;
        StartCoroutine(timer.TimerCR(enemyUnit.AttackCDLength, () => attackUp = true));
    }


    private void KnockbackPlayer()
    {
        Vector3 xz = transform.position - Game.I.PlayerTransform.position;
        xz = xz.normalized * 22f;
        Vector3 y = Vector3.up * 3f;

        playerPhysicsBus.AddForceToPlayer(xz + y, ForceMode.Impulse);
    }
    private void Bounce()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // Reset vertical velocity
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }


}
