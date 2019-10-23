using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    class ConditionContainer : Expression
    {
        public Condition condition1;
        public Condition condition2;
        public LogicalOperator relationship;
        public TimeState holdingTimer;
        public bool isSatisfied;

        public ConditionContainer(Condition c1, LogicalOperator ope, Condition c2, TimeState timeState = null)
        {
            condition1 = c1;
            relationship = ope;
            condition2 = c2;
            holdingTimer = timeState;
        }

        public override bool Check(State state1, Operator ope, State state2, TimeState timeState = null)
        {
            throw new NotImplementedException();
        }

        public override void Activate()
        {
            if (condition1 != null)
                condition1.Activate();
            if (condition2 != null)
                condition2.Activate();
        }

        public override void Deactivate()
        {
            if (condition1 != null)
                condition1.Deactivate();
            if (condition2 != null)
                condition2.Deactivate();
        }

        public override void ActivateAndStartMonitoring()
        {
            if (condition1 != null)
                condition1.ActivateAndStartMonitoring();
            if (condition2 != null)
                condition2.ActivateAndStartMonitoring();
        }

        public bool Check(State state, TimeState timeState = null)
        {
            isSatisfied = false;
            //Debug.Log("Condition1 : " + condition1.Check(state, timeState));
            //Debug.Log("Condition2 : " + condition2.Check(state, timeState));
            if (relationship == LogicalOperator.And)
                isSatisfied = condition1.Check(state, timeState) && condition2.Check(state, timeState);
            else if (relationship == LogicalOperator.Or)
                isSatisfied = condition1.Check(state, timeState) && condition2.Check(state, timeState);
            else
                throw new Exception("No logical operator set.");
            return isSatisfied;
        }

        public bool Check()
        {
            // for the case where two conditions are both passive types...
            if (condition1.ShouldCheckPassively() && condition2.ShouldCheckPassively())
            {
                //Debug.Log("Condition1 : " + condition1.Check());
                //Debug.Log("Condition2 : " + condition2.Check());
                if (relationship == LogicalOperator.And)
                    return condition1.Check() && condition2.Check();
                else if (relationship == LogicalOperator.Or)
                    return condition1.Check() || condition2.Check();
                else
                    throw new Exception("Invalid logical operator.");
            }
            else
                return false;
        }
    }
}
