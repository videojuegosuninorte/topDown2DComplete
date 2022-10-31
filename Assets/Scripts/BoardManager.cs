using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.IO;

public class BoardManager : MonoBehaviour
{
    private PowerSource powerSource;
    private PathManager pathManager;
    private List<CellInfo> towers;
    private List<CellInfo> players;
    [SerializeField] private Cell CellPrefab;
    [SerializeField] private Player PlayerPrefab;
    [SerializeField] private PowerSource PowerSourcePrefab;
    [SerializeField] private Tower TowerPrefab;
    [SerializeField] private ExternalWall ExternalWallPrefab;
    [SerializeField] private Grid GridPrefab;
    private bool started = false;
    private int START_P = 0, END_P = 3, START_T = 15, END_T = 18;

    private Grid grid;
    private Player player;

    private int towerCountRepeat = 50;


    private void Awake()
    {
        pathManager = new PathManager();
        towers = new List<CellInfo>();
        players = new List<CellInfo>();
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

        if (Input.GetKeyDown(KeyCode.C))
        {
            towers.Clear();
        }

        if (!started)
            return;

        if (!transform.Find("Player(Clone)"))
        {

            Debug.Log(transform.name + " restart, no players");
            writeString(0);
            restart();
        }



        if (!transform.Find("PowerSource(Clone)"))
        {
            Debug.Log(transform.name + " restart, no power source");
            writeString(1);
            restart();
        }

        //Debug.Log(GetInstanceID() + " " + transform.name);

    }

   

    private int returnUnitType(int x, int y)
    {
        foreach (CellInfo cellInfo in towers)
        {
            if (cellInfo.x == x && cellInfo.y == y)
            {
                return cellInfo.unitType;
            }
        }

        foreach (CellInfo cellInfo in players)
        {
            if (cellInfo.x == x && cellInfo.y == y)
            {
                return cellInfo.unitType;
            }
        }

        return UnitType.NONE;

    }

    private void writeString(int win)
    {
        StreamWriter writer = new StreamWriter("gameResults.txt", true);
        writer.Write(win.ToString() + ",");
        for (int i = 0; i < 11; i++)
        {
            for (int j = START_T; j < END_T; j++)
            {
                writer.Write(returnUnitType(i, j) + ",");
            }
        }
        for (int i = 0; i < 11; i++)
        {
            for (int j = START_P; j < END_P; j++)
            {
                writer.Write(returnUnitType(i, j) + ",");
            }
        }
        writer.WriteLine("");
        writer.Close();
        StreamReader reader = new StreamReader("gameResults.txt");
        //Print the text from the file
        //Debug.Log(reader.ReadToEnd());
        reader.Close();
    }



    private void SetupBoard()
    {

        setupPieces();
        
    }

    private void restart()
    {
        Debug.Log(GetInstanceID() + " restart");
        clearPieces();
        setupPieces();
    }

    private void setupPieces()
    {
        grid = Instantiate(GridPrefab, transform.localPosition, Quaternion.identity);
        grid.Init(11, 20, 1, CellPrefab, transform, ExternalWallPrefab);
        grid.transform.SetParent(transform);

        powerSource = Instantiate(PowerSourcePrefab, new Vector2(5 + transform.localPosition.x, 19 + transform.localPosition.y), Quaternion.identity);
        powerSource.transform.SetParent(transform);
        powerSource.Init();
        //powerSource.SetGrid(grid);

        pathManager.powerUnitLocation = new Vector2Int((int)powerSource.transform.localPosition.x, (int)powerSource.transform.localPosition.y);

        if (towers.Count == 0 || towerCountRepeat == 0) { 
            setRandomTower(3, UnitType.TOWER_L);

            setRandomTower(3, UnitType.TOWER_H);

            towerCountRepeat = 50;
        } else
        {
            recreateTowers();
            towerCountRepeat = towerCountRepeat - 1;
        }

        players.Clear();

        setRandomPlayers(13, pathManager, UnitType.INFANTERY_L);

        setRandomPlayers(1, pathManager, UnitType.INFANTERY_H);

        started = true;
    }



    private void clearPieces()
    {

        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
       

    }

    private void decreadPlayer()
    {
    
    }

    private void powerSourceDestroyed()
    {
        Debug.Log("Power source destroyed");
        restart();
    }

    private void setRandomTower(int towerCount, int unitType)
    {
        
        int posX, posY;
        for (int i = 0; i < towerCount; i++)
        {
            do
            {
                posX = Random.Range(0, 11);
                posY = Random.Range(START_T, END_T);
            } while (!grid.isWalkable(posX, posY));

            Tower tower = Instantiate(TowerPrefab, new Vector2(posX + transform.position.x, posY + transform.position.y), Quaternion.identity);

            tower.transform.SetParent(transform);

            tower.SetGrid(grid);

            tower.Init(unitType);

            towers.Add(new CellInfo(posX, posY, unitType));
        }
    }

    private void recreateTowers()
    {
        foreach (CellInfo cellInfo in towers)
        {
            Tower tower = Instantiate(TowerPrefab, new Vector2(cellInfo.x + transform.position.x, cellInfo.y + transform.position.y), Quaternion.identity);

            tower.transform.SetParent(transform);

            tower.SetGrid(grid);

            tower.Init(cellInfo.unitType);
        }
    }

    private void setRandomPlayers(int playerCount, PathManager pathManager, int unitType)
    {
        int posX, posY;
        
        for (int i = 0; i < playerCount; i++)
        {
            do
            {
                posX = Random.Range(0, 11);
                posY = Random.Range(START_P, END_P);
            } while (!grid.isWalkable(posX, posY));

            player = Instantiate(PlayerPrefab, new Vector2(posX + transform.position.x, posY + transform.position.y), Quaternion.identity);
            player.transform.SetParent(transform);
            player.SetGrid(grid);
            player.starMoving(grid, pathManager, unitType);
            players.Add(new CellInfo(posX, posY, unitType));
        }
    }
}
