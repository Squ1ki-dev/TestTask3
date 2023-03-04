using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Shooting")]
	[SerializeField] private Settings settings;

	[System.NonSerialized] public bool isCanShoot = true;

	private GameObject bullet;
	[SerializeField] Transform bulletSpawnPos;
	[SerializeField] GameObject bulletPrefab;

	public float GetPathWidth()
	{
		return transform.localScale.x + 0.3f;
	}

	public float GetBulletWidth() 
	{
		return bullet != null ? bullet.transform.localScale.x : 0;
	}

	public void InitShoot() 
	{
		bullet = Instantiate(bulletPrefab, bulletSpawnPos.position, Quaternion.identity, null);
		bullet.transform.localScale = settings.minScale;
		transform.localScale -= settings.minScale;
	}

	public void OnHold(Vector3 pos, float holdTime) 
	{
		if (bullet == null)
			return;

		Vector3 scaleChange = Vector3.one * holdTime / 100f;
		bullet.transform.localScale += scaleChange;
		transform.localScale -= scaleChange;

		if (transform.localScale.x < settings.minScale.x)
			GameManager.Instance.OnLose();
	}

	public void ShootTo(Vector3 pos, float holdTime) 
	{
		if (bullet == null)
			return;

		Rigidbody rb = bullet.GetComponent<Rigidbody>();
		Vector3 velocity = (pos - rb.transform.position).normalized;
		velocity.y = 0.0f;
		rb.velocity = velocity.normalized * settings.bulletSpeed;
		bullet = null;
	}
}
