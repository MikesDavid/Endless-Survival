using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCanvas : MonoBehaviour
{
    public Transform SelectionUiTarget;
    public Transform SkillsUiTarget;

    void Start()
    {
        
    }

    void Update()
    {

    }
    public void SelectionScreen()
    {
        //Vector3 a = transform.position;
        //Vector3 b = SelectionUiTarget.position;
        //transform.position = Vector3.MoveTowards(a, b, speed);
        transform.position = SelectionUiTarget.position;
    }

    public void SkillScreen()
    {
        //Vector3 a = transform.position;
        //Vector3 b = SkillsUiTarget.position;
        //transform.position = Vector3.MoveTowards(a, b, speed);
        transform.position = SkillsUiTarget.position;
    }
}
