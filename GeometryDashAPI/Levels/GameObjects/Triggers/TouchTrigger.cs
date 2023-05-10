﻿using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(1595)]
    public class TouchTrigger : Trigger
    {
        [GameProperty("51", 0, true, Order = OrderTriggerBase + 1)] public int TargetGroupID { get; set; }
        [GameProperty("81", false, false, Order = OrderTriggerBase + 2)] public bool HoldMode { get; set; } = false;
        [GameProperty("82", ToggleMode.None, false, Order = OrderTriggerBase + 3)] public ToggleMode ToggleMode { get; set; } = ToggleMode.None;
        [GameProperty("89", false, false, Order = OrderTriggerBase + 4)] public bool DualMode { get; set; } = false;

        // robtop 300iq coder
        [GameProperty("87", false, Order = 102)] private bool _multiTrigger { get; set; }
        public override bool MultiTrigger
        {
            get => !_multiTrigger;
            set => _multiTrigger = value;
        }
        
        public TouchTrigger() : base(1595)
        {
            IsTrigger = true;
        }
    }
}
