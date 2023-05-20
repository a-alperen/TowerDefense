using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public GameObject MapTile;
    public GameObject towerPrefab;
    [SerializeField]int difficulty = 2;

    public int mapWidth;
    public int mapHeight;

    public static List<GameObject> mapTiles = new();
    public static List<GameObject> pathTiles = new();
    

    public static GameObject startTile;
    public static GameObject endTile;
    
    public GameObject spawnPoint; // Ogrenci spawn noktasi.

    private bool reachedX = false;
    private bool reachedY = false;

    private GameObject currentTile;
    private int currentIndex;
    private int nextIndex;

    public GameObject parentMapTiles;
    public GameObject parentTowers;
    public Color pathColor;
    public Color startColor;
    public Color endColor;
    public Color towerPlaceColor;

    // Harita olusturulurken kullanilan tile in kameraya tam oturmasi icin gereken degiskenler.
    private float cameraWidth;  // Kamera genisligi
    private float cameraHeight; // Kamera yuksekligi
    private float tileScaleX;   // Tile in x ekseninde olmasi gereken scale miktari
    private float tileScaleY;   // Tile in y ekseninde olmasi gereken scale miktari
    private float _tileScaleX;  // Tile lar arasi bosluk icin olmasi gereken scaleX miktari
    private float _tileScaleY;  // Tile lar arasi bosluk icin olmasi gereken scaleY miktari
    private readonly float spaceRate = 0.98f;

    private void Awake()
    {
        mapTiles.Clear();
    }
    private void Start()
    {

        CalculateTileScale(mapWidth, mapHeight);
        ChangeScaleOfTile(_tileScaleX, _tileScaleY);
        
        GenerateMap();
        SpawnPoint();

        GetTowerGeneratorLocation(pathTiles);
        GenerateTower(difficulty, Towers.towers);

        
    }
    /// <summary>
    /// Baslangic tile'i uzerine bir spawnpoint yerlestirir.
    /// </summary>
    private void SpawnPoint()
    {
        Vector3 position = startTile.transform.position + new Vector3(0, tileScaleY / 2, 0);
        spawnPoint = Instantiate(MapTile, position, Quaternion.identity);
        spawnPoint.name = "SpawnTile";
        Destroy(spawnPoint.GetComponent<SpriteRenderer>());
        //pathTiles.Add(spawnPoint);
        
    }

    /// <summary>
    /// Generatorda kullanilan tile game objesinin scale miktarini hesaplar.
    /// </summary>
    /// <param name="mapWidth"></param>
    /// <param name="mapHeight"></param>
    public void CalculateTileScale(int mapWidth, int mapHeight)
    {
        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Camera.main.aspect;

        tileScaleX = (cameraWidth / mapWidth) * 2;
        tileScaleY = (cameraHeight / mapHeight) * 2;
        _tileScaleX = (cameraWidth / mapWidth) * 2 * spaceRate;
        _tileScaleY = (cameraHeight / mapHeight) * 2 * spaceRate;
    }
    public void ChangeScaleOfTile(float tileScaleX,float tileScaleY)
    {
        MapTile.transform.localScale = new Vector3(tileScaleX,tileScaleY,0);
    }

    private List<GameObject> GetTopEdgeTiles()
    {
        List<GameObject> edgeTiles = new();

        for (int i = (mapWidth * (mapHeight - 1)); i < mapWidth * mapHeight ; i++)
        {
            edgeTiles.Add(mapTiles[i]);
        }

        return edgeTiles;
    }

    private List<GameObject> GetBottomEdgeTiles()
    {
        List<GameObject> edgeTiles = new();

        for (int i = 0; i < mapWidth; i++)
        {
            edgeTiles.Add(mapTiles[i]);
        }

        return edgeTiles;
    }

    private void MoveDown()
    {
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex - mapWidth;
        currentTile = mapTiles[nextIndex];
    }
    private void MoveDown(int index)
    {
        for (int i = 0; i < index; i++)
        {
            pathTiles.Add(currentTile);
            currentIndex = mapTiles.IndexOf(currentTile);
            nextIndex = currentIndex - mapWidth;
            currentTile = mapTiles[nextIndex];
        }
    }

    private void MoveLeft()
    {
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex - 1;
        currentTile = mapTiles[nextIndex];
    }
    private void MoveRight()
    {
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex + 1;
        currentTile = mapTiles[nextIndex];
    }
    private void GenerateMap()
    {
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                GameObject newTile = Instantiate(MapTile);
                Vector2 tilePos = new(_tileScaleX / 2f + -cameraWidth + x * tileScaleX,_tileScaleY /2f+ -cameraHeight + y * tileScaleY);
                mapTiles.Add(newTile);
                newTile.transform.position = tilePos;
                newTile.transform.parent = parentMapTiles.transform;
            }
        }

        List<GameObject> topEdgeTiles = GetTopEdgeTiles();
        List<GameObject> bottomEdgeTiles = GetBottomEdgeTiles();


        int startIndex = 1;
        int endIndex = mapWidth - 2;

        startTile = topEdgeTiles[startIndex];
        endTile = bottomEdgeTiles[endIndex];


        currentTile = startTile;
        MoveDown(2);

        int loopTile = 0;

        while (reachedX == false)
        {
            loopTile++;
            if(loopTile > 100)
            {
                Debug.Log("Loop run too long. Broke out of it!");
                break;
            }
            if (currentTile.transform.position.x > endTile.transform.position.x)
            {
                MoveLeft();
            }
            else if (currentTile.transform.position.x < endTile.transform.position.x)
            {
                MoveRight();
            }
            else
            {
                reachedX = true;
            }
        }

        while(reachedY == false)
        {
            if(currentTile.transform.position.y > endTile.transform.position.y)
            {
                MoveDown();
            }
            else
            {
                reachedY = true;
            }
        }
        pathTiles.Add(endTile);

        foreach (GameObject item in pathTiles)
        {
            item.GetComponent<SpriteRenderer>().color = pathColor;
        }

        startTile.GetComponent<SpriteRenderer>().color = startColor;
        endTile.GetComponent<SpriteRenderer>().color = endColor;
    }
    
    /// <summary>
    /// Haritaya zorluga gore tower ekleme islemini yapar.
    /// </summary>
    /// <param name="difficulty"></param>
    private void GenerateTower(int difficulty, List<GameObject> towerPlaceTiles)
    {
        if (difficulty == 0)
        {
            PlaceTower(TowerCount(difficulty), towerPlaceTiles);
        }
        else if (difficulty == 1)
        {
            PlaceTower(TowerCount(difficulty), towerPlaceTiles);
        }
        else if (difficulty == 2)
        {
            PlaceTower(TowerCount(difficulty), towerPlaceTiles);
        }
    }
    private void PlaceTower(int towerCount, List<GameObject> towerPlaceTiles)
    {
        List<GameObject> towerTile = new();
        while(towerCount > 0)
        {
            foreach (GameObject tile in towerPlaceTiles)
            {
                if (Random.Range(0f, 1f) <= 0 && towerCount > 0)
                {
                    if (!towerTile.Contains(tile))
                    {
                        GameObject tower = Instantiate(towerPrefab, tile.transform.position /*+ new Vector3(0, (Random.Range(0, 1) < 0.5) ? 1 * tileScaleY : -1 * tileScaleY)*/, Quaternion.identity);
                        towerCount -= 1;
                        towerTile.Add(tower);
                        tower.transform.parent = parentTowers.transform;
                        Towers.towers.Add(tower);
                    }
                    
                }
            }
            if (towerCount == 0)
            {
                break;
            }
        }
        
    }
    /// <summary>
    /// Kulelerin yerlestirilecegi yerleri belirler.
    /// </summary>
    /// <param name="pathTiles"></param>
    /// <returns>GameObject listesi dondurur.</returns>
    private List<GameObject> GetTowerGeneratorLocation(List<GameObject> pathTiles)
    {
        List<GameObject> list = new(); // 
        
        for(int i = 0; i < pathTiles.Count; i++)
        {
            int tileIndex = mapTiles.IndexOf(pathTiles[i]); // Haritanin tamaminin tutuldugu listede pathTile objesinin oldugu index'i tileIndex'e kaydeder.
            
            if (!pathTiles.Contains(mapTiles[tileIndex + 1]) && pathTiles[i] != startTile && pathTiles[i] != endTile) // Yola yerlestirme yapilmamasi icin kosullar kontrol edilir.
            {
                if (!pathTiles.Contains(mapTiles[tileIndex - 1]))
                {
                    list.Add(mapTiles[tileIndex - 1]);
                    Towers.towers.Add(mapTiles[tileIndex - 1]);
                }
                
                list.Add(mapTiles[tileIndex + 1]);
                Towers.towers.Add(mapTiles[tileIndex + 1]);

            }
            if (!pathTiles.Contains(mapTiles[tileIndex - 1]) && pathTiles[i] != startTile && pathTiles[i] != endTile) // Yola yerlestirme yapilmamasi icin kosullar kontrol edilir.
            {
                if (!pathTiles.Contains(mapTiles[tileIndex + 1]))
                {
                    list.Add(mapTiles[tileIndex + 1]);
                    Towers.towers.Add(mapTiles[tileIndex + 1]);
                }

                list.Add(mapTiles[tileIndex - 1]);
                Towers.towers.Add(mapTiles[tileIndex - 1]);

            }
            if (tileIndex - mapWidth > 0 && tileIndex + mapWidth < mapTiles.Count) // Yola yerlestirme yapilmamasi icin kosullar kontrol edilir. Liste disina cikmamasi saglanir.
            {
                
                if (!pathTiles.Contains(mapTiles[tileIndex + mapWidth]))
                {
                    list.Add(mapTiles[tileIndex + mapWidth]);
                    Towers.towers.Add(mapTiles[tileIndex + mapWidth]);
                }
                if (!pathTiles.Contains(mapTiles[tileIndex - mapWidth]))
                {
                    list.Add(mapTiles[tileIndex - mapWidth]);
                    Towers.towers.Add(mapTiles[tileIndex - mapWidth]);
                }

            }
            
        }
        // Kulelerin yerlestirilecegi yerlerin belli olmasi icin belirlenen bir renge boyanir.
        foreach (GameObject tile in Towers.towers)
        {

            tile.GetComponent<SpriteRenderer>().color = towerPlaceColor;
        }
        
        return list;
    }


    /// <summary>
    /// Zorluga gore spawn olacak tower adedi belirlenir.
    /// </summary>
    /// <param name="difficulty"></param>
    /// <returns></returns>
    private int TowerCount(int difficulty)
    {
        int count = 0;
        if (difficulty == 0) count = 2;
        else if (difficulty == 1) count = 4;
        else if (difficulty == 2) count = 6;

        return count;
    }

}
