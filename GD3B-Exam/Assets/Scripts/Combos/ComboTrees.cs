using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    LIGHT,
    HEAVY,
    NULL
}

[System.Serializable]
public class Node
{
    [SerializeField] private string comboName;

    [Header("Node Information")] 
    [SerializeField] private AttackType attackType;
    [SerializeField] private Animation attackAnimation;
    
    public List<Node> childNodes;
}


public class ComboTrees : MonoBehaviour
{
    public Node rootNode = null;
}
