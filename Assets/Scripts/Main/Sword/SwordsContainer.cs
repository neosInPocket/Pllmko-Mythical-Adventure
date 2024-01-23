using System.Collections.Generic;
using UnityEngine;

public class SwordsContainer : MonoBehaviour
{
	[SerializeField] private List<Sword> swords;

	public Sword Initialize()
	{
		var sword = swords[PlayerDataReader.CustomData.m_swordIndex];
		sword.gameObject.SetActive(true);
		sword.Initialize();
		return sword;
	}
}
