using UnityEngine;
using UnityEngine.UI;

public class Bullets : MonoBehaviour
{
    public int numberBullet;
    private Transform player;
    private Button pick;

    public void PickUpBullets() // nhat bang dan
    {
        if (Input.GetAxis("PickUp") != 0)
        {
            if (player.transform.gameObject.GetComponent<PlayerFire_Network>().GetCurGun().name.Equals(transform.name))
            {
                player.transform.gameObject.GetComponent<PlayerFire_Network>().SetMaxBullet(numberBullet);
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {

        pick = transform.GetChild(0).GetChild(0).GetComponent<Button>();
    }

    void UpdatePlayer() // kiem tra khoang cach toi item 
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            if (player.transform.gameObject.GetComponent<PlayerFire_Network>().GetCurGun().name.Equals(transform.name))
            {

                if (Vector3.Distance(transform.position, player.position) <= 4f)
                {

                    pick.transform.gameObject.SetActive(true);
                    PickUpBullets();
                }
                else
                {
                    pick.transform.gameObject.SetActive(false);
                }
            }
        }
    }

    private void Update()
    {
        UpdatePlayer();
    }
}
