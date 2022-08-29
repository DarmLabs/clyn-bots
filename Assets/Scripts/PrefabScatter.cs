using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PrefabScatter : MonoBehaviour
{
    GameObject[] prefab;
    [SerializeField] float spaceAboveGround = 0.2f;
    float yPos;
    bool isAboveOther;
    [SerializeField] string targetFolder;
    [SerializeField] int xMin, xMax;
    [SerializeField] int zMin, zMax;
    [SerializeField] int prefabCount;

    void Awake()
    {
        prefab = Resources.LoadAll(targetFolder, typeof(GameObject))
            .Cast<GameObject>()
            .ToArray();
    }
    void Start()
    {
        for (int i = 0; i < prefabCount; i++)
        {
            isAboveOther = false;
            int xPos = Random.Range(xMin, xMax);
            int zPos = Random.Range(zMin, zMax);
            CheckHeight(xPos, zPos);
            int randomIndex = Random.Range(0, prefab.Length);
            if (!isAboveOther)
            {
                Instantiate(prefab[randomIndex], new Vector3(transform.position.x + xPos, yPos, transform.position.z + zPos), Quaternion.Euler(prefab[randomIndex].transform.eulerAngles.x, Random.Range(0, 360), prefab[randomIndex].transform.eulerAngles.z), transform);
            }
            else
            {
                i--;
            }
        }
    }
    void CheckHeight(int xPos, int zPos)
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x + xPos, 100, transform.position.z + zPos), Vector3.down, out hit) && hit.transform.gameObject.tag == "Terrain")
        {
            yPos = hit.point.y + spaceAboveGround;
        }
        else
        {
            isAboveOther = true;
        }
    }
}
