using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour {
	public dfPanel _optionsPanel;
	public dfLabel _tutorialText;
	public dfPanel _darkenPanel;
	public dfPanel _buildMenuPanel;
	public dfPanel _upgradeMenuPanel;
	public dfButton _optionsButton;
	public dfButton _resetButton;
	public dfPanel _allOptionsPanel;

	// Use this for initialization
	void Start () {
		_optionsPanel.IsVisible = false;
		_darkenPanel.IsVisible = false;
		_allOptionsPanel.IsVisible = true;
		_optionsPanel.BringToFront ();
		_optionsButton.BringToFront ();
		_resetButton.BringToFront ();
		_resetButton.Opacity = 1.0f;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void ShowMenu(){
		if(!_optionsPanel.IsVisible){
			_optionsPanel.IsVisible = true;
			_tutorialText.IsVisible = false;
			//_darkenPanel.IsVisible = true;
			if(_upgradeMenuPanel.IsVisible){
				_upgradeMenuPanel.Opacity = 0.1f;
			} else if (_buildMenuPanel.IsVisible){
				_buildMenuPanel.Opacity = 0.1f;
			}
			Time.timeScale = 0.0f;
		} else if(_optionsPanel.IsVisible){
			_optionsPanel.IsVisible = false;
			_tutorialText.IsVisible = true;
			//_darkenPanel.IsVisible = false;
			if(_upgradeMenuPanel.IsVisible){
				_upgradeMenuPanel.Opacity = 1.0f;
			} else if (_buildMenuPanel.IsVisible){
				_buildMenuPanel.Opacity = 1.0f;
			}
			Time.timeScale = 1.0f;
		}
	}
}
