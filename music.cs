using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class music: MonoBehaviour
{
    int count = 0;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        if (count == 0)
        {
            SceneManager.LoadScene("opning");
            count = 1;
        }    
    }
 }