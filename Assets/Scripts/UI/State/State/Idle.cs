using System.Threading;
using UnityEngine;

public class Idle : State
{
    public Idle()
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        //if (GetClosestEnemyInArea(visionDistance) != null)
        //{
        //    nextState = new Tracking(turret, barrel);
        //    stage = EVENT.EXIT;
        //}
        //else if (GetClosestEnemyInArea(attackDistance) != null)
        //{
        //    nextState = new Attacking(turret, barrel);
        //    stage = EVENT.EXIT;
        //}
    }

    public override void Exit()
    {
        base.Exit();
    }
}
