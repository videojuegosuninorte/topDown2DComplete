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
    private Grid grid;
    private Player player;

    private void Awake()
    {
        SetupBoard();
    }

    public void SetupBoard()
    {
        grid = new Grid(11, 20, 1, CellPrefab, transform);

        PowerSource powerSource = Instantiate(PowerSourcePrefab, new Vector2(5 + transform.localPosition.x, 19 + transform.localPosition.y), Quaternion.identity);
        powerSource.transform.SetParent(transform);

        PathManager pathManager = new PathManager();

        pathManager.powerUnitLocation = new Vector2Int((int)powerSource.transform.localPosition.x, (int)powerSource.transform.localPosition.y);

        Tower tower = Instantiate(TowerPrefab, new Vector2(2+transform.position.x, 17+transform.position.y), Quaternion.identity);

        tower.transform.SetParent(transform);

        tower.SetGrid(grid);

        //tower = Instantiate(TowerPrefab, new Vector2(7, 17), Quaternion.identity);

        //tower.SetGrid(grid);



        player = Instantiate(PlayerPrefab, new Vector2(0+transform.position.x, 0+transform.position.y), Quaternion.identity);

        player.transform.SetParent(transform);

        player.starMoving(grid, 4, pathManager);

        //player = Instantiate(PlayerPrefab, new Vector2(8, 0), Quaternion.identity);

        //player.starMoving(grid, 3);

        //player = Instantiate(PlayerPrefab, new Vector2(8, 0), Quaternion.identity);

        //player.starMoving(grid, 3);


        //player = Instantiate(PlayerPrefab, new Vector2(6, 0), Quaternion.identity);

        //player.starMoving(grid, 3);


        //player = Instantiate(PlayerPrefab, new Vector2(6, 6), Quaternion.identity);

        //player.starMoving(grid, 3);

    }
}
