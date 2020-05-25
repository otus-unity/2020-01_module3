using UnityEngine;


public class Notification
{
	public Texture2D notificationImage;
	public string notificationText;

	public Notification(Texture2D _notificationImage, string _notificationText)
	{
		notificationImage = _notificationImage;
		notificationText = _notificationText;
	}
}
