using UnityEngine;

public class MonkeyController : MonoBehaviour
{
    public Transform player;
    public GameObject Banana;
    private int trowSpeed = 3;

    private float moveSpeed = 3;

    private int minDist = 8;
    private int maxDist = 20;
    private float timer = 2;
    private RaycastHit hit;
    private bool canAttack;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    { 
        transform.LookAt(player);

        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit))
        {
            //Debug.Log("hitdist: " + hit.distance);

            if (hit.transform.CompareTag("Player") && hit.distance > minDist && hit.distance < maxDist)
            {
                //move monkey towards the player
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }

            //IF monkey is close to player it starts trowing banana's
            if (hit.transform.CompareTag("Player") && hit.distance < minDist +1)
            {
                Debug.Log("I can attack you");
                AttackPlayer();
            }
        }

        Debug.Log(canAttack);
    }

    private void AttackPlayer()
    {
        timer -= Time.deltaTime;
        GameObject banana;

        //Debug.Log("time: "+ timer);

        if (timer < 0)
        {
            //Debug.Log("Trowing");
            //Trow banana
            Vector3 TrowPos = transform.position + new Vector3(0, 2, 0);
            Vector3 targetPlayer = player.transform.position - transform.position;

            banana = Instantiate(Banana, TrowPos, transform.rotation);
            banana.GetComponent<Rigidbody>().AddForce(targetPlayer + new Vector3(0, 60, 0) * trowSpeed * Time.deltaTime, ForceMode.Impulse);
            timer = 3;
        }
    }
}
