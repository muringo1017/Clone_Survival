using UnityEngine;

public class Interaction_Hit : M_Object
{
  
    public override void Interaction()
    {
        PlayerMovement.instance.AnimatorChange(m_Data.m_Type.ToString());
        base.Interaction();
    }
}
