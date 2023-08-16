using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArgoCombatCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    private Animator animator;

    private int comboIndex = 0;

    public void OnAttack()
    {
        animator.CrossFade(gameData.comboList[comboIndex++ % gameData.comboList.Length], 0.1f);
    }

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }


}
