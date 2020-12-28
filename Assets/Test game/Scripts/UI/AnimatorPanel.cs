using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPanel : Panel
{
	protected string anim_identifier_Appearing = "isMenuAppearing";
	[SerializeField] protected Animator animator;

	public override void Show()
	{
		base.Show();
		animator.SetBool(anim_identifier_Appearing, true);
	}

	public override void Hide()
	{
		animator.SetBool(anim_identifier_Appearing, false);
		CancelInvoke();
		Invoke("HidePanel", 1.0f);
	}
	private void HidePanel()
	{
		base.Hide();
	}
}
