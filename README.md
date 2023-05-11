# SceneManager


The Real-Time Unity SceneManager is a utility that keeps track of the names of scenes in Unity projects and generates scripts that can be utilized for code completion purposes. With this tool, developers can quickly navigate between scenes in their Unity projects without having to manually switch between them.

### [View Demo](https://www.youtube.com/watch?v=uomME14JcSc)

## Motivation

Managing scene names in Unity can be a daunting task. To help alleviate this, I have created a package that automates the process of fetching all scenes that have been added to the build and provides code completion for them. This package simplifies scene management, making it more accessible to beginners and reducing the likelihood of errors in scene naming.

## Usage

- import package
- press `shift + s` to open scene window.
- click on `Open Scene Editor` to add scenes.
- click on reload to recompile the project.


**Scripting APIS**
```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// add namespace on the top
using Randoms.SceneManger;

public class Test : MonoBehaviour
{
    private void ReStartGame ()
    {
        // realtime 
        SceneLoaderManager.LoadScene(SceneLoaderManager.SceneName.MyScene);
    }
}

```


**Load Scene Using Unity Button**

- Select btn
- add component of type `SceneLoader`
- select targeted scene from the drop down


-----

## About Us


<div align="center">
<h4 font-weight="bold">This repository is maintained by <a href="https://github.com/Zain-ul-din">Zain-Ul-Din</a></h4>
<p> Show some ❤️ by starring this awesome repository! </p>
</div>


<div align="center">
<a href="https://www.buymeacoffee.com/zainuldin" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png" alt="Buy Me A Coffee" style="height: 41px !important;width: 174px !important;box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;-webkit-box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;" ></a>

</div>
