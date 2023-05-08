using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private QuestionManager questionManager;
    private void Start()
    {
        questionManager = GameObject.Find("QuestionManager").GetComponent<QuestionManager>(); 
        Destroy(gameObject,5f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (questionManager != null)
        {
            questionManager.AskQuestion();
        }
        Destroy(gameObject);
        
    }
    private void Update()
    {
        transform.position += transform.right * 0.1f;

    }
}
