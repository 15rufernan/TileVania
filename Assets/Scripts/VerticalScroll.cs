using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0f, scrollSpeed * Time.deltaTime));
    }
}
