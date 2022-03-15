using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    [Header("Настройка объектов")]
    public GameObject Exit;
    public GameObject LostExplosion;
    public Text endgamer;
    [HideInInspector]
    public int remains = 0;
    [HideInInspector]
    public static bool alive = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (PlayerStats.Lives <= 0)
        {
           EndGameLost();
        }


    }

    void EndGameLost()
    {
        if (alive)
        {
            GameObject imp_inst = (GameObject)Instantiate(LostExplosion, Exit.transform.position, Exit.transform.rotation);
            Destroy(imp_inst, 2.0f);

            Destroy(Exit.gameObject);
            endgamer.text = "You lost!";
            alive = false;
        }

        

    }

}
