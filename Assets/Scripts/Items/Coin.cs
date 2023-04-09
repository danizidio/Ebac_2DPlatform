using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] GameObject _particleFx;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.OnTakingCoins?.Invoke();

        Instantiate(_particleFx, this.transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }
}
