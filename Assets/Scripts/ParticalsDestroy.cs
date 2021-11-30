using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalsDestroy : MonoBehaviour
	
{

	public float lifeTime = 2.0f;

	private IEnumerator Start()
	{
		yield return new WaitForSeconds(lifeTime);
		Destroy(gameObject);
	}
}
