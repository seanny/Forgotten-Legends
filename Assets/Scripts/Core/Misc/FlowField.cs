//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public abstract class FlowField : MonoBehaviour
{
    public float unitsPerCell = 0.5f;

    protected Renderer m_Renderer;
    protected Vector3 m_BoundsSize;
    protected DebugLineRenderer m_LineRenderer;

    public Vector3[,] Grid { get; protected set; }

    // Use this for initialization
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        m_LineRenderer = GetComponent<DebugLineRenderer>();
        m_BoundsSize = m_Renderer.bounds.extents * 2;

        Grid = new Vector3[(int)(m_BoundsSize.x / unitsPerCell), (int)(m_BoundsSize.z / unitsPerCell)];
    }

    protected abstract void SetFlowVectors();

    public Vector3? GetFlowVectors(Vector3 worldPosition)
    {
        Vector3 origin = transform.position - m_Renderer.bounds.extents;
        //Get position relative to bottom left corner of flow field
        Vector3 localPos = worldPosition - origin;

        //Divide by the cell size in each direction to determine the index in the grid array
        int x = (int)(localPos.x / unitsPerCell);
        int z = (int)(localPos.z / unitsPerCell);

        //Confirm that each index is valid in the Grid array, otherwise return null
        if (IndexInGrid(x, 0) && IndexInGrid(z, 1))
            return Grid[x, z];
        else
            return null;
    }

    private bool IndexInGrid(int index, int dimension)
    {
        return index >= 0 && index < Grid.GetLength(dimension);
    }

    private void DrawDebugLines()
    {
        Vector3 origin = transform.position - m_Renderer.bounds.extents;

        for (int x = 0; x < Grid.GetLength(0); x++)
        {
            for (int z = 0; z < Grid.GetLength(1); z++)
            {
                Vector3 direction = Grid[x, z];
                Vector3 localPos = new Vector3(x, 0, z) * unitsPerCell;
                Vector3 worldPos = origin + localPos;
                m_LineRenderer.DrawLine(0, worldPos, worldPos + direction);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        DrawDebugLines();
    }
}
