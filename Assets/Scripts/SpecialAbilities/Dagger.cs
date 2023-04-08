using System.Collections;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] int _damage;

    bool _isStuck;
    float _moveDir = 1;

    private void Start()
    {
        StartCoroutine(TimeToDie());
        GetComponent<Rigidbody2D>().velocity = new Vector2(_moveDir, 2);
    }

    public void MoveDirection(float a)
    {
        _moveDir = _speed * a;
        transform.eulerAngles = new Vector2(a, 0);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        Component damageable = collision.gameObject.GetComponent(typeof(ICanBeDamaged));

        if(damageable == null)
        {
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            this.GetComponent<PolygonCollider2D>().enabled = false;

            return;
        }

        (damageable as ICanBeDamaged).SufferDamage(_damage);

        Destroy(this.gameObject);

    }

    public IEnumerator TimeToDie()
    {
        yield return new WaitForSeconds(5);

        Destroy(this.gameObject);
    }
}
