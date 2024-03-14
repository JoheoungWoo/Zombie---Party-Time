using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public struct Node
{
    public Vector2 position;
    public int layerIndex;

    public Node(Vector2 position, int layerIndex)
    {
        this.position = position;
        this.layerIndex = layerIndex;
    }
}

public class Map : MonoBehaviour
{
    //Init Range
    public int rangeSize;

    public Transform gridTransform;

    public Node[,] nodes;

    private void Awake()
    {
        //nodes = new Node[rows,cols];
    }

    public int GetTileMap(int tileX, int tileY)
    {
        var tileMaps = gridTransform.GetComponentsInChildren<Tilemap>();
        foreach (var tileMap in tileMaps)
        {
            var cell = tileMap.GetTile(new Vector3Int(tileX, tileY, 0));
            if (cell != null)
            {
                int layer = tileMap.gameObject.layer;
                Debug.Log("Tile layer: " + layer + "/" + tileX + "/" + tileY);
                return layer;
            }
        }
        return 0;
    }


    public void ReadMap(Vector2Int playerVec2)
    {
        //int gridSize = rows; // �׸��� ũ��
        int halfGridSize = rangeSize; // �߽� ��ǥ�� ����ϱ� ���� ��
        Debug.Log("�������");

        for (int i = -halfGridSize; i <= halfGridSize; i++)
        {
            for (int j = -halfGridSize; j <= halfGridSize; j++)
            {
                int row = playerVec2.x + i;
                int col = playerVec2.y + j;

                Debug.Log(row + "/" + col);

                //�� ã���� ���� 0
                var layerIndex = GetTileMap(row, col);
                //nodes[row, col] = new Node(new Vector2(, col), layerIndex);
            }
        }
    }

}
