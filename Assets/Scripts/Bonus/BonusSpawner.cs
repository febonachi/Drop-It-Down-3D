﻿using System.Linq;
using UnityEngine;

public class BonusSpawner : MonoBehaviour {
    public int chance = 10;
    public GameObject[] prefabs;

    private GameObject[] bonuses;
    private bool activated = false;

    private void Start() {
        if (prefabs.Length == 0) {
            bonuses = Resources.LoadAll("Bonuses").Select(o => (o as GameObject).gameObject).ToArray();
        } else {
            bonuses = prefabs;
        }

        if (Random.Range(1, 101) <= chance) {
            SpawnBonus();
        }
	}

    private void SpawnBonus() {
        if (activated) return;
        GameObject bonus = bonuses[Random.Range(0, bonuses.Length)];
        if (bonus && GameController.instance.Score >= Bonus.BonusAccessibility(bonus.GetComponent<Bonus>().type)) {
            Instantiate(bonus, transform.position, transform.rotation, transform);
            activated = true;
        }
    }

    public bool SpawnActivatedBonus(BonusEffect type) {
        if (activated && bonuses == null) return false;
        GameObject bonus = bonuses[Random.Range(0, bonuses.Length)];
        BonusEffect bonusType = bonus.GetComponent<Bonus>().type;
        if (bonusType != type || bonusType == BonusEffect.Star) return false;
        if (bonus && GameController.instance.Score >= Bonus.BonusAccessibility(bonusType)) {
            Instantiate(bonus, transform.position, transform.rotation, transform);
            activated = true;
        }
        return true;
    }
}
