using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class Extraction : MonoBehaviour
{
    [SerializeField] private StateManager levelManager;
    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI extractionTxt;
    [SerializeField] private float time = 5f;
    [SerializeField] private GameObject ShowExtrPoint, HideExtrPoint;
    [SerializeField] private LoadingScreen loadingScreen;
    [SerializeField] private GameObject ShowArrow, HideArrow, Arrow;
    private bool countDown;

    [SerializeField] Transform show, hide;
    [SerializeField] VisualEffect portal;
    [SerializeField] LoadingScreen LoadingScreen;

    [SerializeField]private bool readyToExtract;
    void Start()
    {
        readyToExtract = false;
        transform.position = HideExtrPoint.transform.position;
        Arrow.transform.position = HideArrow.transform.position;
    }
    private void Update()
    {
        extractionTxt.text = time.ToString("0").PadLeft(2, '0');
        if (countDown)
        {
            time = time -= Time.deltaTime;
            if (time <= 0)
            {
                levelManager.CurrentScene(GameScene.Wictory);
                LoadingScreen.LoadLevel(3);
            }

        }
    }

    void FixedUpdate()
    {
        if (timer.minutes == 5)
            readyToExtract = true;
        if (readyToExtract)
        {
            portal.gameObject.SetActive(true);

            transform.position = ShowExtrPoint.transform.position;
            Arrow.transform.position = ShowArrow.transform.position;
        }
        else extractionTxt.transform.position = hide.position;

    }
    private void OnTriggerEnter(Collider collider)
    {
        if (readyToExtract)
        {
            if(collider.tag == "Player")
                countDown = true;
            extractionTxt.transform.position = show.position;            
        }

    }
    private void OnTriggerExit(Collider collider)
    {
        if (readyToExtract)
        {
            if (collider.tag == "Player")
            {
                countDown = false;
                extractionTxt.transform.position = hide.position;
                time = 5;
            }
            
        }

    }
    private IEnumerator Extracting()
    {
        yield return new WaitForSeconds(100);
    }
}
