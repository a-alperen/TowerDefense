using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentPlacementManager : MonoBehaviour
{
    public GameObject mapGenerator; // Spawn noktasi referansi.

    private void Start()
    {

    }
    public void SpawnStudent(GameObject studentObject)
    {
        float cost = GameManager.Instance.StudentCost(studentObject);
        float gameMoney = GameManager.Instance.gameMoney;
        Debug.Log(gameMoney);
        Debug.Log(cost);
        if (gameMoney >= cost)
        {
            GameObject studentGO = Instantiate(studentObject, mapGenerator.GetComponent<MapGenerator>().spawnPoint.transform.position, Quaternion.identity);
            Student student = studentGO.GetComponent<Student>();
            student.bulletPrefab.GetComponent<StudentBullet>().damage = Mathf.Floor(GameManager.Instance.data.upgradePowers[student.id][0]);
            student.startHealth = Mathf.Floor( GameManager.Instance.data.upgradePowers[student.id][1]);
            student.speed = GameManager.Instance.data.upgradePowers[student.id][2];
            student.range = GameManager.Instance.data.upgradePowers[student.id][3];
            
            Students.students.Add(studentGO);
            gameMoney -= cost;
            Debug.Log(gameMoney);
            GameManager.Instance.gameMoney = gameMoney;
            UISystem.Instance.UpdateUIText(gameMoney);
        }
        
    }
}
