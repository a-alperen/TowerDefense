using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpeedUp()
    {   
        if(GameManager.Instance.gameMoney >= 50)
        {
            GameManager.Instance.gameMoney -= 50;
            StartCoroutine(IncreaseSpeed());
        }
        
    }
    private IEnumerator IncreaseSpeed()
    {

        if(Students.students.Count != 0)
        {
            List<GameObject> validStudents = new();

            foreach (var item in Students.students)
            {
                if (item != null && item.activeInHierarchy)
                {
                    validStudents.Add(item);
                    item.GetComponent<Student>().speed *= 1.5f;
                }
            }

            yield return new WaitForSeconds(3f);

            if (Students.students.Count != 0)
            {
                foreach (var item in validStudents)
                {
                    if (item != null && item.activeInHierarchy)
                    {
                        item.GetComponent<Student>().speed /= 1.5f;
                    }
                }
            }
        }
        else
        {
            Debug.Log("Liste boş.");
        }
        
    }

    public void ProtectFromDamage()
    {
        if(GameManager.Instance.gameMoney >= 100)
        {
            GameManager.Instance.gameMoney -= 100;
            StartCoroutine(Protect());
        }
        
    }
    private IEnumerator Protect()
    {
        if (Students.students.Count != 0)
        {
            List<GameObject> validStudents = new();

            foreach (var item in Students.students)
            {
                if (item != null && item.activeInHierarchy)
                {
                    validStudents.Add(item);
                    item.GetComponent<Student>().isInvincible = true;
                }
            }

            yield return new WaitForSeconds(3f);

            if (Students.students.Count != 0)
            {
                foreach (var item in validStudents)
                {
                    if (item != null && item.activeInHierarchy)
                    {
                        item.GetComponent<Student>().isInvincible = false;
                    }
                }
            }
        }
        else
        {
            Debug.Log("Liste boş.");
        }
    }

    public void DestroyTower()
    {
        if(GameManager.Instance.gameMoney >= 100)
        {
            GameManager.Instance.gameMoney -= 100;
            StartCoroutine(DestroyTowerr());
        }
        
    }
    private IEnumerator DestroyTowerr()
    {
        if (Towers.towers.Count != 0)
        {
            GameObject go = Towers.towers[^1];
            Towers.towers.Remove(go);
            Destroy(go);
            
            yield return null;
            
        }
        else
        {
            Debug.Log("Liste boş.");
        }
    }
}
