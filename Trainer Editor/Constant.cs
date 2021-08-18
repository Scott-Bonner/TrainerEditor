﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace Trainer_Editor {
    public enum Constants {
        Species, Moves, Items, TrainerClass
    };
    public class Constant : ObservableObject {
        public Constant() { }
        public Constant(Constants type) {
            switch (type) {
                case Constants.Species:
                    DefaultRegex = new Regex(@"(?<=#define\s+)SPECIES\w+(?=\s)");
                    PartialHeaderPath = @"\include\constants\species.h";
                    break;
                case Constants.Moves:
                    DefaultRegex = new Regex(@"MOVE_\w+");
                    PartialHeaderPath = @"\include\constants\moves.h";
                    break;
                case Constants.Items:
                    DefaultRegex = new Regex(@"(?<=#define\s+)ITEM(\w(?!USE_|B_USE|_COUNT|FIELD_ARROW))+(?=\s)");
                    PartialHeaderPath = @"\include\constants\items.h";
                    break;
                case Constants.TrainerClass:
                    DefaultRegex = new Regex(@"(?<=#define\s+)TRAINER_CLASS_\w+");
                    PartialHeaderPath = @"\include\constants\trainers.h";
                    break;
                default:
                    break;
            }
            Type = type;
            Regex = DefaultRegex;
        }

        public Constants Type { get; set; }
        public string PartialHeaderPath { get; set; }
        public Regex DefaultRegex { get; set; }
        [JsonIgnore]
        public string HeaderPath { get => FileManager.Instance.FilePaths.PokeEmeraldDirectory + PartialHeaderPath; }
        [JsonIgnore]
        public string JsonPath { get => Directory.GetCurrentDirectory() + @"\Constants\" + Type.ToString() + ".json";}
        private Regex regex;
        public Regex Regex {
            get => regex;
            set {
                if (!string.IsNullOrEmpty(value?.ToString()))
                    regex = value;
                OnPropertyChanged("Regex");
            }
        }
        private List<string> list;
        public List<string> List {
            get => list;
            set { list = value; OnPropertyChanged("List"); }
        }
        public void SetRegexToDefault() {
            Regex = DefaultRegex;
        }
    }

}
