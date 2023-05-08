using UnityEngine;
using UnityEngine.UI;

public class Student : MonoBehaviour
{
    [SerializeField] private float startHealth;
    [SerializeField] private float health;

    [SerializeField] private float speed;

    private int killRewards;
    private int damage;

    private GameObject targetTile;
    private GameManager gameManager;
    private UISystem uiManager;

    public Image healthBar;

    private void Awake()
    {
        Students.students.Add(gameObject);
    }
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiManager = GameObject.Find("GameManager").GetComponent<UISystem>();
        InitializeStudent();
    }

    private void InitializeStudent()
    {
        health = startHealth;
        targetTile = MapGenerator.pathTiles[0];
        
    }
    private void MoveStudent()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTile.transform.position, speed * Time.deltaTime);
       
    }

    private void CheckPosition()
    {
        if (GameManager.isPlaying)
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
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                }

            }
            if ((transform.position - MapGenerator.endTile.transform.position).magnitude < 0.001f)
            {
                Die();
                uiManager.ShowGameOverPanel();
                gameManager.DecreaseEnemyHealth();
                //Tower.DestroyAllTower();
            }
        }
        
    }
    private void Update()
    {
        //TakeDamage(0);
        CheckPosition();
        MoveStudent();

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
