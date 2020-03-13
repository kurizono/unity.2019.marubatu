using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class opning : MonoBehaviour
{
    music musiccs;
    Battle battlecs;

    public Text player1, player2;
    public Text Mode;
    public static string name1, name2;
    public static int setting = 1;

    public Transform rule;      //ルール説明
    public Transform cube;      //ルール説明の時の背景

    int explain = 0;
    int page = 0;

    Vector3 ruru;

    public int music = 0;

    // Start is called before the first frame update
    void Start()
    {
        musiccs = GetComponent<music>();

        setting = 1;

        Vector3 ruru = new Vector3(5000, 3700.5f, 0);
        rule.position = ruru;
        Vector3 Cube = new Vector3(0, -100, 0);
        cube.position = Cube;
    }

    // Update is called once per frame
    void Update()
    {
        if (explain == 0)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                name1 = player1.text;
                name2 = player2.text;
                SceneManager.LoadScene("Battle");
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                switch (setting)
                {
                    case 0:

                        setting = 1;
                        Mode.text = "mode:battle";
                        break;
                    case 1:
                        setting = 0;
                        Mode.text = "mode:dealer";
                        break;
                }
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (page > 0)
                {
                    page = page - 1;
                    ruru.x = rule.position.x + 150;
                    ruru.y = 1;
                    rule.position = ruru;
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (page < 6)
                {
                    page = page + 1;
                    ruru.x = rule.position.x - 150;
                    ruru.y = 1;
                    rule.position = ruru;
                }
            }

        }
        

        if (Input.GetKeyDown(KeyCode.X))
        {
            switch (explain)
            {
                case 0:
                    explain = 1;
                    Vector3 Rule1 = new Vector3(0, 1, 0);
                    rule.position = Rule1;
                    Vector3 Cube1 = new Vector3(0, 0, 0);
                    cube.position = Cube1;
                    page = 0;
                    break;
                case 1:
                    explain = 0;
                    Vector3 Rule0 = new Vector3(0, -50000, 0);
                    rule.position = Rule0;
                    Vector3 Cube0 = new Vector3(0, -100, 0);
                    cube.position = Cube0;
                    break;
            }
        }
    }

   
   

    public static string getname1()
    {
        return name1;
    }
    public static string getname2()
    {
        return name2;
    }
    public static int getsetting()
    {
        return setting;
    }
}
