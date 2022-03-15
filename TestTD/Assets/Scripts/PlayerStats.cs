using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    public static int Money;
    public static int Lives;
    public Text Live_Text;
    public int StartMoney = 400;
    public int StartLives = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        Money = StartMoney;
        Lives = StartLives;
    }

    // Update is called once per frame
    void Update()
    {
        Live_Text.text = "Lives: " + Lives;

        
       
    }

    
}

