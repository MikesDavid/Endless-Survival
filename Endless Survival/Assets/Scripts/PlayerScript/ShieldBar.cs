using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    
    private Image barImage;
    private void Awake()
    {
        barImage = transform.Find("ShieldBar").GetComponent<Image>();

        barImage.fillAmount = .3f;
    }
}
public class Shield
{
    [SerializeField] private Player player;

    public void Update()
    {
        
    }

}