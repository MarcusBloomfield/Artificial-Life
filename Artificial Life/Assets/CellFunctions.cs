using System.Collections.Generic;
using UnityEngine;

public class CellFunctions : MonoBehaviour
{
    [SerializeField] GameObject sphere;

    List<Rigidbody[]> all = new List<Rigidbody[]>();

    [SerializeField] float bounds;

    [SerializeField] int relationShipAmount;
    List<Vector4> relationShips = new List<Vector4>();

    [SerializeField] int typeAmount = 4;
    [SerializeField] int cellsPerType = 100;

    float timer = 0;
    private void Start()
    {
        GenerateCells();
        GenerateRelationShips();
    }
    void GenerateCells()
    {
        for (int i = 0; i < typeAmount; i++)
        {
            Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
            Rigidbody[] cells = Spawn(sphere, cellsPerType);
            for (int k = 0; k < cells.Length; k++)
            {
                cells[k].GetComponent<MeshRenderer>().materials[0].color = color;
                cells[k].GetComponent<MeshRenderer>().materials[0].EnableKeyword("_EMISSION");
                cells[k].GetComponent<MeshRenderer>().materials[0].SetColor("_EmissiveColor", color * 2);
                cells[k].GetComponent<MeshRenderer>().materials[0].SetInt("_EmissiveIntensityUnit", 2);
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
        ProcessRelationShips();
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            Application.LoadLevel(Application.loadedLevel);
        }
        timer += 1 * Time.deltaTime;
        if (timer > 60)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
    bool cycle = true;
    void ProcessRelationShips()
    {
        if (cycle)
        {
            for (int i = 0; i < relationShips.Count / 2; i++)
            {
                interprateRelationShip(relationShips[i]);
            }
            cycle = false;
        }
        else
        {
            for (int i = relationShips.Count / 2; i < relationShips.Count; i++)
            {
                interprateRelationShip(relationShips[i]);
            }
            cycle = true;
        }
    }
    Rigidbody[] Spawn(GameObject type, int amount)
    {
        Rigidbody[] items = new Rigidbody[amount];
        float size = Random.Range(1f, 5f);
        Vector3 scale = new Vector3(size, size, size);
        for (int i = 0; i < amount; i++)
        {
            items[i] = Instantiate(type, new Vector3(Random.Range(-bounds, bounds), Random.Range(-bounds, bounds), Random.Range(-bounds, bounds)), Quaternion.identity).GetComponent<Rigidbody>();
            items[i].transform.localScale = scale;
        }
        return items;
    }
    Vector4 SetRelationShip(float x, float z)
    {
        return new Vector4(x, Random.Range(0, all.Count), z, Random.Range(30f, 100f));
    }
    void interprateRelationShip(Vector4 vector4)
    {
        Simulate(all[(int)vector4.x], all[(int)vector4.y], vector4.z, vector4.w);
    }
    void Simulate(Rigidbody[] first, Rigidbody[] second, float force, float effectRange)
    {
        for (int i = 0; i < first.Length; i++)
        {
            Vector3 targetPosition = second[Random.Range(0, second.Length)].transform.position;
            float distance = Vector3.Distance(first[i].transform.position, targetPosition);
            if (distance > 0 && distance < effectRange)
            {
                first[i].transform.LookAt(targetPosition);
                first[i].velocity += first[i].transform.forward * force / distance;
            }
        }
    }
}
