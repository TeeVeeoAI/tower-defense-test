using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tower_defense__Priv.Waves
{
    public struct Waves
    {
        private int[] amoutOfEnemiesInWave;
        private double[] lastBloonDelay;
        private int[] spawnedEnemiesInWave;
        private int[] enemiesKilledInWave;

        public int[] AmoutOfEnemiesInWave { get; }
        public double[] LastBloonDelay{ get; set; }
        public int[] SpawnedEnemiesInWave{ get; set; }
        public int[] EnemiesKilledInWave{ get; set; }

        public Waves(int[] amoutOfEnemiesInWave, double[] lastBloonDelay, int[] spawnedEnemiesInWave, int[] enemiesKilledInWave)
        {
            this.amoutOfEnemiesInWave = amoutOfEnemiesInWave;
            this.lastBloonDelay = lastBloonDelay;
            this.spawnedEnemiesInWave = spawnedEnemiesInWave;
            this.enemiesKilledInWave = enemiesKilledInWave;
        }
        public Waves(Waves wave)
        {
            this = wave;
        }
    }
}