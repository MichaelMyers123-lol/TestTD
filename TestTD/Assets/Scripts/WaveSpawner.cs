using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [Header("Настройка объектов в юнити")]
    public GameObject Spawner;
    public GameObject WonExplosion;
    public Transform enemyPrefab;
    public Text TextwaveNumber;
    public Text endgamer;
    [Header("Настройка игры")]
    public float TimeBetweenWaves = 5f;
    public float counter= 10f;
    public  int waveNumber =1;
    public  int NumberOfWaves = 1;


    public static int remainings = 0;
    public static bool checker = false;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Проверяем жив ли игрок чтобы код не выполнялся до бесконечности
        if (GM.alive)
        {
            //Проверяем есть ли живые враги
            if (remainings <= 0)
            {
                //Счётчик
                counter -= Time.deltaTime;
                counter = Mathf.Clamp(counter, 0f, Mathf.Infinity);


                    TextwaveNumber.text = "Wave in: " + string.Format("{0:00.00}", counter);
                
                //Если счётчик меньше или равен 0 , то начинаем спавнить ребят
                if (counter <= 0f)
                {
                    if (waveNumber <= NumberOfWaves)
                    {
                        //Счётчик живых равен количеству ребят, которые будут созданы
                        remainings = waveNumber * waveNumber + 1;
                        checker = true;
                        StartCoroutine(SpawnWave());
                        counter = TimeBetweenWaves;
                    }

                }

            }
            else
            {

                    TextwaveNumber.text = "Kill them all!";

            }


            if (waveNumber >= NumberOfWaves && remainings == 0)
            {
                EndGameWin();
            }
        }
        
        else
        {
            TextwaveNumber.text = " " ;
        }

    }
    //
    IEnumerator SpawnWave ()
    {
        PlayerStats.Money += waveNumber * 50;
        //Если поменяли количество мобов, которое создаётся каждую волну - обязательно измените вычисление remainings
        for (int i=0;i<(waveNumber* waveNumber+1);i++)
        //Если поменяли количество мобов, которое создаётся каждую волну - обязательно измените вычисление remainings
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.4f);
        }

        waveNumber++;

    }
    //Инициализурем префаб врага в точке спавна
    void SpawnEnemy()
    {

            Instantiate(enemyPrefab, Spawner.transform.position, Spawner.transform.rotation);

    }
    void EndGameWin()
    {
        //Проверяем жив ли игрок чтобы код не выполнялся до бесконечности
        if (GM.alive)
        {
            GameObject imp_inst = (GameObject)Instantiate(WonExplosion, Spawner.transform.position, Spawner.transform.rotation);
            Destroy(imp_inst, 2.0f);

            Destroy(Spawner.gameObject);
            TextwaveNumber.text = " ";
            endgamer.text = "You won ";
            GM.alive = false;
        }
    }




}
