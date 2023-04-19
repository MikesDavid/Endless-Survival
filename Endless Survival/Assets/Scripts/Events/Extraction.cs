using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Extraction : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI extractionTxt;
    [SerializeField] private float time = 10f;

    [SerializeField] Transform show, hide;

    private bool readyToExtract;
    void Start()
    {
        readyToExtract = false;
    }


    void FixedUpdate()
    {
        if (timer.minutes == 5)
            readyToExtract = true;
        else
            readyToExtract = false;

    }
    private void OnTriggerEnter(Collider collider)
    {
        if (readyToExtract)
        {
            transform.position = show.position;
            time = time -= Time.deltaTime;
            if (time >= 0 && collider.tag == "Player")
                Extracting();
        }

    }
    private void OnTriggerExit(Collider collider)
    {
        if (readyToExtract)
        {
            transform.position = hide.position;
            time = default;
        }

    }
    private IEnumerator Extracting()
    {
        levelManager.CurrentScene(GameScene.Wictory);
        yield return new WaitForSeconds(5000);
        SceneManager.LoadScene(3, LoadSceneMode.Single);


    }
}
