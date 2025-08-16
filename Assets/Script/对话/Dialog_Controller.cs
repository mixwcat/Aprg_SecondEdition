using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog_Controller : MonoBehaviour
{
    [Header("场景物品赋值")]
    public Image dialog_image; // 对话框背景
    public TMP_Text dialog_TMP; // 对话框
    public TextAsset csvFile; // 对话的文件
    public GameObject optionPrefab_One; // 选项预设体
    public GameObject optionPrefab_Two; // 选项预设体


    [Header("文件内容")]
    private int nextDialog_index = 1; // 对话序号
    private string[] rows; // 分割对话
    private bool isChoosingOption = false; // 是否正在选择对话选项


    [Header("Coroutine")]
    private Coroutine currentCoroutine;

    #region Unity Methods
    void Start()
    {
        // 读取对话文本
        ReadText(csvFile);

        // 开始时显示第一行
        ShowDialog();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isChoosingOption) // 按空格键显示对话
        {
            ShowDialog();
        }
    }
    #endregion

    #region 具体实现
    /// <summary>
    /// 读取对话文本
    /// </summary>
    /// <param name="_csvFile"></param>
    public void ReadText(TextAsset _csvFile)
    {
        rows = _csvFile.text.Split('\n'); // 读取对话文件
    }


    /// <summary>
    /// 更新一行对话
    /// </summary>
    public void ShowDialog()
    {
        foreach (string row in rows)
        {
            string[] cells = row.Split(',');
            if (cells[0] == "a" && int.Parse(cells[1]) == nextDialog_index)
            {
                UpdateText(cells[3]); // 更新对话内容
                nextDialog_index = int.Parse(cells[4]); // 更新对话序号
                isChoosingOption = bool.Parse(cells[5]); // 是否正在选择选项

                // 显示选项
                if (isChoosingOption)
                    ShowOptions();

                break;
            }
            else if (cells[0] == "END" && int.Parse(cells[1]) == nextDialog_index)
            {
                gameObject.SetActive(false); // 结束对话，隐藏对话框
                nextDialog_index = int.Parse(cells[4]); // 更新对话序号

                EventCenter.Trigger("OnResume", null);
                Debug.Log("对话结束，隐藏对话框");
                break;
            }
        }
    }


    /// <summary>
    /// 逐字更新文本
    /// </summary>
    /// <param name="_text"></param>
    public void UpdateText(string _text)
    {
        // 如果有正在运行的协程，先停止它
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        // 启动新的协程并保存引用
        currentCoroutine = StartCoroutine(TypeText(_text));
    }

    private IEnumerator TypeText(string _text)
    {
        dialog_TMP.text = ""; // 清空对话框内容
        foreach (char letter in _text.ToCharArray())
        {
            dialog_TMP.text += letter; // 每次添加一个字符
            yield return new WaitForSeconds(0.05f); // 控制每个字符出现的间隔时间
        }
    }


    /// <summary>
    /// 选择对话选项
    /// </summary>
    /// <param name="newIndex"></param>
    public void ChooseOption(int newIndex)
    {
        nextDialog_index = newIndex;
        isChoosingOption = false; // 选择完选项后，设置为false
        ShowDialog();
        HideOptions();
    }

    private void ShowOptions()
    {
        optionPrefab_One.SetActive(true); // 显示选项预设体
        optionPrefab_Two.SetActive(true); // 显示选项预设体
    }

    private void HideOptions()
    {
        optionPrefab_One.SetActive(false); // 隐藏选项预设体
        optionPrefab_Two.SetActive(false); // 隐藏选项预设体
    }

    #endregion
}