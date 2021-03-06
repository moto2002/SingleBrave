

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Game.Resource;

//  GUIBase.cs
//  Author:Lu Zexi
//  2013-11-12



/// <summary>
/// GUI基类
/// </summary>
public abstract class GUIBase : IGameGUI
{
    private const int LAYER_OFFSET = -100; //位置层级偏移量
    private const int DEPTH_OFFSET = 100;   //深度偏移量
    protected GameObject m_cGUIObject;  //GUI父级实例
    //protected GUIBase m_cGUI;   //子类实例
    protected GUIManager m_cGUIMgr;     //GUI管理实例
    protected UILAYER m_eLayer;        //GUI层级
    protected int m_iID = -1;   //界面的ID
    protected bool m_bShow; //是否展示状态
    protected LOADING_STATE m_eLoadingState = LOADING_STATE.NONE;    //加载状态
    protected bool m_bGuideShow;    //是否可以展示新手引导
    protected float m_fLastHidenTime;    //最后一次隐藏时间

    //加载状态
    public enum LOADING_STATE
    { 
        NONE = 0,
        START,
        LOADING,
        END,
    }

    public int ID
    {
        get { return this.m_iID;}
    }

    public UILAYER Layer
    {
        get { return this.m_eLayer; }
    }

    public float LastHidenTime
    {
        get { return this.m_fLastHidenTime; }
    }

