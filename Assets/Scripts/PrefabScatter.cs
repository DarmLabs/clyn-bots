using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PrefabScatter : MonoBehaviour
{
    [SerializeField] GameObject[] trash;
    [SerializeField] float spaceAboveGround = 0.5f;

    void Awake()
    {
        trash = Resources.LoadAll("Basuras", typeof(GameObject))
            .Cast<GameObject>()
            .ToArray();
    }
    void Start()
    {
        for (int i = 0; i < 1000; i++)
        {
            int xPos = Random.Range(-73, 76);
            int zPos = Random.Range(-122, 128);
            Instantiate(trash[Random.Range(0, trash.Length + 1)], new Vector3(xPos, CalculateHeight(xPos, zPos), zPos), Quaternion.identity);
        }
    }

    float CalculateHeight(int xPos, int zPos)
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(xPos, 10, zPos), Vector3.down, out hit) && hit.transform.gameObject.tag != "Water")
        {
            return hit.point.y + spaceAboveGround;
        }
        return 0;
    }
}
