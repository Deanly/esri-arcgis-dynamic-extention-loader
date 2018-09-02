# ArcGIS Extension - Dynamic Dll Loader

## Summary
이 프로젝트는 Esri extension 개발 프로젝트에서 생산성 향상을 위해 개발되었습니다.   
개발 당시 사용했던 라이브러리 ESRI ArcGIS .net 의 버전은 `10.5.0.0` 입니다.  
참고하시면서 조금이나마 도움이되면 좋겠습니다. 

This project was developed to improve productivity in Esri extension development projects.  
The version of ESRI ArcGIS .net is `10.5.0.0`.  
I hope it helps a little while you refer to it.

## Requirements
- ArgGIS development license
- The starting point for dynamic loading in your project
``` cs
public class YourExtension: ESRI.ArcGIS.Desktop.AddIns.Button
{
    // The starting point
    public void externCall()
    {
        OnClick();
    }

    protected override void Onclick() 
    {
        // Code for start addIn..
    }
}
```

## How to use
1. Build this with your ArcGIS.
2. Add an extensions to ArcGIS.
3. When you run it, a small window appears. 
4. Select the dll file you want to dynamically load.
5. Select the **class**.
6. Select the **Entry function** you want to execute. 
7. click the **Execute** button.