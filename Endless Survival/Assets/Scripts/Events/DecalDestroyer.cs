using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class DecalDestroyer : MonoBehaviour
{
    [SerializeField] private int timer;

    private async void Start()
    {
        await Task.Delay(timer);
        Destroy(gameObject);
    }
    
}
