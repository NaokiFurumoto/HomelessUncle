using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player�̈ړ��Ɋւ���N���X
/// </summary>
public partial class Player
{
    /// <summary> �A�j���[�V���� </summary>
    [SerializeField]
    private Animator playerAnim;

    //�L���b�V���p
    private InputManager inputManager;

    /// <summary> �ړ��X�s�[�h </summary>
    [SerializeField]
    private float moveSpeed;

    /// <summary> �^�b�v�����ʒu </summary>
    [SerializeField]
    private Vector2 tapPos;

    /// <summary> �����ׂ����� </summary>
    private Vector2 direction;

    /// <summary> �v���[���[�̌����̈ꎞ�I�ȑޔ� </summary>
    private Vector3 tempScale;

    /// <summary> �v���C���[���ړ��\���ǂ����̔��� </summary>
    private bool isMove;

    #region �v���p�e�B
    public Vector2 Direction => direction;
    public Animator PlayerAnim => playerAnim;
   public bool IsMove { get { return isMove; } set { isMove = value; } }
    #endregion

    private void MoveAwake()
    {
        direction = -Vector2.up;
        moveSpeed = 4.0f;
        //isMove = true;//�Ƃ肠����������
    }

    /// <summary> �������F�O���N���X </summary>
    private void MoveStart()
    {
        inputManager = InputManager.Instance;
        playerAnim ??= GameObject.FindGameObjectWithTag("PlayerAnimator")
                                  .GetComponent<Animator>();

        //gameController ??= GameObject.FindGameObjectWithTag("GameController")
        //                               .GetComponent<GameController>();
    }

    /// <summary>
    /// �ړ��X�V
    /// �ҋ@���������Ă�Ƃ��ɏo����H
    /// </summary>
    private void MoveUpdate()
    {
        if (!isMove)
            return;

        //�^�b�`��Ԃňړ��𐧌�
        switch (inputManager.TouchPhase)
        {
            case TouchPhase.Began://��ʂɎw���G�ꂽ
                ChangeState(stateWalking);
                tapPos = inputManager.TouchBeginPos;
                PlayerTurning();
                CharacterMovement();
                break;

            case TouchPhase.Moved://��ʏ�Ŏw�������Ă�Ƃ�
                PlayerTurning();
                CharacterMovement();
                break;

            case TouchPhase.Ended://�w�������
                tapPos = inputManager.TouchingLastPos;
                transform.position = new Vector2(tapPos.x, tapPos.y);
                ChangeState(stateIdle);
                inputManager.TouchPhase = TouchPhase.Canceled;
                break;
        }
    }

    /// <summary>
    /// �^�b�v�����ꏊ�ɕ����]��
    /// </summary>
    private void PlayerTurning()
    {
        tapPos = inputManager.TouchingPos;
        direction = new Vector2(tapPos.x - transform.position.x,
                                  tapPos.y - transform.position.y).normalized;

        PlayerAnimation(direction.x, direction.y);
    }

    /// <summary>
    /// �����ɍ��킹���A�j���[�V�����̐؂�ւ�
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void PlayerAnimation(float x, float y)
    {
        //0.5�̐��������ɍ��킹��
        x = Mathf.RoundToInt(x);
        y = Mathf.RoundToInt(y);

        tempScale = transform.localScale;
        tempScale.x = x > 0 ? Mathf.Abs(tempScale.x) : -Mathf.Abs(tempScale.x);
        transform.localScale = tempScale;

        //�A�j���[�V�����̂��߂ɏ�����������
        x = Mathf.Abs(x);
        playerAnim.SetFloat("FaceX", x);
        playerAnim.SetFloat("FaceY", y);
    }

    /// <summary>
    /// �v���C���[�̈ړ�
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void CharacterMovement()
    {
        var pos = Vector2.Lerp(transform.position,
                               inputManager.TouchingPos,
                               moveSpeed * Time.deltaTime);

        var x = Mathf.Clamp(pos.x, -5, 5);
        var y = Mathf.Clamp(pos.y, -9, 8);

        transform.position = new Vector2(x, y);
    }
}
