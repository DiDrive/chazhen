using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private Transform startPoint;
    private Transform spawnPoint;
    private pin currentPin;
    private bool isGameOver = false;
    private int score = 0;
    private Camera mainCamera;

    public Text scoreText;
    public GameObject pinPrefab;
    public float ColorAnimationSpeed = 3;//背景颜色渐变的速度
	
	void Start () {
        startPoint= GameObject.Find("StartPoint").transform;//找到名字为StartPoint的物体并得到他的组件
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        mainCamera = Camera.main;
        Spawnpin();
	}
	

    void Spawnpin() {
      currentPin=GameObject.Instantiate(pinPrefab,spawnPoint.position,pinPrefab.transform.rotation).GetComponent<pin>();

    }//实例化小针
    public void GameOver() {
        if (isGameOver) return;           //break 满足条件则跳出当前所在层循环  continue 满足条件则跳过然后继续剩余的循环   return 满足条件则跳出所有的循环与逻辑，不再执行后面任何逻辑 
        GameObject.Find("Circle").GetComponent<Rotateself>().enabled = false;
        StartCoroutine(GameOverAnimation());
        isGameOver = true;
    }//游戏结束
    IEnumerator GameOverAnimation()//多线程调用，详情：https://blog.csdn.net/beihuanlihe130/article/details/76098844
    {
        while (true) {
            mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, Color.red, ColorAnimationSpeed*Time.deltaTime);//死亡后背景颜色渐变到红色
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize,4,ColorAnimationSpeed*Time.deltaTime);//死亡后游戏屏幕放大
            if (Mathf.Abs(mainCamera.orthographicSize - 4) < 0.01f) {
                break;
            }//判断是否放大到相应距离，如果是则停止放大跳出循环
            yield return 0;
        }
        yield return new WaitForSeconds(2);//暂停两秒再执行下面的语句
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//重新加载游戏场景即重新开始游戏
    }


    private void Update() {
        if (isGameOver) return;
        if (Input.GetMouseButtonDown(0)) {
            currentPin.StartFly();
                
            score++;
            scoreText.text = score.ToString();//控制分数
            Spawnpin();

        }

    }
	
}
