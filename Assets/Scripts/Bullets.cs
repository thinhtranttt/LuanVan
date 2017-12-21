using UnityEngine;
using UnityEngine.UI;

public class Bullets : MonoBehaviour {
    public int numberBullet;
    public Transform Player;

    private Button pick;

    public void PickUpBullets() // nhat bang dan
    {
        Debug.Log(transform.name + " " + numberBullet);
        if (Player.transform.gameObject.GetComponent<PlayerFire>().GetCurGun().name.Equals(transform.name))
        {
            Player.transform.gameObject.GetComponent<PlayerFire>().SetMaxBullet(numberBullet);
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        pick = transform.GetChild(0).GetChild(0).GetComponent<Button>();
    }
}
