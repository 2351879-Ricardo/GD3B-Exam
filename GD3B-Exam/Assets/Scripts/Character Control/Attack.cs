using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    private int _ltAttackVal = 0;

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

    private void OnAttack()
    {
        _ltAttackVal++;
        Debug.Log(_ltAttackVal);

        _anim.SetBool($"ltAttack{_ltAttackVal}", true);
        
    }

    public void EndCombo(int animInd)
    {
        for (int i = 1; i <= animInd; i++)
        {
            _anim.SetBool($"ltAttack{i}", false);
        }

        _ltAttackVal = 0;
    }
}
