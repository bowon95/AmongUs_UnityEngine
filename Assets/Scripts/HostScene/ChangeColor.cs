using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    bool m_ChangeOn = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(m_ChangeOn)
            GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void SetColor()
    {
        m_ChangeOn = true;
    }
}
