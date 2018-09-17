using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	[SerializeField] private GameObject[] _menus;
	private GameObject _currentMenu;

	private void OnEnable()
	{
		CourseManager.OnStartRound += OnStartRound;
		CourseManager.OnEndRound += OnEndRound;
		PlayerController.onHitBall += OnHitBall;
		CourseManager.OnEndGame += OnEndGame;
	}

	private void OnDisable()
	{
		CourseManager.OnEndGame -= OnEndGame;
		CourseManager.OnStartRound -= OnStartRound;
		CourseManager.OnEndRound -= OnEndRound;
		PlayerController.onHitBall -= OnHitBall;
	}

	private void OnEndGame()
	{
		OpenMenu(2);
	}
	private void OnEndRound()
	{
		OpenMenu(1);
	}

	private void OnHitBall()
	{
		CloseMenus();
	}

	private void OnStartRound()
	{
		OpenMenu(0);
	}

	private void OpenMenu(int menu)
	{
		if(_currentMenu == null)
			_menus[0].SetActive(false);
		else
			_currentMenu.SetActive(false);
		
		_menus[menu].SetActive(true);
		_currentMenu = _menus[menu];
	}

	private void CloseMenus()
	{
		_currentMenu.SetActive(false);
	}

	public void CloseCourse()
	{
		SceneManager.LoadScene("MainMenu");
	}
	
}
