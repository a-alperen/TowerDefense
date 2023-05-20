using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tower : MonoBehaviour
{
    private Transform currentTarget; //Kulenin menzilindeki mevcut durumdaki en yakin hedef ogrenci.

    [Header("Attributes")]
    [SerializeField] private float range; // Kulenin atis yapabilecegi max menzili.
    [SerializeField] private float fireRate = 1f;
    private float fireCountDown = 0f;

    [Header("Unity Setup Fields")]
    public string studentTag = "Student";
    public Transform partToRotate;
    public float turnSpeed = 10f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }
    private void Update()
    {
        if (currentTarget == null) return;
        RotateTower();
        if(fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }

    
    void UpdateTarget()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject nearestStudent = null;
        float distanceToEnemy = Mathf.Infinity;
        if (Students.students != null)
        {
            foreach (GameObject student in Students.students)
            {
                if(student != null)
                    distanceToEnemy = Vector3.Distance(transform.position, student.transform.position);

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestStudent = student;
                }
            }
        }
        
        if (nearestStudent != null && shortestDistance <= range)
        {
            currentTarget = nearestStudent.transform;
        }
        else
        {
            currentTarget = null;
        }
    }

    void Shoot()// Kule ates eder.
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        TowerBullet bullet = bulletGO.GetComponent<TowerBullet>();
        if (bullet != null)
        {
            bullet.Seek(currentTarget);
        }
    }
    private void RotateTower()
    {
        Vector3 dir = currentTarget.position - transform.position;
        Vector3 rotatedVectorDir = Quaternion.Euler(0, 0, 90) * dir;
        Quaternion lookRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorDir);
        Quaternion rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed);
        partToRotate.rotation = rotation;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
