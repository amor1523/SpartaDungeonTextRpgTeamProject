﻿using _A05_SpartaDungeonTextRpg;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SpartaDungeonTextRpg
{
    public class GameManager
    {
        Dictionary<Job, string> dict_Job = new Dictionary<Job, string>()
            {
                {Job.Knight, "전사" },
                {Job.Mage, "마법사" },
                {Job.Archer, "궁수" }
            };
        private Player player;
        private List<Monster> monsters; // 몬스터 리스트 추가
        private Random random = new Random();
        public GameManager()
        {
            InitializeGame();
            PlayerName();
            PlayerClass();
            MainMenu();
        }

        private void InitializeGame()
        {
            player = new Player();
            monsters = new List<Monster>(); // 몬스터 리스트 초기화

            // 몬스터 생성 및 추가
            Monster monster = new Monster(0, "고블린", 1, 20, 35, 20, 50);
            monster.Monsters(player.Level); // 플레이어 레벨에 맞게 몬스터 생성
            monster.GenerateMonster(); // 몬스터 생성
            monsters.AddRange(monster.CreatedMonster); // 생성된 몬스터를 리스트에 추가
        }

        private void PlayerName()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.Write("원하시는 이름을 설정해주세요\n>> ");
            player.Name = Console.ReadLine();
        }

        private void PlayerClass()
        {
            Console.Clear();
            Console.WriteLine("이제 직업을 선택하실 수 있습니다.");
            Console.WriteLine("각 직업마다 기본 스텟과 스킬이 다를 수 있습니다.");
            Console.WriteLine("[1] 전사   | Atk: 10 Def: 5 Hp: 100");
            Console.WriteLine("[2] 마법사 | Atk:  8 Def: 3 Hp: 80");
            Console.WriteLine("[3] 궁수   | Atk: 13 Def: 4 Hp: 90");

            int input = ConsoleUtility.PromptMenuChoice(1, 3);
            switch (input)
            {
                case 1:
                    player.Job = Job.Knight;
                    player.Atk = 10;
                    player.Def = 5;
                    player.HP = 100;
                    break;

                case 2:
                    player.Job = Job.Mage;
                    player.Atk = 8;
                    player.Def = 3;
                    player.HP = 80;
                    break;
                
                case 3:
                    player.Job = Job.Archer;
                    player.Atk = 13;
                    player.Def = 4;
                    player.HP = 90;
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
                    BattleMenu();
                    break;
            }
        }

        public void StatusMenu()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("[상태보기]");
            Console.WriteLine("캐릭터의 정보를 표시합니다.\n");
            Console.WriteLine($"Lv. {player.Level}");
            Console.WriteLine($"{player.Name} ({dict_Job[player.Job]})");
            Console.WriteLine($"공격력 :{player.Atk}");
            Console.WriteLine($"방어력 :{player.Def}");
            Console.WriteLine($"체력 :{player.HP}");
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

        public void BattleMenu()
        {
            Console.Clear();
            ConsoleUtility.PrintTextHighlights(ConsoleColor.Cyan, "", "Battle!!\n");
            for (int i = 0; i < monsters.Count; i++)
            {
                if (!monsters[i].IsDead)
                {
                    Console.WriteLine($"Lv.{monsters[i].Level} {monsters[i].Name} HP {(monsters[i].IsDead ? "Dead" : monsters[i].Hp.ToString())}");
                }
                else
                {
                    ConsoleUtility.PrintTextHighlights(ConsoleColor.DarkGray, "", $"Lv.{monsters[i].Level} {monsters[i].Name} HP Dead");
                }
            }
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level}  {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.HP}\n");
            Console.WriteLine("1. 공격\n");

            int input = ConsoleUtility.PromptMenuChoice(1, 1);
            switch (input)
            {
                case 1:
                    PlayerAttack();
                    break;
            }
        }
        public void PlayerAttack()
        {
            Console.Clear();
            ConsoleUtility.PrintTextHighlights(ConsoleColor.Cyan, "", "Battle!!\n");
            Thread.Sleep(500);
            for (int i = 0; i < monsters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Lv.{monsters[i].Level} {monsters[i].Name} HP {monsters[i].Hp}");
            }
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level}  {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.HP}\n");
            Console.WriteLine("0.취소");

            int input = ConsoleUtility.PromptMenuChoice(0, monsters.Count);
            if (input == 0)
            {
                Console.Clear();
                BattleMenu();
            }
           
            Monster selectedMonster = monsters[input - 1];
            int damageDealt = player.Atk;
            selectedMonster.TakeDamage(damageDealt);
            Console.WriteLine($"Lv.{selectedMonster.Level} {selectedMonster.Name} 을/를 맞췄습니다. [데미지 : {damageDealt}]");
            Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine($"Lv.{selectedMonster.Level} {selectedMonster.Name}");
            Thread.Sleep(500);
            Console.WriteLine($"HP {selectedMonster.Hp}\n");
            Thread.Sleep(1000);
            Console.WriteLine("0.다음");
            int inputs = ConsoleUtility.PromptMenuChoice(0, 0);
            if (inputs == 0)
            {
                // 몬스터들이 플레이어를 한 번씩 공격
                foreach (Monster monster in monsters)
                {
                    Console.Clear();
                    EnemyAttack(monster);
                }
                // 모든 몬스터가 공격한 후에 플레이어가 살아있는지 확인
                if (!player.IsDead)
                {
                    BattleMenu();
                }
                else
                {
                    BattleResult(false);
                }
            }
        }
        public void EnemyAttack(Monster targetMonster)
        {   //공격할 몬스터가  살아있는지 확인
            if (!targetMonster.IsDead)
            {
                ConsoleUtility.PrintTextHighlights(ConsoleColor.Cyan, "", "공격!!\n");
                Console.WriteLine($"{targetMonster.Name} 의 공격!");
                Thread.Sleep(500);
                int damageDealt = targetMonster.Atk;
                player.TakeDamage(damageDealt);
                Console.WriteLine($"Lv.{player.Level} {player.Name} 을/를 맞췄습니다. [데미지 : {damageDealt}]");
                Thread.Sleep(1000);
                Console.WriteLine();
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Thread.Sleep(500);
                Console.WriteLine($"HP {player.HP}\n");
                Thread.Sleep(1000);
            }
            else
            {
                ConsoleUtility.PrintTextHighlights(ConsoleColor.DarkGray, "", $"{targetMonster.Name} 은/는 이미 죽었습니다.\n");
            }

        }

        public void BattleResult(bool victory)
        {
            Console.Clear();
            ConsoleUtility.PrintTextHighlights(ConsoleColor.Cyan, "", "전투 결과\n");
            if (victory)
            {
                ConsoleUtility.PrintTextHighlights(ConsoleColor.Green, "", "전투 승리\n");
                Console.WriteLine("던전에서 몬스터를 잡았습니다.");
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP (전투 전 HP) -> {player.HP}\n");
            }
            else
            {
                ConsoleUtility.PrintTextHighlights(ConsoleColor.Red, "", "전투 패배\n");
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP (전투 전 HP) -> {player.HP}\n");
            }
            Console.WriteLine("0. 다음\n");
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
