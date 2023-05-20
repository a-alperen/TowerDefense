using UnityEngine;
using UnityEngine.UI;

public class Student : MonoBehaviour
{
    private Transform currentTarget;

    [Header("Attributes")]
    [SerializeField] private float startHealth;
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float fireRate = 1f;
    private float fireCountDown = 0f;

    [Header("Unity Setup Fields")]
    public Vector3 healthBarOffset;
    public GameObject healthBarGo;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Image healthBar;
    public Transform partToRotate;
    public float turnSpeed = 10f;

    //private int killRewards;
    //private int damage;

    private GameObject targetTile;
    public bool isAttacking;

    private void Awake()
    {
        Students.students.Add(gameObject);
        isAttacking = false;
    }
    private void Start()
    {
        InitializeStudent();
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }
    private void Update()
    {
        if (isAttacking && currentTarget != null)
        {
            RotateStudent();
            if (fireCountDown <= 0f)
            {
                Shoot();
                fireCountDown = 1f / fireRate;
            }
        }
        else
        {
            CheckPosition();
            MoveStudent();
        }
        
        healthBarGo.transform.position = transform.position + healthBarOffset;
        
        fireCountDown -= Time.deltaTime;
    }
    private void InitializeStudent()
    {
        health = startHealth;
        targetTile = MapGenerator.pathTiles[0];
        
    }
    void UpdateTarget()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject nearestTower = null;

        foreach (GameObject tower in Towers.towers)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, tower.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestTower = tower;
            }
        }

        if (nearestTower != null && shortestDistance <= range)
        {
            currentTarget = nearestTower.transform;
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
            currentTarget = null;
        }
    }
    private void RotateStudent()
    {
        if(currentTarget != null)
        {
            Vector3 dir = currentTarget.position - transform.position;
            Vector3 rotatedVectorDir = Quaternion.Euler(0, 0, 90) * dir;
            Quaternion lookRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorDir);
            Quaternion rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed);
            partToRotate.rotation = rotation;
        }
        
    }
    private void MoveStudent()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTile.transform.position, speed * Time.deltaTime);
       
    }

    private void CheckPosition()
    {
        if (GameManager.Instance.isPlaying)
        {
            if (targetTile != null && targetTile != MapGenerator.endTile)
            {
                float distance = (transform.position - targetTile.transform.position).magnitude;

                if (distance < 0.001f)
                {
                    int currentIndex = MapGenerator.pathTiles.IndexOf(targetTile);

                    targetTile = MapGenerator.pathTiles[currentIndex + 1];
                    Vector3 dir = MapGenerator.pathTiles[currentIndex + 1].transform.position - transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    partToRotate.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                }

            }
            if ((transform.position - MapGenerator.endTile.transform.position).magnitude < 0.001f)
            {
                Die();
                UISystem.Instance.ShowGameOverPanel();
            }
        }
        
    }
    
    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        StudentBullet bullet = bulletGO.GetComponent<StudentBullet>();
        if (bullet != null)
        {
            bullet.Seek(currentTarget);
        }
    }
    /// <summary>
    /// Ogrencinin canini azaltir.
    /// </summary>
    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if(health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Students.students.Remove(gameObject);
        Destroy(transform.gameObject);
    }
    
}
