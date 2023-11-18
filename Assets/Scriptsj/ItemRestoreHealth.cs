using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[CreateAssetMenu(menuName = "Powerups/Health")]
public class ItemRestoreHealtph : ItemsPowerups
{
    public int h;
    // Start is called before the first frame update
   public override void Apply(GameObject target)
    {
        target.GetComponentInParent<PlayerLife>().RestoreLife(h);
    }
}
