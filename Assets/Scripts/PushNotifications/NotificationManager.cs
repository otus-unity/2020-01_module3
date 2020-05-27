using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

//https://docs.unity3d.com/Manual/PlatformDependentCompilation.html


public class NotificationManager : MonoBehaviour
{
    public Button CreateNotificationButton;

    [Tooltip("Select inputfield in scene.")]
	public InputField InputField;

	[Tooltip("Select canvas in scene.")]
	public Canvas Canvas;

	[Tooltip("Select notification panel that you want to send in prefab folder.")]
	public GameObject NotificationPanelPrefab;
	private GameObject _currentNotificationPanel; //This Gameobject variable for active notification.

	[Tooltip("Enter an URL for notification image.")]
	public string NotificationImageURL;

	[Tooltip("Select an SFX for notification")]
	public AudioClip NotificationSfx;

	[Tooltip("Time between notifications in second unit")]
	public float BufferTime;
	private float _bufferTime = -1; //

	private Texture2D _saved;

	[Tooltip("DO NOT CHANGE MANUALLY!")]
	public bool IsThereNotification; //This controls that is there any active notification.
    private List<Notification> _notificationList = new List<Notification>(); //Notification order.



    private WWW _www;
	private bool _imageCached = false;
	private bool _fileFound = true;


    private void Awake()
	{
        
        _saved = new Texture2D (72, 72);
		if (File.Exists (Application.dataPath + "/cachedimage.png"))
		{
			_fileFound = true;
			_saved.LoadImage (File.ReadAllBytes(Application.dataPath + "/cachedimage.png"));
		}
		else
		{
			_fileFound = false;
			StartCoroutine (DownloadImage());
		}
	}

    private void OnEnable()
    {
        CreateNotificationButton.onClick.AddListener(RegisterNotification);
    }

    private void OnDisable()
    {
        CreateNotificationButton.onClick.RemoveListener(RegisterNotification);
    }

    private IEnumerator DownloadImage()
	{
		_www = new WWW(NotificationImageURL);
		yield return _www;
		_www.LoadImageIntoTexture(_saved);
		_www.Dispose();
		_www = null;
	}

    private void Update()
	{
		if (!_imageCached && !_fileFound && _www==null )
		{
			File.WriteAllBytes (Application.dataPath + "/cachedimage.png", _saved.EncodeToPNG());
			_imageCached = true;
		}
		
		if (_notificationList.Count > 0)
		{
			if (!IsThereNotification && _bufferTime < 0)
			{
				InstantiateNotification ();
				IsThereNotification = true;
				_bufferTime = BufferTime;
			}
			else if (!IsThereNotification && _bufferTime > 0)
			{
				_bufferTime -= Time.deltaTime;
			}
		}
		else
		{
			if (_bufferTime > 0)
			{
				_bufferTime -= Time.deltaTime;
			}
		}
	}

	public void InstantiateNotification()
	{
		GameObject currentNotificationPanel = Instantiate(NotificationPanelPrefab) as GameObject;
		currentNotificationPanel.gameObject.transform.SetParent (Canvas.gameObject.transform,false);
		currentNotificationPanel.gameObject.transform.GetChild (0).GetComponent<RawImage> ().texture = _notificationList [0].notificationImage;
		currentNotificationPanel.gameObject.transform.GetChild (1).GetComponent<Text> ().text = _notificationList [0].notificationText;
		currentNotificationPanel.gameObject.transform.GetComponent<NotificationController> ().notificationManager = this.gameObject;
		_notificationList.RemoveAt (0);
	}

#if UNITY_IOS
    public void RegisterNotification()
	{
        //https://stackoverflow.com/questions/45057452/why-the-skstorereviewcontroller-does-not-let-me-submit-a-review
        //https://forum.unity.com/threads/using-requeststorereview-and-how-it-works.517841/
        if (Device.RequestStoreReview())
        {
            // Reviewed
        }

    }
#elif UNITY_ANDROID 
    public void RegisterNotification()
    {
        string testUrl = "https://play.google.com/store/apps/details?id=testApp";
        Application.OpenURL (testUrl);
    }
#elif UNITY_STANDALONE_WIN
    public void RegisterNotification()
    {
        _notificationList.Add(new Notification(_saved, InputField.text));
    }
#endif
        
}
