﻿using System;

namespace _A05_SpartaDungeonTextRpg
{
    public enum Job
    {
        Knight,
        Mage,
        Archer
    }

    public class Player
    {   
        Random rand  = new Random();
<<<<<<< HEAD
        public string Name { get; set; }
        public Job Job { get; set; }
        public int Level { get; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int HP { get; set; }
        public float Gold { get; set; }
        public bool IsDead => HP <= 0;

        public Player()
        {
            Level = 1;
            Gold = 1500f;
            
      
=======
        public string Name { get; }
        public string Job { get; }

        // Level Atk Def 레벨업에 따른 수치 변경으로 인해 set 추가
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public float Gold { get; set; }
        public bool IsDead => Hp <= 0;

        // Exp, LevelUpExp 변수 추가
        public int Before_Exp { get; set; }
        public int After_Exp { get; set; }
        public int LevelUpExp { get; set; }

        public Player(string name, string job, int level, int atk, int def, int hp, float gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;

            // 초기값
            Before_Exp = 0;
            After_Exp = 0;
            LevelUpExp = 10;
>>>>>>> origin/Dev1
        }

        public void TakeDamage(int damage)
        {
            Hp -= damage;
            if (Hp < 0)
                Hp = 0;
        }
      

    }
}
    




