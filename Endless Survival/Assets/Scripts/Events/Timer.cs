using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private EnemySpawner enemySpawner;
    public TextMeshProUGUI timerTxt,finalTime;

    [Header("Timer Settings")]

    public float currentTime, seconds, minutes, hours;
    public float enemy1SpawnDec, enemy2SpawnDec, enemy3SpawnDec, increaseHealth;
    public static float saveSec, saveMin, saveHour;
    public bool countDown,ingame; //, spawnReduced1, spawnReduced2, spawnReduced3;
    [Header("Limit Settings")]

    public bool hasLimit;
    public float timeLimit;
    // Start is called before the first frame update
    void Start()
    {
        //spawnReduced1 = true;
        //spawnReduced2 = true;
        //spawnReduced3 = true;
        StartCoroutine(DecreaseEnemy1Timer(enemy1SpawnDec));
        StartCoroutine(DecreaseEnemy2Timer(enemy2SpawnDec));
        StartCoroutine(DecreaseEnemy3Timer(enemy3SpawnDec));
        StartCoroutine(IncreaseEnemyHealth(increaseHealth));
        StateManager.OnSceneChanged += LevelManager_OnSceneChanged;
    }

    private void LevelManager_OnSceneChanged(GameScene state)
    {
        if (state == GameScene.Defeat || state == GameScene.Wictory)
        {
            saveHour = hours;
            saveMin = minutes;
            saveSec = seconds;
        }

    }
    private void OnDestroy()
    {
        StateManager.OnSceneChanged -= LevelManager_OnSceneChanged;
    }

    // Update is called once per frame
    void Update()
    {


        //DecreaseEnemyTimer();

       
        if (seconds >= 60)
        {
            minutes++;
            seconds = 0;
        }
        if (minutes == 60)
        { 
            hours++;
            minutes = 0;
        }
        currentTime = (seconds + (minutes + hours*60)*60);
        if (hasLimit && ((countDown && currentTime <= timeLimit) || (!countDown && currentTime >= timeLimit)))
        {
            currentTime = timeLimit;
            SetTimerText();
            timerTxt.color = Color.red;
            enabled = false;
        }
        SetTimerText();
        if(ingame)
            seconds = countDown ? seconds -= Time.deltaTime : seconds += Time.deltaTime;

    }



    private void SetTimerText()
    {
        if(ingame)
            timerTxt.text = (hours.ToString().PadLeft(2, '0') + ":" + minutes.ToString().PadLeft(2, '0') + ":" + seconds.ToString("0").PadLeft(2, '0')); 
    }
    //private void DecreaseEnemyTimer()
    //{

    //    if (minutes % 5 == 0 && seconds ==0 && !spawnReduced2 )
    //    {
    //        enemySpawner.enemy2Interval -= 0.5f;
    //        spawnReduced2 = true;
    //    }
    //    else
    //        spawnReduced2 = false;
    //    if (seconds / 60 == 0 && !spawnReduced1)
    //    {
    //        enemySpawner.enemy1Interval -= 0.5f;
    //        spawnReduced1 = true;
    //    }
    //    else
    //        spawnReduced1 = false;
    //    if (minutes == 10 && !spawnReduced3)
    //    {
    //        enemySpawner.enemy3Interval -= 0.5f;
    //        spawnReduced3 = true;
    //    }

    //    if (enemySpawner.enemy1Interval == 3)
    //        spawnReduced1 = true;
    //    if (enemySpawner.enemy2Interval == 5)
    //        spawnReduced2 = true;
    //    if (enemySpawner.enemy3Interval == 2.5)
    //        spawnReduced3 = true;
    //}
    private IEnumerator IncreaseEnemyHealth(float interval)
    {
        yield return new WaitForSeconds(interval);
        foreach (Target target in Target.enemyList)
        {
            target.health += 10;
        };
        StartCoroutine(IncreaseEnemyHealth(interval));
    }
    private IEnumerator DecreaseEnemy1Timer(float interval)
    {
        yield return new WaitForSeconds(interval);
        if (enemySpawner.enemy1Interval != 3f && !countDown)
            enemySpawner.enemy1Interval -= .5f;
        StartCoroutine(DecreaseEnemy1Timer(interval));
    }
    private IEnumerator DecreaseEnemy2Timer(float interval)
    {
        yield return new WaitForSeconds(interval);
        if (enemySpawner.enemy2Interval != 5f && !countDown)
            enemySpawner.enemy2Interval -= .5f;
        StartCoroutine(DecreaseEnemy2Timer(interval));
    }
    private IEnumerator DecreaseEnemy3Timer(float interval)
    {
        yield return new WaitForSeconds(interval);
        if (enemySpawner.enemy3Interval != 2.5f && !countDown)
            enemySpawner.enemy3Interval -= .5f;
        StartCoroutine(DecreaseEnemy3Timer(interval));
    }
}
