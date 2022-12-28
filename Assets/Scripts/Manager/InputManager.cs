using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �^�b�`���͊Ǘ�
/// </summary>
public class InputManager : MonoBehaviour
{
    /// <summary> �C���X�^���X <summary>
    public static InputManager Instance;

    /// <summary> ���C���J���� <summary>
    private Camera mainCamera;

    /// <summary> ��ʃ^�b�`�\�Ȃ�true <summary>
    private bool touchFlag;

    /// <summary> �^�b�`�ʒu <summary>
    private Vector2 touchBeginPos, touchingPos, touchLastPos;

    /// <summary> �^�b�`��� <summary>
    private TouchPhase touchPhase;

    //�Q�[���Ǘ��N���X
    private Player player;


    /// <summary> �|�C���g�f�[�^ </summary>
    private PointerEventData pointerEventData;

    #region �v���p�e�B
    public bool TouchFlag => touchFlag;
    public Vector2 TouchBeginPos => touchBeginPos;
    public Vector2 TouchingPos => touchingPos;
    public Vector2 TouchingLastPos => touchLastPos;
    public TouchPhase TouchPhase
    {
        get { return touchPhase; }
        set { touchPhase = value; }
    }
    #endregion

    /// <summary> ������ <summary>
    private void Awake()
    {
        Instance ??= this;
        mainCamera = Camera.main;
        touchFlag = true;
        touchBeginPos = touchingPos
                      = touchLastPos
                      = Vector2.zero;
        touchPhase = TouchPhase.Canceled;
        //touchPhase = TouchPhase.Began;

        pointerEventData = new PointerEventData(EventSystem.current);

        player = GameObject.FindGameObjectWithTag("Player")
                                   .GetComponent<Player>();
    }

    /// <summary> ���͍X�V </summary>
    private void Update()
    {
        //Editor
        //if (Application.isEditor)
        //{ }
        //else//�[��
        //{
        //    //TODO:�ǉ��ŕK�v����
        //    if (Input.touchCount > 0)
        //    {
        //        Touch touch = Input.GetTouch(0);
        //        touchingPos = touch.position;
        //        touchPhase = touch.phase;
        //        touchFlag = true;
        //    }
        //}



        //�������u�Ԃ̃^�b�`�͎󂯕t����
        if (Input.GetMouseButtonDown(0))
        {
            var hitobjects = GetObjectAll();
            if (hitobjects?.Count > 0)
            {
                foreach (RaycastResult obj in hitobjects)
                {
                    if (obj.gameObject.layer == LayerMask.NameToLayer("UI"))
                    {
                        //�^�b�`�����Ȃ�
                        touchFlag = false;
                        return;
                    }
                }

            }

            touchFlag = true;
            touchPhase = TouchPhase.Began;
            touchBeginPos = touchingPos =
            mainCamera.ScreenToWorldPoint(Input.mousePosition);
            //player.IsMove = true;
            //player.ChangeState(player.Walking);
            Debug.Log("Begin");
        }

        if (touchFlag)
        {
            //�������u��
            if (Input.GetMouseButtonUp(0))
            {
                touchFlag = false;
                touchPhase = TouchPhase.Ended;
                touchLastPos = touchingPos;
                touchBeginPos = touchingPos
                                = Vector2.zero;
                //player.ChangeState(player.Idle);
                Debug.Log("End");
            }

            //�������ςȂ�
            if (Input.GetMouseButton(0))
            {
                var distance = Vector2.Distance(touchBeginPos, touchingPos);
                if (distance >= 0.5f)
                {
                    touchPhase = TouchPhase.Moved;

                }
                touchingPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
        }


    }

    /// <summary> �q�b�g�����I�u�W�F�N�g��S�Ď擾 <summary>
    private List<RaycastResult> GetObjectAll()
    {
        if (EventSystem.current != null)
        {
            List<RaycastResult> result = new List<RaycastResult>();
            pointerEventData.position = Input.mousePosition;
            EventSystem.current.RaycastAll(pointerEventData, result);
            return result;
        }

        return default;
    }
}
