



//  IUISprite
//  Author: Lu Zexi
//  2012-11-21



/// <summary>
/// 精灵物体接口
/// </summary>
public interface IUISprite
{
    /// <summary>
    /// 精灵图
    /// </summary>
    ISprite ISprite
    {
        get;
        set;
    }

    void MakePixelPerfect();
}
