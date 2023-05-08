using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float range; // Kulenin atis yapabilecegi max menzili.
    [SerializeField] private float damage;//Kulenin ogrenciye verecegi hasar.
    [SerializeField] private float timeBetweenShots; //Kule atislari arasinda gecen sure.
    
    private float nextTimeToShoot;

    public GameObject currentTarget; //Kulenin menzilindeki mevcut durumdaki en yakin hedef ogrenci.


    
    private void Start()
    {
        nextTimeToShoot = Time.time;
    }
    private void Update()
    {
        UpdateNearestStudent();

        if(Time.time > nextTimeToShoot)
        {
            if(currentTarget != null)
            {
                Shoot();
                nextTimeToShoot= Time.time + timeBetweenShots;
            }
        }
        
    }

    private void UpdateNearestStudent()
    {
        GameObject currentNearestStudent = null; //Mevcut durumdaki en yakin ogrenci.

        float distance = Mathf.Infinity; // Baslangicta mesafeyi sonsuz yaptik. Zaten ilk ogrenci ile bu mesafe degisecek zaten.

        foreach (GameObject student in Students.students) // Students statik sinifindaki static ogrenciler listesini gez.
        {
            if(student != null)
            {
                float _distance = (transform.position - student.transform.position).magnitude;//Kule ile ogrenci arasindaki mesafenin vektorunu hesapladik.

                if (_distance < distance)//Eger kule ile ogrenci arasindaki mesafe gereken mesafeden kucukse
                {
                    distance = _distance; // Gereken mesafeyi kule-ogrenci arasindaki mesafeye esitle.
                    currentNearestStudent = student;// Ogrenciyi de en yakin ogrenci olarak isaretle.
                }
            }
            
        }

        if(distance <= range)// Eger ogrenci mesafesi kule range inden kucukse
        {
            currentTarget = currentNearestStudent; // Ogrenciyi mevcut hedef haline getir.
        }
        else
        {
            currentTarget = null; // Degilse mevcut hedef bos kalsin.
        }
    }

    protected virtual void Shoot()// Kule ates eder.
    {
        Student studentScript = currentTarget.GetComponent<Student>();// Ogrenci scriptindeki TakeDamage() methodu icin. 'currentTarget' student game objesi zaten.
        studentScript.TakeDamage(damage); // Ogrenciye hasar verir.
    }

    
}
