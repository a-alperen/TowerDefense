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
        Debug.Log(cost);
        if (gameMoney >= cost)
        {
            GameObject student = Instantiate(studentObject, mapGenerator.GetComponent<MapGenerator>().spawnPoint.transform.position, Quaternion.identity);
            Students.students.Add(student);
            gameMoney -= cost;
            GameManager.Instance.gameMoney = gameMoney;
            UISystem.Instance.UpdateUIText(gameMoney);
        }
        
    }
}
