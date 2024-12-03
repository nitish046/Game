using System.Reflection;
using UnityEngine;

public class UseObject : MonoBehaviour
{
	private ItemHotbar itemHotbar;
	[SerializeField] private GameObject itemHotbarObject;
	
	GameObject _projectile;


	// Tomato
	public Transform launchPoint;
	public GameObject projectile;
	public float launchSpeed = 10f;
	public AudioSource audioSource;

	// Banana
	[SerializeField] private GameObject bananaTrapPrefab;
	[SerializeField] private Transform placePoint;

	// Cheese Puff
	[SerializeField] private GameObject cheesePuffSmokeBomb;

	// Shuriken
	[SerializeField] private GameObject crackerShuriken;
	private Throw _shuriken;

	private void Start()
	{
		itemHotbar = itemHotbarObject.GetComponent<ItemHotbar>();
	}
	private void Update()
	{

		// if (Input.GetMouseButtonDown(0))
		// {
		//   _projectile = Instantiate(projectile, launchPoint.position, launchPoint.rotation);


		// }
		// if (Input.GetMouseButtonUp(0))
		// {
		//   audioSource.Play();
		//   _projectile.GetComponent<Rigidbody>().isKinematic = false;
		//   _projectile.GetComponent<Rigidbody>().velocity = launchSpeed * ((launchPoint.up / 2) + launchPoint.forward);
		// }
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("Mouse Down");
			if (itemHotbar.SelectedBoxIsOccupied())
			{
				string itemName = itemHotbar.UseSelectedItem();
				MethodInfo useMethod = this.GetType().GetMethod("Use" + itemName);
				useMethod.Invoke(this, null);
			}
		}

	}

	public void UseShuriken()
	{
		Debug.Log("Using Shuriken");
		_shuriken = new Throw();
		_shuriken.transform.position = launchPoint.position;
		_shuriken.Toss(10f, launchPoint.rotation);
	}

	public void UseTomato()
	{
		Debug.Log("Using Tomato...");
		_projectile = Instantiate(projectile, launchPoint.position, launchPoint.rotation);
		audioSource.Play();
		_projectile.GetComponent<Rigidbody>().isKinematic = false;
		_projectile.GetComponent<Rigidbody>().velocity = launchSpeed * ((launchPoint.up / 2) + launchPoint.forward);
	}

	public void UseBanana()
	{
		if (bananaTrapPrefab != null)
		{
			Debug.Log("Using Banana...");
			Vector3 placePosition = placePoint.position + placePoint.forward * 1f;
			Debug.Log(placePosition);
			Debug.Log(placePoint.rotation);
			GameObject.Instantiate(bananaTrapPrefab, placePosition, placePoint.rotation);
		}
		else
		{
			Debug.LogError("Trap prefab is not assigned in UseObject.");
		}
	}

	public void UseCheesePuffBomb()
	{
		Debug.Log("Using CheesePuffBomb...");
		Vector3 placePosition = placePoint.position + placePoint.forward * 1f;
		GameObject bomb = GameObject.Instantiate(cheesePuffSmokeBomb, placePosition, placePoint.rotation);
		bomb.GetComponent<CPSmokeBomb>().ActivateTrap(null);
	}
}