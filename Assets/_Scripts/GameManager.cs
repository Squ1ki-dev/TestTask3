using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[System.NonSerialized] public List<Obstacles> obstacles = new List<Obstacles>();

	private bool isPlaying;
	private float holdTime = 0;

	private Vector3 lastTouchPos;

	public TrackLine trackLine;
	[SerializeField] private LineRenderer shootLine;
	
	[SerializeField] private LayerMask hitMask;
	[SerializeField] private GameObject panel;

	[SerializeField] private Player player;

	[SerializeField] private Transform[] jumpPos;

	private void Awake() 
    {
		Instance = this;
		isPlaying = true;
		shootLine.gameObject.SetActive(false);
	}

	private void Update() 
	{
		MouseClick();

		if (!isPlaying || !player.isCanShoot) return;

		if(trackLine.EnemiesCount == 0)
			OnWin();
	}

	private void MouseClick()
	{
		if (Input.GetMouseButton(0)) 
        {
			holdTime += Time.deltaTime;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 50f, hitMask)) 
            {
				lastTouchPos = new Vector3(hit.point.x, 0.01f, hit.point.z);
				shootLine.SetPosition(1, lastTouchPos);
				trackLine.SetWidth(player.GetPathWidth());
				shootLine.startWidth = shootLine.endWidth = player.GetBulletWidth();
			}

			player.OnHold(lastTouchPos, holdTime);
		}

		if (Input.GetMouseButtonDown(0)) 
        {
			shootLine.gameObject.SetActive(true);
			holdTime = 0.0f;

			player.InitShoot();
		}
		
		if (Input.GetMouseButtonUp(0)) 
        {
			shootLine.gameObject.SetActive(false);
			player.ShootTo(lastTouchPos, holdTime);
		}
	}

	public void OnWin() 
	{
		isPlaying = false;
		shootLine.gameObject.SetActive(false);
		player.ShootTo(lastTouchPos, holdTime);

		for(int i = 1; i < jumpPos.Length; ++i) 
		{
			int curr = i;
			LeanTween.value(0.0f, 1.0f, 1.0f)
			.setDelay(1.0f * curr)
			.setOnUpdate((float f) => {
				Vector3 newPos = Vector3.Lerp(jumpPos[curr - 1].position, jumpPos[curr].position, f);
				if(f < 0.5f)
					newPos.y = Mathf.Lerp(jumpPos[curr - 1].position.y, jumpPos[curr].position.y + 3.0f, f * 2f);
				else
					newPos.y = Mathf.Lerp(jumpPos[curr].position.y + 3.0f, jumpPos[curr].position.y, (f - 0.5f) * 2f);
				newPos.y = Mathf.Sqrt(newPos.y);
				player.transform.position = newPos;
			});
		}

		LeanTween.delayedCall(7.0f, () => {
			panel.SetActive(true);
		});
	}

	public void OnLose() 
    {
		panel.SetActive(true);
	}

	public void RestartScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
