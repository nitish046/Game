using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HenryController : MonoBehaviour
{
    private bool raccoonOnTrash = false;
    private bool doneWaiting = true;
    private int movementIndex = 0;

    public Button restartButton;
    public Button quitButton;
    public TMP_Text loseText;
    public Vector3[] movementPoints;
    [SerializeField] float speed = 5f;
    [SerializeField] private HideOnCollide collisionOccur;

    private void Start()
    {
        collisionOccur.onRaccoonFirstTimeOnTrash += CollisionOccur_onRaccoonFirstTimeOnTrash;
    }

    private void CollisionOccur_onRaccoonFirstTimeOnTrash(object sender, System.EventArgs e)
    {
        raccoonOnTrash = true;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    void Update()
    {
        if((raccoonOnTrash && movementIndex < movementPoints.Length) && doneWaiting)
        {
            HenryMovement();
        }

    }

    private void HenryMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, movementPoints[movementIndex], speed * Time.deltaTime);
        transform.LookAt(movementPoints[movementIndex]);

        if(transform.position == movementPoints[movementIndex])
        {
            StartCoroutine(waitHenry());
            
        }
    }

    IEnumerator waitHenry()
    {
        doneWaiting = false;
        yield return new WaitForSeconds(2f);
        movementIndex++;
        doneWaiting = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            loseText.text = "You were Caught! You Lose!";
            loseText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
        }
    }
}
