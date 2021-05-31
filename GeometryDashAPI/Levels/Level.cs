﻿using GeometryDashAPI.Data.Models;
using GeometryDashAPI.Exceptions;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Levels.GameObjects.Triggers;
using GeometryDashAPI.Parsers;

namespace GeometryDashAPI.Levels
{
    public class Level
    {
        /// <summary>
        /// The temporal property
        /// </summary>
        public string LoadedString { get; private set; }

        public const string DefaultLevelString = "H4sIAAAAAAAAC6WQ0Q3CMAxEFwqSz4nbVHx1hg5wA3QFhgfn4K8VRfzci-34Kcq-1V7AZnTCg5UeQUBwQc3GGzgRZsaZICKj09iJBzgU5tcU-F-xHCryjhYuSZy5fyTK3_iI7JsmTjX2y2umE03ZV9RiiRAmoZVX6jyr80ZPbHUZlY-UYAzWNlJTmIBi9yfXQXYGDwIAAA==";

        public List<string> BlocksWithoutLoad { get; set; }
        public BindingBlockID BlockBinding { get; set; }

        public ColorList Colors { get; private set; }
        public BlockList Blocks { get; private set; }

        #region Properties
        public int CountBlock { get => Blocks.Count; }
        public int CountColor { get => Colors.Count; }
        #endregion

        #region Level properties
        public GameMode GameMode { get; set; } = GameMode.Cube;
        public SpeedType PlayerSpeed { get; set; } = SpeedType.Default;
        public bool Dual { get; set; }
        public bool TwoPlayerMode { get; set; }
        public bool Mini { get; set; }
        public byte Fonts { get; set; }
        public byte Background { get; set; }
        public byte Ground { get; set; }

        public float MusicOffset { get; set; }
        public int kA15 { get; set; }
        public int kA16 { get; set; }
        public string kA14 { get; set; }
        public int kA17 { get; set; }
        public int kS39 { get; set; }
        public int kA9 { get; set; }
        public int kA11 { get; set; }
        #endregion

        #region Constructor
        public Level(BindingBlockID blockBinding = null)
        {
            this.Initialize(blockBinding);
            this.Load(DefaultLevelString);
        }

        public Level(string data, BindingBlockID blockBinding = null)
        {
            this.Initialize(blockBinding);
            this.Load(data);
        }

        public Level(LevelCreatorModel model, BindingBlockID blockBinding = null)
        {
            this.Initialize(blockBinding);
            this.Load(model.LevelString);
        }
        #endregion

        protected virtual void Initialize(BindingBlockID blockBinding)
        {
            BlocksWithoutLoad = new List<string>();
            BlockBinding = blockBinding;
            Colors = new ColorList();
            Blocks = new BlockList();
        }

        public void AddBlock(IBlock block)
        {
            this.Blocks.Add(block);
        }

        public void AddColor(Color color)
        {
            this.Colors.AddColor(color);
        }

        #region Load and save methods
        protected virtual void Load(string compressData)
        {
            string data = Crypt.GZipDecompress(GameConvert.FromBase64(compressData));
            LoadedString = data;
            string[] splitData = data.Split(';');
            string[] levelProperties = splitData[0].Split(',');
            for (int i = 0; i < levelProperties.Length; i += 2)
            {
                switch (levelProperties[i])
                {
                    case "kS38":
                        this.LoadColors(levelProperties[i + 1]);
                        break;
                    case "kA13":
                        MusicOffset = GameConvert.StringToSingle(levelProperties[i + 1]);
                        break;
                    case "kA15":
                        kA15 = int.Parse(levelProperties[i + 1]);
                        break;
                    case "kA16":
                        kA16 = int.Parse(levelProperties[i + 1]);
                        break;
                    case "kA14":
                        kA14 = levelProperties[i + 1];
                        break;
                    case "kA6":
                        Background = byte.Parse(levelProperties[i + 1]);
                        break;
                    case "kA7":
                        Ground = byte.Parse(levelProperties[i + 1]);
                        break;
                    case "kA17":
                        kA17 = int.Parse(levelProperties[i + 1]);
                        break;
                    case "kA18":
                        Fonts = byte.Parse(levelProperties[i + 1]);
                        break;
                    case "kS39":
                        kS39 = int.Parse(levelProperties[i + 1]);
                        break;
                    case "kA2":
                        GameMode = (GameMode)int.Parse(levelProperties[i + 1]);
                        break;
                    case "kA3":
                        Mini = GameConvert.StringToBool(levelProperties[i + 1], false);
                        break;
                    case "kA8":
                        Dual = GameConvert.StringToBool(levelProperties[i + 1], false);
                        break;
                    case "kA4":
                        PlayerSpeed = (SpeedType)int.Parse(levelProperties[i + 1]);
                        break;
                    case "kA9":
                        kA9 = int.Parse(levelProperties[i + 1]);
                        break;
                    case "kA10":
                        TwoPlayerMode = GameConvert.StringToBool(levelProperties[i + 1], false);
                        break;
                    case "kA11":
                        kA11 = int.Parse(levelProperties[i + 1]);
                        break;
                    default:
                        throw new PropertyNotSupportedException(levelProperties[i], levelProperties[i + 1]);
                }
            }
            this.LoadBlocks(splitData);
        }

        protected virtual void LoadColors(string colorsData)
        {
            foreach (string colorData in colorsData.Split('|'))
            {
                if (colorData == string.Empty)
                    continue;
                this.Colors.AddColor(new Color(colorData));
            }
        }

        protected virtual void LoadBlocks(string[] blocksData)
        {
            for (var i = 1; i < blocksData.Length - 1; i++)
            {
                var block = ObjectParser.DecodeBlock(blocksData[i]);
                Blocks.Add(block);
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("kS38,");
            List<Color> colorList = Colors.ToList();
            foreach (Color color in colorList)
                builder.Append($"{color.ToString()}|");

            builder.Append($",kA13,{MusicOffset},kA15,{kA15},kA16,{kA16},kA14,{kA14},kA6,{Background}," +
                $"kA7,{Ground},kA17,{kA17},kA18,{Fonts},kS39,{kS39},kA2,{(byte)GameMode}," +
                $"kA3,{GameConvert.BoolToString(Mini)},kA8,{GameConvert.BoolToString(Dual)}," +
                $"kA4,{(byte)PlayerSpeed},kA9,{kA9},kA10,{GameConvert.BoolToString(TwoPlayerMode)},kA11,{kA11};");

            foreach (Block block in Blocks)
            {
                if (block is MoveTrigger)
                {
                    
                }
                builder.Append(ObjectParser.EncodeBlock(block.GetType(), block));
                builder.Append(';');
            }

            foreach (string rawblock in BlocksWithoutLoad)
            {
                builder.Append(rawblock);
                builder.Append(';');
            }

            byte[] bytes = Crypt.GZipCompress(Encoding.ASCII.GetBytes(builder.ToString()));
            return GameConvert.ToBase64(bytes);
        }
        #endregion
    }
}
