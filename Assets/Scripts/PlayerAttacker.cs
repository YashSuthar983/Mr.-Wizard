using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YS;

public class PlayerAttacker : MonoBehaviour
{

    public Transform attackInstantiatePoint;
    private Transform tempIsnsPoint;
    PlayerMovement pm;

    public GameObject[] attackSpells;

    public GameObject currentSpell;
    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
        currentSpell = attackSpells[0];
        tempIsnsPoint=attackInstantiatePoint;
    }
    private void Update()
    {
        attackInstantiatePoint = pm.wallCheck;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(currentSpell, attackInstantiatePoint);
        }
    }
}
