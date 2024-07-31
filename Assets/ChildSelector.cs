using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSelector : MonoBehaviour
{

    void Start()
    {
        SelectChild();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectChild();
        }
    }

    void SelectChild()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            child.gameObject.SetActive(false);
        }
        var randomIndex = Random.Range(0, transform.childCount);
        var randomChild = transform.GetChild(randomIndex);
        randomChild.gameObject.SetActive(true);
    }
}
