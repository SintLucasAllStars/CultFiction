using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerAbilitys {
	void OnStart();
	void EveryFrame();
	void BeforeAbility();
	void WhileAbility();
	void AfterAbility();
}
