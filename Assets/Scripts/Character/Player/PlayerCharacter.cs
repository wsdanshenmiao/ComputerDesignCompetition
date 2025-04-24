using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : Character, ITargetable
{
    public PlayerSO templatePlayerPara;
    [HideInInspector] public PlayerSO playerPara;

    private bool canBeTargeted = true;

    public bool CanBeTargeted => canBeTargeted;
    [HideInInspector] public bool isCast = false;

    [HideInInspector] public float currSprintCD;

    private void OnEnable()
    {
        OnDieEvent.AddListener(ResurgencePlayer);
    }

    protected override void Update()
    {
        base.Update();
        currSprintCD -= Time.deltaTime;
    }

    private void OnDisable()
    {
        OnDieEvent.RemoveListener(ResurgencePlayer);
    }

    public void ResurgencePlayer()
    {
        GameManager.Instance.SetPlayerPosition();
        ResetData();
    }
    
    public void SetTargetable(bool targetable)
    {
        canBeTargeted = targetable;
    }

    public Transform GetTargetTransform()
    {
        return transform;
    }

    protected override void ResetData()
    {
        base.ResetData();
        playerPara = Instantiate(templatePlayerPara);
        currSprintCD = playerPara.sprintCD;
        canBeTargeted = true;
        isCast = false;
    }

    
}
