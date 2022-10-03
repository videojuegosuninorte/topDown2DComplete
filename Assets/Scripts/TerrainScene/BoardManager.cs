using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    [SerializeField] private Cell CellPrefab;
    [SerializeField] private Player PlayerPrefab;
    [SerializeField] private PowerSource PowerSourcePrefab;
    [SerializeField] private Tower TowerPrefab;
    private Grid grid;
    private Player player;
    [SerializeField]
    private float moveSpeed = 2f;

    private void Awake()
    {
        Instance = this;
    }

    public void SetupBoard()
    {
        grid = new Grid(11, 20, 1, CellPrefab);

        Instantiate(PowerSourcePrefab, new Vector2(5, 19), Quaternion.identity);

        PathManager.Instance.powerUnitLocation = new Vector2Int(5, 19);

        //Tower tower = Instantiate(TowerPrefab, new Vector2(2, 17), Quaternion.identity);

        //tower.SetGrid(grid);

        //tower = Instantiate(TowerPrefab, new Vector2(7, 17), Quaternion.identity);

        //tower.SetGrid(grid);

        

        player = Instantiate(PlayerPrefab, new Vector2(0, 0), Quaternion.identity);

        player.starMoving(grid, 4);

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
