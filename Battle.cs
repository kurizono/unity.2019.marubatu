using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SceneManagement;

public class Battle : MonoBehaviour
{
    opning opningcs;

    public AudioSource se;
    public AudioClip se1;
    public AudioClip se2;
    public AudioClip sewin;

    public GameObject select;
    Vector3[] selectposi = new Vector3[6];

    public Text player1, player2;
    public Text[] mark = new Text[9];          //〇×表示
    public Text[] result = new Text [4];       //結果
    public Text[] tatp = new Text[9];          //最小tp
    public Text[] trtp = new Text[2];          //残りtp
    public Text[] tplace = new Text[2];        //投票場所
    public Text[] tvtp = new Text[2];          //投票tp
    public Text check;                         //確認画面

    int[] atp = { 1000, 500, 1000, 500, 2000, 500, 1000, 500, 1000 };　  //最小tp
    int[] rtp = { 20000, 20000 };                   //残りtp
    int[] judge = { 3, 3, 3, 3, 3, 3, 3, 3, 3 };   //ジャッジ用
    int[][] bingo = { new int[] { 0, 1, 2 }, new int[] { 3, 4, 5 }, new int[] { 6, 7, 8 }, new int[] { 0, 3, 6 }, new int[] { 1, 4, 7 }, new int[] { 2, 5, 8 }, new int[] { 0, 4, 8 }, new int[] { 2, 4, 6 } };
    
    int[] win = { 0, 0 };                           //勝敗判定
    int[] place = { 0, 0 };                         //投票場所
    int[] vtp = { 500, 500 };                      //投票tp

    
    public Transform player01, player10, player11;          //player1,player2,確認画面
    public Transform rule;      //ルール説明
    public Transform cube;      //ルール説明の時の背景
    public Transform filed;
    public Transform marker;
    public Transform Check;
    Vector3 ruru;

    int explain = 0;   //ルール
    int page = 0;   //ルール
    int n = 1;      //進行             
    int m = 1;      //for用               
    int i = 1;      //player                   
    int r = 0;      //ログ表記用             
    int o = 0;      //ログ表記用                    
    int y = 0;      //進行2用
    int z = 0;      //for2用
    int a = 0;      //勝敗判定用
    int[] b = { 0,0 };      //勝敗判定用2
    int q = 3;      //最終勝敗判定用

