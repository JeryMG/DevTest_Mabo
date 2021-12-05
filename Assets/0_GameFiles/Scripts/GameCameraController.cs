using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraController : MonoBehaviour
{
    #region Members
	public static GameCameraController Instance;

	private Transform target;

	private Transform hor;
	private Transform vert;
	private Transform parent;
	
	public Vector3 positionOffset;
	public Vector3 eulerAnglesOffset;
	public float lerpSpeed = 12f;
	public float xDiv = 2;
	
	#endregion

	#region Monobehaviors
	void Awake()
	{
		Instance = this;

		Init();
	}

    private IEnumerator Start()
    {
		while (playerController.Instance == null)
			yield return null;

		target = playerController.Instance.transform;
		transform.position = new Vector3(target.position.x / xDiv, target.position.y, target.position.z) + positionOffset;
	}

#if UNITY_EDITOR
	private void Update()
	{
		target = FindObjectOfType<playerController>().transform;

		if (Application.isPlaying || target == null)
			return;

		transform.position = target.transform.position + positionOffset;
		transform.GetChild(0).eulerAngles = eulerAnglesOffset;
	}
#endif

	void LateUpdate()
	{
#if UNITY_EDITOR
		if (!Application.isPlaying)
			return;
#endif

		FollowTarget();
	}
	#endregion

	void Init()
	{
		hor = transform;
		vert = hor.GetChild(0);
		parent = vert.GetChild(0);
	}

	void FollowTarget()
	{
		if (target == null)
		{
			if (FindObjectOfType<playerController>() != null)
				target = FindObjectOfType<playerController>().transform;
			if (target == null)
				return;

			transform.position = new Vector3(target.position.x / xDiv, target.position.y, target.position.z) + positionOffset;
		}

		transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x / xDiv, target.position.y, target.position.z) + positionOffset, Time.deltaTime * lerpSpeed);
		vert.eulerAngles = eulerAnglesOffset;
	}
}
