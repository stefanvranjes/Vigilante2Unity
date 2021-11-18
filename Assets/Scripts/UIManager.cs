using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using Unity.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public OcclusionCamera occlusionPrefab;
    public LensFlares lensFlarePrefab;
    public Image flash; //gp+CC8h
    public RectTransform hudRect;
    public RectTransform flaresRect;
    public List<Sprite> enemySprites;
    public List<Sprite> weaponSprites;
    public List<Sprite> digitSprites;
    public List<Sprite> slotSprites;
    public List<Sprite> asciiSprites;
    public GameObject unitPrefab;
    public GameObject characterPrefab;
    public Image weaponRect;
    public Image enemyRect;
    public RectTransform radarRect;
    public RectTransform feedbackRect;
    public Image playerHealthSlider;
    public Slider enemyHealthSlider;
    public List<Image> digits;
    public List<Image> slots;
    public List<Image> powerups;
    public RectTransform youWinRect;
    public RectTransform youLoseRect;
    public RectTransform gameOverRect;
    public RectTransform destroyedRect;
    public Text completionTime;
    public Text arcadeWhammies;
    public Text arcadeTotaled;
    public Text survivalTime;
    public Text survivalWhammies;
    public Text survivalTotaled;
    public Text survivalDestroyed;
    public Text difficulty;
    public Text destroyed;
    public List<LensFlares> flares;
    public List<OcclusionCamera> cameras;
    public Image underwater;
    public float units;
    public float radius;
    public float printSpeed;
    public float under;
    public float waterOffset;

    private Image printedChar;
    private List<Image> feedbackElements;
    private int printIndex;
    private bool CR_Running;

    private static Color32 GREEN = new Color32(0x25, 0xff, 0x00, 0xff);
    private static Color32 RED = new Color32(0xff, 0x00, 0x00, 0xff);
    private static Color32 YELLOW = new Color32(0xff, 0xff, 0x00, 0xff);

    private void Awake()
    {
        instance = this;
        int scale = (Screen.width + Screen.height) / 800;
        if (scale < 1) scale = 1;
        hudRect.localScale = new Vector3(scale, scale, 1);

        if (GameManager.instance.gameMode == _GAME_MODE.Survival)
            destroyedRect.gameObject.SetActive(true);
    }

    public void Underwater(float cameraY, float waterY)
    {
        float radius = (waterY + waterOffset - cameraY) / under;
        float height = Mathf.Clamp(radius, 0, 1);
        underwater.rectTransform.anchorMax = new Vector2(1, height);
    }

    public void RefreshDestroyed(int value)
    {
        destroyed.text = value.ToString();
    }

    public RawImage InstantiateUnit()
    {
        RawImage unit = Instantiate(unitPrefab, radarRect).GetComponent<RawImage>();
        unit.color = RED;
        return unit;
    }

    public void InstantiateCharacter()
    {
        printedChar = Instantiate(characterPrefab, feedbackRect).GetComponent<Image>();
        feedbackElements.Add(printedChar);
    }

    public void ReplaceCharacter(char c)
    {
        printedChar.sprite = asciiSprites[c];
        printedChar.SetNativeSize();
    }

    public IEnumerator Printf(string text, bool overwrite = true)
    {
        CR_Running = true;

        if (feedbackElements != null)
        {
            for (int i = 0; i < feedbackElements.Count; i++)
                DestroyImmediate(feedbackElements[i].gameObject, false);

            feedbackElements.Clear();
            feedbackElements = null;
        }

        if (feedbackElements == null)
        {
            feedbackElements = new List<Image>();

            for (int i = 0; i < text.Length; i++)
            {
                InstantiateCharacter();
                yield return new WaitForSeconds(printSpeed);
                ReplaceCharacter(text[i]);
            }

            yield return new WaitForSeconds(5f);

            for (int i = 0; i < 128; i++)
            {
                for (int j = 0; j < feedbackElements.Count; j++)
                {
                    Color32 newColor = feedbackElements[j].color;
                    newColor.r--;
                    newColor.g--;
                    newColor.b--;
                    feedbackElements[j].color = newColor;
                }

                yield return null;
            }

            for (int i = 0; i < feedbackElements.Count; i++)
                DestroyImmediate(feedbackElements[i].gameObject, false);

            feedbackElements.Clear();
            feedbackElements = null;
        }

        CR_Running = false;
    }

    private IEnumerator _WinScreen()
    {
        yield return new WaitForSeconds(5f);
        Vehicle player = GameManager.instance.playerObjects[0];
        TimeSpan t = TimeSpan.FromSeconds(Time.timeSinceLevelLoadAsDouble);
        string answer = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}",
                        t.Hours,
                        t.Minutes,
                        t.Seconds,
                        t.Milliseconds);
        arcadeWhammies.text = player.DAT_BE.ToString();
        arcadeTotaled.text = player.DAT_BF + " of " + GameManager.instance.totalSpawns;
        completionTime.text = answer;
        youWinRect.gameObject.SetActive(true);
    }

    private IEnumerator _LoseScreen()
    {
        yield return new WaitForSeconds(5f);
        youLoseRect.gameObject.SetActive(true);
    }

    private IEnumerator _GameOver()
    {
        yield return new WaitForSeconds(5f);
        Vehicle player = GameManager.instance.playerObjects[0];
        TimeSpan t = TimeSpan.FromSeconds(Time.timeSinceLevelLoadAsDouble);
        string answer = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}",
                        t.Hours,
                        t.Minutes,
                        t.Seconds,
                        t.Milliseconds);
        survivalWhammies.text = player.DAT_BE.ToString();
        survivalTotaled.text = player.DAT_BF.ToString();
        survivalTime.text = answer;
        survivalDestroyed.text = GameManager.instance.DAT_CC4.ToString();

        switch (GameManager.instance.DAT_C6E)
        {
            case 0:
                difficulty.text = "Easy";
                break;
            case 1:
                difficulty.text = "Medium";
                break;
            default:
                difficulty.text = "Hard";
                break;
        }

        gameOverRect.gameObject.SetActive(true);
    }

    public void WinScreen()
    {
        hudRect.gameObject.SetActive(false);
        StartCoroutine("_WinScreen");
    }

    public void LoseScreen()
    {
        hudRect.gameObject.SetActive(false);
        StartCoroutine("_LoseScreen");
    }

    public void GameOver()
    {
        hudRect.gameObject.SetActive(false);
        StartCoroutine("_GameOver");
    }

    public void CalculateUnitPosition(RawImage unit, Vehicle obj)
    {
        Vehicle player = GameManager.instance.playerObjects[0];
        float alpha = Vector3.Angle(player.transform.forward, Vector3.forward);
        Vector3 cross = Vector3.Cross(player.transform.forward, Vector3.forward);
        if (cross.y < 0) alpha = -alpha;
        Vector3 playerPosition = (Vector3)player.vTransform.position / GameManager.instance.translateFactor;
        Vector3 targetPosition = (Vector3)obj.screen / GameManager.instance.translateFactor;
        Vector3 direction = targetPosition - playerPosition;
        direction = Quaternion.Euler(0, alpha, 0) * direction;
        Vector3 offset = direction / units;
        unit.color = player.target == obj ? obj.jammer == 0 && (obj.flags & 0x8000000) == 0 ? GREEN : YELLOW : RED;
        Vector3 position = Vector3.ClampMagnitude(offset, radius);
        unit.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
    }

    public void UpdateHUD(Vehicle player, int tick)
    {
        float playerHealth = GetHealth(player);
        playerHealthSlider.fillAmount = playerHealth;

        if (playerHealth < 0.3f)
            playerHealthSlider.enabled = (tick & 0x10) == 0 ? true : false;
        else
            playerHealthSlider.enabled = true;

        VigObject target = player.target;

        if (target != null && target.type == 2)
        {
            Vehicle targetVehicle = (Vehicle)target;
            enemyRect.enabled = true;
            enemyRect.sprite = enemySprites[(int)targetVehicle.vehicle];
            enemyRect.SetNativeSize();
            enemyHealthSlider.gameObject.SetActive(true);
            enemyHealthSlider.value = GetHealth(targetVehicle);
        }
        else
        {
            enemyRect.enabled = false;
            enemyHealthSlider.gameObject.SetActive(false);
        }

        for (int i = 0; i < 3; i++)
        {
            if (player.weapons[i] != null)
            {
                if (!slots[i].gameObject.activeSelf)
                    slots[i].gameObject.SetActive(true);
            }
            else
            {
                if (slots[i].gameObject.activeSelf)
                    slots[i].gameObject.SetActive(false);
            }
        }

        VigObject weapon = player.weapons[player.weaponSlot];

        if (weapon != null && weapon.maxHalfHealth != 0)
        {
            weaponRect.enabled = true;
            weaponRect.sprite = weaponSprites[weapon.tags - 1];
            weaponRect.SetNativeSize();
            digits[0].enabled = true;
            digits[1].enabled = true;
            digits[0].sprite = digitSprites[weapon.maxHalfHealth / 10];
            digits[1].sprite = digitSprites[weapon.maxHalfHealth % 10];
            slots[0].sprite = slotSprites[0];
            slots[1].sprite = slotSprites[0];
            slots[2].sprite = slotSprites[0];
            slots[player.weaponSlot].sprite = slotSprites[1];
        }
        else
        {
            weaponRect.enabled = false;
            digits[0].enabled = false;
            digits[1].enabled = false;
        }

        if (player.transformation != 0)
        {
            int index;

            switch (player.wheelsType)
            {
                case _WHEELS.Air:
                    index = 0;
                    break;
                case _WHEELS.Sea:
                    index = 1;
                    break;
                case _WHEELS.Snow:
                    index = 2;
                    break;
                default:
                    index = 0;
                    break;
            }

            if (!powerups[index].gameObject.activeSelf)
            {
                powerups[0].gameObject.SetActive(false);
                powerups[1].gameObject.SetActive(false);
                powerups[2].gameObject.SetActive(false);
                powerups[index].gameObject.SetActive(true);
            }

            if (player.transformation < 300)
                powerups[index].enabled = (tick & 0x10) == 0 ? true : false;
            else
                powerups[index].enabled = true;
        }
        else
        {
            if (powerups[0].gameObject.activeSelf ||
                powerups[1].gameObject.activeSelf ||
                powerups[2].gameObject.activeSelf)
            {
                powerups[0].gameObject.SetActive(false);
                powerups[1].gameObject.SetActive(false);
                powerups[2].gameObject.SetActive(false);
            }
        }

        if (player.doubleDamage != 0)
        {
            if (!powerups[3].gameObject.activeSelf)
                powerups[3].gameObject.SetActive(true);

            if (player.doubleDamage < 300)
                powerups[3].enabled = (tick & 0x10) == 0 ? true : false;
            else
                powerups[3].enabled = true;
        }
        else
        {
            if (powerups[3].gameObject.activeSelf)
                powerups[3].gameObject.SetActive(false);
        }

        if (player.shield != 0)
        {
            if (!powerups[4].gameObject.activeSelf)
                powerups[4].gameObject.SetActive(true);

            if (player.shield < 300)
                powerups[4].enabled = (tick & 0x10) == 0 ? true : false;
            else
                powerups[4].enabled = true;
        }
        else
        {
            if (powerups[4].gameObject.activeSelf)
                powerups[4].gameObject.SetActive(false);
        }

        if (player.jammer != 0)
        {
            if (!powerups[5].gameObject.activeSelf)
                powerups[5].gameObject.SetActive(true);

            if (player.jammer < 300)
                powerups[5].enabled = (tick & 0x10) == 0 ? true : false;
            else
                powerups[5].enabled = true;
        }
        else
        {
            if (powerups[5].gameObject.activeSelf)
                powerups[5].gameObject.SetActive(false);
        }
    }

    public float GetHealth(Vehicle obj)
    {
        int health;

        if (obj.body[0] == null)
            health = obj.maxHalfHealth;
        else
            health = obj.body[0].maxHalfHealth +
                    obj.body[1].maxHalfHealth;

        return (float)health / obj.maxFullHealth;
    }

    public void RefreshFlares(int counter)
    {
        for (int i = 0; i < flares.Count; i++)
        {
            if (flares[i].update != counter)
                if (flares[i].gameObject.activeSelf)
                    flares[i].gameObject.SetActive(false);
        }
    }

    public void RefreshCameras()
    {
        while (cameras.Count > 0 && cameras[0] == null)
            cameras.RemoveAt(0);

        if (cameras.Count > 0)
        {
            cameras[0].RenderW();
            cameras.RemoveAt(0);
        }
    }

    public void FUN_1D00C(LensFlares param1)
    {
        uint uVar9;
        uint uVar10;
        uint uVar11;
        int iVar14;

        float fVar19 = Mathf.Abs(param1.rectTransform.anchoredPosition.x / 1.5f);
        float fVar18 = Mathf.Abs(param1.rectTransform.anchoredPosition.y / 1.5f);
        int iVar8 = (int)Mathf.Sqrt(fVar19 * fVar19 + fVar18 * fVar18);

        if (iVar8 < 0x40)
        {
            Color32 color = param1.color;
            Color32 newColor = new Color32();

            if (!(color.r == 0 && color.g == 0 && color.b == 0))
            {
                //if (param1.request.hasError) return;

                if (!param1.request.hasError && param1.request.done)
                {
                    NativeArray<Color32> buffer = param1.request.GetData<Color32>();
                    param1.renderTexture.LoadRawTextureData(buffer);
                    param1.renderTexture.Apply();
                }

                //param1.renderTexture.ReadPixels(new Rect(32, 32, 1, 1), 0, 0, false);
                Color32 renderColor = param1.renderTexture.GetPixel(32, 32);

                if (renderColor.r != 0)
                {
                    iVar14 = 0x40 - iVar8;
                    uVar9 = (uint)(((Color32)flash.color).b + ((uint)(iVar14 * color.b) >> 6));
                    uVar11 = 0xff;

                    if (uVar9 < 0xff)
                        uVar11 = uVar9;

                    uVar10 = (uint)(((Color32)flash.color).g + ((uint)(iVar14 * color.g) >> 6));
                    uVar9 = 0xff;

                    if (uVar10 < 0xff)
                        uVar9 = uVar10;

                    newColor.b = (byte)uVar11;
                    newColor.g = (byte)uVar9;
                    uVar11 = (uint)(((Color32)flash.color).r + ((uint)(iVar14 * color.r) >> 6));
                    newColor.r = 0xff;

                    if (uVar11 < 0xff)
                        newColor.r = (byte)uVar11;

                    newColor.a = 255;
                    flash.color = newColor;
                }
            }
        }
    }

    public Flash FUN_4E414(Vector3Int param1, Color32 param2)
    {
        bool bVar1;
        Flash fVar2;

        if (GameManager.instance.screenMode == _SCREEN_MODE.Whole)
        {
            bVar1 = GameManager.instance.FUN_2E22C(param1, 0);
            fVar2 = null;

            if (bVar1)
                fVar2 = FUN_4E338(param2);
        }
        else
            fVar2 = null;

        return fVar2;
    }

    public Flash FUN_4E338(Color32 param1)
    {
        Flash ppcVar1;

        if (GameManager.instance.screenMode == _SCREEN_MODE.Whole)
        {
            GameObject obj = new GameObject();
            ppcVar1 = obj.AddComponent<Flash>();
            ppcVar1.flags = 0xa0;
            ppcVar1.DAT_3C = 0x80;
            ppcVar1.DAT_34 = param1;
            ppcVar1.FUN_305FC();
        }
        else
            ppcVar1 = null;

        return ppcVar1;
    }

    public Flash FUN_4E3A8(Color32 param1)
    {
        Flash ppcVar1;

        if (GameManager.instance.screenMode == _SCREEN_MODE.Whole)
        {
            GameObject obj = new GameObject();
            ppcVar1 = obj.AddComponent<Flash>();
            ppcVar1.flags = 0xa0;
            ppcVar1.DAT_34 = param1;
            ppcVar1.DAT_3C = 0;
            ppcVar1.FUN_305FC();
        }
        else
            ppcVar1 = null;

        return ppcVar1;
    }
}
