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
    private int loopCounter = 0;

    private Grid grid;
    private Player player;

    private int towerCountRepeat = 10;

    private List<string> towerString = new List<string>();
    //{
    //    "300000000020000000003000002200003",
    //    "020000030200030000000002300000000",
    //    "000300000000000200032300020000000",
    //    "330000000200020300000000200000000",
       
    //};
private List<string> playerString = new List<string>();
    //{
    //    "000044440040444404000440004000500",
    //    "000404000440000400400440004444045",
    //    "044040004440004004000004000404445",
    //    "000000440400000440450444044404000",
    //    };


private void Awake()
    {
        pathManager = new PathManager();
        towers = new List<CellInfo>();
        players = new List<CellInfo>();
        setupPieces();
    }
    /*
    private void FixedUpdate()
    {
        if (loopCounter > 2000)
        {
            Debug.Log("loopCounter RESTART");
            loopCounter = 0;
            restart();
        }

        loopCounter++;
    }
    */

    private bool loadSetup()
    {
        int unitPos, type;
        if (towerString.Count == 0)
        {
            Debug.Log("loadSetup no data");
            return false;
        }
        Debug.Log("loadSetup start "+ towerString.Count+ " "+ playerString.Count);
        unitPos = 0;
        towers.Clear();
        for (int i = 0; i < 11; i++)
        {
            for (int j = START_T; j < END_T; j++)
            {
                type = int.Parse(towerString[0].Substring(unitPos, 1));
                unitPos++;
                switch(type){
                    case UnitType.TOWER_L:
                    case UnitType.TOWER_H:
                        //Debug.Log("loadSetup tower type "+ type);
                        towers.Add(new CellInfo(i, j, type));
                        break;
                    case UnitType.POWER_SOURCE:
                    case UnitType.NONE:
                        break;
                    default:
                        Debug.Log("loadSetup problem loading towers "+ type+ " "+i+ " "+j);
                        return false;

                }
            }
        }
        unitPos = 0;
        players.Clear();
        for (int i = 0; i < 11; i++)
        {
            for (int j = START_P; j < END_P; j++)
            {
                type = int.Parse(playerString[0].Substring(unitPos, 1));
                unitPos++;
                switch (type)
                {
                    case UnitType.INFANTERY_H:
                    case UnitType.INFANTERY_L:
                    case UnitType.INFANTERY_K:
                        //Debug.Log("loadSetup player type " + type);
                        players.Add(new CellInfo(i, j, type));
                        break;
                    case UnitType.NONE:
                        break;
                    default:
                        Debug.Log("loadSetup problem loading players " + type + " " + i + " " + j);
                        return false;

                }
            }
        }
        Debug.Log("loadSetup ok "+ towers.Count + "  " + players.Count);
        towerString.RemoveAt(0);
        playerString.RemoveAt(0);

        if (towerString.Count == 0)
        {
            writeEndOfData();
        }
        return true;

    }

    void FixedUpdate()
    {
        if (!started)
            return;

        if (loopCounter > 2000)
        {
            Debug.Log("loopCounter RESTART");
            loopCounter = 0;
            restart();
        }

        loopCounter++;

        //Debug.Log("loopCounter " + loopCounter);

        if (!transform.Find("Player(Clone)"))
        {

            //Debug.Log(transform.name + " restart, no players");
            loopCounter = 0;
            writeString(0);
            restart();
        }



        if (!transform.Find("PowerSource(Clone)"))
        {
            //Debug.Log(transform.name + " restart, no power source");
            loopCounter = 0;
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

    private bool verifySetup()
    {
        int s = 0;

        for (int i = 0; i < 11; i++)
        {
            for (int j = START_T; j < END_T; j++)
            {
                s = s + returnUnitType(i, j);
            }
        }
        if (s != 15)
        {
            return false;
        }
        s = 0;
        for (int i = 0; i < 11; i++)
        {
            for (int j = START_P; j < END_P; j++)
            {
                s = s + returnUnitType(i, j);
            }
        }
        if (s != 57)
        {
            return false;
        }
        return true;
    }

    private void writeEndOfData()
    {
        
        StreamWriter writer = new StreamWriter("gameResults2.txt", true);
        writer.WriteLine("EOD");
        writer.Close();
    }

    private void writeString(int win)
    {
        string concatT, concatP;
        StreamWriter writer = new StreamWriter("gameResults2.txt", true);
        writer.Write(win.ToString() + ",");
        concatT = "";
        for (int i = 0; i < 11; i++)
        {
            for (int j = START_T; j < END_T; j++)
            {
                writer.Write(returnUnitType(i, j) + ",");
                concatT = concatT + returnUnitType(i, j);
            }
        }
        concatP = "";
        for (int i = 0; i < 11; i++)
        {
            for (int j = START_P; j < END_P; j++)
            {
                writer.Write(returnUnitType(i, j) + ",");
                concatP = concatP + returnUnitType(i, j); ;
            }
        }
        writer.Write(concatT + "," + concatP);
        writer.WriteLine("");
        writer.Close();
    }

    private void restart()
    {
        //Debug.Log(GetInstanceID() + " restart");
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

        if (loadSetup())
        {
            recreateTowers();
            recreatePlayers();
            towerCountRepeat = 50;
        } else { 

            if (towers.Count == 0 || towerCountRepeat == 0) {
                towers.Clear();

                setRandomTower(3, UnitType.TOWER_L);

                setRandomTower(3, UnitType.TOWER_H);

                players.Clear();

                setRandomPlayers(13, pathManager, UnitType.INFANTERY_L);

                setRandomPlayers(1, pathManager, UnitType.INFANTERY_H);

                towerCountRepeat = 10;

                writeEndOfData();
            } else
            {
                recreateTowers();
                recreatePlayers();
                towerCountRepeat = towerCountRepeat - 1;
            }

            //players.Clear();

            //setRandomPlayers(13, pathManager, UnitType.INFANTERY_L);

            //setRandomPlayers(1, pathManager, UnitType.INFANTERY_H);

        }

        if (!verifySetup())
        {
            Debug.Log("verifySetup FAILED!!");
            loopCounter = 0;
            restart();
        }

        started = true;
    }



    private void clearPieces()
    {

        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
       

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

    private void recreatePlayers()
    {

        foreach (CellInfo cellInfo in players)
        {
            Player player = Instantiate(PlayerPrefab, new Vector2(cellInfo.x + transform.position.x, cellInfo.y + transform.position.y), Quaternion.identity);

            player.transform.SetParent(transform);
            player.SetGrid(grid);
            player.starMoving(grid, pathManager, cellInfo.unitType);
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
