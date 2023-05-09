using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Extraction : MonoBehaviour
{
    [SerializeField] private StateManager levelManager;
    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI extractionTxt;
    [SerializeField] private float time = 10f;
    [SerializeField] private GameObject ShowExtrPoint, HideExtrPoint;
    [SerializeField] private LoadingScreen loadingScreen;
    [SerializeField] private GameObject ShowArrow, HideArrow, Arrow;

    [SerializeField] Transform show, hide;

    [SerializeField]private bool readyToExtract;
    void Start()
    {
        readyToExtract = false;
        transform.position = HideExtrPoint.transform.position;
        Arrow.transform.position = HideArrow.transform.position;
    }


    void FixedUpdate()
    {
        if (timer.minutes == 5)
            readyToExtract = true;
        if (readyToExtract)
        {
            extractionTxt.transform.position = show.position;
            transform.position = ShowExtrPoint.transform.position;
            Arrow.transform.position = ShowArrow.transform.position;
        }
        else extractionTxt.transform.position = hide.position;

    }
    private void OnTriggerEnter(Collider collider)
    {
        if (readyToExtract)
        {
            extractionTxt.transform.position = show.position;
            time = time -= Time.deltaTime;
            if (time >= 0 && collider.tag == "Player")
                Extracting();
        }

    }
    private void OnTriggerExit(Collider collider)
    {
        if (readyToExtract)
        {
            extractionTxt.transform.position = hide.position;
            time = default;
        }

    }
    private IEnumerator Extracting()
    {
        levelManager.CurrentScene(GameScene.Wictory);
        yield return new WaitForSeconds(5000);
        loadingScreen.LoadLevel(3);


    }
}
