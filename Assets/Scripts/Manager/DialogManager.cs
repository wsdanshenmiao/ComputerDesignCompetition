/****************************************************************************************
	Author:			Crusher
	Versions:		1.0
	Creation time:	2025.1.14
	Finish time:	
	Abstract:       用于管理对话系统的Manager
****************************************************************************************/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : Singleton<DialogManager>
{
    [Header("Canvas对象")]
    public GameObject DialogCanvas;

    [Header("两侧图像")]
    public Image LeftImage;
    public Image RightImage;

    [Header("图片的属性,Color只需要更改透明度")]
    public Color fadecolor; // 主要设置透明度
    public float Offset; // 设置y坐标的偏移量

    [Header("文本")]
    public TMP_Text nameText;
    public TMP_Text diaLogText;

    [Header("角色图片列表")]
    public List<Sprite> characterSprite = new List<Sprite>();

    [Header("继续按钮")]
    public Button continueButton;

    [Header("选择按键")]
    public GameObject optionButton; // 预制件
    public Transform ButtonSet; // 父对象

    // 角色的名字会对应调用图片
    private Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();

    // 对应获取的脚本文件
    private TextAsset dialogDataFile; 

    private int dialogIndex; // 保存当前对话的索引值
    private string[] dialogRows; // 按行分割对话文本

    protected override void Awake()
    {
        base.Awake();

        // 主角名称
        imageDic["主角"] = characterSprite[0];
        // 导师名称
        imageDic["导师"] = characterSprite[1];

        // 为参数赋值
        dialogIndex = 0;

        
    }

    public void GetText(TextAsset DialogText)
    {
        dialogDataFile = DialogText;
        ReadDate();
    }

    private void ReadDate()
    {
        // 读取文件中的数据
        dialogRows = dialogDataFile.text.Split('\n'); // 先把文件分割
        ShowDialogRow(); // 再读取数据
    }

    public void OpenDialog()
    {
        Debug.Log("成功调用");
        DialogCanvas.gameObject.SetActive(true);
    }

    // 显示对话行
    public void ShowDialogRow()
    {
        for (int index = 0; index < dialogRows.Length; index++)
        {
            string[] cells = dialogRows[index].Split(',');
            if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex) // 对话框
            {
                UpdateText(cells[2], cells[4]); // 写姓名并输出文本
                UpdateImage(cells[2], cells[3]); // 显示图片

                dialogIndex = int.Parse(cells[5]); // 更新索引值
                break; // 结束循环
            }
            else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex) // 可选选项
            {
                continueButton.gameObject.SetActive(false);
                GenerateOption(index); // 当前的索引值,而不是对话索引
            }
            else if (cells[0] == "END" && int.Parse(cells[1]) == dialogIndex) // 终止
            {
                dialogIndex = 0;
                CloseDialogCanvas();

                // 结束对话,开启时间
                Time.timeScale = 1;
            }
        }
    }

    public void OnClickContinue()
    {
        ShowDialogRow(); // 显示新内容
        Debug.Log("点击");
    }

    // 生成选项
    public void GenerateOption(int index)
    {
        // 显示文本信息
        string[] cells = dialogRows[index].Split(',');
        if (cells[0] == "&") // 遇见对话框索引,不再更新
        {
            GameObject button = Instantiate(optionButton, ButtonSet);
            button.GetComponentInChildren<TMP_Text>().text = cells[4];

            // 绑定按钮事件
            button.GetComponent<Button>().onClick.AddListener
            (
                delegate
                {
                    OnClickOption(int.Parse(cells[5]));
                }
            );
            GenerateOption(index + 1);
        }
    }

    public void OnClickOption(int id)
    {
        dialogIndex = id; // 更新索引值
        ShowDialogRow(); // 显示对应索引值的内容

        // 销毁ButtonSet的所有子对象
        for (int i = 0; i < ButtonSet.childCount; i++)
        {
            Destroy(ButtonSet.GetChild(i).gameObject);
        }
        // 开启继续按钮
        continueButton.gameObject.SetActive(true);
    }

    public void CloseDialogCanvas()
    {
        DialogCanvas.gameObject.SetActive(false);
    }

    #region 图片与文本的显示逻辑
    // 更新文本内容
    public void UpdateText(string _name, string _text)
    {
        nameText.text = _name;
        diaLogText.text = _text;
    }

    /// <summary>
    /// 显示对应图片
    /// </summary>
    /// <param name="_name">键--名字</param>
    /// <param name="_pos">生成位置</param>
    public void UpdateImage(string _name, string _pos)
    {
        if (_pos == "左")
        {
            // 给予左边的图片
            LeftImage.sprite = imageDic[_name];
            ChangeColorPos(LeftImage, RightImage);
        }
        else
        {
            RightImage.sprite = imageDic[_name];
            ChangeColorPos(RightImage, LeftImage);
        }
    }

    /// <summary>
    /// 改变照片的颜色和位置
    /// </summary>
    /// <param name="thisImg">说话对象的照片</param>
    /// <param name="otherImage">不说话对象的照片</param>
    public void ChangeColorPos(Image thisImg, Image otherImage)
    {
        Color color = thisImg.color;
        // 修改说话照片的透明度
        color.a = 1;
        thisImg.color = color;
        // 修改不说话照片的透明度
        if(otherImage.color.a != 0) // 图片从未出现就不需要设置
        {
            color.a = fadecolor.a;
            otherImage.color = color;
        }
        Debug.Log("获取位置");
        float yPos = (thisImg.transform.position.y + otherImage.transform.position.y) / 2;
        Vector3 pos1 = thisImg.transform.position;
        Vector3 pos2 = otherImage.transform.position;
        // 修改说话照片的位置
        pos1.y = yPos + Offset;
        thisImg.transform.position = pos1;
        // 修改不说话照片的位置
        pos2.y = yPos - Offset;
        otherImage.transform.position = pos2;
    }
    #endregion
}
