using UnityEngine;
using System.Collections;

public class Clip : MonoBehaviour {
	private int bullets = 0;
	public bool empty = false;
	
	public Clip(int shells)
	{
		bullets = shells;
	}
	
	public bool shoot()
	{
        if (bullets == 0)
            empty = false;

        if (empty)
            return false;
        else
        {
            bullets--;
            return true;
        }
	}
}
