using UnityEngine;

public class Disappear : MonoBehaviour
{
	[SerializeField] private float disappearAfter;

	void Start()
	{
		Destroy(gameObject, disappearAfter);
	}
}