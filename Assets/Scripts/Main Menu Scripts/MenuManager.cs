using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private static MenuManager instance;
    private int arrowPosition = 0;
    private List<int> arrowYValue = new List<int> {-30, -230, -430};

    public Image leftArrow;
    public Image rightArrow;

    [Header("Scenes")]
    [SerializeField] private string PlayScene = "Scene_1";
    [SerializeField] private string ControlScene = "Controls";

    void Awake()
    {
        instance = this;
    }

    public static MenuManager GetInstance()
    {
        return instance;
    }

    void Start()
    {
        ImageFade.GetInstance().StartFade(Color.clear, 1f, 1f);
    }

    void Update()
    {
        UpdateArrows();
        
    }

    public void OnMoveUp(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            UpdatePosition((arrowPosition == 0) ? 2 : (arrowPosition - 1));
        }
    }

    public void OnMoveDown(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            UpdatePosition((arrowPosition + 1) % 3);
        }
    }

    public void OnEnter(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(arrowPosition == 0)
            {
                StartCoroutine(ChangeScene(PlayScene));
            }
            else if(arrowPosition == 2)
            {
                StartCoroutine(ChangeScene(ControlScene));
            }
        }
    }

    private void UpdateArrows()
    {
        RectTransform leftTransform = leftArrow.GetComponent<RectTransform>();
        Vector3 newleft = leftTransform.localPosition;
        newleft.y = arrowYValue[arrowPosition];
        leftTransform.localPosition = newleft;

        RectTransform rightTransform = rightArrow.GetComponent<RectTransform>();
        Vector3 newRight = rightTransform.localPosition;
        newRight.y = arrowYValue[arrowPosition];
        rightTransform.localPosition = newRight;
    }

    private IEnumerator ChangeScene(string scene)
    {
        ImageFade.GetInstance().StartFade(Color.black, 2f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(scene);
    }

    public void UpdatePosition(int newPos)
    {
        arrowPosition = newPos;
    }
}
