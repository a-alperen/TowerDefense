using UnityEngine;

public class EkrandaTut : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -EkranHesaplayicisi.instance.Genislik)
        {
            Vector2 temp = transform.position;
            temp.x = -EkranHesaplayicisi.instance.Genislik;
            transform.position = temp;
           
        }
        if (transform.position.x > EkranHesaplayicisi.instance.Genislik)
        {
            Vector2 temp = transform.position;
            temp.x = EkranHesaplayicisi.instance.Genislik;
            transform.position = temp;

        }
        if(transform.position.y < -EkranHesaplayicisi.instance.Yukseklik)
        {
            Vector2 temp = transform.position;
            temp.y = -EkranHesaplayicisi.instance.Yukseklik;
            transform.position = temp;
        }
        if (transform.position.y > EkranHesaplayicisi.instance.Yukseklik)
        {
            Vector2 temp = transform.position;
            temp.y = EkranHesaplayicisi.instance.Yukseklik;
            transform.position = temp;
        }
    }
}
