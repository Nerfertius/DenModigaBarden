using UnityEngine;
using UnityEngine.UI;

class HP
{
    private static HP instance = null;

    private GameManager gm;
    private Image[] hp = null;

    public static HP GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new HP();
            }
            return instance;
        }
    }

    public HP()
    {
        gm = GameManager.instance;
        GetHP();
    }

    public void Update()
    {
        if (GameManager.PlayCanvas != null && hp != null)
        {
            float health = GameManager.instance.player.health;

            if (health > 0)
            {
                hp[0].sprite = health < 1 ? gm.halfHeart : gm.fullHeart;
            }
            else
            {
                hp[0].sprite = gm.emptyHeart;
            }
            if (health > 1)
            {
                hp[1].sprite = health < 2 ? gm.halfHeart : gm.fullHeart;
            }
            else
            {
                hp[1].sprite = gm.emptyHeart;
            }
            if (health > 2)
            {
                hp[2].sprite = health < 3 ? gm.halfHeart : gm.fullHeart;
            }
            else
            {
                hp[2].sprite = gm.emptyHeart;
            }
        }
        else {
            if (hp == null) {
                GetHP();
            }
        }
    }

    public void GetHP()
    {
        if(GameManager.PlayCanvas)
            hp = GameManager.PlayCanvas.transform.Find("HP").GetComponentsInChildren<Image>();
    }
}