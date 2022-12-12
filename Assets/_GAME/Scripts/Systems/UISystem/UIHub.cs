using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
public enum EmojiType {good,bad}
[Serializable]
public class Emoji
{
   public EmojiType emojiType;
   public Sprite emojiSprite;
}
public class UIHub : PersistentSingleton<UIHub>
{
    [SerializeField] private List<BaseUI> uisByPriority;

    private readonly Dictionary<Type, BaseUI> uis = new Dictionary<Type, BaseUI>();

    [SerializeField] List<Emoji> emojiList;
    Queue<Emoji> goodStateEmojis;
    Queue<Emoji> badStateEmojis;

    protected override void Awake()
    {
        base.Awake();
        PopulateDictionary();
        goodStateEmojis = new Queue<Emoji>();
        badStateEmojis = new Queue<Emoji>();
        foreach (var item in emojiList)
        {
            if (item.emojiType == EmojiType.good) { goodStateEmojis.Enqueue(item); }
            else { badStateEmojis.Enqueue(item); }
        }
    }

    private void PopulateDictionary()
    {
        foreach (BaseUI ui in uisByPriority)
        {
            uis.Add(ui.GetType(), ui);
        }
    }

    public static T Get<T>() where T : BaseUI
    {
        return (T)Instance.uis[typeof(T)];
    }
    public void ShowEmoji(EmojiType emojiType,Image imageObject)
    {
        imageObject.sprite = SelectEmoji(emojiType).emojiSprite;
        EmojiAnimation(imageObject.gameObject);
    }
    Emoji SelectEmoji(EmojiType emojiType)
    {
        Emoji tempEmoji;
        if (emojiType==EmojiType.good)
        {
            tempEmoji = goodStateEmojis.Dequeue();
            goodStateEmojis.Enqueue(tempEmoji);
            return tempEmoji;
        }
        else
        {
            tempEmoji = badStateEmojis.Dequeue();
            goodStateEmojis.Enqueue(tempEmoji);
            return tempEmoji;
        }
    }
    void EmojiAnimation(GameObject emojiGameObject)
    {
        emojiGameObject.transform.DOLookAt(Camera.main.transform.position, 1);
        emojiGameObject.SetActive(true);
        emojiGameObject.transform.localPosition = Vector3.zero;
        emojiGameObject.transform.DOLocalMoveY(emojiGameObject.transform.localPosition.y + 5, 1).OnComplete(() => emojiGameObject.gameObject.SetActive(false));
    }
   
}
