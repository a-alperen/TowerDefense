using UnityEngine;

public class BarrelRotation : MonoBehaviour
{
    public Transform pivot; //Kulenin donus merkezi.
    public Transform barrel;//Kule namlusu.

    public Tower tower;

    private void Update()
    {
        if(tower != null) 
        {
            //if(tower.currentTarget != null)
            //{
            //    Vector2 relative = tower.currentTarget.transform.position - pivot.position; //Kulenin mevcut hedefi ile pivot nokta arasi mesafenin vektorunun hesabi.

            //    float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg; //'relative' vektorunun bilesenlerine gore aci hesabinin yapilmasi.

            //    Vector3 newRotation = new(0, 0, angle); //'angle' acisina gore yeni rotation vektor hesabi.

            //    pivot.localRotation = Quaternion.Euler(newRotation);//Hesaplanan 'newRotation' vektorune gore pivot noktasindan rotate islemi.
            //} 
        }
    }
}
