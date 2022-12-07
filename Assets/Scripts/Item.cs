using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
	public UnityEvent OnGetItem;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		OnGetItem?.Invoke();
		Destroy(gameObject);
	}
}
