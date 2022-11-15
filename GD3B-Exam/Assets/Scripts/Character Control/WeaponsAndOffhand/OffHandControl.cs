using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffHandControl : MonoBehaviour
{
    [SerializeField] private GameObject offHand;

    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnOffHandActivate()
    {
        var temp = offHand.GetComponentInChildren<Offhand>().ActivateAbility();
        var bTemp = _anim.GetBool(temp);
        _anim.SetBool(temp, !bTemp);
    }
}
