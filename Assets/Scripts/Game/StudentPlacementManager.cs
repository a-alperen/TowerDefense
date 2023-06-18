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
        float cost = StudentCost(studentObject);
        float gameMoney = GameManager.Instance.gameMoney;

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
            GameManager.Instance.gameMoney = gameMoney;
            UISystem.Instance.UpdateUIText(gameMoney);
        }

        float StudentCost(GameObject gameObject)// Öğrenci maliyeti hesaplar ve değerini döndürür.
        {
            float cost = 0;
            if (gameObject.name == "NormalStudent")
            {
                cost = 25;
            }
            else if (gameObject.name == "FastStudent")
            {
                cost = 50;
            }
            else if (gameObject.name == "TankStudent")
            {
                cost = 100;
            }
            return cost;
        }
    }
}
