using UnityEngine;

public class StudentBullet : MonoBehaviour
{
    private Transform target;
    public float speed = 10f;
    public float damage = 10f;
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
    void HitTarget()
    {
        if (target == null) return;
        Debug.Log("hitttt");
        Destroy(gameObject);
    }
}
