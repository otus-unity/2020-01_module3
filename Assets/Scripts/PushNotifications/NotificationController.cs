using UnityEngine;


public class NotificationController : MonoBehaviour
{
	[Tooltip("DO NOT CHANGE MANUALLY!")]
	public GameObject notificationManager;

	[Tooltip("Notification duration in second unit.")]
	public float life;

	void Update ()
	{
		life -= Time.deltaTime;
		if (life < 0)
		{
			Destroy (gameObject);
		}
	}

	void OnDestroy()
	{
		if (notificationManager!=null)
		{
			notificationManager.GetComponent<NotificationManager> ().IsThereNotification = false;
		}
	}
}
