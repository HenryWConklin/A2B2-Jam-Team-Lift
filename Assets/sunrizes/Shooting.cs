using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    public int ammoCount = 20;
    public TextMeshProUGUI ammoCountText;

    public float fireRate;

    private bool hasShot;
    private bool canShoot = true;

    private void Start()
    {
        ammoCountText.text = ammoCount.ToString();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && !hasShot)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        hasShot = true;
        Debug.Log("SHOOT");
        if (!canShoot)
        {
            yield return new WaitForSeconds(1f);
            ammoCount = 20;
            ammoCountText.text = ammoCount.ToString();
            canShoot = true;
            hasShot = false;
            yield break;
        }
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        ammoCount--;
        if (ammoCount <= 0)
        {
            canShoot = false;
        }
        ammoCountText.text = ammoCount.ToString();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shootPoint.up * bulletForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(fireRate);
        hasShot = false;
    }
}