    public GUIBase( GUIManager gui_mgr , int ID , UILAYER lay)
    {
        this.m_iID = ID;
        this.m_eLayer = lay;
        this.m_cGUIMgr = gui_mgr;
        this.m_bShow = false;
        this.m_fLastHidenTime = 0;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Initialize()
    { 
        //
    }

    /// <summary>
    /// 虚函数
    /// </summary>
    protected virtual void InitGUI()
    { 
    }

    /// <summary>
    /// 设置父级
    /// </summary>
    /// <param name="parent"></param>
    public virtual void SetParent(Transform parent)
    {
        this.m_cGUIObject.transform.parent = parent;
        this.m_cGUIObject.transform.localScale = Vector3.one;
    }

    /// <summary>
    /// 销毁GUI
    /// </summary>
    public virtual void Destory()
    {
        if (this.m_cGUIObject != null)
        {
            GameObject.DestroyImmediate(this.m_cGUIObject);
        }
        this.m_bShow = false;
        this.m_cGUIObject = null;
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }

    /// <summary>
    /// 获取主物体
    /// </summary>
    /// <returns></returns>
    public GameObject GetGUIObject()
    {
        return this.m_cGUIObject;
    }

    /// <summary>
    /// 是否正在展示
    /// </summary>
    /// <returns></returns>
    public virtual bool IsShow()
    {
        return this.m_bShow;
    }

    /// <summary>
    /// 展示GUI
    /// </summary>
    public virtual void Show()
    {
        this.m_bShow = true;
        //if (this.m_bGuideShow)
        //{
        //    GuideManager.GetInstance().ShowGuide(this.m_iID);
        //}
    }

    /// <summary>
    /// 隐藏GUI
    /// </summary>
    public virtual void Hiden()
    {
        this.m_bShow = false;
        this.m_fLastHidenTime = Time.fixedTime;
        //this.m_cGUIMgr.CheckDestory();
    }

    /// <summary>
    /// 立即展示GUI
    /// </summary>
    public virtual void ShowImmediately()
    {
        this.m_bShow = true;
        //GuideManager.GetInstance().ShowGuide(this.m_iID);
    }

    /// <summary>
    /// 立即隐藏GUI
    /// </summary>
    public virtual void HidenImmediately()
    {
        this.m_bShow = false;
        this.m_fLastHidenTime = Time.fixedTime;
        //this.m_cGUIMgr.CheckDestory();
    }

    /// <summary>
    /// 设置位置
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public virtual void SetLocalPos(float x, float y)
    {
        if (this.m_cGUIObject == null)
            return;

        UIPanel[] lstP = this.m_cGUIObject.GetComponentsInChildren<UIPanel>();
        if (lstP != null && lstP.Length > 0 )
        {
            foreach (UIPanel item in lstP)
            {
                item.depth = (int)this.m_eLayer * DEPTH_OFFSET;
            }
        }

        this.m_cGUIObject.transform.localPosition = new Vector3(x, y, (int)this.m_eLayer * LAYER_OFFSET);
    }

    /// <summary>
    /// 设置Z方向位置
    /// </summary>
    /// <param name="z"></param>
    public virtual void SetLocalPos(float z)
    {
        if (this.m_cGUIObject == null)
            return;

        UIPanel[] lstP = this.m_cGUIObject.GetComponentsInChildren<UIPanel>();
        if (lstP != null && lstP.Length > 0)
        {
            foreach (UIPanel item in lstP)
            {
                item.depth = (int)this.m_eLayer * DEPTH_OFFSET;
            }
        }
        this.m_cGUIObject.transform.localPosition = new Vector3(this.m_cGUIObject.transform.localPosition.x, this.m_cGUIObject.transform.localPosition.y, (int)this.m_eLayer * LAYER_OFFSET);
    }


    /// <summary>
    /// 设置位置从世界坐标
    /// </summary>
    /// <param name="pos"></param>
    public virtual void SetLocalPos(Vector3 pos)
    {
        if (this.m_cGUIObject == null)
            return;

        UIPanel[] lstP = this.m_cGUIObject.GetComponentsInChildren<UIPanel>();
        if (lstP != null && lstP.Length > 0)
        {
            foreach (UIPanel item in lstP)
            {
                item.depth = (int)this.m_eLayer * DEPTH_OFFSET;
            }
        }
        this.m_cGUIObject.transform.localPosition = new Vector3(pos.x, pos.y, (int)this.m_eLayer * LAYER_OFFSET);
    }

    /// <summary>
    /// 设置为焦点
    /// </summary>
    public virtual void SetFocus()
    {
        //
    }

    /// <summary>
    /// 获取界面内物体
    /// </summary>
    /// <returns></returns>
    public GameObject FindGameObject(string path)
    {
        if (m_cGUIObject == null)
        {
            return null;
        }

        Transform reTran = m_cGUIObject.transform.Find(path);
        if (reTran == null) return null;

        return reTran.gameObject;
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public virtual bool Update()
    {
        //if (this.m_bShow && !this.m_bTmpShow)
        //{
        //    if (this.m_iID == GUI_DEFINE.GUIID_TITTLE ||
        //        this.m_iID == GUI_DEFINE.GUIID_BACKGROUND ||
        //        this.m_iID == GUI_DEFINE.GUIID_RESOURCE_LOADING ||
        //        this.m_iID == GUI_DEFINE.GUIID_HIDEN ||
        //        this.m_iID == GUI_DEFINE.GUIID_ACCOUNT ||
        //        this.m_iID == GUI_DEFINE.GUIID_AYSNC_LOADING ||
        //        this.m_iID == GUI_DEFINE.GUIID_LOADING ||
        //        this.m_iID == GUI_DEFINE.GUIID_LOCKPANEL ||
        //        this.m_iID == GUI_DEFINE.GUIID_MESSAGEL ||
        //        this.m_iID == GUI_DEFINE.GUIID_MESSAGEM ||
        //        this.m_iID == GUI_DEFINE.GUIID_MESSAGES ||
        //        this.m_iID == GUI_DEFINE.GUIID_MESSAGESS
        //        )
        //    {
        //        return true;
        //    }

        //    this.m_bTmpShow = true;
        //    GuideManager.GetInstance().ShowGuide(this.m_iID);
        //}
        return true;
    }

    /// <summary>
    /// 渲染更新
    /// </summary>
    /// <returns></returns>
    public virtual bool Render()
    { 
        return true;
    }

   

}

