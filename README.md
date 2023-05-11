### SceneManager


The Real-Time Unity SceneManager is a utility that keeps track of the names of scenes in Unity projects and generates scripts that can be utilized for code completion purposes. With this tool, developers can quickly navigate between scenes in their Unity projects without having to manually switch between them.

## [View Demo](https://www.youtube.com/watch?v=uomME14JcSc)

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
