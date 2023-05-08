using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentPlacementManager : MonoBehaviour
{
    public GameObject mapGenerator; // Spawn noktasi referansi.
    private GameManager gameManager;
    private UISystem uiManager;

    private void Start()
    {
        uiManager = GameObject.Find("GameManager").GetComponent<UISystem>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void SpawnStudent(GameObject studentObject)
    {
        float cost = gameManager.StudentCost(studentObject);
        float gameMoney = gameManager.GetGameMoney();
        Debug.Log(cost);
        if (gameMoney >= cost)
        {
            GameObject student = Instantiate(studentObject, mapGenerator.GetComponent<MapGenerator>().spawnPoint.transform.position, Quaternion.identity);
            Students.students.Add(student);
            gameMoney -= cost;
            gameManager.SetGameMoney(gameMoney);
            uiManager.UpdateUIText(gameMoney);
        }
        
    }
}
