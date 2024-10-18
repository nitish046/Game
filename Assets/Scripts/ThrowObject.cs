using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
		public Transform launchPoint;
		public GameObject projectile;
		public float launchSpeed = 10f;
		public AudioSource audioSource;

		GameObject _projectile;
		void Update()
		{

				if (Input.GetMouseButtonDown(0))
				{
						_projectile = Instantiate(projectile, launchPoint.position, launchPoint.rotation);


				}
				if (Input.GetMouseButtonUp(0))
				{
						audioSource.Play();
						_projectile.GetComponent<Rigidbody>().isKinematic = false;
						_projectile.GetComponent<Rigidbody>().velocity = launchSpeed * ((launchPoint.up/2) + launchPoint.forward) ;
				}

		}


}