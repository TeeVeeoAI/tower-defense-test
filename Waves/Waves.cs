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

        public int[] AmoutOfEnemiesInWave { get => amoutOfEnemiesInWave; }
        public double[] LastBloonDelay{ get => lastBloonDelay; set => LastBloonDelay = value; }
        public int[] SpawnedEnemiesInWave{ get => spawnedEnemiesInWave; set => spawnedEnemiesInWave = value; }
        public int[] EnemiesKilledInWave{ get => enemiesKilledInWave; set => enemiesKilledInWave = value; }

        public Waves(int[] amoutOfEnemiesInWave, double[] lastBloonDelay, int[] spawnedEnemiesInWave, int[] enemiesKilledInWave)
        {
            this.amoutOfEnemiesInWave = amoutOfEnemiesInWave;
            this.lastBloonDelay = lastBloonDelay;
            this.spawnedEnemiesInWave = spawnedEnemiesInWave;
            this.enemiesKilledInWave = enemiesKilledInWave;
        }
        public Waves(Waves wave, int currWaveNum)
        {
            for (int i = 0; i < wave.amoutOfEnemiesInWave.Length; i++)
            {
                wave.amoutOfEnemiesInWave[i] = (int)(wave.amoutOfEnemiesInWave[i] * (currWaveNum * 0.7));
            }
            for (int i = 0; i < wave.lastBloonDelay.Length; i++)
            {
                wave.lastBloonDelay[i] = 2 * i+1;
            }
            for (int i = 0; i < wave.spawnedEnemiesInWave.Length; i++)
            {
                wave.spawnedEnemiesInWave = [0, 0, 0, 0];
            }
            for (int i = 0; i < wave.enemiesKilledInWave.Length; i++)
            {
                wave.enemiesKilledInWave = [0, 0, 0, 0];
            }

            this = wave;
        }
    }
}