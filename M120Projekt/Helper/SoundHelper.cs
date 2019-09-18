using M120Projekt.Enum;
using System.Media;

namespace M120Projekt.Helper
{
    public static class SoundHelper
    {
        public static bool IsSoundOn = true;
        public static SoundPlayer SoundPlayer = new SoundPlayer();

        public static void PlayVictorySound()
        {
            SoundPlayer.Stream = Properties.Resources.Win;
            if (IsSoundOn)
            {
                SoundPlayer.Play();
            }
        }

        public static void PlayGameOverSound()
        {
            SoundPlayer.Stream = Properties.Resources.Gameover;
            if (IsSoundOn)
            {
                SoundPlayer.Play();
            }
        }

        public static void Play(GameState gameState)
        {
            if (gameState == GameState.Running && IsSoundOn)
            {
                PlayMusic();
            }
            else
            {
                SoundPlayer.Stop();
            }
        }

        public static void PlayMusic()
        {
            SoundPlayer.Stream = Properties.Resources.Maingame;
            if (IsSoundOn)
            {
                SoundPlayer.PlayLooping();
            }
        }
    }
}
