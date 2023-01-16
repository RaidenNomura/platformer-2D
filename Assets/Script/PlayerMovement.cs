using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Exposed

    [SerializeField] float _moveSpeed;
    [SerializeField] float _jumpForce;
    [SerializeField] int _maxJump;
    [SerializeField] float _fallGravity;
    [SerializeField] Animator _animator;
    [SerializeField] float _radius;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Récupération des bouttons pour le déplacement
        _direction.x = Input.GetAxisRaw("Horizontal") * _moveSpeed;

        //Récupération des bouttons pour le saut
        if (Input.GetButtonDown("Jump") && _numbJump < _maxJump)
            _isJumping = true;

        Collider2D floorCollider = Physics2D.OverlapCircle(transform.position, _radius);
        CheckGround(floorCollider);
    }

    private void FixedUpdate()
    {
        _animator.SetFloat("moveSpeedX", Mathf.Abs(_direction.x));
        _animator.SetFloat("moveSpeedY", _direction.y);

        _direction.y = _rb2D.velocity.y;
        //Application de la force pour le déplacement
        _rb2D.velocity = _direction;

        if (_direction.x < -0.1f || _direction.x > 0.1f)
        {
            transform.right = new Vector2(_direction.x, 0);
        }

        //Application de la force pour le saut
        if (_isJumping && _numbJump < _maxJump)
        {
            _numbJump++;
            _isJumping = false;
            Vector2 jumpinVector = new Vector2(_direction.x, _direction.y = _jumpForce * 100);
            _rb2D.AddForce(jumpinVector);
        }
    }

    #endregion

    #region Methods

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 positionTransformOffset = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Gizmos.DrawWireSphere(positionTransformOffset, _radius);
    }

    void CheckGround(Collider2D floorCollider)
    {
        if (floorCollider != null)
        {
            if (floorCollider.tag == "Floor" || floorCollider.tag == "PlateForme")
            {
                _animator.SetTrigger("Grounded");
                _numbJump = 0;

                if (floorCollider.CompareTag("PlateForme"))
                {
                    transform.SetParent(floorCollider.transform);
                }
                else
                {
                    transform.SetParent(null);
                }
            }
        }

    }

    #endregion

    #region Private & Protected

    private Rigidbody2D _rb2D;
    private Vector2 _direction;
    private bool _isJumping;
    private int _numbJump = 0;

    #endregion
}
