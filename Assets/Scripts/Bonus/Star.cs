﻿using UnityEngine;

public class Star : Bonus {
    private void Start() {
        ColorUtility.TryParseHtmlString("#f7ff1e", out color);
    }

    protected override void SelfBonusEffect() {
        base.SelfBonusEffect();
        AudioController.instance.Play("StarEffect");
    }
}
