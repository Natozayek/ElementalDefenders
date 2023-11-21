using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> trees;

    [SerializeField]
    private int treeCount;

    [SerializeField]
    private Vector3 size;

    public void Generate()
    {
        for(int i = 0; i < treeCount; i++)
        {
            var tree = Instantiate(trees[Random.Range(0, trees.Count)], transform);
            Vector3 newPos = new Vector3();
            newPos.x = Random.Range(-size.x / 2, size.x / 2);
            newPos.y = Random.Range(-size.y / 2, size.y / 2);
            newPos.z = Random.Range(-size.z / 2, size.z / 2);

            tree.transform.position = tree.transform.position + newPos;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawCube(this.transform.position, size);
    }
}
