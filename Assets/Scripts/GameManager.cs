using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ゲーム全体管理
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LoadingAnimation _loadingAnimation;
    [SerializeField]
    private GameObject _titleUI;
    [SerializeField]
    private GameObject _inGameUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }
    private void OnClick()
    {
        StartCoroutine(Load());
    }
    private IEnumerator Load()
    {
        _loadingAnimation.StartLoadAnimation(() => {
            _titleUI.SetActive(false);
            _inGameUI.SetActive(true);
        });
        yield return new WaitForSeconds(4.5f);
        _loadingAnimation.FinsihLoadAnimation(()=> { });
    }
}
