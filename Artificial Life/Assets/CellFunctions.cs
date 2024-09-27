using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using TMPro;

public class CellFunctions : MonoBehaviour
{
    [SerializeField] GameObject sphere;

    List<Cell[]> all = new List<Cell[]>();

    [SerializeField] float bounds;

    [SerializeField] int relationShipAmount;
    List<Vector4> relationShips = new List<Vector4>();

    [SerializeField] int typeAmount = 4;
    [SerializeField] int cellsPerType = 100;

    List<Cell> cells = new List<Cell>();
    float timer  = 0;

    private void Start()
    {
        GenerateCells();
        GenerateRelationShips();
        InvokeRepeating("ProcessRelationShips", 1, .1f);
    }
    void GenerateCells()
    {
        for (int i = 0; i < typeAmount; i++)
        {
            Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
            Cell[] cells = Spawn(sphere, cellsPerType);
            for (int k = 0; k < cells.Length; k++)
            {
                cells[k].GetComponent<MeshRenderer>().materials[0].color = color;
                cells[k].GetComponent<MeshRenderer>().materials[0].EnableKeyword("_EMISSION");
                cells[k].GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", color * 2);
            }
            all.Add(cells);
        }
    }
    void GenerateRelationShips()
    {
        for (int i = 0; i < typeAmount; i++)
        {
            relationShips.Add(SetRelationShip(i, Random.Range(50, 100f) * 2));
        }
        for (int i = 0; i < relationShipAmount - typeAmount; i++)
        {
            relationShips.Add(SetRelationShip(Random.Range(0, all.Count), Random.Range(-100f, 100f) * 2));
        }
    }
    private void Update()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].Move();
        }
        timer += 1 * Time.deltaTime;

        if(timer > 60)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
    bool cunt = false;
    void ProcessRelationShips()
    {
        if (cunt== false)
        {
            for (int i = 0; i < relationShips.Count / 2; i++)
            {
                Simulate(all[(int)relationShips[i].x], all[(int)relationShips[i].y], relationShips[i].z, relationShips[i].w);
            }
            cunt = true;
        }
        else
        {
            for (int i = relationShips.Count / 2; i < relationShips.Count; i++)
            {
                Simulate(all[(int)relationShips[i].x], all[(int)relationShips[i].y], relationShips[i].z, relationShips[i].w);
            }
            cunt = false;
        }
    }
    Cell[] Spawn(GameObject type, int amount)
    {
        Cell[] items = new Cell[amount];
        float size = Random.Range(1f, 5f);
        Vector3 scale = new Vector3(size, size, size);
        for (int i = 0; i < amount; i++)
        {
            items[i] = Instantiate(type, new Vector3(Random.Range(-bounds, bounds), Random.Range(-bounds, bounds), Random.Range(-bounds, bounds)), Quaternion.identity).GetComponent<Cell>();
            items[i].body = items[i].GetComponent<Rigidbody>();
            items[i].transform.localScale = scale;
            cells.Add(items[i]);
        }
        return items;
    }
    Vector4 SetRelationShip(float x, float z)
    {
        return new Vector4(x, Random.Range(0, all.Count), z, Random.Range(30f, 100f));
    }
    void Simulate(Cell[] first, Cell[] second, float force, float effectRange)
    {
        for (int i = 0; i < first.Length; i++)
        {

            first[i].target = second[Random.Range(0, second.Length)];
            float distance = Vector3.Distance(first[i].transform.position, first[i].target.transform.position);
            first[i].force = force / distance;
            first[i].effectRange = effectRange;
            first[i].distance = distance;
        }
    }
}