    // Start is called before the first frame update
    void Start()
    {
        opningcs = GetComponent<opning>();

        se = GetComponent<AudioSource>();

        selectposi[0] = new Vector3(100, 100, 0);
        selectposi[1] = new Vector3(-6.5f, -3.2f, 0);
        selectposi[2] = new Vector3(-6.5f, -4.9f, 0);
        selectposi[3] = new Vector3(7.3f, -3.2f, 0);
        selectposi[4] = new Vector3(7.3f, -4.9f, 0);
        selectposi[5] = new Vector3(100, 100, 0);
        select.transform.position = selectposi[1];

        Vector3 ruru = new Vector3(5000, 3700.5f, 0);
        rule.position = ruru;
        Vector3 Cube = new Vector3(0, -100, 0);
        cube.position = Cube;

        if (opning.setting == 0)
        {
            Vector3 player001 = new Vector3(-100, -12, -1);
            player01.localPosition = player001;
            Vector3 player002 = new Vector3(-100, -12, -1);
            player10.localPosition = player002;
            Vector3 player003 = new Vector3(0, -100, 50);
            player11.localPosition = player003;
        }
        else
        {
            Vector3 player001 = new Vector3(33, -11.5f, -1);
            player01.localPosition = player001;
            Vector3 player002 = new Vector3(-100, -15, -1);
            player10.localPosition = player002;
            Vector3 player003 = new Vector3(0, -100, 50);
            player11.localPosition = player003;
        }

        player1.text = opning.getname1();
        player2.text = opning.getname2();
        check.text = "";

        

        for (m = 0; m < 9; m++)
        {
            mark[m].text = "";
            tatp[m].text = atp[m].ToString();
        }
        for (m = 0; m < 4; m++)
        {
            result[m].text = "";
        }
        for (m = 0; m < 2; m++)
        {
            trtp[m].text = rtp[m].ToString();
            tvtp[m].text = vtp[m].ToString();
            tplace[m].text = "a";
        }
        r = 1;
}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("opning");
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
                    filed.position = new Vector3(1000, 2.55f, 0);
                    marker.localPosition = new Vector3(-1000, -85, 82.28909f);
                    Check.localPosition = new Vector3(1000, -160, 88);
                    break;
                case 1:
                    explain = 0;
                    Vector3 Rule0 = new Vector3(0, -50000, 0);
                    rule.position = Rule0;
                    Vector3 Cube0 = new Vector3(0, -100, 0);
                    cube.position = Cube0;
                    filed.position = new Vector3(0, 2.55f, 0);
                    marker.localPosition = new Vector3(-49, -85, 82.28909f);
                    Check.localPosition = new Vector3(0, -160, 88);
                    break;
            }
        }


        if (explain == 0) {         //ルールを表示していない場合
            if (q == 3)             //最終決定が終了してない場合
            {
                switch (y)
                {
                    case 0:
                        if (Input.GetKeyDown(KeyCode.Return) && n < 5)
                        {   //ここから投票場所
                            if (opning.getsetting() == 1 && (n == 2 || n == 4))
                            {
                                playerchange();
                            }
                            n++;
                            if (opning.setting == 1 & n == 2 || n == 4 || opning.setting == 0)
                            {
                                select.transform.position = selectposi[n];
                            }
                        }
                        if (Input.GetKeyDown(KeyCode.Backspace) && (n == 2 || n == 4))  //ここから投票tp
                        {
                            n--;        //n = 1||3
                            select.transform.position = selectposi[n];
                        }
                        if (n == 1 || n == 3)             //投票場所
                        {
                            i = n / 2;
                            numplace();
                        }
                        else if (n == 2 || n == 4)             //投票tp
                        {
                            i = n / 2 - 1;
                            numtp();
                        }
                        else if (n == 5)
                        {
                            for (i = 0; i < 2; i++)
                            {
                                action();
                            }
                            for (i = 0; i < 2; i++)
                            {

                                winlose();                              //iの勝敗判定１
                            }
                            for (i = 0; i < 2; i++)
                            {
                                reset();
                            }
                            winlose2();
                            n = 1;
                        }
                        break;

                    case 1:
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            check.text = "";
                            playercheck();
                        }
                        break;
                    case 2:
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            Vector3 player001 = new Vector3(33, -11.5f, -1);
                            player01.localPosition = player001;
                            Vector3 player002 = new Vector3(-100, -12, -1);
                            player10.position = player002;
                            Vector3 player003 = new Vector3(0, -100, 50);
                            player11.position = player003;
                            select.transform.position = selectposi[n];
                            y = 0;
                            check.text = "";
                        }
                        break;
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
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
    }


    private void numplace()                     //投票場所変更
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            place[i] = (place[i] + 6) % 9;
            voteplace();
            //+6
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            place[i] = (place[i] + 3) % 9;
            voteplace();
            //+3
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            place[i] = (place[i] + 8) % 9;
            voteplace();
            //+8
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            place[i] = (place[i] + 1) % 9;
            voteplace();
            //+1
        }
    }


    private void voteplace()            //投票場所表示
    {
        switch(place[i])
        {
            case 0:
                tplace[i].text = "a";
                break;
            case 1:
                tplace[i].text = "b";
                break;
            case 2:
                tplace[i].text = "c";
                break;
            case 3:
                tplace[i].text = "d";
                break;
            case 4:
                tplace[i].text = "e";
                break;
            case 5:
                tplace[i].text = "f";
                break;
            case 6:
                tplace[i].text = "g";
                break;
            case 7:
                tplace[i].text = "h";
                break;
            case 8:
                tplace[i].text = "i";
                break;
        }
    }


    private void numtp()                //投票pt
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && vtp[i] <= rtp[i]-100)
        {
            vtp[i] = vtp[i] + 100;
            tvtp[i].text = vtp[i].ToString();
            //+100
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && vtp[i] >= 600)
        {
            vtp[i] = vtp[i] - 100;
            tvtp[i].text = vtp[i].ToString();
            //-100
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && vtp[i] >= 1500)
        {
            vtp[i] = vtp[i] - 1000;
            tvtp[i].text = vtp[i].ToString();
            //-1000
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && vtp[i] <= rtp[i]-1000)
        {
            vtp[i] = vtp[i] + 1000;
            tvtp[i].text = vtp[i].ToString();
            //+1000
        }
    }


    private void action()               //ターンの処理まとめ
    {
        if (vtp[i] >= atp[place[i]])
        {
            if (place[i] != place[(i + 1) % 2] || vtp[i] > vtp[(i + 1) % 2]) {
                //投票ｔｐがそのマスの最低値以上　かつ　(２人が違うマスに投票している　または　自分の投票tpが相手の投票tpよりも多い)

                switch (i)
                {
                    case 0:
                        mark[place[i]].color = new Color(255f / 255f, 0f / 255f, 0f / 255f);
                        mark[place[i]].text = "〇";       //player1は〇
                        break;
                    case 1:
                        mark[place[i]].color = new Color(0f / 255f, 0f / 255f, 255f / 255f);
                        mark[place[i]].text = "✕";       //player2は✕
                        break;
                }
                judge[place[i]] = i;                //judge[投票場所]の値をiと同じ値に(0or1)
                rtp[i] = rtp[i] - vtp[i];           //残りtpは残りtpから投票tpを引いた値
                trtp[i].text = rtp[i].ToString();
                atp[place[i]] = vtp[i] + 100;       //投票したとこの次からの最低値tpは投票tp＋100
                if (opning.getsetting() == 0)
                {
                    tatp[place[i]].text = atp[place[i]].ToString();
                }
            }
        }
        if (r < 13)
        {
            m = i * 2;
        }
        else {
            m = i * 2 + 1;
        }
        o = (r + 1) / 2;
        StringBuilder sb = new StringBuilder();
        sb.Append(place[i] + " " + vtp[i] + "tp");
        result[m].text += o.ToString("00") + ":" + tplace[i].text + ":" + vtp[i].ToString("00000")+ "\r\n";
        n = 1;
        r++;
        //winlose();                              //iの勝敗判定１
    }

    public void reset()
    {
        vtp[i] = 500;                           //投票tpの初期設定を500
        tvtp[i].text = vtp[i].ToString();
        place[i] = 0;                           //投票場所の初期設定をa
        tplace[i].text = "a";
    }


    public void playerchange()      //選手変更
    {
        y = 1;       //check画面
        select.transform.position = selectposi[0];
        if (n == 2)  //player1→2
        {
            Vector3 player001 = new Vector3(33, -11.5f, -1);
            player01.localPosition = player001;
            Vector3 player002 = new Vector3(-33, -11.5f, -1);
            player10.localPosition = player002;
            Vector3 player003 = new Vector3(0, -25, 50);
            player11.localPosition = player003;
            check.text = "確認"+ "\r\n" + "あなたは"+ opning.getname2()+"ですか？" + "\r\n" + "“はい”ならEnter";
            se.PlayOneShot(se2);
        }
        if (n == 4)  //player2→result
        {
            Vector3 player001 = new Vector3(33, -11.5f, -1);
            player01.localPosition = player001;
            Vector3 player002 = new Vector3(-33, -11.5f, -1);
            player10.localPosition = player002;
            Vector3 player003 = new Vector3(0, -25, 50);
            player11.localPosition = player003;
            check.text = "確認" + "\r\n" + "両者とも" + "結果を確認しましたか？" + "\r\n" + "“はい”ならEnter";
        }
    }


    public void playercheck()       //選手変更チェック
    {
        if (n == 3)
        {
            Vector3 player001 = new Vector3(100, -12, -1);
            player01.position = player001;
            Vector3 player002 = new Vector3(-33, -11.5f, -1);
            player10.localPosition = player002;
            Vector3 player003 = new Vector3(0, -100, 50);
            player11.position = player003;
            if (opning.setting == 1)
            {
                select.transform.position = selectposi[n];
            }
            y = 0;
        }
        if (n == 1)
        {
            check.text = "確認" + "\r\n" + "あなたは" + opning.getname1() + "ですか？" + "\r\n" + "“はい”ならEnter";
            se.PlayOneShot(se1);
            y = 2;
        }
    }


    public void winlose()       //勝敗判定１
    {
        b[i] = 0;
        for (m = 0; m < 8; m++)
        {
            a = 0;
            for (z = 0; z < 3; z++)
            {
                if (judge[bingo[m][z]] == i)
                {
                    a++;
                }

            }
            if (a == 3)     //ビンゴがあったならば
            {
                b[i]++;
            }
        }        
    }

    public void winlose2()
    {
        if (b[0] == b[1])           //ビンゴの数が同じ場合
        {
            if (rtp[0] < 500)       //player1が500未満の時
            {
                if (rtp[1] < 500)   //ドロー
                {
                    q = 2;
                }
                else                //player1のオーバー
                {
                    q = 1;
                }
            }
            else                    //player1が500未満でない時
            {   
                if (rtp[1] < 500)   //player2のオーバー
                {
                    q = 0;          
                }
            }
        }
        else if (b[0] < b[1])       //ビンゴの数に差がある場合
        {
            q = 1;
        }
        else
        {
            q = 0;
        }
        if (q != 3)
        {
            Vector3 player001 = new Vector3(0, -18.5f, 5);
            player01.position = player001;
            Vector3 player002 = new Vector3(100, -12, -1);
            player10.position = player002;
            Vector3 player003 = new Vector3(0, -25, 50);
            player11.position = player003;
        }
        switch (q)
        {
            case 0:
                check.text = "\r\n" + opning.getname1() + "の勝利です" + "\r\n" + "タイトルに戻るにはESCキー";
                se.PlayOneShot(sewin);
                break;
            case 1:
                check.text = "\r\n" + opning.getname2() + "の勝利です" + "\r\n" + "タイトルに戻るにはESCキー";
                se.PlayOneShot(sewin);
                break;
            case 2:
                check.text = "\r\n" + "引き分けです" + "\r\n" + "タイトルに戻るにはESCキー";
                se.PlayOneShot(sewin);
                break;
            case 3:
                break;         
        }           
    }
}
