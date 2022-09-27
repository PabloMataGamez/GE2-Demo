using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField]
    private GameObject _controls;

    [SerializeField]
    private Animator _animator;

    public string _popUp;
    public bool _openPopUp;

    public void OpenLevel()
    {
        SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Single);
        AudioManager._mainMenu = false;
	    FindObjectOfType<AudioManager>().StopPlaying("StartMenu");
        FindObjectOfType<AudioManager>().Play("Select");
        FindObjectOfType<AudioManager>().Play("Background");
    }

    public void ShowControls()
    {
        _animator.SetTrigger("pop");
        FindObjectOfType<AudioManager>().Play("Select");
        _openPopUp = true;
    }

    public void SetPopUpFalse()
    {
        _openPopUp = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !_openPopUp)
        {
            FindObjectOfType<AudioManager>().Play("OpenMenu");
            _openPopUp = true;
            _animator.SetTrigger("pop");
        }
        else if (Input.GetKeyDown(KeyCode.P) && _openPopUp)
        {
            FindObjectOfType<AudioManager>().Play("CloseMenu");
            _animator.SetTrigger("close");
            _openPopUp = false;
        }
    }
}
