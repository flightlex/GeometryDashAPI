﻿using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(1520)]
    public class ShakeTrigger : Trigger
    {
        [GameProperty("10", 0.5f, true, Order = OrderTriggerBase + 1)] public float Duration { get; set; } = 0.5f;
        [GameProperty("75", 0f, false, Order = OrderTriggerBase + 2)] public float Strength { get; set; }
        [GameProperty("84", 0f, false, Order = OrderTriggerBase + 3)] public float Interval { get; set; }

        // robtop 300iq coder
        [GameProperty("87", false, Order = 102)] private bool _multiTrigger { get; set; }
        public override bool MultiTrigger
        {
            get => !_multiTrigger;
            set => _multiTrigger = value;
        }

        public ShakeTrigger() : base(1520)
        {
        }
    }
}
