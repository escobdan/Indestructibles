using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{

    private Transform target;
    private EnemyMovement targetEnemy;

    [Header("General")]
    public float range = 15f;
    public static float dmgVar = 1f;
    public float turretVar;

    [Header("Math Stuff")]
    public Text dmgVarText;
    public static bool correctAnswer;
    public static bool turretPlaced;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;

    public int damageOverTime = 30;
    //for math implementation, option to make slow stack and only kill enemy once the slow reaches a certain number
    public float slowAmount = .5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        //dmgVarText.text = turretVar.ToString();
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            //optimization option
            targetEnemy = nearestEnemy.GetComponent<EnemyMovement>();
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }

            return;
        }

        if (turretVar == WaveSpawner.answerVar)
        {
            LockOnTarget();

            if (useLaser)
            {
                Laser();
            }
            else
            {
                if (fireCountdown <= 0f)
                {
                    Shoot();
                    fireCountdown = 1f / fireRate;
                }

                fireCountdown -= Time.deltaTime;
            }
        }
    }

    public void DoOperation(float amount, Node.op operation)
    {
        switch (operation)
        {
            case Node.op.None:
            break;
            case Node.op.Add:
            dmgVarText.text = turretVar + " + " + amount;
            turretVar += amount;
            //dmgVarText.text = turretVar.ToString();
            if (turretVar == WaveSpawner.answerVar)
            {
                correctAnswer = true;
                turretPlaced = true;
            }
            break;
            case Node.op.Subtract:
            dmgVarText.text = turretVar + " - " + amount;
            turretVar -= amount;
            //dmgVarText.text = turretVar.ToString();
            if (turretVar == WaveSpawner.answerVar)
            {
                correctAnswer = true;
                turretPlaced = true;
            }
            break;
            case Node.op.Multiply:
            dmgVarText.text = turretVar + " x " + amount;
            turretVar *= amount;
            //dmgVarText.text = turretVar.ToString();
            if (turretVar == WaveSpawner.answerVar)
            {
                correctAnswer = true;
                turretPlaced = true;
            }
            break;
            case Node.op.Divide:
            dmgVarText.text = turretVar + " ÷ " + amount;
            turretVar /= amount;
            //dmgVarText.text = turretVar.ToString();
            if (turretVar == WaveSpawner.answerVar)
            {
                correctAnswer = true;
                turretPlaced = true;
            }
            break;
            default:
            break;
        }
    }

    void LockOnTarget()
    {
        //rotate head to enemy LOCK ON
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        targetEnemy.GetComponent<EnemyMovement>().TakeDamage(damageOverTime * Time.deltaTime, turretVar);
        targetEnemy.Slow(slowAmount, turretVar);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        //get direction from a to b, go to b-a
        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target, turretVar);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
