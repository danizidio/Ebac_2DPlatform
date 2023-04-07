using UnityEngine;

public class SpecialAttack : MonoBehaviour
{

    [SerializeField] float _speed;
    [SerializeField] int _damage;

    float _moveDir = 1;

    private void LateUpdate()
    {
        transform.Translate(Vector2.right * _moveDir);
    }

    public void MoveDirection(float a)
    {
        _moveDir = _speed * a;
        transform.eulerAngles = new Vector2(a, 0);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Component damageable = collision.gameObject.GetComponent(typeof(ICanBeDamaged));

        if (damageable == null) return;

        (damageable as ICanBeDamaged).SufferDamage(_damage);

        damageable.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(800 * transform.eulerAngles.x, 500), ForceMode2D.Force);
    }
}
