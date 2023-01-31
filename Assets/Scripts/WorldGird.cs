using System;
using System.Collections;
using System.Collections.Generic;
using Spellcast;
using UnityEngine;

public class WorldGird : MonoBehaviour
{
    public Vector3Int Dimension;
    public float BlockSize;
    public List<GameObject> TetrisPiece;

    private List<Material> meterialPieces;
    private int[][][] grid;
    private List<GameObject> gridBlocks;
    
    private void Start()
    {
        //Build color array
        meterialPieces = new List<Material>();

        foreach (GameObject go in TetrisPiece)
        {
            meterialPieces.Add(go.GetComponent<TetrisPiece>().ColoredMaterial);
        }

        gridBlocks = new List<GameObject>();
        
        BuildEmptyGrid();

        IEnumerator RandomLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                
                Random(10);
                RenderGrid();
            }
        }

        StartCoroutine(RandomLoop());
    }

    private void Random(int count)
    {
        BuildEmptyGrid();

        for (int i = 0; i < count; i++)
        {
            int x = UnityEngine.Random.Range(0, Dimension.x);
            int y = UnityEngine.Random.Range(0, Dimension.y);
            int z = UnityEngine.Random.Range(0, Dimension.z);
            int c = UnityEngine.Random.Range(0, meterialPieces.Count - 1);

            grid[x][y][z] = c;
        }
    }
    private void Tick()
    {
        ClearGridRender();
        RenderGrid();
    }
    private void BuildEmptyGrid()
    {
        grid = new int[Dimension.x][][];

        for (int x = 0; x < Dimension.x; x++)
        {
            grid[x] = new int[Dimension.y][];
            for (int y = 0; y < Dimension.y; y++)
            {
                grid[x][y] = new int[Dimension.z];
            }
        }
    }

    private void ClearGridRender()
    {
        foreach (GameObject go in gridBlocks)
        {
            Destroy(go);
        }

        gridBlocks = new List<GameObject>();
    }

    private void RenderGrid()
    {
        for (int x = 0; x < Dimension.x; x++)
        {
            for (int y = 0; y < Dimension.y; y++)
            {
                for (int z = 0; z < Dimension.z; z++)
                {
                    int id = grid[x][y][z];

                    if (id == 0)
                    {
                        continue;
                    }

                    Vector3 locPos = new Vector3(x, y, z) * BlockSize;
                    GameObject block = GenerateBlock(meterialPieces[id - 1]);
                    block.transform.position = transform.position + locPos + (Vector3.one * BlockSize/2);
                }
            }
        }
    }

    private GameObject GenerateBlock(Material material)
    {
        GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
        block.GetComponent<MeshRenderer>().sharedMaterial = material;
        
        gridBlocks.Add(block);
        return block;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 d = new Vector3(Dimension.x, Dimension.y, Dimension.z);
        Gizmos.DrawWireCube(transform.position + d/2, d*BlockSize);
    }
}
