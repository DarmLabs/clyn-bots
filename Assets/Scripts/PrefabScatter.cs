using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PrefabScatter : MonoBehaviour
{
    [SerializeField] GameObject[] trash;
    [SerializeField] float spaceAboveGround = 0.2f;
    [SerializeField] float yPos;
    [SerializeField] bool isAboveOther;

    void Awake()
    {
        trash = Resources.LoadAll("Basuras", typeof(GameObject))
            .Cast<GameObject>()
            .ToArray();
    }
    void Start()
    {
        for (int i = 0; i < 500; i++)
        {
            isAboveOther = false;
            int xPos = Random.Range(-97, 0);
            int zPos = Random.Range(0, 100);
            CheckHeight(xPos, zPos);
            int randomIndex = Random.Range(0, trash.Length);
            if (!isAboveOther)
            {
                Instantiate(trash[randomIndex], new Vector3(transform.position.x + xPos, yPos, transform.position.z + zPos), Quaternion.Euler(trash[randomIndex].transform.eulerAngles.x, Random.Range(0, 360), trash[randomIndex].transform.eulerAngles.z), transform);
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
