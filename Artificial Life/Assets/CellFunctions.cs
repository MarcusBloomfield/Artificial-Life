using System.Collections.Generic;
using UnityEngine;

public class CellFunctions : MonoBehaviour
{
    [SerializeField] GameObject cube;

    List<Rigidbody[]> all = new List<Rigidbody[]>();

    [SerializeField] float bounds;

    [SerializeField] int relationShipAmount;
    List<Vector4> relationShips = new List<Vector4>();

    [SerializeField] int typeAmount = 4;
    [SerializeField] int cellsPerType = 100;
    private void Start()
    {
        for (int i = 0; i < typeAmount; i++)
        {
            Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
            Rigidbody[] cubes = Spawn(cube, cellsPerType);
            for (int k = 0; k < cubes.Length; k++)
            {
                cubes[k].GetComponent<MeshRenderer>().materials[0].color = color;
            }
            all.Add(cubes);
        }
        for (int i = 0; i < relationShipAmount; i++)
        {
            relationShips.Add(SetRelationShip());
        }
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < relationShips.Count; i++)
        {
            interprateRelationShip(relationShips[i]);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
    Rigidbody[] Spawn(GameObject type, int amount)
    {
        Rigidbody[] items = new Rigidbody[amount];
        Vector3 scale = new Vector3(Random.Range(1f, 5f), Random.Range(1f, 5f), Random.Range(1f, 5f));
        for (int i = 0; i < amount; i++)
        {
            items[i] = Instantiate(type, new Vector3(Random.Range(-bounds, bounds), Random.Range(-bounds, bounds), Random.Range(-bounds, bounds)), Quaternion.identity).GetComponent<Rigidbody>();
            items[i].transform.localScale = scale;
        }
        return items;
    }
    Vector4 SetRelationShip()
    {
        return new Vector4(Random.Range(0, all.Count), Random.Range(0, all.Count), Random.Range(-10f, 10f), Random.Range(30f, 100f));
    }
    void interprateRelationShip(Vector4 vector4)
    {
        Simulate(all[(int)vector4.x], all[(int)vector4.y], vector4.z, vector4.w);
    }
    void Simulate(Rigidbody[] first, Rigidbody[] second, float force, float effectRange)
    {
        if (force != 0)
        {
            for (int i = 0; i < first.Length; i++)
            {
                Rigidbody obj = second[Random.Range(0, second.Length)];
                float distance = Vector3.Distance(first[i].transform.position, obj.transform.position);
                float a = force / distance;
                if (distance > 0 && distance < effectRange)
                {
                    first[i].transform.LookAt(obj.transform.position);
                    first[i].velocity += first[i].transform.forward * a;
                }
            }
        }
    }
}
