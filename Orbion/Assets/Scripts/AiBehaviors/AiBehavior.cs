using UnityEngine;
using System.Collections;

public abstract class AiBehavior : MonoBehaviour {

	abstract public void OnBehaviorEnter();

	abstract public void OnBehaviorExit();

	abstract public void FixedUpdateAB();

	abstract public void UpdateAB();
}
