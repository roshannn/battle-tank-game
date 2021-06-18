using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class AchievementManager : MonoBehaviour
{
    [SerializeField]
    private Text bulletsFiredByPlayer;
    [SerializeField]
    private Text enemiesDead;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Animator achievementAnimator;
    [SerializeField]
    private Text achievementText;
    private int enemiesKilled;
    private int bulletsFired;

    private int score;
    private bool canDisplay;

    private void Start()
    {
        enemiesKilled = 0;
        bulletsFired = 0;
        score = 0;
        EventService.Instance.onFire += UpdateBulletsFired;
        EventService.Instance.onEnemyDeath += UpdateDeadEnemies;
        canDisplay = true;
    }

    private void UpdateDeadEnemies()
    {
        enemiesKilled++;
        score += 100;
        scoreText.text = "Score: " + score;
        enemiesDead.text = "Enemies Killed: " + enemiesKilled;
        CheckForKillAchievements(enemiesKilled);
    }

    private void UpdateBulletsFired()
    {
        bulletsFired++;
        score += 1;
        scoreText.text = "Score: " + score;
        bulletsFiredByPlayer.text = "Bullets Fired: " + bulletsFired;
        CheckForShootingAchievements(bulletsFired);
    }
        
    private void CheckForKillAchievements(int enemiesKilled)
    {
        if (canDisplay)
        {
            switch (enemiesKilled)
            {
                case 1:
                    achievementText.text = "First Kill";
                    PlayAnimation();
                    canDisplay = false;
                    break;
                case 10:
                    achievementText.text = "Assassin(Killed 10 enemies)";
                    PlayAnimation();
                    canDisplay = false;
                    break;
                case 25:
                    achievementText.text = "Serial Killer(Killed 25 enemies)";
                    PlayAnimation();
                    canDisplay = false;
                    break;
                case 50:
                    achievementText.text = "Trailblazer(Killed 50 enemies)";
                    PlayAnimation();
                    canDisplay = false;
                    break;
            }
            canDisplay = true;
            
        }
    }

    private void CheckForShootingAchievements(int bulletsFired)
    {
        switch (bulletsFired)
        {
            case 1:
                achievementText.text = "First Bullet Shot";
                PlayAnimation();
                canDisplay = false;
                break;
            case 50:
                achievementText.text = "Noob Shooter(Shot 50 Bullets)";
                PlayAnimation();
                canDisplay = false;
                break;
            case 100:
                achievementText.text = "Amateur Shooter(Shot 100 Bullets)";
                PlayAnimation();
                canDisplay = false;
                break;
            case 200:
                achievementText.text = "Marksman(Shot 200 Bullets)";
                PlayAnimation();
                canDisplay = false;
                break;
        }
        canDisplay = true;
    }

    private void PlayAnimation()
    {
        achievementAnimator.SetBool("PushDown", true);
        PushUp();
    }

    async private void PushUp()
    {
        await new WaitForSeconds(2f);
        achievementAnimator.SetBool("PushDown", false);
    }

}
