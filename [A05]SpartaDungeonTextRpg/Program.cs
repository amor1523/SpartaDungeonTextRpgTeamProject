﻿using _A05_SpartaDungeonTextRpg;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SpartaDungeonTextRpg
{
    public class GameManager
    {
        Dictionary<Job, string> dict= new Dictionary<Job, string>()
        {
            {Job.Knight, "전사"},
            {Job.Mage, "마법사"},
            {Job.Archer, "궁수"}
        };
        private Player player;
        private List<Monster> monsters;
        private Random random = new Random();
        private Battle battle;

        public GameManager()
        {
            InitializeGame();
            PlayerName();
            PlayerJob();
            battle = new Battle(player, monsters);
        }

        private void InitializeGame()
        {
            player = new Player();
            monsters = new List<Monster>(); // 몬스터 리스트 초기화

            // 몬스터 생성 및 추가
            Monster monster = new Monster();
            monster.Monsters(player.Level); // 플레이어 레벨에 맞게 몬스터 생성
            monster.GenerateMonster(); // 몬스터 생성
            monsters.AddRange(monster.CreatedMonster); // 생성된 몬스터를 리스트에 추가
        }

        private void PlayerName()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.Write("원하시는 이름을 성정해주세요\n>> ");
            player.Name = Console.ReadLine();
        }

        private void PlayerJob()
        {
            Console.Clear();
            Console.WriteLine("던전에 들어가기 전 당신의 직업을 선택해주세요.");
            Console.WriteLine("직업마다 기본 스탯과 스킬이 다를 수 있습니다.");
            Console.WriteLine("[1] 전사   | Atk: 10, Def: 5, Hp: 100");
            Console.WriteLine("[2] 마법사 | Atk: 8, Def: 3, Hp: 80");
            Console.WriteLine("[1] 전사 | Atk: 13, Def: 4, Hp: 90");

            int input = ConsoleUtility.PromptMenuChoice(1, 3);
            switch (input)
            {
                case 1:
                    player.Job = Job.Knight;
                    MainMenu();
                    break;

                case 2:
                    player.Job = Job.Mage;
                    MainMenu();
                    break;
                case 3:
                    player.Job = Job.Archer;
                    MainMenu();
                    break;

            }
        }

        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 전투시작");
            Console.WriteLine();

            int input = ConsoleUtility.PromptMenuChoice(1, 2);
            switch (input)
            {
                case 1:
                    StatusMenu();
                    break;

                case 2:
                    battle.BattleMenu();
                    break;
            }
        }

        public void StatusMenu()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("[상태보기]");
            Console.WriteLine("캐릭터의 정보를 표시합니다.\n");
            Console.WriteLine($"Lv. {player.Level}");
            Console.WriteLine($"{player.Name} ({})");
            Console.WriteLine($"공격력 :{player.Atk}");
            Console.WriteLine($"방어력 :{player.Def}");
            Console.WriteLine($"체력 :{player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G\n");
            Console.WriteLine("0. 나가기\n");

            int input = ConsoleUtility.PromptMenuChoice(0, 0);
            switch (input)
            {
                case 0:
                    MainMenu();
                    break;
            }
        }

        
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                GameManager gameManager = new GameManager();
                gameManager.MainMenu();
            }
        }
    }
}
