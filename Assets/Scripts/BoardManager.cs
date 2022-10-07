using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private Cell CellPrefab;
    [SerializeField] private Player PlayerPrefab;
    [SerializeField] private PowerSource PowerSourcePrefab;
    [SerializeField] private Tower TowerPrefab;
    [SerializeField] private ExternalWall ExternalWallPrefab;

    private Grid grid;
    private Player player;

    private void Awake()
    {
        SetupBoard();
    }

    public void SetupBoard()
    {
        grid = new Grid(11, 20, 1, CellPrefab, transform, ExternalWallPrefab);

        PowerSource powerSource = Instantiate(PowerSourcePrefab, new Vector2(5 + transform.localPosition.x, 19 + transform.localPosition.y), Quaternion.identity);
        powerSource.transform.SetParent(transform);
        powerSource.Init();

        PathManager pathManager = new PathManager();

        pathManager.powerUnitLocation = new Vector2Int((int)powerSource.transform.localPosition.x, (int)powerSource.transform.localPosition.y);

        setRandomTower(3, UnitType.TOWER_L);

        setRandomTower(3, UnitType.TOWER_L);

        setRandomPlayers(13, pathManager, UnitType.INFANTERY_L);

        setRandomPlayers(13, pathManager, UnitType.INFANTERY_H);
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
