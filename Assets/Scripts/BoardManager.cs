using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.IO;

public class BoardManager : MonoBehaviour
{
    private PowerSource powerSource;
    private PathManager pathManager;
    [SerializeField] private Cell CellPrefab;
    [SerializeField] private Player PlayerPrefab;
    [SerializeField] private PowerSource PowerSourcePrefab;
    [SerializeField] private Tower TowerPrefab;
    [SerializeField] private ExternalWall ExternalWallPrefab;

    private Grid grid;
    private Player player;

    private int playerCount = 14;

    private void Awake()
    {
        pathManager = new PathManager();
        
        SetupBoard();
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            restart();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SetupBoard();
        }

        if (!FindObjectOfType(typeof(Player))){
            Debug.Log("restart, no players");
            writeString(false);
            restart();
        }

        if (!FindObjectOfType(typeof(PowerSource)))
        {
            Debug.Log("restart, no power source");
            writeString(true);
            restart();
        }

    }

    private void writeString(bool win)
    {
        StreamWriter writer = new StreamWriter("gameResults.txt", true);
        writer.WriteLine(win.ToString());
        writer.Close();
        StreamReader reader = new StreamReader("gameResults.txt");
        //Print the text from the file
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }


    private void SetupBoard()
    {
        setupPieces();
    }

    private void restart()
    {
        Debug.Log("restart");
        clearPieces();
        setupPieces();
    }

    private void setupPieces()
    {
        grid = new Grid(11, 20, 1, CellPrefab, transform, ExternalWallPrefab);
        powerSource = Instantiate(PowerSourcePrefab, new Vector2(5 + transform.localPosition.x, 19 + transform.localPosition.y), Quaternion.identity);
        powerSource.transform.SetParent(transform);
        powerSource.Init();

        pathManager.powerUnitLocation = new Vector2Int((int)powerSource.transform.localPosition.x, (int)powerSource.transform.localPosition.y);

        setRandomTower(3, UnitType.TOWER_L);

        setRandomTower(3, UnitType.TOWER_H);

        setRandomPlayers(13, pathManager, UnitType.INFANTERY_L);

        setRandomPlayers(1, pathManager, UnitType.INFANTERY_H);
    }

    private void clearPieces()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("Found " + allObjects.Length + " players");
        foreach (GameObject obj in allObjects)
        {
            Destroy(obj);
        }

        GameObject[] allTowers = GameObject.FindGameObjectsWithTag("Tower");
        Debug.Log("Found " + allTowers.Length + " towers");
        foreach (GameObject obj in allTowers)
        {
            Destroy(obj);
        }

        GameObject[] allPowerSource = GameObject.FindGameObjectsWithTag("PowerSource");
        Debug.Log("Found " + allPowerSource.Length + " power sources");
        foreach (GameObject obj in allPowerSource)
        {
            Destroy(obj);
        }


    }

    private void decreadPlayer()
    {
        playerCount--;
        Debug.Log("Remaining " + playerCount + " units");
        if (playerCount == 0)
        {
            restart();
        }
    }

    private void powerSourceDestroyed()
    {
        Debug.Log("Power source destroyed");
        restart();
    }

    private void setRandomTower(int towerCount, UnitType unitType)
    {
        int posX, posY;
        for (int i = 0; i < towerCount; i++)
        {
            do
            {
                posX = Random.Range(0, 9);
                posY = Random.Range(15, 17);
            } while (!grid.isWalkable(posX, posY));

            Tower tower = Instantiate(TowerPrefab, new Vector2(posX + transform.position.x, posY + transform.position.y), Quaternion.identity);

            tower.transform.SetParent(transform);

            tower.SetGrid(grid);

            tower.Init(unitType);
        }
    }

    private void setRandomPlayers(int playerCount, PathManager pathManager, UnitType unitType)
    {
        int posX, posY;
        for (int i = 0; i < playerCount; i++)
        {
            do
            {
                posX = Random.Range(0, 9);
                posY = Random.Range(0, 6);
            } while (!grid.isWalkable(posX, posY));

            player = Instantiate(PlayerPrefab, new Vector2(posX + transform.position.x, posY + transform.position.y), Quaternion.identity);
            player.transform.SetParent(transform);
            player.starMoving(grid, pathManager, unitType);

        }
    }
}
