using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DialogManager2 : MonoBehaviour
{
    #region 变量
    private TextAsset dialogDataFile;
    /// <summary>
    /// 在这个数组中配置好所有会用到的csv文件
    /// </summary>
    public TextAsset[] dialogDataFiles;

    /// <summary>
    /// 左侧角色图像组件
    /// </summary>
    public SpriteRenderer spriteLeft;
    /// <summary>
    /// 右侧角色图像组件
    /// </summary>
    public SpriteRenderer spriteRight;

    /// <summary>
    /// 角色名字文本组件
    /// </summary>
    public TMP_Text nameText;

    /// <summary>
    /// 对话内容文本组件
    /// </summary>
    public TMP_Text dialogText;

    /// <summary>
    /// 角色图片列表
    /// </summary>
    public List<Sprite> sprites = new List<Sprite>();

    /// <summary>
    /// 角色名字作为索引，可以得到对应名字的图片
    /// </summary>
    private Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();

    /// <summary>
    /// 当前即将开始的对话的索引值
    /// </summary>
    private int dialogIndex = 0;

    /// <summary>
    /// 按行分割好的对话文本
    /// </summary>
    private string[] dialogRows;
    
    /// <summary>
    /// 对话继续按钮
    /// </summary>
    public Button nextButton;

    /// <summary>
    /// 选项按钮的预制体
    /// </summary>
    public GameObject optionButtonPrefab;

    /// <summary>
    /// 选项按钮父物体，用于自动排列
    /// </summary>
    public GameObject buttonGroup;
    
    
    [Header("Canvas物体")]
    public GameObject DialogCanvas;
    public GameObject KeyCanvas;
    public GameObject ButtonCanvas;

    [Header("打字效果相关")]
    private bool isTyping = false; // 当前是否正在打字
    public float typingSpeed = 0.05f; // 每个字符的显示间隔
    private string currentSentence; // 储存正在显示的完整例子
    private Coroutine typingCoroutine; // 打字效果的携程引用

    [Header("立绘效果相关")]
    public float fadeEffect = 0.5f; // 设置立绘出现但不是正在说话时的半透明的程度

    [Header("背景图片切换器")]
    public SpriteSwitcher SpriteSwitcher;

    [Header("事件")] 
    public UnityEngine.Events.UnityEvent OnDialogEnd; //可以监听更换场景之类的函数方法
    
    #endregion
    
    private void Awake()
    {
        imageDic["角色A"] = sprites[0];
        imageDic["角色B"] = sprites[1];
        imageDic["你"] = sprites[2];
        imageDic["？？"] = sprites[3];
        imageDic["大喵"] = sprites[4];
    }

    // Start is called before the first frame update
    void Start()
    {
        OpenDialog(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 更新文本
    /// </summary>
    /// <param name="_name">角色名字文本</param>
    /// <param name="_text">对话内容文本</param>
    public void UpdateText(string _name, string _text)
    {
        nameText.text = _name;
        dialogText.text = _text;
    }

    /// <summary>
    /// 打字携程，可以实现打字显示文字的效果
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_text"></param>
    /// <returns></returns>
    private IEnumerator TypeText(string _name, string _text)
    {
        isTyping = true;
        currentSentence = _text;
        nameText.text = _name;
        dialogText.text = ""; // 清空当前文本

        foreach (char letter in _text.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    /// <summary>
    /// 更新图片
    /// </summary>
    /// <param name="_name">图片角色的名字（在字典中已经做了索引）</param>
    /// <param name="_atLeft">图片是否处于左侧</param>
    public void UpdateImage(string _name, string _position)
    {
        if (_position == "左")
        {
            spriteLeft.sprite = imageDic[_name];
            spriteLeft.color = Color.white;
            //TODO可能要增加调整图片大小的逻辑

            if (spriteRight.sprite != null)
            {
                spriteRight.color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
        }
        else if (_position == "右")
        {
            spriteRight.sprite = imageDic[_name];
            spriteRight.color = Color.white;

            if (spriteLeft.sprite != null)
            {
                spriteLeft.color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
        }
    }

    // 目前没用到
    public void UpdateBGImage(int _index)
    {
        SpriteSwitcher.NextSprite(_index);
    }
    

    public void ReadText(TextAsset _textAsset)
    {
        dialogRows = _textAsset.text.Split('\n');
        // foreach (var row in rows)
        // {
        //     string[] cells = row.Split(',');
        // }
        Debug.Log("读取成功");
    }

    public void ShowDialogRow()
    {
        int index = 0;
        foreach (var row in dialogRows)
        {
            string[] cells = row.Split(',');
            
            // 正常对话
            if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
            {
                OpenKeyCanvas();
                if (cells[2] == "")
                    CloseNormalDialog(); //环境说话
                else
                    OpenNormalDialog(); // 人物正常对话
                
                // UpdateText(cells[2], cells[4]);
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                }
                typingCoroutine = StartCoroutine(TypeText(cells[2], cells[4]));
                
                if(cells[2] != "")
                    UpdateImage(cells[2], cells[3]); // 只有人物对话才要执行这个函数

                dialogIndex = int.Parse(cells[5]);
                break; //如果成功读取了，就可以停止查找了
            }
            
            // 选项
            else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex)
            {
                OpenKeyCanvas();
                OpenNormalDialog();
                nextButton.gameObject.SetActive(false);
                GenerateOption(index); // 这里需要递归加载按钮
            }
            
            else if (cells[0] == "^" && int.Parse(cells[1]) == dialogIndex)
            {
                CloseButtonCanvas();
                CloseKeyCanvas();
                CloseNormalDialog();
                
                if (cells[2] == "")
                    SpriteSwitcher.NextSprite(-1); // 如果是-1就是默认选择当前图片数组的下一张图片
                else
                    SpriteSwitcher.NextSprite(int.Parse(cells[2])); // 可以选择指定第i张图片
                dialogIndex = int.Parse(cells[5]);
                break;
            }
            
            // 结束
            else if (cells[0] == "END" && int.Parse(cells[1]) == dialogIndex)
            {
                CloseButtonCanvas();
                CloseKeyCanvas();
                CloseNormalDialog();

                Debug.Log("剧情结束");
                OnDialogEnd?.Invoke();
                break; //防止反复执行本代码段，从而反复出发结束事件
            }

            index++;
        }
    }

    public void OnClickNext()
    {
        if (isTyping)
        {
            // 如果正在打字，立即停止打字携程，并直接显示完整的文字
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            dialogText.text = currentSentence;
            isTyping = false;
        }
        else
        {
            // 正常显示下一行
            ShowDialogRow();
        }
    }

    public void GenerateOption(int _index)
    {
        // 防御性代码
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogText.text = currentSentence;
            isTyping = false;
        }
        
        string[] cells = dialogRows[_index].Split(',');
        if (cells[0] == "&")
        {
            GameObject button = Instantiate(optionButtonPrefab, buttonGroup.transform);
        
            //绑定按钮事件
            button.GetComponentInChildren<TMP_Text>().text = cells[4];
            button.GetComponent<Button>().onClick.AddListener(delegate { OnOptionClick(int.Parse(cells[5])); });
            // 这里给按钮绑定委托，在Unity编辑器里是不会显示的（）
        
            GenerateOption(_index + 1);
        }
        
    }

    public void OnOptionClick(int _id)
    {
        dialogIndex = _id;
        ShowDialogRow();
        for (int i = 0; i < buttonGroup.transform.childCount; i++)
        {
            Destroy(buttonGroup.transform.GetChild(i).gameObject);
        }
        nextButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// 开启第i段对话
    /// </summary>
    /// <param name="_index"></param>
    public void OpenDialog(int _index)
    {
        dialogDataFile = dialogDataFiles[_index];
        ReadText(dialogDataFile);
        
        // 防止开始人物对话是残留上次对话用到的人物立绘（可以在DialogCanvas未激活的情况下调整其子物体的组件吗？）
        spriteLeft.sprite = null;
        spriteRight.sprite = null;
        
        ShowDialogRow();
    }

    #region 显示or隐藏Canvas
    /// <summary>
    /// 打开对话框、人物名字和人物立绘
    /// </summary>
    public void OpenNormalDialog()
    {
        DialogCanvas.SetActive(true);
        spriteLeft.gameObject.SetActive(true);
        spriteRight.gameObject.SetActive(true);
    }

    /// <summary>
    /// 关闭对话框、人物名字和人物立绘
    /// </summary>
    public void CloseNormalDialog()
    {
        DialogCanvas.SetActive(false);
        spriteLeft.gameObject.SetActive(false);
        spriteRight.gameObject.SetActive(false);
    }

    public void OpenKeyCanvas()
    {
        KeyCanvas.SetActive(true);
    }

    public void CloseKeyCanvas()
    {
        KeyCanvas.SetActive(false);
    }

    public void OpenButtonCanvas()
    {
        ButtonCanvas.SetActive(true);
    }

    public void CloseButtonCanvas()
    {
        ButtonCanvas.SetActive(false);
    }
    #endregion
    
}
