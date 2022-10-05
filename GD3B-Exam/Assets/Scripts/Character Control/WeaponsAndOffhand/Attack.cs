using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField] private float heavyTime, chargeTime, attackTime, windowTime;
    
    private int _ltAttackVal = 0;

    private Animator _anim;
    private CharacterState _cs;

    public bool attacking, inCombo;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _cs = GetComponent<CharacterState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            chargeTime += Time.deltaTime;
        }

        if (inCombo && !attacking)
        {
            windowTime += Time.deltaTime;
        }

        if (windowTime >= attackTime)
        {
            inCombo = false;
            windowTime = 0;
            if (_ltAttackVal > 0)
            {
                EndCombo();
            }
        }

        if (!attacking && chargeTime >= heavyTime)
        {
            HeavyAttack();
            chargeTime = 0;
        }
        else if (!attacking && chargeTime < heavyTime && chargeTime > 0)
        {
            LightAttack();
            chargeTime = 0;
        }
    }

    private void OnAttack()
    {
        _anim.SetBool("break", false);
        attacking = !attacking;
        inCombo = true;
        windowTime = 0;
        if (attacking)
        {
            _ltAttackVal++;
        }
    }

    private void HeavyAttack()
    {
        _anim.SetBool($"right{_ltAttackVal}", true);
    }

    private void LightAttack()
    {
        _anim.SetBool($"left{_ltAttackVal}", true);
    }

    private void AttackWindowInit()
    {
        
    }

    private void EndCombo()
    {
        _anim.SetBool("break", true);
        for (var i = 1; i <= _ltAttackVal; i++)
        {
            _anim.SetBool($"left{i}", false);
            _anim.SetBool($"right{i}", false);
        }

        _ltAttackVal = 0;
    }

    public void LandHit()
    {
        Debug.Log("Hit Landed");
    }
}
