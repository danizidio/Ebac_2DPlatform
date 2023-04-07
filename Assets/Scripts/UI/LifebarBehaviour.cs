using System;
using UnityEngine;
using UnityEngine.UI;

public class LifebarBehaviour : MonoBehaviour
{
    public event Action<Transform> OnSetTarget;

    delegate void _onFollowTarget();
    _onFollowTarget OnFollowTarget;

    Transform target;

    Animator _anim;

    [SerializeField] float targetx;
    [SerializeField] float targety;
    [SerializeField] float targetz;

    [SerializeField] Image _redBar;

    GameObject _cam;

    void Awake()
    {
        OnSetTarget = SetTarget;

        _cam = GameObject.FindGameObjectWithTag("MainCamera");

        GetComponent<Canvas>().worldCamera = _cam.GetComponent<Camera>();

        _anim = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        OnFollowTarget?.Invoke();
    }

    void FollowTarget()
    {
        transform.position = new Vector3(target.position.x + targetx, target.position.y + targety, targetz);
        transform.eulerAngles = new Vector2(0, 0);
    }

    void SetTarget(Transform obj)
    {
        target = obj;

        OnFollowTarget = FollowTarget;
    }

    public void Show(float v)
    {
        _anim.SetTrigger("SHOW");

        _redBar.fillAmount = v;
    }
}
