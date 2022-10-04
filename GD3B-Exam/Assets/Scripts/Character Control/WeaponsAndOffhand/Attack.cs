using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField] private float attackWindowTime;
    
    private int _ltAttackVal = 0;

    private Animator _anim;
    private CharacterState _cs;

    public float _windowTime;

    public bool _windowOpen, attacking;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _cs = GetComponent<CharacterState>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Attack Window Control
        if (_windowOpen)
        {
            _windowTime -= Time.deltaTime;
        }
        else if (!_windowOpen)
        {
            EndCombo(_ltAttackVal);
        }

        if (_windowTime <= 0 && _windowOpen)
        {
            _windowOpen = false;
        }
        #endregion
    }

    private void OnAttack()
    {
        attacking = true;
        
        SendMessageUpwards("StartAttack");
        
        if (_ltAttackVal == 0 || _windowOpen)
        {
            _ltAttackVal++;
            AttackWindowInit();
            Debug.Log(_ltAttackVal);
            _anim.SetBool($"ltAttack{_ltAttackVal}", true);
        }
        else
            return;
    }

    private void AttackWindowInit()
    {
        _windowTime = attackWindowTime*1.1f;
        _windowOpen = true;
    }

    private void EndCombo(int animInd)
    {
        for (int i = 1; i <= animInd; i++)
        {
            _anim.SetBool($"ltAttack{i}", false);
        }
        if (attacking)
            SendMessageUpwards("EndAttack");

        attacking = false;
        _ltAttackVal = 0;
    }

    public void LandHit()
    {
        Debug.Log("Hit Landed");
    }
}
