﻿using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Specific
{
    [GameBlock(914)]
    public class TextBlock : DetailBlock
    {
        [GameProperty("25", 1)] public override int ZOrder { get; set; } = 1;

        public string Text
        {
            get => GameConvert.FromBase64String(text);
            set => text = GameConvert.ToBase64String(value);
        }
        [GameProperty("31", "A", true)] private string text = "A";

        public TextBlock() : base(914)
        {
        }
    }
}
