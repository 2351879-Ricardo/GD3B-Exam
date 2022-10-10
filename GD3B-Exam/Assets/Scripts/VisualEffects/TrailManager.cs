using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    [SerializeField] private GameObject lightTrails, heavyTrails;
    // Start is called before the first frame update
    void Start()
    {
        lightTrails.SetActive(false);
        heavyTrails.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLightAttack()
    {
        lightTrails.SetActive(true);
    }

    public void StartHeavyAttack()
    {
        heavyTrails.SetActive(true);
    }

    public void EndAttack()
    {
        lightTrails.SetActive(false);
        heavyTrails.SetActive(false);
    }
}
