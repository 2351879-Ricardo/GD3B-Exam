using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField] private float heavyTime, chargeTime, attackTime, windowTime;
    [SerializeField]private float lightAttackMultiplier, heavyAttackMultiplier;
    
    private int _ltAttackVal = 0;
    private int _maxAttackVal;

    private Animator _anim;
    private CharacterState _cs;

    public bool attacking, inCombo;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _cs = GetComponent<CharacterState>();
        BroadcastMessage("GetLongestCombo");
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
        else if (!attacking && _cs.currentState == CharacterState.PlayerStates.IsDodging && _ltAttackVal >0)
        {
            DodgeAttack();
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
        attacking = !attacking;
        if (_ltAttackVal < _maxAttackVal)
        {
            _anim.SetBool("break", false);
            
            inCombo = true;
            windowTime = 0;

            if (attacking)
            {
                _ltAttackVal++;
            }
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

    private void DodgeAttack()
    {
        _anim.SetBool("DodgeAttack", true);
        Debug.Log("DodgeAttack");
    }

    private void LightEffect()
    {
        BroadcastMessage("StartLightAttack");
        BroadcastMessage("SetDamageMultiplier", lightAttackMultiplier*_ltAttackVal);
    }

    private void HeavyEffect()
    {
        BroadcastMessage("StartHeavyAttack");
        BroadcastMessage("SetDamageMultiplier", heavyAttackMultiplier*_ltAttackVal);
    }

    private void EndAttackWindow()
    {
        BroadcastMessage("EndAttack");
    }

    private void EndCombo()
    {
        _anim.SetBool("break", true);
        for (var i = 1; i <= _ltAttackVal; i++)
        {
            _anim.SetBool($"left{i}", false);
            _anim.SetBool($"right{i}", false);
            _anim.SetBool("DodgeAttack", false);
        }

        _ltAttackVal = 0;
    }

    public void LandHit()
    {
        Debug.Log("Hit Landed");
    }

    private void SetMaxCombo(int length)
    {
        _maxAttackVal = length;
    }
}
