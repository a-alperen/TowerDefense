using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    private Transform target;
    public float speed = 10f;
    public float damage = 10f;
    public void Seek(Transform _target)
    {
        target = _target;
    }
    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        if (target == null) return;
        if(GameManager.Instance.hitCount % 6 == 0)
        {
            if (QuestionManager.Instance != null)
            {
                QuestionManager.Instance.AskQuestion();
            }
            GameManager.Instance.hitCount = 0;
        }
        GameManager.Instance.hitCount++;
        target.GetComponent<Student>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
//private void OnCollisionEnter2D(Collision2D collision)
//{
//    if (QuestionManager.Instance != null)
//    {
//        QuestionManager.Instance.AskQuestion();
//    }
//    Destroy(gameObject);

//}