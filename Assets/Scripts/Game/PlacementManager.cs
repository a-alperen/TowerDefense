using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public GameObject basicTowerObject; // Kule objesini temsil eder.

    private GameObject dummyPlacement; // Yerlestirme nesnesinin islevsiz halini(gorsel olarak) temsil eder.

    private GameObject hoverTile; // Yerlestirilecek nesneyi temsil eder.
    
    public Camera cam;

    public GameObject mapGenerator; // Scale islemi icin gerekli.

    public LayerMask mask;
    public LayerMask towerMask;

    public bool isBuilding;

    private float cameraWidth;  // Kamera genisligi
    private float cameraHeight; // Kamera yuksekligi
    private float tileScaleX;   // Tile in x ekseninde olmasi gereken scale miktari
    private float tileScaleY;   // Tile in y ekseninde olmasi gereken scale miktari
    private readonly float tileRate = 0.75f;

    private void Start()
    {

        CalculateTileScale(mapGenerator.GetComponent<MapGenerator>().mapWidth,mapGenerator.GetComponent<MapGenerator>().mapHeight); // Tower objesinin buyuklugu icin gerekli scale degeri hesaplandi.
        ChangeScaleOfTile(tileScaleX * tileRate, tileScaleY * tileRate); // Tower objesinin buyuklugu ayarlandi.
        StartBuilding();
    }
    private void Update()
    {
        if (isBuilding == true)
        {
            if (dummyPlacement!= null)
            {
                GetCurrentHoverTile();

                if (hoverTile != null)
                {
                    dummyPlacement.transform.position = hoverTile.transform.position;
                }
                
            }

            if (Input.GetMouseButtonDown(0)) // Mouse sol tiki ile yerlestirmeyi saglar.
            {
                PlaceBuilding();
            }
        }

    }
    public void CalculateTileScale(int mapWidth, int mapHeight)
    {
        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Camera.main.aspect;

        tileScaleX = (cameraWidth / mapWidth) * 2;
        tileScaleY = (cameraHeight / mapHeight) * 2;
        
    }
    public void ChangeScaleOfTile(float tileScaleX, float tileScaleY)
    {
        basicTowerObject.transform.localScale = new Vector3(tileScaleX, tileScaleY, 0);
    }

    /// <summary>
    /// Mevcut mouse konumunu dondurur.
    /// </summary>
    /// <returns></returns>
    public Vector2 GetMousePosition()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }

    /// <summary>
    /// Yerlestirilecek nesneyi alir.
    /// </summary>
    public void GetCurrentHoverTile()
    {
        Vector2 mousePosition = GetMousePosition();

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, new Vector2(0, 0), 0.1f, mask, -100, 100);

        if (hit.collider != null)
        {
            // Isinin degdigi yerin maptile olup olmadigi kontrol ediliyor.
            if (MapGenerator.mapTiles.Contains(hit.collider.gameObject))
            {
                // Maptile olan yerin pathTile olmamasi kontrol ediliyor.
                if (!MapGenerator.pathTiles.Contains(hit.collider.gameObject))
                {
                    hoverTile = hit.collider.gameObject;
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool CheckForTower()
    {
        bool towerOnSlot = false;
        Vector2 mousePosition = GetMousePosition();

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, new Vector2(), 0.1f, towerMask, -100, 100);
        
        if (hit.collider != null)
        {
            towerOnSlot = true;
        }
        
        
        return towerOnSlot;
    }

    /// <summary>
    /// Kule yerlestirme islemi yapilir.
    /// </summary>
    public void PlaceBuilding()
    {
        if (hoverTile != null)
        {
            if (CheckForTower() == false)
            {
                GameObject newTowerObject = Instantiate(basicTowerObject);
                newTowerObject.layer = LayerMask.NameToLayer("Tower");
                newTowerObject.transform.position = hoverTile.transform.position;

                EndBuilding();
            }
        }
    }
    /// <summary>
    /// Yerlestirme islemini baslatir Yerlestirme yapilirken kule fonksiyonlarinin calismamasi saglanir.
    /// </summary>
    public void StartBuilding()
    {
        isBuilding = true;

        dummyPlacement = Instantiate(basicTowerObject);

        if (dummyPlacement.GetComponent<Tower>() != null)
        {
            Destroy(dummyPlacement.GetComponent<Tower>());
        }
        if (dummyPlacement.GetComponent<BarrelRotation>() != null)
        {
            Destroy(dummyPlacement.GetComponent<BarrelRotation>());
        }
        if(dummyPlacement.GetComponent<BoxCollider2D>() != null)
        {
            Destroy(dummyPlacement.GetComponent<BoxCollider2D>());
        }
    }

    /// <summary>
    /// Yerlestirme isini bitirir.
    /// </summary>
    public void EndBuilding()
    {
        isBuilding = false;

        if (dummyPlacement != null)
        {
            Destroy(dummyPlacement);
        }
    }

    
}
