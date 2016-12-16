﻿using UnityEngine;
using UnityEngine.UI;

public class HexGrid : AManager<HexGrid> {

	public int width;
	public int height;
	public Color[] colors;

	public HexCell cellPrefab;

	public HexCell[] cells;

	HexMesh hexMesh;

	void Awake () {

		hexMesh = GetComponentInChildren<HexMesh>();

		cells = new HexCell[height * width];

		for (int z = 0, i = 0; z < height; z++) {
			for (int x = 0; x < width; x++) {
				CreateCell(x, z, i++);
			}
		}
	    InvokeRepeating("Refresh", 1f, .1f);
	}

	public Vector3 GetRandomPos()
	{
		Vector3 pos = cells[Random.Range(0, cells.Length - 1)].transform.position;
		return pos;
	}
	public HexCell GetRandomCell()
	{
		HexCell cell = cells[Random.Range(0, cells.Length - 1)];
		return cell;
	}
	void Start () {
		hexMesh.Triangulate(cells);
	}

	public HexCell GetCell (Vector3 position) {
		position = transform.InverseTransformPoint(position);
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
		return cells[index];
	}

	public void Refresh () {
		hexMesh.Triangulate(cells);
	}

	void CreateCell (int x, int z, int i) {
		Vector3 position;
		position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f);

		HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.CellId = i;
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
		cell.transform.name = "" + cell.CellId + " " + HexCoordinates.FromOffsetCoordinates(x, z);

		cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);

		if (x > 0) {
			cell.SetNeighbor(HexDirection.W, cells[i - 1]);
		}
		if (z > 0) {
			if ((z & 1) == 0) {
				cell.SetNeighbor(HexDirection.SE, cells[i - width]);
				if (x > 0) {
					cell.SetNeighbor(HexDirection.SW, cells[i - width - 1]);
				}
			}
			else {
				cell.SetNeighbor(HexDirection.SW, cells[i - width]);
				if (x < width - 1) {
					cell.SetNeighbor(HexDirection.SE, cells[i - width + 1]);
				}
			}
		}
	}
	
	public void TouchCell(Vector3 position)
	{
		HexCell cell = GetCell(position);
		EventManager.TriggerEvent("PlacementMap");
		//cancel previous invokes
		//cell.GetComponent<CellInterface>().CancelInvoke();
		//cell.GetComponent<CellLogic>().CancelInvoke();
		//CancelInvoke();
		//show menu
		//cell.GetComponent<CellInterface>().Invoke("DisplayCell", .1f);
		//make selected cell black
		cell.GetComponent<CellLogic>().Invoke("CellSelected", .05f);
		//Invoke("Refresh", .1f);
	}
}